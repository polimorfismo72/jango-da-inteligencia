using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class MultaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Mes")]
        public Guid MesId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("AlunoMatriculado")]
        public Guid AlunoMatriculadoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Classe")]
        public Guid ClasseId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Turma")]
        public Guid TurmaId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(29, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Descrição da Multa")]
        public string DescricaoMulta { get; set; }
        public decimal PrecoPropina { get; set; }
        public bool Estado { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(9, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Ano Letivo")]
        public string AnoLetivo { get; set; }

        /* EF Relations, Lado MUITO na Entidade Multa */
        public MesViewModel Mes { get; set; }
        public AlunoMatriculadoViewModel AlunoMatriculado { get; set; }
        public ClasseViewModel Classe { get; set; }
        public TurmaViewModel Turma { get; set; }

    }
}
