SELECT TOP (1000) 
       A.Nome
	   ,A.NumDocumento
	  ,N.NomeNiveisDeEnsino AS 'Niveis De Ensino'
	   ,C.Nome AS Classe
	  --,Ar.Nome AS 'Area De Conhecimento'
      ,A.ValorDaInscricao AS 'Pagamento ao Inscriver'
	  --,A.NiveisDeEnsinoId
      --,A.ClasseId
      
  FROM [JANGOBD].[dbo].[AlunoInscritos] AS A
    INNER JOIN Classes AS C  ON A.ClasseId = C.Id
    INNER JOIN AreaDeConhecimentos AS Ar  ON A.AreaDeConhecimentoId = Ar.Id
    INNER JOIN NiveisDeEnsinos AS N  ON A.NiveisDeEnsinoId = N.Id
  WHERE  A.ClasseId  = C.Id
   --AND A.ClasseId ='DA26C014-CAA4-4957-93CF-1904196B555B'
  AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'
  AND A.ValorDaInscricao > 0
