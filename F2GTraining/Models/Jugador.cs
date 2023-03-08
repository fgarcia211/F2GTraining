using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F2GTraining.Models
{
    [Table("JUGADORES")]
    public class Jugador
    {
        [Key]
        [Column("ID")]
        public int IdJugador { get; set; }

        [Column("IDEQUIPO")]
        public int IdEquipo { get; set; }

        [Column("IDPOSICION")]
        public int IdPosicion { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("DORSAL")]
        public int Dorsal { get; set; }

        [Column("EDAD")]
        public int Edad { get; set; }

        [Column("PESO")]
        public float Peso { get; set; }

        [Column("ALTURA")]
        public float Altura { get; set; }


    }
}
