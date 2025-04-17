namespace DevJANGO.Business.Models
{
    public class Multa : Entity
    {
        public Guid MesId { get; set; }
        public Guid AlunoMatriculadoId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid TurmaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string DescricaoMulta { get; set; }
        public decimal PrecoPropina { get; set; }
        public bool Estado { get; set; }
        public string AnoLetivo { get; set; }

        /* EF Relations, Lado MUITO na Entidade Multa */
        public Mes Mes { get; set; }
        public AlunoMatriculado AlunoMatriculado { get; set; }
        public Classe Classe { get; set; }
        public Turma Turma { get; set; }

    }
}
