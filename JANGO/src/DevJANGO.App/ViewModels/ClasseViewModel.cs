using DevJANGO.App.Extensions;
using DevJANGO.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevJANGO.App.ViewModels
{
    public class ClasseViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Preço da Propina")]
        public decimal PrecoPropina { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Niveis DeEnsino")]
        public Guid NiveisDeEnsinoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Curso")]
        public Guid CursoId { get; set; }

        public bool ClassDeExame { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public NiveisDeEnsinoViewModel NiveisDeEnsino { get; set; }
        public CursoViewModel Curso { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculadoViewModel> AlunoMatriculados { get; set; } 
        public IEnumerable<AlunoInscritoViewModel> AlunoInscritos { get; set; }
        public IEnumerable<TurmaViewModel> Turmas { get; set; }
        public IEnumerable<NiveisDeEnsinoViewModel> NiveisDeEnsinos { get; set; }
        public IEnumerable<CursoViewModel> Cursos { get; set; }
        public IEnumerable<AvaliacaoViewModel> Avaliacaos { get; set; }
        public IEnumerable<PropinaViewModel> Propinas { get; set; }
        public IEnumerable<ProfessorDisciplinaClasseViewModel> ProfessorDisciplinaClasses { get; set; }
    }
}