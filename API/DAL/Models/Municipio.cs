using DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_MUNICIPIO")]
    public class Municipio : Entity
    {
        [Required]
        [Precision(7)]
        public int COD_MUN { get; set; }

        [Required]
        [StringLength(100)]
        public string NM_MUN { get; set; }

        [Required]
        [StringLength(2)]
        public string SG_ESTADO { get; set; }

        [Required]
        [Precision(10, 2)]
        public double AREA_KM2 {  get; set; }

        public double[] Geometry { get; set; }
        public double[] Cord_Central { get; set; }
    }
}
