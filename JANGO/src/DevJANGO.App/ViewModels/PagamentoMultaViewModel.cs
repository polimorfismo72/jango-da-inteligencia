using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class PagamentoMultaViewModel 
    {
        [Key]
        public Guid Id { get; set; } 
        [ScaffoldColumn(false)]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("AlunoMatriculado")]
        public Guid AlunoMatriculadoId { get; set; } 
        public DateTime DataCadastro { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal TotalPago { get; set; }
        public int TipoPagamento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        //public string NumeroDeTransacaoDePagamento { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations, Lado MUITO na Entidade PagamentoMulta */
        public AlunoMatriculadoViewModel AlunoMatriculado { get; set; }
     }

}