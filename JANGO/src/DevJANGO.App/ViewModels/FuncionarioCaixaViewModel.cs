using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
  public class FuncionarioCaixaViewModel
  {
      [Key]
  public Guid Id { get; set; }
   //public string Nome { get; set; }
   public string Email { get; set; }
   public bool Ativo { get; set; }
   [Required(ErrorMessage = "O campo {0} é obrigatório")]
   [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
   [DisplayName("Nome")]
   public string Nome { get; set; }
   /* EF Relations side 1*/
   public IEnumerable<PagamentoMultaItemViewModel> PagamentoMultaItems { get; set; }
   public IEnumerable<AlunoInscritoViewModel> AlunoInscritos { get; set; }
        public IEnumerable<PedidoItemViewModel> PedidoItems { get; set; }
    }
}
