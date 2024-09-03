using DAL.Enums;

namespace DAL.DTOs
{
    public class OcorreciaDto
    {
        public string Primeiro_Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public OcorrenciaAssuntoEnum Assunto { get; set; }

        public string Descricao { get; set; }

        public string Mensagem { get; set; }

        public char Concordo_Termos_Condicoes { get; set; }
    }
}
