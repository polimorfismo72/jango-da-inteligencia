namespace DevJANGO.Business.Models
{
    public class Curso : Entity
    {
        public string Nome { get; set; }
        //public string NiveisDeEnsinoId { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; } 
        public IEnumerable<Classe> Classes { get; set; }
    }
}