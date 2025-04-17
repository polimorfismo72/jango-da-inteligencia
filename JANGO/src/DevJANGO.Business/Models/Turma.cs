namespace DevJANGO.Business.Models
{
    public class Turma : Entity
    {
        public Guid ClasseId { get; set; }
        public Guid AreaDeConhecimentoId { get; set; }
        public string NomeTurma { get; set; }
        public int NumDeVagas { get; set; }
        public bool Estado { get; set; }

        public Classe Classe { get; set; }
        public AreaDeConhecimento AreaDeConhecimento { get; set; }

        /* EF Relations, Lado UM na Entidade Turma */
        public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; }
        public IEnumerable<Avaliacao> Avaliacaos { get; set; }
        public IEnumerable<Propina> Propinas { get; set; }
    }
}