using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F2GTraining.Models
{
    [Table("ENTRENAMIENTOS")]
    public class Entrenamiento
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("IDEQUIPO")]
        public int IdEquipo { get; set; }

        [Column("FECHA_INICIO")]
        public DateTime? FechaInicio { get; set; }

        [Column("FECHA_FIN")]
        public DateTime? FechaFin { get; set; }

        [Column("ACTIVO")]
        public bool Activo { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }
    }
}
