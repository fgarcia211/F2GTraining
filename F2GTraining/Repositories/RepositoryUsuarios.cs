using System.Data.Common;
using System.Data;
using F2GTraining.Data;
using F2GTraining.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region PROCEDURES

/*CREATE PROCEDURE SP_INSERT_USUARIO (@NOMBRE NVARCHAR(50),@CORREO NVARCHAR(100), @CONTRASENIA NVARCHAR(50), @TELEFONO INT)
AS
    INSERT INTO USUARIOS VALUES ((SELECT MAX(ID) FROM USUARIOS)+1,@NOMBRE,@CORREO,@CONTRASENIA,@TELEFONO,NULL)
GO*/

#endregion
namespace F2GTraining.Repositories
{
    public class RepositoryUsuarios
    {
        private F2GDataBaseContext context;

        public RepositoryUsuarios(F2GDataBaseContext context)
        {
            this.context = context;
        }

        public async Task InsertUsuario(string nombre, string correo, string contrasenia, int telefono)
        {
            string sql = "SP_INSERT_USUARIO @NOMBRE, @CORREO, @CONTRASENIA, @TELEFONO";

            SqlParameter pamNom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamCor = new SqlParameter("@CORREO", correo);
            SqlParameter pamCon = new SqlParameter("@CONTRASENIA", contrasenia);
            SqlParameter pamTel = new SqlParameter("@TELEFONO", telefono);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamNom, pamCor, pamCon, pamTel);

        }
    }
}
