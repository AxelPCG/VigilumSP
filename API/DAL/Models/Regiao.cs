using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Regiao")]
    public class Regiao : Entity
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Cidade")]
        public int Cidade_Id { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}