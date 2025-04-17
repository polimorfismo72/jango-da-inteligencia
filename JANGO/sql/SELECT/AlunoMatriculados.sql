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
  --FROM [JANGOBD].[dbo].[AlunoMatriculados] AS A
  FROM [TESTE_JANGOBD_Restore].[dbo].[AlunoMatriculados] AS A
  INNER JOIN Classes AS C ON A.ClasseId = C.Id
  INNER JOIN  NiveisDeEnsinos AS N ON A.NiveisDeEnsinoId = N.Id
  INNER JOIN  Cursos AS Cu ON A.CursoId = Cu.Id
  WHERE  A.ClasseId = C.Id  AND 
  A.NiveisDeEnsinoId =  N.Id  AND A.CursoId = Cu.Id  AND
  --A.ClasseId = '74B08060-6FD9-47BD-4843-08DCA977AE93'-- PRODUÇÃO
  A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'-- PRODUÇÃO
  --A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'-- PRODUÇÃO
  --A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'-- PRODUÇÃO
  --A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'-- PRODUÇÃO
  --A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'-- PRODUÇÃO
  --A.NiveisDeEnsinoId = 'B3FB841A-06FC-4EB1-948A-BB1A6FA03FB7'-- PRODUÇÃO

  -- WHERE A.NumDocumento ='000056718LA9025'