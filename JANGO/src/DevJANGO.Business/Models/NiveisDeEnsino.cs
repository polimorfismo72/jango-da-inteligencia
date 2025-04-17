namespace DevJANGO.Business.Models
{
    public class NiveisDeEnsino : Entity
    {
        public string NomeNiveisDeEnsino { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; }
        public IEnumerable<AlunoInscrito> AlunoInscritos { get; set; } 
        public IEnumerable<Classe> Classes { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
      
    }
}