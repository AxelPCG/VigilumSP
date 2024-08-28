﻿using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Temperatura")]
    public class Temperatura : Entity
    {
        [Required]
        public DateTime Data { get; set; }

        [Required]
        public float Dia_1 { get; set; }

        [Required]
        public float Dia_2 { get; set; }

        [Required]
        public float Dia_3 { get; set; }

        [Required]
        public float Dia_4 { get; set; }

        [Required]
        public float Dia_5 { get; set; }

        [Required]
        public float Dia_6 { get; set; }

        [Required]
        public float Dia_7 { get; set; }

        public virtual ICollection<Previsao> Previsoes { get; set; }
    }
}
