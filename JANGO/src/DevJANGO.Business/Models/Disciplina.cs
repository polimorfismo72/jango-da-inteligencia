namespace DevJANGO.Business.Models
{
    public class Disciplina : Entity
    {
        public Guid NiveisDeEnsinoId { get; set; }
        public string NomeDisciplina { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public NiveisDeEnsino NiveisDeEnsino { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<Avaliacao> Avaliacaos { get; set; } 
        //public IEnumerable<Professor> Professores { get; set; }
        public IEnumerable<ProfessorDisciplinaClasse> ProfessorDisciplinaClasses { get; set; }
    }

}