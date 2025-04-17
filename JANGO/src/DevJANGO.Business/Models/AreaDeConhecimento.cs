namespace DevJANGO.Business.Models
{
    public class AreaDeConhecimento : Entity
    {
        public string Nome { get; set; }

        /* EF Relations, Lado UM na Entidade */
        //public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; }
        public IEnumerable<AlunoInscrito> AlunoInscritos { get; set; }
        public IEnumerable<Turma> Turmas { get; set; }
    }
}