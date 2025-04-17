namespace DevJANGO.Business.Models
{
    public class PagamentoMulta : Entity
    {  /* Pedido Multa */
        public int Codigo { get; set; }
        public Guid AlunoMatriculadoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal TotalPago { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
     
        public bool Ativo { get; set; }

        /* EF Relations, Lado MUITO na Entidade PagamentoMulta */
        public AlunoMatriculado AlunoMatriculado { get; set; }
     }

}