using F2GTraining.Data;
using F2GTraining.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region PROCEDURES

/*CREATE OR ALTER PROCEDURE SP_INSERT_EQUIPO (@IDUSER INT, @NOMBRE NVARCHAR(50),@IMAGEN NVARCHAR(1000))
AS
	INSERT INTO EQUIPOS VALUES ((SELECT ISNULL(MAX(ID),0) FROM EQUIPOS)+1,@IDUSER,@NOMBRE,@IMAGEN)
GO

CREATE OR ALTER PROCEDURE SP_FIND_EQUIPOS_USER (@IDUSER INT)
AS
	SELECT * FROM EQUIPOS
	WHERE IDUSUARIO = @IDUSER
GO

CREATE OR ALTER PROCEDURE SP_FIND_EQUIPO_ID (@IDEQUIPO INT)
AS
	SELECT * FROM EQUIPOS
	WHERE ID = @IDEQUIPO
GO*/
#endregion

namespace F2GTraining.Repositories
{
    public class RepositoryEquipos
    {
        private F2GDataBaseContext context;

        public RepositoryEquipos(F2GDataBaseContext context)
        {
            this.context = context;
        }

        public async Task InsertEquipo(int iduser, string nombre, string imagen)
        {
            string sql = "SP_INSERT_EQUIPO @IDUSER, @NOMBRE, @IMAGEN";

            SqlParameter pamIdUs = new SqlParameter("@IDUSER", iduser);
            SqlParameter pamNom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamIma = new SqlParameter("@IMAGEN", imagen);

            await this.context.Database.ExecuteSqlRawAsync(sql,pamIdUs, pamNom, pamIma);

        }

        public List<Equipo> GetEquiposUser(int iduser)
        {
            string sql = "SP_FIND_EQUIPOS_USER @IDUSER";
            SqlParameter pamIdUs = new SqlParameter("@IDUSER", iduser);
            var consulta = this.context.Equipos.FromSqlRaw(sql, pamIdUs);
            List<Equipo> equiposusuario = consulta.AsEnumerable().ToList();
            return equiposusuario;
        }

        public Equipo GetEquipo(int idequipo)
        {
            string sql = "SP_FIND_EQUIPO_ID @IDEQUIPO";
            SqlParameter pamIdUs = new SqlParameter("@IDEQUIPO", idequipo);
            var consulta = this.context.Equipos.FromSqlRaw(sql, pamIdUs);
            Equipo equipo = consulta.AsEnumerable().FirstOrDefault();
            return equipo;
        }

    }
}
