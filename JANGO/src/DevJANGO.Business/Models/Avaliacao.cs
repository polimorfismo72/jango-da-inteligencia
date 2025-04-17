namespace DevJANGO.Business.Models
{
    public class Avaliacao : Entity
    {
        public decimal Nota { get; set; }
        public string AnoLetivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid AlunoMatriculadoId { get; set; }
        public Guid TipoAvaliacaoId { get; set; }
        public Guid TrimestreId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid TurmaId { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid ProfessorId { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public AlunoMatriculado AlunoMatriculado { get; set; }
        public TipoAvaliacao TipoAvaliacao { get; set; }
        public Trimestre Trimestre { get; set; }
        public Classe Classe{ get; set; }
        public Turma Turma { get; set; }
        public Disciplina Disciplina { get; set; }
        public Professor Professor { get; set; }

    
    }

}
