using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class ProfessorDisciplinaClasseViewModel
    {
         [Key]
        public Guid Id { get; set; }
       

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Professor")]
        public Guid ProfessorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Disciplina")]
        public Guid DisciplinaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Classe")]
        public Guid ClasseId { get; set; }

        [DisplayName("Disciplina")]
        public string NomeDisciplina { get; set; }

        [DisplayName("Classe")]
        public string NomeClasse { get; set; }
        [DisplayName("Ano letivo")]
        public string AnoLetivo { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("telefone Ou BI")]
        public string TelefoneBI { get; set; }
        
        [ScaffoldColumn(false)]
        public string Nome { get; set; }
        [ScaffoldColumn(false)]
        public int DisciplinasAssociadas { get; set; }
        
        public ProfessorViewModel Professor { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public ClasseViewModel Classe { get; set; }

        public IEnumerable<ProfessorViewModel> Professores { get; set; }
        public IEnumerable<DisciplinaViewModel> Disciplinas { get; set; }
        public IEnumerable<ClasseViewModel> Classes { get; set; }

    }
}