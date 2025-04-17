namespace DevJANGO.Business.Models
{
    public class Propina : Entity
    { 
        public Guid AlunoMatriculadoId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid TurmaId { get; set; }
        public Guid MesId { get; set; }
        public Guid PagamentoPropinaId { get; set; }

        public DateTime DataCadastro { get; set; } // Data da Cobrança
        public string DescricaoPropina { get; set; }
        public decimal PrecoPropina { get; set; }
        public Situacao Situacao { get; set; }
        public string AnoLetivo { get; set; }

        /* EF Relations, Lado MUITO na Entidade Propina */
        public AlunoMatriculado AlunoMatriculado { get; set; }
        public Classe Classe { get; set; }
        public Turma Turma { get; set; }
        public Mes Mes { get; set; }
        public PagamentoPropina PagamentoPropina { get; set; }
    }

}