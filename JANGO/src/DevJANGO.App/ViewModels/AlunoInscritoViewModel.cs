using DevJANGO.App.Extensions;
using DevJANGO.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class AlunoInscritoViewModel
    {
        #region ATRIBUTOS DA MODEL
        [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nivel de Ensino")]
        public Guid NiveisDeEnsinoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Classe")]
        public Guid ClasseId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Encarregado")]
        public Guid EncarregadoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Grau de Parentesco")]
        public Guid GrauDeParentescoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Area de Conhecimento")]
        public Guid AreaDeConhecimentoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Funcionario")]
        public Guid FuncionarioCaixaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Nome do aluno")]
        public string Nome { get; set; }
        
        [ScaffoldColumn(false)]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Nome do Pai")]
        public string NomeDoPai { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Nome da Mãe")] 
        public string NomeDaMae { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime Datanascimento { get; set; }

        [DisplayName("Foto")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }

        [DisplayName("Documento")]
        public int TipoDocumento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Número BI/Cédula")]
        public string NumDocumento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Escola de Origem")]
        public string EscolaDeOrgigem { get; set; }
        

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public string NomeClasse { get; set; }
        public string AnoLetivo { get; set; }

        [DisplayName("Valor da Inscrição")]
        public decimal ValorDaInscricao { get; set; }

        [DisplayName("Validar Inscrição")]
        public bool Estado { get; set; }

        [DisplayName("Masculino")]
        public bool Sexo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        [DisplayName("É Aluno Interno?")]
        public bool AlunoInterno { get; set; }
        /* EF Relations, Lado MUITO na Entidade */
        public NiveisDeEnsinoViewModel NiveisDeEnsino { get; set; }
        public ClasseViewModel Classe { get; set; }
        public EncarregadoViewModel Encarregado { get; set; }
        public GrauDeParentescoViewModel GrauDeParentesco { get; set; }
        public AreaDeConhecimentoViewModel AreaDeConhecimento { get; set; }
        public FuncionarioCaixaViewModel FuncionarioCaixa { get; set; }

        /* EF Relations, Lado UM na Entidade */

        public IEnumerable<AlunoMatriculadoViewModel> AlunoMatriculados { get; set; }
       
        public IEnumerable<NiveisDeEnsinoViewModel> NiveisDeEnsinos { get; set; }
       
        public IEnumerable<ClasseViewModel> Classes { get; set; }
       
        public IEnumerable<EncarregadoViewModel> Encarregados { get; set; }
       
        public IEnumerable<GrauDeParentescoViewModel> GrauDeParentescos { get; set; }
       
        public IEnumerable<AreaDeConhecimentoViewModel> AreaDeConhecimentos { get; set; }
        //public IEnumerable<FuncionarioCaixaViewModel> FuncionarioCaixas { get; set; }
        #endregion
    }
}