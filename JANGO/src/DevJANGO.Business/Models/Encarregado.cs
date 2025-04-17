using System.ComponentModel.DataAnnotations;

namespace DevJANGO.Business.Models
{
    public class Encarregado : Entity
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Proficao { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; } 
        public IEnumerable<AlunoInscrito> AlunoInscritos { get; set; }
    }
}