using DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Municipio")]
    public class Municipio : Entity
    {
        [Required]
        [MaxLength(7)]
        public int Cod_Mun { get; set; }

        [Required]
        [StringLength(100)]
        public string Nm_Mun { get; set; }

        [Required]
        [StringLength(2)]
        public string Sg_Estado { get; set; }

        [Required]
        [Precision(10, 2)]
        public double Area_Km2 {  get; set; }

        public double[] Geometry { get; set; }
        public double[] Cord_Central { get; set; }
    }
}
