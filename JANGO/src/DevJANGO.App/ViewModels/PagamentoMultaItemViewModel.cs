using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class PagamentoMultaItemViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("PagamentoMulta")]
        public Guid PagamentoMultaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Multa")]
        public Guid MultaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("FuncionarioCaixa")]
        public Guid FuncionarioCaixaId { get;  set; }

        [ScaffoldColumn(false)]
        public string NomeMulta { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoMulta { get; set; }

       /* EF Relations, Lado MUITO na Entidade PagamentoMultaItem */
      public MultaViewModel Multa { get; set; }
      public PagamentoMultaViewModel PagamentoMulta { get; set; }
      public FuncionarioCaixaViewModel FuncionarioCaixa { get; set; }
        
    }
}
