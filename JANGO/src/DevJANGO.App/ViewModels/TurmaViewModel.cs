using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class TurmaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Classe")]
        public Guid ClasseId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Area De Conhecimento")]
        public Guid AreaDeConhecimentoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Turma")]
        public string NomeTurma { get; set; }
        [DisplayName("Número De Vagas")]
        public int NumDeVagas { get; set; }
        public bool Estado { get; set; }
        public ClasseViewModel Classe { get; set; }
        public AreaDeConhecimentoViewModel AreaDeConhecimento { get; set; }

        /* EF Relations, Lado UM na Entidade Turma */
        public IEnumerable<AlunoMatriculadoViewModel> AlunoMatriculados { get; set; }
        public IEnumerable<AvaliacaoViewModel> Avaliacaos { get; set; }
        public IEnumerable<PropinaViewModel> Propinas { get; set; }
        public IEnumerable<ClasseViewModel> Classes { get; set; }
        public IEnumerable<AreaDeConhecimentoViewModel> AreaDeConhecimentos { get; set; }
    }
  
}