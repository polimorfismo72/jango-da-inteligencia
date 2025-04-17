using DevJANGO.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class AplicaMultaViewModel
    {
         [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public string Nome { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        [ScaffoldColumn(false)]
        public string Usuario { get; set; }
       
       

    }
}