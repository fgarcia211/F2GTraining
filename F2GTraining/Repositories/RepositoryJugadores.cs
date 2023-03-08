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

CREATE OR ALTER PROCEDURE SP_FIND_JUGADOR_IDEQUIPO (@IDEQUIPO INT)
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

            //CONTINUA HACIENDO LOS PROCEDURES

            await this.context.Database.ExecuteSqlRawAsync(sql);

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

        public Usuario GetUsuarioNamePass(string nombre, string contrasenia)
        {
            string sql = "SP_FIND_USUARIO @NOMBRE, @CONTRASENIA";
            SqlParameter pamNom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamCon = new SqlParameter("@CONTRASENIA", contrasenia);
            var consulta = this.context.Usuarios.FromSqlRaw(sql, pamNom, pamCon);
            Usuario user = consulta.AsEnumerable().FirstOrDefault();
            return user;

        }
    }
}
