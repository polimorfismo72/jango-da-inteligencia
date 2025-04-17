using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class PedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("AlunoMatriculado")]
        public Guid AlunoMatriculadoId { get; set; }

        [ScaffoldColumn(false)]
        public string Nome { get; set; }

        [ScaffoldColumn(false)]
        public int Codigo { get; set; }

        [NotMapped]
        public string Documento { get; set; }
        [NotMapped]
        public Guid ProdutoId { get; set; }

        [DisplayName("Valor do Desconto")]
        public decimal ValorDesconto { get; set; }

        [DisplayName("Total a Pagar")]
        public decimal ValorTotal { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }


        [DisplayName("Tipo de Operação")]
        public int OperacaoPedidos { get; set; }

        [DisplayName("Situação do Pedido")]
        public int Situacao { get; set; }

        [DisplayName("Tipo de Pagamento")]
        public int TipoPagamento { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        //[DisplayName("Código da Transação")]
        //public string NumeroDeTransacaoDePagamento { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public PedidoItemViewModel PedidoItem { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public AlunoMatriculadoViewModel AlunoMatriculado { get; set; }

        public IEnumerable<AlunoMatriculadoViewModel> AlunoMatriculados { get; set; }
        public IEnumerable<PedidoItemViewModel> PedidoItems { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public List<PedidoItemViewModel> PedidoItemDetalhe { get; set; }

    }
}