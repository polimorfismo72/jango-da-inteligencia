namespace DevJANGO.Business.Models
{
    public class ProfessorDisciplinaClasse : Entity
    {
        public Guid ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }
        public string NomeDisciplina { get; set; }
        public Guid ClasseId { get; set; }
        public string NomeClasse { get; set; }
        public string AnoLetivo { get; set; }

        //public string NomeDisciplina { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public Professor Professor { get; set; }
        public Disciplina Disciplina { get; set; }
        public Classe Classe { get; set; }

    }

}