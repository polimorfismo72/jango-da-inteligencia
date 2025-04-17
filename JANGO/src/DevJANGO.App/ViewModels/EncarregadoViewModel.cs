using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevJANGO.App.ViewModels
{
    public class EncarregadoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "O campo {0}  só permite números")]
        [StringLength(9, ErrorMessage = "O campo {0} deve ter exatamente {1} caracteres", MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(25, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Profissão")] 
        public string Proficao { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoInscritoViewModel> AlunoInscritos { get; set; }
    }
}