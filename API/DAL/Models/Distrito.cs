using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Distrito")]
    public class Distrito : Entity
    {
        [Required]
        [MaxLength(7)]
        public int Cd_Dist {  get; set; }

        [Required]
        [StringLength(100)]
        public string Nm_Dist { get; set; }

        public double[] Cord_Central { get; set; }

        [Required]
        [ForeignKey("Zona")]
        public int Zona_Id { get; set; }   
        public virtual Zona Zona { get; set; }
    }
}