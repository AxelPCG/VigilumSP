using DAL.Enums;
using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Ocorrencia")]
    public class Ocorrencia : Entity
    {
        [Required]
        [StringLength(50)]
        public string Primeiro_Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Sobrenome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefone { get; set; }

        [Required]
        public OcorrenciaAssuntoEnum Assunto { get; set; }

        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(500)]
        public string Mensagem { get; set; }

        [Required]
        public char Concordo_Termos_Condicoes { get; set; }
    }
}
