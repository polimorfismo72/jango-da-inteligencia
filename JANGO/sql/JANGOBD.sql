IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [MinhaSequencia] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [AreaDeConhecimentos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(10) NOT NULL,
    CONSTRAINT [PK_AreaDeConhecimentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cursos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(31) NOT NULL,
    CONSTRAINT [PK_Cursos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Disciplinas] (
    [Id] uniqueidentifier NOT NULL,
    [NomeDisciplina] varchar(25) NOT NULL,
    CONSTRAINT [PK_Disciplinas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Encarregados] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(60) NOT NULL,
    [Telefone] varchar(16) NOT NULL,
    [Proficao] varchar(25) NOT NULL,
    CONSTRAINT [PK_Encarregados] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FuncionarioCaixas] (
    [Id] uniqueidentifier NOT NULL,
    [Email] varchar(254) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_FuncionarioCaixas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [GrauDeParentescos] (
    [Id] uniqueidentifier NOT NULL,
    [NomeGrauParentesco] varchar(60) NOT NULL,
    CONSTRAINT [PK_GrauDeParentescos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Meses] (
    [Id] uniqueidentifier NOT NULL,
    [NomeMes] varchar(9) NOT NULL,
    [CodMes] int NOT NULL,
    CONSTRAINT [PK_Meses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [NiveisDeEnsinos] (
    [Id] uniqueidentifier NOT NULL,
    [NomeNiveisDeEnsino] varchar(10) NOT NULL,
    CONSTRAINT [PK_NiveisDeEnsinos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoAvaliacaos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(18) NOT NULL,
    CONSTRAINT [PK_TipoAvaliacaos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Trimestres] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(13) NOT NULL,
    CONSTRAINT [PK_Trimestres] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Professores] (
    [Id] uniqueidentifier NOT NULL,
    [DisciplinaId] uniqueidentifier NOT NULL,
    [Nome] varchar(60) NOT NULL,
    [BI] varchar(16) NOT NULL,
    [Telefone] varchar(9) NOT NULL,
    [Email] varchar(254) NOT NULL,
    [Endereco] varchar(250) NOT NULL,
    CONSTRAINT [PK_Professores] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Professores_Disciplinas_DisciplinaId] FOREIGN KEY ([DisciplinaId]) REFERENCES [Disciplinas] ([Id])
);
GO

CREATE TABLE [Classes] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(10) NOT NULL,
    [PrecoPropina] decimal(18,2) NOT NULL,
    [NiveisDeEnsinoId] uniqueidentifier NOT NULL,
    [CursoId] uniqueidentifier NOT NULL,
    [ClassDeExame] bit NOT NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Classes_Cursos_CursoId] FOREIGN KEY ([CursoId]) REFERENCES [Cursos] ([Id]),
    CONSTRAINT [FK_Classes_NiveisDeEnsinos_NiveisDeEnsinoId] FOREIGN KEY ([NiveisDeEnsinoId]) REFERENCES [NiveisDeEnsinos] ([Id])
);
GO

CREATE TABLE [AlunoInscritos] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL DEFAULT (NEXT VALUE FOR MinhaSequencia),
    [NiveisDeEnsinoId] uniqueidentifier NOT NULL,
    [ClasseId] uniqueidentifier NOT NULL,
    [EncarregadoId] uniqueidentifier NOT NULL,
    [GrauDeParentescoId] uniqueidentifier NOT NULL,
    [AreaDeConhecimentoId] uniqueidentifier NOT NULL,
    [Nome] varchar(60) NOT NULL,
    [Idade] int NOT NULL,
    [NomeDoPai] varchar(60) NOT NULL,
    [NomeDaMae] varchar(60) NOT NULL,
    [Datanascimento] datetime NOT NULL,
    [Imagem] varchar(250) NOT NULL,
    [TipoDocumento] int NOT NULL,
    [NumDocumento] varchar(15) NOT NULL,
    [EscolaDeOrgigem] varchar(60) NOT NULL,
    [NumPautaDaEscolaOrigem] varchar(10) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [Sexo] bit NOT NULL,
    [Endereco] varchar(250) NOT NULL,
    CONSTRAINT [PK_AlunoInscritos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AlunoInscritos_AreaDeConhecimentos_AreaDeConhecimentoId] FOREIGN KEY ([AreaDeConhecimentoId]) REFERENCES [AreaDeConhecimentos] ([Id]),
    CONSTRAINT [FK_AlunoInscritos_Classes_ClasseId] FOREIGN KEY ([ClasseId]) REFERENCES [Classes] ([Id]),
    CONSTRAINT [FK_AlunoInscritos_Encarregados_EncarregadoId] FOREIGN KEY ([EncarregadoId]) REFERENCES [Encarregados] ([Id]),
    CONSTRAINT [FK_AlunoInscritos_GrauDeParentescos_GrauDeParentescoId] FOREIGN KEY ([GrauDeParentescoId]) REFERENCES [GrauDeParentescos] ([Id]),
    CONSTRAINT [FK_AlunoInscritos_NiveisDeEnsinos_NiveisDeEnsinoId] FOREIGN KEY ([NiveisDeEnsinoId]) REFERENCES [NiveisDeEnsinos] ([Id])
);
GO

CREATE TABLE [Turmas] (
    [Id] uniqueidentifier NOT NULL,
    [ClasseId] uniqueidentifier NOT NULL,
    [NomeTurma] varchar(10) NOT NULL,
    [NumDeVagas] int NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Turmas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Turmas_Classes_ClasseId] FOREIGN KEY ([ClasseId]) REFERENCES [Classes] ([Id])
);
GO

CREATE TABLE [AlunoMatriculados] (
    [Id] uniqueidentifier NOT NULL,
    [CodigoAluno] int NOT NULL,
    [NumDocumento] varchar(15) NOT NULL,
    [AlunoInscritoId] uniqueidentifier NOT NULL,
    [ClasseId] uniqueidentifier NOT NULL,
    [TurmaId] uniqueidentifier NOT NULL,
    [NiveisDeEnsinoId] uniqueidentifier NOT NULL,
    [CursoId] uniqueidentifier NOT NULL,
    [EncarregadoId] uniqueidentifier NOT NULL,
    [GrauDeParentescoId] uniqueidentifier NOT NULL,
    [Nome] varchar(60) NOT NULL,
    [Imagem] varchar(250) NOT NULL,
    [Sexo] bit NOT NULL,
    [Idade] int NOT NULL,
    [Estado] bit NOT NULL,
    [AnoLetivo] varchar(9) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_AlunoMatriculados] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AlunoMatriculados_AlunoInscritos_AlunoInscritoId] FOREIGN KEY ([AlunoInscritoId]) REFERENCES [AlunoInscritos] ([Id]),
    CONSTRAINT [FK_AlunoMatriculados_Classes_ClasseId] FOREIGN KEY ([ClasseId]) REFERENCES [Classes] ([Id]),
    CONSTRAINT [FK_AlunoMatriculados_Cursos_CursoId] FOREIGN KEY ([CursoId]) REFERENCES [Cursos] ([Id]),
    CONSTRAINT [FK_AlunoMatriculados_Encarregados_EncarregadoId] FOREIGN KEY ([EncarregadoId]) REFERENCES [Encarregados] ([Id]),
    CONSTRAINT [FK_AlunoMatriculados_GrauDeParentescos_GrauDeParentescoId] FOREIGN KEY ([GrauDeParentescoId]) REFERENCES [GrauDeParentescos] ([Id]),
    CONSTRAINT [FK_AlunoMatriculados_NiveisDeEnsinos_NiveisDeEnsinoId] FOREIGN KEY ([NiveisDeEnsinoId]) REFERENCES [NiveisDeEnsinos] ([Id]),
    CONSTRAINT [FK_AlunoMatriculados_Turmas_TurmaId] FOREIGN KEY ([TurmaId]) REFERENCES [Turmas] ([Id])
);
GO

CREATE TABLE [Avaliacaos] (
    [Id] uniqueidentifier NOT NULL,
    [Nota] decimal(18,0) NOT NULL,
    [AnoLetivo] varchar(9) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [AlunoMatriculadoId] uniqueidentifier NOT NULL,
    [TipoAvaliacaoId] uniqueidentifier NOT NULL,
    [TrimestreId] uniqueidentifier NOT NULL,
    [ClasseId] uniqueidentifier NOT NULL,
    [TurmaId] uniqueidentifier NOT NULL,
    [DisciplinaId] uniqueidentifier NOT NULL,
    [ProfessorId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Avaliacaos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Avaliacaos_AlunoMatriculados_AlunoMatriculadoId] FOREIGN KEY ([AlunoMatriculadoId]) REFERENCES [AlunoMatriculados] ([Id]),
    CONSTRAINT [FK_Avaliacaos_Classes_ClasseId] FOREIGN KEY ([ClasseId]) REFERENCES [Classes] ([Id]),
    CONSTRAINT [FK_Avaliacaos_Disciplinas_DisciplinaId] FOREIGN KEY ([DisciplinaId]) REFERENCES [Disciplinas] ([Id]),
    CONSTRAINT [FK_Avaliacaos_Professores_ProfessorId] FOREIGN KEY ([ProfessorId]) REFERENCES [Professores] ([Id]),
    CONSTRAINT [FK_Avaliacaos_TipoAvaliacaos_TipoAvaliacaoId] FOREIGN KEY ([TipoAvaliacaoId]) REFERENCES [TipoAvaliacaos] ([Id]),
    CONSTRAINT [FK_Avaliacaos_Trimestres_TrimestreId] FOREIGN KEY ([TrimestreId]) REFERENCES [Trimestres] ([Id]),
    CONSTRAINT [FK_Avaliacaos_Turmas_TurmaId] FOREIGN KEY ([TurmaId]) REFERENCES [Turmas] ([Id])
);
GO

CREATE TABLE [Multas] (
    [Id] uniqueidentifier NOT NULL,
    [MesId] uniqueidentifier NOT NULL,
    [AlunoMatriculadoId] uniqueidentifier NOT NULL,
    [ClasseId] uniqueidentifier NOT NULL,
    [TurmaId] uniqueidentifier NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [DescricaoMulta] varchar(29) NOT NULL,
    [PrecoPropina] decimal(18,2) NOT NULL,
    [Estado] bit NOT NULL,
    [AnoLetivo] varchar(9) NOT NULL,
    CONSTRAINT [PK_Multas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Multas_AlunoMatriculados_AlunoMatriculadoId] FOREIGN KEY ([AlunoMatriculadoId]) REFERENCES [AlunoMatriculados] ([Id]),
    CONSTRAINT [FK_Multas_Classes_ClasseId] FOREIGN KEY ([ClasseId]) REFERENCES [Classes] ([Id]),
    CONSTRAINT [FK_Multas_Meses_MesId] FOREIGN KEY ([MesId]) REFERENCES [Meses] ([Id]),
    CONSTRAINT [FK_Multas_Turmas_TurmaId] FOREIGN KEY ([TurmaId]) REFERENCES [Turmas] ([Id])
);
GO

CREATE TABLE [PagamentoMultas] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL DEFAULT (NEXT VALUE FOR MinhaSequencia),
    [AlunoMatriculadoId] uniqueidentifier NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [PercentualDesconto] decimal(18,2) NOT NULL,
    [ValorDesconto] decimal(18,2) NOT NULL,
    [TotalPago] decimal(18,2) NOT NULL,
    [TipoPagamento] int NOT NULL,
    [NumeroDeTransacaoDePagamento] varchar(150) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_PagamentoMultas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PagamentoMultas_AlunoMatriculados_AlunoMatriculadoId] FOREIGN KEY ([AlunoMatriculadoId]) REFERENCES [AlunoMatriculados] ([Id])
);
GO

CREATE TABLE [PagamentoPropinas] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL,
    [MesId] uniqueidentifier NOT NULL,
    [AlunoMatriculadoId] uniqueidentifier NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [PercentualDesconto] decimal(18,2) NOT NULL,
    [ValorDesconto] decimal(18,2) NOT NULL,
    [TotalPago] decimal(18,2) NOT NULL,
    [TipoPagamento] int NOT NULL,
    [NumeroDeTransacaoDePagamento] varchar(29) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_PagamentoPropinas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PagamentoPropinas_AlunoMatriculados_AlunoMatriculadoId] FOREIGN KEY ([AlunoMatriculadoId]) REFERENCES [AlunoMatriculados] ([Id]),
    CONSTRAINT [FK_PagamentoPropinas_Meses_MesId] FOREIGN KEY ([MesId]) REFERENCES [Meses] ([Id])
);
GO

CREATE TABLE [Propinas] (
    [Id] uniqueidentifier NOT NULL,
    [MesId] uniqueidentifier NOT NULL,
    [AlunoMatriculadoId] uniqueidentifier NOT NULL,
    [ClasseId] uniqueidentifier NOT NULL,
    [TurmaId] uniqueidentifier NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [DescricaoPropina] varchar(29) NOT NULL,
    [PrecoPropina] decimal(18,2) NOT NULL,
    [Estado] bit NOT NULL,
    [AnoLetivo] varchar(9) NOT NULL,
    CONSTRAINT [PK_Propinas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Propinas_AlunoMatriculados_AlunoMatriculadoId] FOREIGN KEY ([AlunoMatriculadoId]) REFERENCES [AlunoMatriculados] ([Id]),
    CONSTRAINT [FK_Propinas_Classes_ClasseId] FOREIGN KEY ([ClasseId]) REFERENCES [Classes] ([Id]),
    CONSTRAINT [FK_Propinas_Meses_MesId] FOREIGN KEY ([MesId]) REFERENCES [Meses] ([Id]),
    CONSTRAINT [FK_Propinas_Turmas_TurmaId] FOREIGN KEY ([TurmaId]) REFERENCES [Turmas] ([Id])
);
GO

CREATE TABLE [PagamentoMultaItems] (
    [Id] uniqueidentifier NOT NULL,
    [PagamentoMultaId] uniqueidentifier NOT NULL,
    [MultaId] uniqueidentifier NOT NULL,
    [FuncionarioCaixaId] uniqueidentifier NOT NULL,
    [NomeMulta] varchar(29) NOT NULL,
    [Quantidade] int NOT NULL,
    [PrecoMulta] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_PagamentoMultaItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PagamentoMultaItems_FuncionarioCaixas_FuncionarioCaixaId] FOREIGN KEY ([FuncionarioCaixaId]) REFERENCES [FuncionarioCaixas] ([Id]),
    CONSTRAINT [FK_PagamentoMultaItems_Multas_MultaId] FOREIGN KEY ([MultaId]) REFERENCES [Multas] ([Id]),
    CONSTRAINT [FK_PagamentoMultaItems_PagamentoMultas_PagamentoMultaId] FOREIGN KEY ([PagamentoMultaId]) REFERENCES [PagamentoMultas] ([Id])
);
GO

CREATE TABLE [PagamentoPropinaItems] (
    [Id] uniqueidentifier NOT NULL,
    [PagamentoPropinaId] uniqueidentifier NOT NULL,
    [PropinaId] uniqueidentifier NOT NULL,
    [FuncionarioCaixaId] uniqueidentifier NOT NULL,
    [NomePropina] varchar(29) NOT NULL,
    [NumeroDeMeses] int NOT NULL,
    [PrecoPropina] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_PagamentoPropinaItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PagamentoPropinaItems_FuncionarioCaixas_FuncionarioCaixaId] FOREIGN KEY ([FuncionarioCaixaId]) REFERENCES [FuncionarioCaixas] ([Id]),
    CONSTRAINT [FK_PagamentoPropinaItems_PagamentoPropinas_PagamentoPropinaId] FOREIGN KEY ([PagamentoPropinaId]) REFERENCES [PagamentoPropinas] ([Id]),
    CONSTRAINT [FK_PagamentoPropinaItems_Propinas_PropinaId] FOREIGN KEY ([PropinaId]) REFERENCES [Propinas] ([Id])
);
GO

CREATE INDEX [IX_AlunoInscritos_AreaDeConhecimentoId] ON [AlunoInscritos] ([AreaDeConhecimentoId]);
GO

CREATE INDEX [IX_AlunoInscritos_ClasseId] ON [AlunoInscritos] ([ClasseId]);
GO

CREATE INDEX [IX_AlunoInscritos_EncarregadoId] ON [AlunoInscritos] ([EncarregadoId]);
GO

CREATE INDEX [IX_AlunoInscritos_GrauDeParentescoId] ON [AlunoInscritos] ([GrauDeParentescoId]);
GO

CREATE INDEX [IX_AlunoInscritos_NiveisDeEnsinoId] ON [AlunoInscritos] ([NiveisDeEnsinoId]);
GO

CREATE INDEX [IX_AlunoMatriculados_AlunoInscritoId] ON [AlunoMatriculados] ([AlunoInscritoId]);
GO

CREATE INDEX [IX_AlunoMatriculados_ClasseId] ON [AlunoMatriculados] ([ClasseId]);
GO

CREATE INDEX [IX_AlunoMatriculados_CursoId] ON [AlunoMatriculados] ([CursoId]);
GO

CREATE INDEX [IX_AlunoMatriculados_EncarregadoId] ON [AlunoMatriculados] ([EncarregadoId]);
GO

CREATE INDEX [IX_AlunoMatriculados_GrauDeParentescoId] ON [AlunoMatriculados] ([GrauDeParentescoId]);
GO

CREATE INDEX [IX_AlunoMatriculados_NiveisDeEnsinoId] ON [AlunoMatriculados] ([NiveisDeEnsinoId]);
GO

CREATE INDEX [IX_AlunoMatriculados_TurmaId] ON [AlunoMatriculados] ([TurmaId]);
GO

CREATE INDEX [IX_Avaliacaos_AlunoMatriculadoId] ON [Avaliacaos] ([AlunoMatriculadoId]);
GO

CREATE INDEX [IX_Avaliacaos_ClasseId] ON [Avaliacaos] ([ClasseId]);
GO

CREATE INDEX [IX_Avaliacaos_DisciplinaId] ON [Avaliacaos] ([DisciplinaId]);
GO

CREATE INDEX [IX_Avaliacaos_ProfessorId] ON [Avaliacaos] ([ProfessorId]);
GO

CREATE INDEX [IX_Avaliacaos_TipoAvaliacaoId] ON [Avaliacaos] ([TipoAvaliacaoId]);
GO

CREATE INDEX [IX_Avaliacaos_TrimestreId] ON [Avaliacaos] ([TrimestreId]);
GO

CREATE INDEX [IX_Avaliacaos_TurmaId] ON [Avaliacaos] ([TurmaId]);
GO

CREATE INDEX [IX_Classes_CursoId] ON [Classes] ([CursoId]);
GO

CREATE INDEX [IX_Classes_NiveisDeEnsinoId] ON [Classes] ([NiveisDeEnsinoId]);
GO

CREATE INDEX [IX_Multas_AlunoMatriculadoId] ON [Multas] ([AlunoMatriculadoId]);
GO

CREATE INDEX [IX_Multas_ClasseId] ON [Multas] ([ClasseId]);
GO

CREATE INDEX [IX_Multas_MesId] ON [Multas] ([MesId]);
GO

CREATE INDEX [IX_Multas_TurmaId] ON [Multas] ([TurmaId]);
GO

CREATE INDEX [IX_PagamentoMultaItems_FuncionarioCaixaId] ON [PagamentoMultaItems] ([FuncionarioCaixaId]);
GO

CREATE INDEX [IX_PagamentoMultaItems_MultaId] ON [PagamentoMultaItems] ([MultaId]);
GO

CREATE INDEX [IX_PagamentoMultaItems_PagamentoMultaId] ON [PagamentoMultaItems] ([PagamentoMultaId]);
GO

CREATE INDEX [IX_PagamentoMultas_AlunoMatriculadoId] ON [PagamentoMultas] ([AlunoMatriculadoId]);
GO

CREATE INDEX [IX_PagamentoPropinaItems_FuncionarioCaixaId] ON [PagamentoPropinaItems] ([FuncionarioCaixaId]);
GO

CREATE INDEX [IX_PagamentoPropinaItems_PagamentoPropinaId] ON [PagamentoPropinaItems] ([PagamentoPropinaId]);
GO

CREATE INDEX [IX_PagamentoPropinaItems_PropinaId] ON [PagamentoPropinaItems] ([PropinaId]);
GO

CREATE INDEX [IX_PagamentoPropinas_AlunoMatriculadoId] ON [PagamentoPropinas] ([AlunoMatriculadoId]);
GO

CREATE INDEX [IX_PagamentoPropinas_MesId] ON [PagamentoPropinas] ([MesId]);
GO

CREATE INDEX [IX_Professores_DisciplinaId] ON [Professores] ([DisciplinaId]);
GO

CREATE INDEX [IX_Propinas_AlunoMatriculadoId] ON [Propinas] ([AlunoMatriculadoId]);
GO

CREATE INDEX [IX_Propinas_ClasseId] ON [Propinas] ([ClasseId]);
GO

CREATE INDEX [IX_Propinas_MesId] ON [Propinas] ([MesId]);
GO

CREATE INDEX [IX_Propinas_TurmaId] ON [Propinas] ([TurmaId]);
GO

CREATE INDEX [IX_Turmas_ClasseId] ON [Turmas] ([ClasseId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240211202827_Initial', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cursos]') AND [c].[name] = N'Nome');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Cursos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Cursos] ALTER COLUMN [Nome] varchar(32) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240212123750_TbCurso', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Disciplinas]') AND [c].[name] = N'NomeDisciplina');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Disciplinas] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Disciplinas] ALTER COLUMN [NomeDisciplina] varchar(45) NOT NULL;
GO

ALTER TABLE [Disciplinas] ADD [NiveisDeEnsinoId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

CREATE INDEX [IX_Disciplinas_NiveisDeEnsinoId] ON [Disciplinas] ([NiveisDeEnsinoId]);
GO

ALTER TABLE [Disciplinas] ADD CONSTRAINT [FK_Disciplinas_NiveisDeEnsinos_NiveisDeEnsinoId] FOREIGN KEY ([NiveisDeEnsinoId]) REFERENCES [NiveisDeEnsinos] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240212150610_TbDisciplinaNivelDeEnsino', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Professores] DROP CONSTRAINT [FK_Professores_Disciplinas_DisciplinaId];
GO

DROP INDEX [IX_Professores_DisciplinaId] ON [Professores];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Professores]') AND [c].[name] = N'DisciplinaId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Professores] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Professores] DROP COLUMN [DisciplinaId];
GO

CREATE TABLE [ProfessorDisciplinaClasses] (
    [Id] uniqueidentifier NOT NULL,
    [ProfessorId] uniqueidentifier NOT NULL,
    [DisciplinaId] uniqueidentifier NOT NULL,
    [ClasseId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ProfessorDisciplinaClasses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProfessorDisciplinaClasses_Classes_ClasseId] FOREIGN KEY ([ClasseId]) REFERENCES [Classes] ([Id]),
    CONSTRAINT [FK_ProfessorDisciplinaClasses_Disciplinas_DisciplinaId] FOREIGN KEY ([DisciplinaId]) REFERENCES [Disciplinas] ([Id]),
    CONSTRAINT [FK_ProfessorDisciplinaClasses_Professores_ProfessorId] FOREIGN KEY ([ProfessorId]) REFERENCES [Professores] ([Id])
);
GO

CREATE INDEX [IX_ProfessorDisciplinaClasses_ClasseId] ON [ProfessorDisciplinaClasses] ([ClasseId]);
GO

CREATE INDEX [IX_ProfessorDisciplinaClasses_DisciplinaId] ON [ProfessorDisciplinaClasses] ([DisciplinaId]);
GO

CREATE INDEX [IX_ProfessorDisciplinaClasses_ProfessorId] ON [ProfessorDisciplinaClasses] ([ProfessorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240216233537_Initial_2', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ProfessorDisciplinaClasses] ADD [NomeClasse] varchar(10) NOT NULL DEFAULT '';
GO

ALTER TABLE [ProfessorDisciplinaClasses] ADD [NomeDisciplina] varchar(45) NOT NULL DEFAULT '';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240217192250_TbProfessorDisciplinaClasse', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ProfessorDisciplinaClasses] ADD [AnoLetivo] varchar(9) NOT NULL DEFAULT '';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240217195033_TbProfessorDisciplinaClasse_1', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AlunoMatriculados] ADD [FuncionarioCaixaId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [AlunoInscritos] ADD [FuncionarioCaixaId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

CREATE INDEX [IX_AlunoMatriculados_FuncionarioCaixaId] ON [AlunoMatriculados] ([FuncionarioCaixaId]);
GO

CREATE INDEX [IX_AlunoInscritos_FuncionarioCaixaId] ON [AlunoInscritos] ([FuncionarioCaixaId]);
GO

ALTER TABLE [AlunoInscritos] ADD CONSTRAINT [FK_AlunoInscritos_FuncionarioCaixas_FuncionarioCaixaId] FOREIGN KEY ([FuncionarioCaixaId]) REFERENCES [FuncionarioCaixas] ([Id]);
GO

ALTER TABLE [AlunoMatriculados] ADD CONSTRAINT [FK_AlunoMatriculados_FuncionarioCaixas_FuncionarioCaixaId] FOREIGN KEY ([FuncionarioCaixaId]) REFERENCES [FuncionarioCaixas] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240221153351_FuncionarioCaixa', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Professores]') AND [c].[name] = N'Telefone');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Professores] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Professores] ALTER COLUMN [Telefone] varchar(16) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240221191031_TBProfessor', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Turmas] ADD [AreaDeConhecimentoId] uniqueidentifier NOT NULL DEFAULT '7be4408e-2027-4c9b-91bc-1cb08aa34bca';
GO

CREATE INDEX [IX_Turmas_AreaDeConhecimentoId] ON [Turmas] ([AreaDeConhecimentoId]);
GO

ALTER TABLE [Turmas] ADD CONSTRAINT [FK_Turmas_AreaDeConhecimentos_AreaDeConhecimentoId] FOREIGN KEY ([AreaDeConhecimentoId]) REFERENCES [AreaDeConhecimentos] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240316192320_TbAreaConhecimento', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PagamentoPropinas] DROP CONSTRAINT [FK_PagamentoPropinas_Meses_MesId];
GO

DROP TABLE [PagamentoPropinaItems];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Propinas]') AND [c].[name] = N'Estado');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Propinas] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Propinas] DROP COLUMN [Estado];
GO

EXEC sp_rename N'[PagamentoPropinas].[MesId]', N'PropinaId', N'COLUMN';
GO

EXEC sp_rename N'[PagamentoPropinas].[IX_PagamentoPropinas_MesId]', N'IX_PagamentoPropinas_PropinaId', N'INDEX';
GO

ALTER TABLE [Propinas] ADD [Situacao] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [PagamentoPropinas] ADD [Descricao] varchar(29) NOT NULL DEFAULT '';
GO

ALTER TABLE [PagamentoPropinas] ADD [FuncionarioCaixaId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [PagamentoPropinas] ADD [NumeroDeMeses] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [PagamentoPropinas] ADD [PrecoPropina] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [AlunoInscritos] ADD [AnoLetivo] varchar(9) NOT NULL DEFAULT '';
GO

CREATE INDEX [IX_PagamentoPropinas_FuncionarioCaixaId] ON [PagamentoPropinas] ([FuncionarioCaixaId]);
GO

ALTER TABLE [PagamentoPropinas] ADD CONSTRAINT [FK_PagamentoPropinas_FuncionarioCaixas_FuncionarioCaixaId] FOREIGN KEY ([FuncionarioCaixaId]) REFERENCES [FuncionarioCaixas] ([Id]);
GO

ALTER TABLE [PagamentoPropinas] ADD CONSTRAINT [FK_PagamentoPropinas_Propinas_PropinaId] FOREIGN KEY ([PropinaId]) REFERENCES [Propinas] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240324213959_Initial_3', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PagamentoPropinas] DROP CONSTRAINT [FK_PagamentoPropinas_Propinas_PropinaId];
GO

DROP INDEX [IX_PagamentoPropinas_PropinaId] ON [PagamentoPropinas];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PagamentoPropinas]') AND [c].[name] = N'PropinaId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PagamentoPropinas] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [PagamentoPropinas] DROP COLUMN [PropinaId];
GO

ALTER TABLE [Propinas] ADD [PagamentoPropinaId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

CREATE INDEX [IX_Propinas_PagamentoPropinaId] ON [Propinas] ([PagamentoPropinaId]);
GO

ALTER TABLE [Propinas] ADD CONSTRAINT [FK_Propinas_PagamentoPropinas_PagamentoPropinaId] FOREIGN KEY ([PagamentoPropinaId]) REFERENCES [PagamentoPropinas] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240328015759_Initial_4', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240328020318_Initial_4_a', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PagamentoPropinas]') AND [c].[name] = N'Descricao');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PagamentoPropinas] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [PagamentoPropinas] ALTER COLUMN [Descricao] varchar(40) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240328165311_Initial_5', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PagamentoPropinas]') AND [c].[name] = N'Codigo');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PagamentoPropinas] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [PagamentoPropinas] ADD DEFAULT (NEXT VALUE FOR MinhaSequencia) FOR [Codigo];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240707114210_Initial_6', N'7.0.16');
GO

COMMIT;
GO

