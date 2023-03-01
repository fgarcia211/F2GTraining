using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace F2GTraining.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("ID")]
        public int IdUsuario { get; set; }

        [Column("NOM_USUARIO")]
        public string Nombre { get; set; }

        [Column("CORREO")]
        public string Correo { get; set; }

        [Column("CONTRASENIA")]
        public string Contrasenia { get; set; }

        [Column("TELEFONO")]
        public int Telefono { get; set; }

        [Column("TOKEN")]
        public string Token { get; set; }
    }

}
