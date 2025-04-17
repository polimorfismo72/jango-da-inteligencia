using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia");

            migrationBuilder.CreateTable(
                name: "AreaDeConhecimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaDeConhecimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(31)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeDisciplina = table.Column<string>(type: "varchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encarregados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(16)", nullable: false),
                    Proficao = table.Column<string>(type: "varchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encarregados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioCaixas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioCaixas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrauDeParentescos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeGrauParentesco = table.Column<string>(type: "varchar(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrauDeParentescos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeMes = table.Column<string>(type: "varchar(9)", nullable: false),
                    CodMes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NiveisDeEnsinos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeNiveisDeEnsino = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiveisDeEnsinos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAvaliacaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAvaliacaos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trimestres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(13)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trimestres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", nullable: false),
                    BI = table.Column<string>(type: "varchar(16)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(9)", nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professores_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(10)", nullable: false),
                    PrecoPropina = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NiveisDeEnsinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassDeExame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_NiveisDeEnsinos_NiveisDeEnsinoId",
                        column: x => x.NiveisDeEnsinoId,
                        principalTable: "NiveisDeEnsinos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlunoInscritos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    NiveisDeEnsinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncarregadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrauDeParentescoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaDeConhecimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    NomeDoPai = table.Column<string>(type: "varchar(60)", nullable: false),
                    NomeDaMae = table.Column<string>(type: "varchar(60)", nullable: false),
                    Datanascimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    Imagem = table.Column<string>(type: "varchar(250)", nullable: false),
                    TipoDocumento = table.Column<int>(type: "int", nullable: false),
                    NumDocumento = table.Column<string>(type: "varchar(15)", nullable: false),
                    EscolaDeOrgigem = table.Column<string>(type: "varchar(60)", nullable: false),
                    NumPautaDaEscolaOrigem = table.Column<string>(type: "varchar(10)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<bool>(type: "bit", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoInscritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlunoInscritos_AreaDeConhecimentos_AreaDeConhecimentoId",
                        column: x => x.AreaDeConhecimentoId,
                        principalTable: "AreaDeConhecimentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoInscritos_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoInscritos_Encarregados_EncarregadoId",
                        column: x => x.EncarregadoId,
                        principalTable: "Encarregados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoInscritos_GrauDeParentescos_GrauDeParentescoId",
                        column: x => x.GrauDeParentescoId,
                        principalTable: "GrauDeParentescos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoInscritos_NiveisDeEnsinos_NiveisDeEnsinoId",
                        column: x => x.NiveisDeEnsinoId,
                        principalTable: "NiveisDeEnsinos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeTurma = table.Column<string>(type: "varchar(10)", nullable: false),
                    NumDeVagas = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlunoMatriculados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoAluno = table.Column<int>(type: "int", nullable: false),
                    NumDocumento = table.Column<string>(type: "varchar(15)", nullable: false),
                    AlunoInscritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NiveisDeEnsinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncarregadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrauDeParentescoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", nullable: false),
                    Imagem = table.Column<string>(type: "varchar(250)", nullable: false),
                    Sexo = table.Column<bool>(type: "bit", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    AnoLetivo = table.Column<string>(type: "varchar(9)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoMatriculados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlunoMatriculados_AlunoInscritos_AlunoInscritoId",
                        column: x => x.AlunoInscritoId,
                        principalTable: "AlunoInscritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoMatriculados_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoMatriculados_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoMatriculados_Encarregados_EncarregadoId",
                        column: x => x.EncarregadoId,
                        principalTable: "Encarregados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoMatriculados_GrauDeParentescos_GrauDeParentescoId",
                        column: x => x.GrauDeParentescoId,
                        principalTable: "GrauDeParentescos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoMatriculados_NiveisDeEnsinos_NiveisDeEnsinoId",
                        column: x => x.NiveisDeEnsinoId,
                        principalTable: "NiveisDeEnsinos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlunoMatriculados_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Avaliacaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nota = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    AnoLetivo = table.Column<string>(type: "varchar(9)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlunoMatriculadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoAvaliacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrimestreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacaos_AlunoMatriculados_AlunoMatriculadoId",
                        column: x => x.AlunoMatriculadoId,
                        principalTable: "AlunoMatriculados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacaos_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacaos_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacaos_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacaos_TipoAvaliacaos_TipoAvaliacaoId",
                        column: x => x.TipoAvaliacaoId,
                        principalTable: "TipoAvaliacaos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacaos_Trimestres_TrimestreId",
                        column: x => x.TrimestreId,
                        principalTable: "Trimestres",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacaos_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Multas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoMatriculadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescricaoMulta = table.Column<string>(type: "varchar(29)", nullable: false),
                    PrecoPropina = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    AnoLetivo = table.Column<string>(type: "varchar(9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multas_AlunoMatriculados_AlunoMatriculadoId",
                        column: x => x.AlunoMatriculadoId,
                        principalTable: "AlunoMatriculados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Multas_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Multas_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Multas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PagamentoMultas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    AlunoMatriculadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PercentualDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoPagamento = table.Column<int>(type: "int", nullable: false),
                    NumeroDeTransacaoDePagamento = table.Column<string>(type: "varchar(150)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoMultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoMultas_AlunoMatriculados_AlunoMatriculadoId",
                        column: x => x.AlunoMatriculadoId,
                        principalTable: "AlunoMatriculados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PagamentoPropinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    MesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoMatriculadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PercentualDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoPagamento = table.Column<int>(type: "int", nullable: false),
                    NumeroDeTransacaoDePagamento = table.Column<string>(type: "varchar(29)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoPropinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoPropinas_AlunoMatriculados_AlunoMatriculadoId",
                        column: x => x.AlunoMatriculadoId,
                        principalTable: "AlunoMatriculados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagamentoPropinas_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Propinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoMatriculadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescricaoPropina = table.Column<string>(type: "varchar(29)", nullable: false),
                    PrecoPropina = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    AnoLetivo = table.Column<string>(type: "varchar(9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Propinas_AlunoMatriculados_AlunoMatriculadoId",
                        column: x => x.AlunoMatriculadoId,
                        principalTable: "AlunoMatriculados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Propinas_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Propinas_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Propinas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PagamentoMultaItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PagamentoMultaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MultaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionarioCaixaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeMulta = table.Column<string>(type: "varchar(29)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoMulta = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoMultaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoMultaItems_FuncionarioCaixas_FuncionarioCaixaId",
                        column: x => x.FuncionarioCaixaId,
                        principalTable: "FuncionarioCaixas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagamentoMultaItems_Multas_MultaId",
                        column: x => x.MultaId,
                        principalTable: "Multas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagamentoMultaItems_PagamentoMultas_PagamentoMultaId",
                        column: x => x.PagamentoMultaId,
                        principalTable: "PagamentoMultas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PagamentoPropinaItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PagamentoPropinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionarioCaixaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomePropina = table.Column<string>(type: "varchar(29)", nullable: false),
                    NumeroDeMeses = table.Column<int>(type: "int", nullable: false),
                    PrecoPropina = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoPropinaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoPropinaItems_FuncionarioCaixas_FuncionarioCaixaId",
                        column: x => x.FuncionarioCaixaId,
                        principalTable: "FuncionarioCaixas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagamentoPropinaItems_PagamentoPropinas_PagamentoPropinaId",
                        column: x => x.PagamentoPropinaId,
                        principalTable: "PagamentoPropinas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagamentoPropinaItems_Propinas_PropinaId",
                        column: x => x.PropinaId,
                        principalTable: "Propinas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoInscritos_AreaDeConhecimentoId",
                table: "AlunoInscritos",
                column: "AreaDeConhecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoInscritos_ClasseId",
                table: "AlunoInscritos",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoInscritos_EncarregadoId",
                table: "AlunoInscritos",
                column: "EncarregadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoInscritos_GrauDeParentescoId",
                table: "AlunoInscritos",
                column: "GrauDeParentescoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoInscritos_NiveisDeEnsinoId",
                table: "AlunoInscritos",
                column: "NiveisDeEnsinoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_AlunoInscritoId",
                table: "AlunoMatriculados",
                column: "AlunoInscritoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_ClasseId",
                table: "AlunoMatriculados",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_CursoId",
                table: "AlunoMatriculados",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_EncarregadoId",
                table: "AlunoMatriculados",
                column: "EncarregadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_GrauDeParentescoId",
                table: "AlunoMatriculados",
                column: "GrauDeParentescoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_NiveisDeEnsinoId",
                table: "AlunoMatriculados",
                column: "NiveisDeEnsinoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_TurmaId",
                table: "AlunoMatriculados",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_AlunoMatriculadoId",
                table: "Avaliacaos",
                column: "AlunoMatriculadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_ClasseId",
                table: "Avaliacaos",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_DisciplinaId",
                table: "Avaliacaos",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_ProfessorId",
                table: "Avaliacaos",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_TipoAvaliacaoId",
                table: "Avaliacaos",
                column: "TipoAvaliacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_TrimestreId",
                table: "Avaliacaos",
                column: "TrimestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_TurmaId",
                table: "Avaliacaos",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CursoId",
                table: "Classes",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_NiveisDeEnsinoId",
                table: "Classes",
                column: "NiveisDeEnsinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Multas_AlunoMatriculadoId",
                table: "Multas",
                column: "AlunoMatriculadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Multas_ClasseId",
                table: "Multas",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Multas_MesId",
                table: "Multas",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Multas_TurmaId",
                table: "Multas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoMultaItems_FuncionarioCaixaId",
                table: "PagamentoMultaItems",
                column: "FuncionarioCaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoMultaItems_MultaId",
                table: "PagamentoMultaItems",
                column: "MultaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoMultaItems_PagamentoMultaId",
                table: "PagamentoMultaItems",
                column: "PagamentoMultaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoMultas_AlunoMatriculadoId",
                table: "PagamentoMultas",
                column: "AlunoMatriculadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinaItems_FuncionarioCaixaId",
                table: "PagamentoPropinaItems",
                column: "FuncionarioCaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinaItems_PagamentoPropinaId",
                table: "PagamentoPropinaItems",
                column: "PagamentoPropinaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinaItems_PropinaId",
                table: "PagamentoPropinaItems",
                column: "PropinaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinas_AlunoMatriculadoId",
                table: "PagamentoPropinas",
                column: "AlunoMatriculadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinas_MesId",
                table: "PagamentoPropinas",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Professores_DisciplinaId",
                table: "Professores",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Propinas_AlunoMatriculadoId",
                table: "Propinas",
                column: "AlunoMatriculadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Propinas_ClasseId",
                table: "Propinas",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Propinas_MesId",
                table: "Propinas",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Propinas_TurmaId",
                table: "Propinas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ClasseId",
                table: "Turmas",
                column: "ClasseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacaos");

            migrationBuilder.DropTable(
                name: "PagamentoMultaItems");

            migrationBuilder.DropTable(
                name: "PagamentoPropinaItems");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropTable(
                name: "TipoAvaliacaos");

            migrationBuilder.DropTable(
                name: "Trimestres");

            migrationBuilder.DropTable(
                name: "Multas");

            migrationBuilder.DropTable(
                name: "PagamentoMultas");

            migrationBuilder.DropTable(
                name: "FuncionarioCaixas");

            migrationBuilder.DropTable(
                name: "PagamentoPropinas");

            migrationBuilder.DropTable(
                name: "Propinas");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "AlunoMatriculados");

            migrationBuilder.DropTable(
                name: "Meses");

            migrationBuilder.DropTable(
                name: "AlunoInscritos");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "AreaDeConhecimentos");

            migrationBuilder.DropTable(
                name: "Encarregados");

            migrationBuilder.DropTable(
                name: "GrauDeParentescos");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "NiveisDeEnsinos");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");
        }
    }
}
