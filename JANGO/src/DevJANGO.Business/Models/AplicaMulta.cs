using System.ComponentModel.DataAnnotations;

namespace DevJANGO.Business.Models
{
    public class AplicaMulta : Entity
    {
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}