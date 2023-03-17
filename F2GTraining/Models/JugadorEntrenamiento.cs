using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F2GTraining.Models
{
    [Table("JUGADORES_ENTRENAMIENTO")]
    public class JugadorEntrenamiento
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("IDJUGADOR")]
        public int IdJugador { get; set; }

        [Column("IDENTRENAMIENTO")]
        public int IdEntrenamiento { get; set; }

        [Column("RITMO_GKSALTO")]
        public int? RitmoGKSalto { get; set; }

        [Column("TIRO_GKPARADA")]
        public int? TiroGKParada { get; set; }

        [Column("PASE_GKSAQUE")]
        public int? PaseGKSaque { get; set; }

        [Column("REGATE_GKREFLEJO")]
        public int? RegateGKReflejo { get; set; }

        [Column("DEFENSA_GKVELOCIDAD")]
        public int? DefensaGKVelocidad { get; set; }

        [Column("FISICO_GKPOSICION")]
        public int? FisicoGKPosicion { get; set; }

        [Column("FINALIZADO")]
        public bool Finalizado { get; set; }

    }
}
