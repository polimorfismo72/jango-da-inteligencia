using DevJANGO.App.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevJANGO.App.ViewModels
{
    public class AlunoMatriculadoViewModel 
    {
        [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public int CodigoAluno { get; set;}

        [ScaffoldColumn(false)]
        public int Numero { get; set;}

        [Required(ErrorMessage = "O número do documento é obrigatório")]
        public string NumDocumento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("AlunoInscrito")]
        public Guid AlunoInscritoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Classe")]
        public Guid ClasseId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Turma")]
        public Guid TurmaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nível de Ensino")]
        public Guid NiveisDeEnsinoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Curso")]
        public Guid CursoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Encarregado")]
        public Guid EncarregadoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Grau de Parentesco")]
        public Guid GrauDeParentescoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Funcionario Caixa")]
        public Guid FuncionarioCaixaId { get; set; }

        [DisplayName("Foto do Aluno")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }

        [ScaffoldColumn(false)]
        public bool Sexo { get; set; }

        [ScaffoldColumn(false)]
        public string AnoLetivoCodigo { get; set; }

        [ScaffoldColumn(false)]
        public int Idade { get; set; }
        [DisplayName("Valor da Matrícula")]
        public decimal ValorDaMatricula { get; set; }
        public bool Estado { get; set; }
        [DisplayName("É Bolseiro?")]
        public bool Bolseiro { get; set; }

        public string AnoLetivo { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        [ScaffoldColumn(false)]
        public DateTime Datanascimento { get; set; }

        #region PagamentoPropinas

        #region Propinas
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Quantos meses vai pagar?")]
        public int QuantidadeMes { get; set; }
        #endregion

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Forneça a Descrição de Pagamento")]
        [DisplayName("Descrição do Pagamento")]
        public string Descricao { get; set; }
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Há desconto?")]
        [DisplayName("Desconto")]
        public decimal ValorDesconto { get; set; }
        [ScaffoldColumn(false)]
        public string NomeClasse { get; set; }
        [ScaffoldColumn(false)]
        public string NomeTurma { get; set; }
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Selecione o Tipo de Pagamento")]
        public int TipoPagamento { get; set; }
    
        #endregion

        /* EF Relations, Lado MUITO na Entidade */

        public AlunoInscritoViewModel AlunoInscrito { get; set; }
        public ClasseViewModel Classe { get; set; }
        public TurmaViewModel Turma { get; set; }
        public NiveisDeEnsinoViewModel NiveisDeEnsino { get; set; }
        public CursoViewModel Curso { get; set; }
        public EncarregadoViewModel Encarregado { get; set; }
        public GrauDeParentescoViewModel GrauDeParentesco { get; set; }
        public FuncionarioCaixaViewModel FuncionarioCaixa { get; set; }
        public PropinaViewModel Propina { get; set; }

        /* EF Relations, Lado UM na Entidade */
       
        public IEnumerable<AlunoInscritoViewModel> AlunoInscritos { get; set; }
        public IEnumerable<ClasseViewModel> Classes { get; set; }
        public IEnumerable<TurmaViewModel> Turmas { get; set; }
        public IEnumerable<NiveisDeEnsinoViewModel> NiveisDeEnsinos { get; set; }
        public IEnumerable<CursoViewModel> Cursos { get; set; }
        public IEnumerable<EncarregadoViewModel> Encarregados { get; set; }
        public IEnumerable<GrauDeParentescoViewModel> GrauDeParentescos { get; set; }
        public IEnumerable<FuncionarioCaixaViewModel> FuncionarioCaixas { get; set; }
        public IEnumerable<AvaliacaoViewModel> Avaliacaos { get; set; } 
        public IEnumerable<PagamentoPropinaViewModel> PagamentoPropinas { get; set; }
        public IEnumerable<PagamentoMultaViewModel> PagamentoMultas { get; set; }
        public IEnumerable<PropinaViewModel> Propinas { get; set; } 
        public IEnumerable<MultaViewModel> Multas { get; set; }
        //-----------------------
        public IEnumerable<PedidoViewModel> Pedidos { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public IEnumerable<PedidoItemViewModel> PedidoItens { get; set; }
        public List<AlunoMatriculadoViewModel> RetornarListaAlunoMatriculado()
        {
            return new List<AlunoMatriculadoViewModel>();
        }

        public List<PropinaViewModel> PropinaDetalhe { get; set; }
        public List<PagamentoPropinaViewModel> PagamentoPropinaDetalhe { get; set; }



    }
}