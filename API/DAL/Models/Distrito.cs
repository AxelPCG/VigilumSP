using DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_DISTRITO")]
    public class Distrito : Entity
    {
        [Required]
        [Precision(9)]
        public int CD_DIST {  get; set; }

        [Required]
        [StringLength(100)]
        public string NM_DIST { get; set; }

        public double[] GEOMETRY { get; set; }
        public double[] CORD_CENTRAL { get; set; }

        [Required]
        [ForeignKey("Zona")]
        public int Zona_Id { get; set; }   
        public virtual Zona Zona { get; set; }
    }
}