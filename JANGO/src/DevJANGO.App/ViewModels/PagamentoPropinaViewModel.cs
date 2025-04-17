using DevJANGO.App.Extensions;
using DevJANGO.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class PagamentoPropinaViewModel
    {
         [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public string Nome { get; set; }

        [ScaffoldColumn(false)]
        public string Documento { get; set; }

        [ScaffoldColumn(false)]
        public int Codigo { get; set; }

        [ScaffoldColumn(false)]
        public string DataCodigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("AlunoMatriculado")]
        public Guid AlunoMatriculadoId { get; set; }
        public Guid FuncionarioCaixaId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        #region Documento do Aluno
        [Required(ErrorMessage = "O número do documento é obrigatório")]
        [ScaffoldColumn(false)]
        public string NumDocumento { get; set; }
        #endregion
        public decimal PrecoPropina { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Quantidade de Meses a Pagar")]
        public int NumeroDeMeses { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        //public decimal PercentualDesconto { get; set; }
        public decimal ValorDesconto { get; set; }
        [DisplayName("Total de Pagamento")]
        public decimal TotalPago { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Tipo de Pagamento")]
        public int TipoPagamento { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(29, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        //[DisplayName("Número do Comprovativo")]
        //public string NumeroDeTransacaoDePagamento { get; set; }
        public bool Ativo { get; set; }
        public bool PagamentoMaticula { get; set; }

        /* EF Relations, Lado MUITO na Entidade PagamentoPropina */
        public AlunoMatriculadoViewModel AlunoMatriculado { get; set; }
        public FuncionarioCaixaViewModel FuncionarioCaixa { get; set; }
        public PropinaViewModel Propina { get; set; }
        public MesViewModel Mes { get; set; }

        public IEnumerable<PropinaViewModel> Propinas { get; set; }
        public IEnumerable<MesViewModel> Meses { get; set; }
        public IEnumerable<AlunoMatriculadoViewModel> AlunoMatriculados { get; set; }

        public List<PropinaViewModel> PropinaDetalhe { get; set; }
 
    }

}