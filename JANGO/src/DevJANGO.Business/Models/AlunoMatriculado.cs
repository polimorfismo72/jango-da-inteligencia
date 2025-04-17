//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

namespace DevJANGO.Business.Models
{
    public class AlunoMatriculado : Entity
    {
        public int CodigoAluno { get; set;}
        public string NumDocumento { get; set; }
        public Guid AlunoInscritoId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid TurmaId { get; set; }
        public Guid NiveisDeEnsinoId { get; set; }
        public Guid CursoId { get; set; }
        public Guid EncarregadoId { get; set; }
        public Guid GrauDeParentescoId { get; set; }
        public Guid FuncionarioCaixaId { get; set; }
        
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public bool Sexo { get; set; }
        public int Idade { get; set; }
        public decimal ValorDaMatricula{ get; set; }
        public bool Estado { get; set; }
        public bool Bolseiro { get; set; }
        public string AnoLetivo { get; set; }
        public DateTime DataCadastro { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        
        public AlunoInscrito AlunoInscrito { get; set; }
        public Classe Classe { get; set; }
        public Turma Turma { get; set; }
        public NiveisDeEnsino NiveisDeEnsino { get; set; }
        public Curso Curso { get; set; }
        public Encarregado Encarregado { get; set; }
        public GrauDeParentesco GrauDeParentesco { get; set; }
        public FuncionarioCaixa FuncionarioCaixa { get; set; }
        
        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<Avaliacao> Avaliacaos { get; set; } 
        public IEnumerable<PagamentoPropina> PagamentoPropinas { get; set; }
        public IEnumerable<PagamentoMulta> PagamentoMultas { get; set; }
        public IEnumerable<Propina> Propinas { get; set; } 
        public IEnumerable<Multa> Multas { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }




    }
}