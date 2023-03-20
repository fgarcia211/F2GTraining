using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace F2GTraining.Models
{
    [Table("ESTADISTICAS")]
    public class EstadisticaJugador
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("IDJUGADOR")]
        public int IdJugador { get; set; }

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

        [Column("TOTAL_RITMO_GKSALTO")]
        public int? TotalRitmoGKSalto { get; set; }

        [Column("TOTAL_TIRO_GKPARADA")]
        public int? TotalTiroGKParada { get; set; }

        [Column("TOTAL_PASE_GKSAQUE")]
        public int? TotalPaseGKSaque { get; set; }

        [Column("TOTAL_REGATE_GKREFLEJO")]
        public int? TotalRegateGKReflejo { get; set; }

        [Column("TOTAL_DEFENSA_GKVELOCIDAD")]
        public int? TotalDefensaGKVelocidad { get; set; }

        [Column("TOTAL_FISICO_GKPOSICION")]
        public int? TotalFisicoGKPosicion { get; set; }

    }

}
