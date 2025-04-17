using DevJANGO.App.Extensions;
using DevJANGO.Business.Models;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class PropinaViewModel
    {
         [Key]
         public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Mes")]
        public Guid MesId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("AlunoMatriculado PagamentoPropina")]
        public Guid AlunoMatriculadoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Pagamento de Propina")]
        public Guid PagamentoPropinaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Classe")]
        public Guid ClasseId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Turma")]
        public Guid TurmaId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; } // Data da Cobrança

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Quantos meses vai pagar?")]
        public int QuantidadeMes { get; set; }
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Selecione o Tipo de Pagamento")]
        public int TipoPagamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(29, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string DescricaoPropina { get; set; }
        [Moeda]
        public decimal PrecoPropina { get; set; }
        public int Situacao { get; set; }
        [ScaffoldColumn(false)]
        public decimal ValorTotal { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Nome do Aluno")]
        public string Nome { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Turma")]
        public string NomeDaTurma { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Classe")]
        public string NomeDaClasse { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Meses")]
        public int Meses { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Propinas Em Atraso")]
        public decimal PropinasEmAtraso { get; set; }

        //Nome,NomeDaTurma,NomeDaClasse,Meses,PropinasEmAtraso

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(9, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string AnoLetivo { get; set; }

        /* EF Relations, Lado MUITO na Entidade Propina */
        public MesViewModel Mes { get; set; }
        public AlunoMatriculadoViewModel AlunoMatriculado { get; set; }
        public PagamentoPropinaViewModel PagamentoPropina { get; set; }
        public ClasseViewModel Classe { get; set; }
        public TurmaViewModel Turma { get; set; }

        public IEnumerable<PagamentoPropinaViewModel> PagamentoPropinas { get; set; }

        internal void CalcularValorTotal()
        {
            ValorTotal = PagamentoPropinas.Sum(p => p.PrecoPropina);
        }

    }

}