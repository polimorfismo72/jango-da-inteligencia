using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class MesViewModel 
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(9, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Mes")]
        public string NomeMes { get; set; }
        [DisplayName("Código do Mês")]
        public int CodMes { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Mês a pagar?")]
        public int Mes { get; set; }
        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<PagamentoPropinaViewModel> PagamentoPropinas { get; set; } 
        public IEnumerable<PropinaViewModel> Propinas { get; set; } 
        public IEnumerable<MultaViewModel> Multas { get; set; }
      
    }

}