using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F2GTraining.Models
{
    [Table("EQUIPOS")]
    public class Equipo
    {
        [Key]
        [Column("ID")]
        public int IdEquipo { get; set; }

        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }
    }
}
