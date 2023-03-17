using F2GTraining.Data;
using F2GTraining.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region PROCEDURES

/*CREATE OR ALTER PROCEDURE SP_INSERT_JUGADOR (@IDEQUIPO INT, @IDPOSICION INT, @NOMBRE NVARCHAR(100), @DORSAL INT, @EDAD INT, @PESO INT, @ALTURA INT)
AS
    INSERT INTO JUGADORES VALUES (
	(SELECT ISNULL(MAX(ID),0) FROM JUGADORES)+1,@IDEQUIPO,@IDPOSICION,@NOMBRE,@DORSAL,@EDAD,@PESO,@ALTURA)

	INSERT INTO ESTADISTICAS VALUES 
	((SELECT ISNULL(MAX(ID),0) FROM ESTADISTICAS)+1,(SELECT ISNULL(MAX(ID),0) FROM JUGADORES),NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
GO

CREATE OR ALTER PROCEDURE SP_FIND_JUGADOR_ID (@IDJUGADOR INT)
AS
	SELECT * FROM JUGADORES
	WHERE ID = @IDJUGADOR
GO

CREATE OR ALTER PROCEDURE SP_FIND_JUGADORES_IDEQUIPO (@IDEQUIPO INT)
AS
	SELECT * FROM JUGADORES
	WHERE IDEQUIPO = @IDEQUIPO
GO

CREATE OR ALTER PROCEDURE SP_DELETE_JUGADOR_ID (@IDJUGADOR INT)
AS
	DELETE FROM ESTADISTICAS
	WHERE IDJUGADOR = @IDJUGADOR

    DELETE FROM JUGADORES
	WHERE ID = @IDJUGADOR
GO

CREATE OR ALTER PROCEDURE SP_FIND_POSITIONS
AS
	SELECT * FROM POSICIONES
GO*/
#endregion

#region VISTAS

/*CREATE VIEW V_ESTADISTICAS_JUGADOR
AS
    SELECT JUG.NOMBRE, EST.* FROM ESTADISTICAS EST INNER JOIN JUGADORES JUG ON EST.IDJUGADOR = JUG.ID
GO*/

#endregion

namespace F2GTraining.Repositories
{
    public class RepositoryJugadores
    {
        private F2GDataBaseContext context;
        private RepositoryEquipos repoEqu;

        public RepositoryJugadores(F2GDataBaseContext context, RepositoryEquipos repoEqu)
        {
            this.context = context;
            this.repoEqu = repoEqu;
        }

        public async Task InsertJugador(int idequipo, int idposicion, string nombre, int dorsal, int edad, int peso, int altura)
        {
            string sql = "SP_INSERT_JUGADOR @IDEQUIPO, @IDPOSICION, @NOMBRE, @DORSAL, @EDAD, @PESO, @ALTURA";

            SqlParameter pamIdEq = new SqlParameter("@IDEQUIPO", idequipo);
            SqlParameter pamIdPos = new SqlParameter("@IDPOSICION", idposicion);
            SqlParameter pamNom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamDor = new SqlParameter("@DORSAL", dorsal);
            SqlParameter pamEda = new SqlParameter("@EDAD", edad);
            SqlParameter pamPes = new SqlParameter("@PESO", peso);
            SqlParameter pamAlt = new SqlParameter("@ALTURA", altura);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamIdEq, pamIdPos, pamNom, pamDor, pamEda, pamPes, pamAlt);

        }

        public List<Posicion> GetPosiciones()
        {
            string sql = "SP_FIND_POSITIONS";
            var consulta = this.context.Posiciones.FromSqlRaw(sql);
            List<Posicion> posiciones = consulta.AsEnumerable().ToList();
            return posiciones;

        }
        public Jugador GetJugadorID(int id)
        {
            string sql = "SP_FIND_JUGADOR_ID @IDJUGADOR";
            SqlParameter pamIdJug = new SqlParameter("@IDJUGADOR", id);
            var consulta = this.context.Jugadores.FromSqlRaw(sql, pamIdJug);
            Jugador player = consulta.AsEnumerable().FirstOrDefault();
            return player;

        }

        public List<Jugador> GetJugadoresEquipo(int idequipo)
        {
            string sql = "SP_FIND_JUGADORES_IDEQUIPO @IDEQUIPO";
            SqlParameter pamIdEq = new SqlParameter("@IDEQUIPO", idequipo);
            var consulta = this.context.Jugadores.FromSqlRaw(sql, pamIdEq);
            List<Jugador> players = consulta.AsEnumerable().ToList();
            return players;

        }

        public async Task DeleteJugador(int idjugador)
        {
            string sql = "SP_DELETE_JUGADOR_ID @IDJUGADOR";
            SqlParameter pamIdJug = new SqlParameter("@IDJUGADOR", idjugador);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamIdJug);

        }

        //OPTIMIZAR METODO (INTENTAR CON VISTA, Y PREGUNTAR LO DEL MODELO)
        public List<Jugador> JugadoresXUsuario(int idusuario)
        {
            List<Equipo> equipos = this.repoEqu.GetEquiposUser(idusuario);

            List<int> idsEquipos = new();

            foreach (Equipo equipo in equipos)
            {
                idsEquipos.Add(equipo.IdEquipo);
            }

            var consulta = from datos in this.context.Jugadores
                           where idsEquipos.Contains(datos.IdEquipo)
                           select datos;


            if (consulta.Count() == 0)
            {
                return null;
            }

            return consulta.ToList();
        }

        public List<Jugador> JugadoresXSesion(int identrenamiento)
        {
            var consulta = (from datos in this.context.JugadoresEntrenamiento
                            where identrenamiento == datos.IdEntrenamiento
                            select datos.IdJugador);

            List<Jugador> jugadores = this.context.Jugadores.Where(x => consulta.Contains(x.IdJugador)).ToList();

            return jugadores;
        }

        public async Task AniadirPuntuacionesEntrenamiento(List<int> idsjugador, List<int> valoraciones, int identrenamiento)
        {
            var contadorPuntuacion = 0;

            foreach (int id in idsjugador)
            {
                JugadorEntrenamiento jugador =
                    this.context.JugadoresEntrenamiento.Where(x => x.IdJugador == id && x.IdEntrenamiento == identrenamiento).First();

                jugador.RitmoGKSalto = valoraciones[contadorPuntuacion];
                jugador.TiroGKParada = valoraciones[contadorPuntuacion + 1];
                jugador.PaseGKSaque = valoraciones[contadorPuntuacion + 2];
                jugador.RegateGKReflejo = valoraciones[contadorPuntuacion + 3];
                jugador.DefensaGKVelocidad = valoraciones[contadorPuntuacion + 4];
                jugador.FisicoGKPosicion = valoraciones[contadorPuntuacion + 5];
                jugador.Finalizado = true;

                contadorPuntuacion += 6;
            }

            await this.context.SaveChangesAsync();
        }

        public async Task AniadirJugadoresSesion(List<int> idsjugador, int identrenamiento)
        {
            List<Jugador> jugadores = this.context.Jugadores.Where(x => idsjugador.Contains(x.IdJugador)).ToList();
            int id = this.context.JugadoresEntrenamiento.Count();

            if (id == 0)
            {
                id = 1;
            }
            else
            {
                id = this.context.JugadoresEntrenamiento.Max(x => x.Id)+1;
            }

            foreach (Jugador jug in jugadores)
            {
                JugadorEntrenamiento jugentre = new JugadorEntrenamiento
                {
                    Id = id,
                    IdJugador = jug.IdJugador,
                    IdEntrenamiento = identrenamiento,
                    RitmoGKSalto = null,
                    TiroGKParada = null,
                    PaseGKSaque = null,
                    RegateGKReflejo = null,
                    DefensaGKVelocidad = null,
                    FisicoGKPosicion = null,
                    Finalizado = false
                };

                this.context.JugadoresEntrenamiento.Add(jugentre);
                id++;

            }
            
            await this.context.SaveChangesAsync();
        }

    }
}
