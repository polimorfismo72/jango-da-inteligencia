using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevJANGO.Business.Models
{
    public class AlunoInscrito : Entity
    {
        public int Codigo { get; set; }
        public Guid NiveisDeEnsinoId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid EncarregadoId { get; set; }
        public Guid GrauDeParentescoId { get; set; }
        public Guid AreaDeConhecimentoId { get; set; }
        public Guid FuncionarioCaixaId { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string NomeDoPai { get; set; }
        public string NomeDaMae { get; set; }

        public DateTime Datanascimento { get; set; }
        public string Imagem { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string NumDocumento { get; set; }
        
        public string EscolaDeOrgigem { get; set; }
        public DateTime DataCadastro { get; set; }
        public string AnoLetivo { get; set; }
      
        public decimal ValorDaInscricao { get; set; }
        public bool Estado { get; set; }

        public bool Sexo { get; set; }
        public string Endereco { get; set; }
        public bool AlunoInterno { get; set; }

        /* EF Relations, Lado MUITO na Entidade */
        public NiveisDeEnsino NiveisDeEnsino { get; set; }
        public Classe Classe { get; set; }
        public Encarregado Encarregado { get; set; }
        public GrauDeParentesco GrauDeParentesco { get; set; }
        public AreaDeConhecimento AreaDeConhecimento { get; set; }
        public FuncionarioCaixa FuncionarioCaixa { get; set; }
        

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; }
    }
}