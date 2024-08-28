using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Cidade")]
    public class Cidade : Entity
    {
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }
    }
}
