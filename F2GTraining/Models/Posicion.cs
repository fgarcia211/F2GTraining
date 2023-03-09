using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F2GTraining.Models
{
    [Table("POSICIONES")]
    public class Posicion
    {
        [Key]
        [Column("ID")]
        public int IdPosicion { get; set; }

        [Column("POSICION")]
        public string Nombre { get; set; }
    }
}
