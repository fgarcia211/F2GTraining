using System.Data.Common;
using System.Data;
using F2GTraining.Data;
using F2GTraining.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

#region PROCEDURES

/*CREATE OR ALTER PROCEDURE SP_INSERT_USUARIO (@NOMBRE NVARCHAR(50),@CORREO NVARCHAR(100), @CONTRASENIA NVARCHAR(50), @TELEFONO INT)
AS
	INSERT INTO USUARIOS VALUES ((SELECT MAX(ID) FROM USUARIOS)+1,@NOMBRE,@CORREO,@CONTRASENIA,@TELEFONO,NULL)
GO

CREATE OR ALTER PROCEDURE SP_FIND_USUARIO (@NOMBRE NVARCHAR(50), @CONTRASENIA NVARCHAR(50))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE NOM_USUARIO = @NOMBRE AND CONTRASENIA = @CONTRASENIA
GO

CREATE OR ALTER PROCEDURE SP_FIND_TOKEN (@TOKEN NVARCHAR(100))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE TOKEN = @TOKEN
GO

CREATE OR ALTER PROCEDURE SP_FIND_NOM_USUARIO (@NOMBRE NVARCHAR(50))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE NOM_USUARIO = @NOMBRE
GO

CREATE OR ALTER PROCEDURE SP_FIND_CORREO (@CORREO NVARCHAR(50))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE CORREO = @CORREO
GO

CREATE OR ALTER PROCEDURE SP_FIND_TELEFONO (@TELEFONO INT)
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE TELEFONO = @TELEFONO
GO

CREATE OR ALTER PROCEDURE SP_FIND_TOKEN (@TOKEN NVARCHAR(100))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE TOKEN = @TOKEN
GO

CREATE OR ALTER PROCEDURE SP_UPDATE_TOKEN (@OLDTOKEN NVARCHAR(100), @NEWTOKEN NVARCHAR(100))
AS
	UPDATE USUARIOS SET TOKEN = @NEWTOKEN WHERE TOKEN = @OLDTOKEN
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
            SqlParameter pamCor = new SqlParameter("@CORREO", correo.ToLower());
            SqlParameter pamCon = new SqlParameter("@CONTRASENIA", contrasenia);
            SqlParameter pamTel = new SqlParameter("@TELEFONO", telefono);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamNom, pamCor, pamCon, pamTel);

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

        public Usuario GetUsuarioToken(string token)
        {
            string sql = "SP_FIND_TOKEN @TOKEN";
            SqlParameter pamTok = new SqlParameter("@TOKEN", token);
            var consulta = this.context.Usuarios.FromSqlRaw(sql, pamTok);
            Usuario user = consulta.AsEnumerable().FirstOrDefault();
            return user;

        }

        public bool CheckTelefonoRegistro(int telefono)
        {
            string sql = "SP_FIND_TELEFONO @TELEFONO";
            SqlParameter pamTel = new SqlParameter("@TELEFONO", telefono);
            var consulta = this.context.Usuarios.FromSqlRaw(sql, pamTel);
            Usuario user = consulta.AsEnumerable().FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public bool CheckUsuarioRegistro(string nombre)
        {
            string sql = "SP_FIND_NOM_USUARIO @NOMBRE";
            SqlParameter pamNom = new SqlParameter("@NOMBRE", nombre);
            var consulta = this.context.Usuarios.FromSqlRaw(sql, pamNom);
            Usuario user = consulta.AsEnumerable().FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool CheckCorreoRegistro(string correo)
        {
            string sql = "SP_FIND_CORREO @CORREO";
            SqlParameter pamCor = new SqlParameter("@CORREO", correo.ToLower());
            var consulta = this.context.Usuarios.FromSqlRaw(sql, pamCor);
            Usuario user = consulta.AsEnumerable().FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public async Task UpdateToken(string oldtoken, string newtoken)
        {
            string sql = "SP_UPDATE_TOKEN @OLDTOKEN, @NEWTOKEN";

            SqlParameter pamOld = new SqlParameter("@OLDTOKEN", oldtoken);
            SqlParameter pamNew = new SqlParameter("@CORREO", newtoken);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamOld, pamNew);

        }

    }
}
