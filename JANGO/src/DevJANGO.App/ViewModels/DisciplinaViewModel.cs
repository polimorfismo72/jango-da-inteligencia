using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class DisciplinaViewModel
  {
      [Key]
       public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Niveis DeEnsino")]
        public Guid NiveisDeEnsinoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(45, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Disciplina")] 
        public string NomeDisciplina { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public NiveisDeEnsinoViewModel NiveisDeEnsino { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<NiveisDeEnsinoViewModel> NiveisDeEnsinos { get; set; }
        public IEnumerable<AvaliacaoViewModel> Avaliacaos { get; set; }
        public IEnumerable<ProfessorViewModel> Professores { get; set; }
        public IEnumerable<ProfessorDisciplinaClasseViewModel> ProfessorDisciplinaClasses { get; set; }
    }

}