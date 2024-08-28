using DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TBL_Previsao")]
    public class Previsao : Entity
    {

        [ForeignKey("Regiao")]
        public int Regiao_Id { get; set; }

        [ForeignKey("Temperatura")]
        public int Temperatura_Id { get; set; }

        [ForeignKey("Umidade")]
        public int Umidade_Id { get; set; }

        [ForeignKey("Ventania")]
        public int Ventania_Id { get; set; }

        [ForeignKey("Precipitacao")]
        public int Precipitacao_Id { get; set; }

        [ForeignKey("Nuvem")]
        public int Nuvem_Id { get; set; }

        public virtual Regiao Regiao { get; set; }
        public virtual Temperatura Temperatura { get; set; }
        public virtual Umidade Umidade { get; set; }
        public virtual Ventania Ventania { get; set; }
        public virtual Precipitacao Precipitacao { get; set; }
        public virtual Nuvem Nuvem { get; set; }
    }
}
