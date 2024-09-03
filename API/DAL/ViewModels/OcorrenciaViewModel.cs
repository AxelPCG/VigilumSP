using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class OcorrenciaViewModel
    {
        [Required]
        public string Primeiro_Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        public OcorrenciaAssuntoEnum Assunto { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Mensagem { get; set; }

        [Required]
        public bool Concordo { get; set; }

        public char Concordo_Termos_Condicoes { get; set; }
    }
}
