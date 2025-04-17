using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevJANGO.App.ViewModels
{
    public class GrauDeParentescoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Grau de parentesco")] 
        public string NomeGrauParentesco { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoInscritoViewModel> AlunoInscritos { get; set; }
    }
}