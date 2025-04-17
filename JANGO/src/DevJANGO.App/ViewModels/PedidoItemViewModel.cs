using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class PedidoItemViewModel
    {
        public decimal CarrinhoCompraTotal { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Produto")]
        public Guid ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Pedido")]
        public Guid PedidoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Vendedor")]
        public Guid FuncionarioCaixaId { get; set; }
        public int Quantidade { get; set; }

        [DisplayName("Produto")]
        public string NomeProduto { get; set; }

        [DisplayName("Valor")]
        public decimal ValorUnitario { get; set; } 
        public ProdutoViewModel Produto { get; set; }
        public PedidoViewModel Pedido { get; set; }
        public FuncionarioCaixaViewModel FuncionarioCaixa { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public IEnumerable<AlunoMatriculadoViewModel> AlunoMatriculados { get; set; }

    }
}