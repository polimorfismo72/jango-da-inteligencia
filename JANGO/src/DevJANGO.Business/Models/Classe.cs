namespace DevJANGO.Business.Models
{
    public class Classe : Entity
    {
        public string Nome { get; set; }
        public decimal PrecoPropina { get; set; }
        public Guid NiveisDeEnsinoId { get; set; }
        public Guid CursoId { get; set; }
        public bool ClassDeExame { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public NiveisDeEnsino NiveisDeEnsino { get; set; }
        public Curso Curso { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; } 
        public IEnumerable<AlunoInscrito> AlunoInscritos { get; set; } 
        public IEnumerable<Turma> Turmas { get; set; } 
        public IEnumerable<Avaliacao> Avaliacaos { get; set; } 
        //public IEnumerable<PagamentoPropina> PagamentoPropinas { get; set; } 
        public IEnumerable<Propina> Propinas { get; set; }
        public IEnumerable<ProfessorDisciplinaClasse> ProfessorDisciplinaClasses { get; set; }
    }
}