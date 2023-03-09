using F2GTraining.Data;
using F2GTraining.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region PROCEDURES

/*CREATE OR ALTER PROCEDURE SP_INSERT_JUGADOR (@IDEQUIPO INT, @IDPOSICION INT, @NOMBRE NVARCHAR(100), @DORSAL INT, @EDAD INT, @PESO FLOAT, @ALTURA FLOAT)
AS
    INSERT INTO JUGADORES VALUES (
	(SELECT ISNULL(MAX(ID),0) FROM JUGADORES)+1,@IDEQUIPO,@IDPOSICION,@NOMBRE,@DORSAL,@EDAD,@PESO,@ALTURA)
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
    DELETE FROM JUGADORES
	WHERE ID = @IDJUGADOR
GO

CREATE OR ALTER PROCEDURE SP_FIND_POSITIONS
AS
	SELECT * FROM POSICIONES
GO*/

#endregion

namespace F2GTraining.Repositories
{
    public class RepositoryJugadores
    {
        private F2GDataBaseContext context;

        public RepositoryJugadores(F2GDataBaseContext context)
        {
            this.context = context;
        }

        public async Task InsertJugador(int idequipo, int idposicion, string nombre, int dorsal, int edad, float peso, float altura)
        {
            string sql = "SP_INSERT_JUGADOR @IDEQUIPO, @IDPOSICION, @NOMBRE, @DORSAL, @EDAD, @PESO, @ALTURA";

            SqlParameter pamIdEq = new SqlParameter("@IDEQUIPO", idequipo);
            SqlParameter pamIdPos = new SqlParameter("@IDPOSICION", idposicion);
            SqlParameter pamNom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamDor = new SqlParameter("@DORSAL", this.InputIntVacio(dorsal));
            SqlParameter pamEda = new SqlParameter("@EDAD", this.InputIntVacio(edad));
            SqlParameter pamPes = new SqlParameter("@PESO", this.InputFloatVacio(peso));
            SqlParameter pamAlt = new SqlParameter("@ALTURA", this.InputFloatVacio(altura));

            await this.context.Database.ExecuteSqlRawAsync(sql);

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
            string sql = "SP_DELETE_JUGADOR @IDJUGADOR";
            SqlParameter pamIdJug = new SqlParameter("@IDJUGADOR", idjugador);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamIdJug);

        }

        public int InputIntVacio(int valor)
        {
            if (valor == null)
            {
                return -1;
            }
            else
            {
                return valor;
            }
        }

        public float InputFloatVacio(float valor)
        {
            if (valor == null)
            {
                return -1;
            }
            else
            {
                return valor;
            }
        }

    }
}
