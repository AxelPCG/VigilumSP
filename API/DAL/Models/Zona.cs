using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Zona")]
    public class Zona : Entity
    {
        [Required]
        [StringLength(50)]
        public string Nome_Zona { get; set; }

        public double[] Geometry { get; set; }
        public double[] Cord_Central { get; set; }

        [Required]
        [ForeignKey("Municipio")]
        public int Municipio_Id { get; set; }
        public virtual Municipio Municipio { get; set; }

        public virtual ICollection<Distrito> Distrios { get; set; }
    }
}
