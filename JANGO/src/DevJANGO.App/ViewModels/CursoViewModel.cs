using DevJANGO.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevJANGO.App.ViewModels
{
    public class CursoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(32, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculadoViewModel> AlunoMatriculados { get; set; }
        public IEnumerable<ClasseViewModel> Classes { get; set; }
    }
}