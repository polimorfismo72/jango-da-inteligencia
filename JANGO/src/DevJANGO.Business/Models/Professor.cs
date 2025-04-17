namespace DevJANGO.Business.Models
{
    public class Professor : Entity
    {
        public string Nome { get; set; }
        public string BI { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<Avaliacao> Avaliacaos { get; set; }
        public IEnumerable<ProfessorDisciplinaClasse> ProfessorDisciplinaClasses { get; set; }

    }

}