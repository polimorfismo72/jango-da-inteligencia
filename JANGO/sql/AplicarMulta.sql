--=========================== Criar Procedimento Aplicar Multa ====================
CREATE PROCEDURE dbo.AplicarMulta
AS
BEGIN
    DECLARE @MesId  UNIQUEIDENTIFIER,
	@AlunoMatriculadoId  UNIQUEIDENTIFIER,
    @ClasseId  UNIQUEIDENTIFIER,
    @TurmaId  UNIQUEIDENTIFIER,
    @DescricaoMulta VARCHAR(29),
	@PrecoPropina DECIMAL(10,2),
    @AnoLetivo VARCHAR(9),
	@Contador INT,
	@MultaAluno INT

	SET @MultaAluno = 1
	SET @Contador = (SELECT COUNT(Id)
	                 FROM Propinas WHERE Situacao = 4 AND MesId = 
					 (SELECT Id FROM Meses WHERE CodMes = DATEPART(MONTH, GETDATE())));

	DECLARE verificaPropinaNaoPago CURSOR FOR 
     SELECT P.MesId,P.AlunoMatriculadoId,P.ClasseId,P.TurmaId,
         P.DescricaoPropina,C.PrecoPropina,P.AnoLetivo
                     FROM Propinas AS P
					 INNER JOIN Classes AS C ON P.ClasseId = C.Id
					 WHERE Situacao = 4 AND MesId = 
					 (SELECT Id FROM Meses WHERE CodMes = DATEPART(MONTH, GETDATE()));
   
   OPEN verificaPropinaNaoPago;
   
  WHILE @MultaAluno <= @Contador 
  BEGIN
  DECLARE @Id uniqueidentifier = NEWID();
   FETCH NEXT FROM  verificaPropinaNaoPago INTO
	   @MesId,@AlunoMatriculadoId,@ClasseId ,@TurmaId,@DescricaoMulta 
	   ,@PrecoPropina, @AnoLetivo;
      

      IF (DATEPART(DAY, GETDATE()) < 11)
      --IF (DATEPART(DAY, GETDATE()) > 10  )
     	 BREAK;
  
     IF (DATEPART(DAY, GETDATE()) > 10 AND DATEPART(DAY, GETDATE()) < 16 ) 
	 SET NOCOUNT ON;
     	INSERT INTO  Multas
           (Id ,MesId,AlunoMatriculadoId ,ClasseId ,TurmaId,DataCadastro ,DescricaoMulta 
		   ,PrecoPropina,Estado ,AnoLetivo)
     VALUES
           (@Id,@MesId,@AlunoMatriculadoId ,@ClasseId ,@TurmaId,GETDATE()  
           ,@DescricaoMulta ,@PrecoPropina,0, @AnoLetivo)
	 
     --Print @Id;
     --Print NEWID() + '|' + @DescricaoMulta + '|' + @AlunoMatriculadoId;
     --Print @AlunoMatriculadoId;
	 SET @MultaAluno=@MultaAluno + 1

  END
  
  	CLOSE verificaPropinaNaoPago;
  	DEALLOCATE verificaPropinaNaoPago;
END
GO
--=========================== Executar o Procedimento ====================
EXEC AplicarMulta
--=========================== Selecionar Aluno Matriculado ====================
SELECT TOP (1000) 
	   A.Nome AS 'Aluno'
      ,A.[CodigoAluno] AS 'Código do Aluno'
      ,A.[NumDocumento] AS 'BI/Cédula'
      ,C.Nome AS Classe
      ,N.NomeNiveisDeEnsino AS 'Nível de Ensino'
      ,Cu.[Nome]  AS Curso
      ,A.[Idade]
      ,A.[Estado]
      ,A.[AnoLetivo]  AS 'Ano Letivo'
  FROM [TESTE].[dbo].[AlunoMatriculados] AS A
  INNER JOIN Classes AS C ON A.ClasseId = C.Id
  INNER JOIN  NiveisDeEnsinos AS N ON A.NiveisDeEnsinoId = N.Id
  INNER JOIN  Cursos AS Cu ON A.CursoId = Cu.Id
  WHERE  A.ClasseId = C.Id  AND 
  A.NiveisDeEnsinoId =  N.Id  AND A.CursoId = Cu.Id  AND
  A.Id = '6E3A26CE-4D04-46AA-F3B2-08DCE3A35835'
  
SELECT TOP (1000) *
  FROM [TESTE].[dbo].[Multas]
--======================== Apagar os dados da tabela Multas ========================

  USE [TESTE]
GO
 SET NOCOUNT ON;
DELETE FROM [dbo].[Multas]
  
GO
