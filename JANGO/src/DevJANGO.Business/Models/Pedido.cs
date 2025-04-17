
namespace DevJANGO.Business.Models
{   
    public class Pedido : Entity
    {
        public int Codigo { get; set;}
        public Guid AlunoMatriculadoId { get; set;}
        public decimal ValorDesconto { get; set;}
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set;}
        public OperacaoPedido OperacaoPedidos { get; set;}
        public Situacao Situacao { get; set;}
        public TipoPagamento TipoPagamento { get; set;}
        //public string NumeroDeTransacaoDePagamento { get; set;}
        public bool Ativo { get; set; }

        /* EF Relations  Lado MUITO */
        public AlunoMatriculado AlunoMatriculado { get; set; }

        /* EF Relations  Lado UM */
        public IEnumerable<PedidoItem> PedidoItems { get; set; }

    }
}