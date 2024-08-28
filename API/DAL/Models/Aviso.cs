using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Aviso")]
    public class Aviso : Entity
    {
        [Required]
        public DateTime Data { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        public int Nivel { get; set; }

        [StringLength(256)]
        public string Descricao { get; set; }

        [ForeignKey("SubPrefeitura")]
        public int SubPrefeitura_Id { get; set; } 

        public virtual SubPrefeitura SubPrefeitura { get; set; }
    }
}
