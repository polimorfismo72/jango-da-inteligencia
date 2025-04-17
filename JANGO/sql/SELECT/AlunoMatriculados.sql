SELECT TOP (1000) 
	   A.Nome AS 'Aluno'
      ,A.[CodigoAluno] AS 'C�digo do Aluno'
      ,A.[NumDocumento] AS 'BI/C�dula'
      ,C.Nome AS Classe
      ,N.NomeNiveisDeEnsino AS 'N�vel de Ensino'
      ,Cu.[Nome]  AS Curso
      ,A.[Idade]
      ,A.[Estado]
      ,A.[AnoLetivo]  AS 'Ano Letivo'
  --FROM [JANGOBD].[dbo].[AlunoMatriculados] AS A
  FROM [TESTE_JANGOBD_Restore].[dbo].[AlunoMatriculados] AS A
  INNER JOIN Classes AS C ON A.ClasseId = C.Id
  INNER JOIN  NiveisDeEnsinos AS N ON A.NiveisDeEnsinoId = N.Id
  INNER JOIN  Cursos AS Cu ON A.CursoId = Cu.Id
  WHERE  A.ClasseId = C.Id  AND 
  A.NiveisDeEnsinoId =  N.Id  AND A.CursoId = Cu.Id  AND
  --A.ClasseId = '74B08060-6FD9-47BD-4843-08DCA977AE93'-- PRODU��O
  A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'-- PRODU��O
  --A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'-- PRODU��O
  --A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'-- PRODU��O
  --A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'-- PRODU��O
  --A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'-- PRODU��O
  --A.NiveisDeEnsinoId = 'B3FB841A-06FC-4EB1-948A-BB1A6FA03FB7'-- PRODU��O

  -- WHERE A.NumDocumento ='000056718LA9025'