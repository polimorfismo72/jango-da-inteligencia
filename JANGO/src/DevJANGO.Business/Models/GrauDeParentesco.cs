namespace DevJANGO.Business.Models
{
    public class GrauDeParentesco : Entity
    {
        public string NomeGrauParentesco { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; } 
        public IEnumerable<AlunoInscrito> AlunoInscritos { get; set; }
    }
}