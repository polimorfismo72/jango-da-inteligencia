using DevJANGO.App.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class AvaliacaoViewModel 
  {
        [Key]
        public Guid Id { get; set; }
        [Range(0,20, ErrorMessage = "O valor da avaliação deve ser de 0 à 20!")]
        public decimal Nota { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(9, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
        [DisplayName("Ano Letivo")]
        public string AnoLetivo { get; set; } 
        public DateTime DataCadastro { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("AlunoMatriculado")]
        public Guid AlunoMatriculadoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("TipoAvaliacao")]
        public Guid TipoAvaliacaoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Trimestre")]
        public Guid TrimestreId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Classe")]
        public Guid ClasseId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Turma")]
        public Guid TurmaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Disciplina")]
        public Guid DisciplinaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Professor")]
        public Guid ProfessorId { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public AlunoMatriculadoViewModel AlunoMatriculado { get; set; }
        public TipoAvaliacaoViewModel TipoAvaliacao { get; set; }
        public TrimestreViewModel Trimestre { get; set; }
        public ClasseViewModel Classe{ get; set; }
        public TurmaViewModel Turma { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public ProfessorViewModel Professor { get; set; }

    
    }

}
