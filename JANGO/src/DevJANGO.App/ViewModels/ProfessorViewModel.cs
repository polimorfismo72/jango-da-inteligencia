using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace DevJANGO.App.ViewModels
{
    public class ProfessorViewModel
    {
         [Key]
        public Guid Id { get; set; }
       
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(16, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string BI { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "O campo {0}  só permite números")]
        [StringLength(9, ErrorMessage = "O campo {0} deve ter exatamente {1} caracteres", MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        
        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AvaliacaoViewModel> Avaliacaos { get; set; }
        public IEnumerable<ProfessorDisciplinaClasseViewModel> ProfessorDisciplinaClasses { get; set; }

    }

}