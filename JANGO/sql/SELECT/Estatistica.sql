SELECT 
      SUM(TotalPago) AS TotalPago,
      Count(TotalPago) AS 'Total de Alunos'
 
  FROM [JANGOBD].[dbo].[PagamentoPropinas] AS P
  INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
  where P.Ativo= 1 AND A.AnoLetivo='2024-2025' AND P.PagamentoMaticula=1
  ---------------------------------------------------------------------
  SELECT  
 SUM(PrecoPropina) AS PrecoPropina
 ,Count(PrecoPropina) AS 'Total de Alunos'
  FROM [JANGOBD].[dbo].[Propinas]
    where [Situacao] = 4 AND AnoLetivo='2024-2025'
-----------------------------------------------------------------------
SELECT TOP (1000) [Id]
      ,[CodigoAluno],[NumDocumento],[Nome]
     ,Estado  = (CASE WHEN [Estado] = 1
	                THEN 'Matriculado'
					ELSE 'Pendente' END) 
  FROM [JANGOBD].[dbo].[AlunoMatriculados]
---------------------------------------------------------------
SELECT top(1) 
     AnoLetivo
  FROM [JANGOBD].[dbo].[AlunoMatriculados]


