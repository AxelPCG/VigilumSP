using DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_PREVISAO")]
    public class Previsao : Entity
    {
        [Required]
        [ForeignKey("Zona")]
        public int ZONA_ID { get; set; }
        public virtual Zona Zona { get; set; }


        [Required]
        public DateTime DATA { get; set; }

        [Precision(2)]
        public double Temperatura_Max { get; set; }

        [Precision(5, 2)]
        public double Temperatura_Min { get; set; }

        [Precision(5, 2)]
        public double Umidade_Max { get; set; }

        [Precision(5, 2)]
        public double Umidade_Min { get; set; }

        [Precision(5, 2)]
        public double Velocidade_Vento { get; set; }

        [Precision(5, 2)]
        public double Volume_Precipitacao { get; set; }

        [StringLength(50)]
        public string Tipo_Nuvem { get; set; }

        [Precision(5, 2)]
        public double Pressao_Atm { get; set; }
    }
}
