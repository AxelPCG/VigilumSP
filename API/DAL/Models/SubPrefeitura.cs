using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_SubPrefeitura")]
    public class SubPrefeitura : Entity
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Regiao")]
        public int Regiao_Id {  get; set; }
        public virtual Regiao Regiao { get; set; }
    }
}
