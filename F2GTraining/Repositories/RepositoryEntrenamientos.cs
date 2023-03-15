using F2GTraining.Data;
using F2GTraining.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region PROCEDURES

/*CREATE OR ALTER PROCEDURE SP_INSERTAR_ENTRENAMIENTO (@IDEQUIPO INT, @NOMBRE NVARCHAR(100))
AS
	INSERT INTO ENTRENAMIENTOS VALUES ((SELECT ISNULL(MAX(ID),0) FROM ENTRENAMIENTOS)+1,@IDEQUIPO,NULL,NULL,0,@NOMBRE)
GO

CREATE OR ALTER PROCEDURE SP_ENTRENAMIENTOS_EQUIPO(@IDEQUIPO INT)
AS
	SELECT * FROM ENTRENAMIENTOS
	WHERE IDEQUIPO = @IDEQUIPO
GO

CREATE OR ALTER PROCEDURE SP_BUSCAR_ENTRENAMIENTO (@IDENTRENAMIENTO INT)
AS
	SELECT * FROM ENTRENAMIENTOS
	WHERE ID = @IDENTRENAMIENTO
GO

CREATE OR ALTER PROCEDURE SP_EMPEZAR_ENTRENAMIENTO (@IDENTRENAMIENTO INT, @FECHAINICIO DATETIME)
AS
	UPDATE ENTRENAMIENTOS SET ACTIVO = 1, FECHA_INICIO=@FECHAINICIO 
	WHERE ID = @IDENTRENAMIENTO
GO

CREATE OR ALTER PROCEDURE SP_FINALIZAR_ENTRENAMIENTO (@IDENTRENAMIENTO INT, @FECHAFIN DATETIME)
AS
	UPDATE ENTRENAMIENTOS SET ACTIVO = 0, FECHA_FIN=@FECHAFIN 
	WHERE ID = @IDENTRENAMIENTO
GO

CREATE OR ALTER PROCEDURE SP_BORRAR_ENTRENAMIENTO (@IDENTRENAMIENTO INT)
AS
	DELETE FROM ENTRENAMIENTOS
	WHERE ID = @IDENTRENAMIENTO
GO
*/

#endregion

namespace F2GTraining.Repositories
{
    public class RepositoryEntrenamientos
    {
		private F2GDataBaseContext context;

		public RepositoryEntrenamientos(F2GDataBaseContext context)
        {
            this.context = context;
        }

        public async Task InsertEntrenamiento(int idequipo, string nombre)
        {
            string sql = "SP_INSERTAR_ENTRENAMIENTO @IDEQUIPO, @NOMBRE";

            SqlParameter pamIdEq = new SqlParameter("@IDEQUIPO", idequipo);
            SqlParameter pamNom = new SqlParameter("@NOMBRE", nombre);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamIdEq, pamNom);

        }

        //SI TE FALLA ESTE PROCEDURE VUELVE A EJECUTARLO
        public List<Entrenamiento> GetEntrenamientosEquipo(int idequipo)
        {
            string sql = "SP_ENTRENAMIENTOS_EQUIPO @IDEQUIPO";
            SqlParameter pamIdEq = new SqlParameter("@IDEQUIPO", idequipo);
            var consulta = this.context.Entrenamientos.FromSqlRaw(sql, pamIdEq);
            List<Entrenamiento> entrenamientos = consulta.AsEnumerable().ToList();
            return entrenamientos;
        }

        public Entrenamiento GetEntrenamiento(int identrena)
        {
            string sql = "SP_BUSCAR_ENTRENAMIENTO @IDENTRENAMIENTO";
            SqlParameter pamEnt = new SqlParameter("@IDENTRENAMIENTO", identrena);
            var consulta = this.context.Entrenamientos.FromSqlRaw(sql, pamEnt);
            Entrenamiento entrenamiento = consulta.AsEnumerable().FirstOrDefault();
            return entrenamiento;
        }

        public async Task EmpezarEntrenamiento(int identrenamiento)
        {
            string sql = "SP_EMPEZAR_ENTRENAMIENTO @IDENTRENAMIENTO, @FECHAINICIO";

            SqlParameter pamIdEnt = new SqlParameter("@IDENTRENAMIENTO", identrenamiento);
            SqlParameter pamFec = new SqlParameter("@FECHAINICIO", DateTimeOffset.Now);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamIdEnt, pamFec);

        }

        public async Task FinalizarEntrenamiento(int identrenamiento)
        {
            string sql = "SP_FINALIZAR_ENTRENAMIENTO @IDENTRENAMIENTO, @FECHAFIN";

            SqlParameter pamIdEnt = new SqlParameter("@IDENTRENAMIENTO", identrenamiento);
            SqlParameter pamFec = new SqlParameter("@FECHAFIN", DateTimeOffset.Now);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamIdEnt, pamFec);

        }

        public async Task BorrarEntrenamiento(int identrenamiento)
        {
            string sql = "SP_BORRAR_ENTRENAMIENTO @IDENTRENAMIENTO";

            SqlParameter pamIdEnt = new SqlParameter("@IDENTRENAMIENTO", identrenamiento);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamIdEnt);

        }

    }
}
