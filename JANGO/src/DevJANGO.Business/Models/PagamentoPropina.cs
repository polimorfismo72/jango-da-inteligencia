namespace DevJANGO.Business.Models
{
    public class PagamentoPropina : Entity
    {  /* Pedido Propina */
        public int Codigo { get; set; }
        public Guid AlunoMatriculadoId { get; set; }
        public Guid FuncionarioCaixaId { get; set; }
        public DateTime DataCadastro { get; set; }
      
        public decimal ValorDesconto { get; set; }
        public decimal TotalPago { get; set; }
        public TipoPagamento TipoPagamento { get; set; }

        public bool Ativo { get; set; }
        public bool PagamentoMaticula { get; set; }
        public string Descricao { get; set; }
        public int NumeroDeMeses { get; set; }
        public decimal PrecoPropina { get; set; }

        /* EF Relations, Lado MUITO na Entidade PagamentoPropina */
        public AlunoMatriculado AlunoMatriculado { get; set; }
        public FuncionarioCaixa FuncionarioCaixa { get; set; }
        public IEnumerable<Propina> Propinas { get; set; }
    }

}