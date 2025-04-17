--Nome, classe,turma,divida de propinas(mês, valor do mês ),divida de Multa(mês, valor do mês )  

  SELECT
  --P.Id,
	  A.Nome
      ,C.Nome As Classe,T.NomeTurma  As Turma
	  --,M.NomeMes
      ,P.DescricaoPropina,P.PrecoPropina
      ,P.Situacao
	  --,P.PagamentoPropinaId
  FROM  Propinas AS P
  INNER JOIN Classes AS C ON P.ClasseId = C.Id
  INNER JOIN Turmas AS T ON P.TurmaId = T.Id
  INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
  INNER JOIN Meses AS M ON P.MesId = M.Id
  --where Situacao = 3 AND T.NomeTurma ='HM'
    --where Nome like'Maria O%'
  where 
  --Situacao = 1 AND
  C.Nome='3ª' order by Nome
  Go
  SELECT A.Nome As Nome,T.NomeTurma  As Turma
  ,C.Nome As Classe,COUNT(P.ClasseId) AS 'Meses'
  ,((COUNT(P.ClasseId))*P.PrecoPropina) As PropinasEmAtraso
  FROM  Propinas AS P
  INNER JOIN Classes AS C ON P.ClasseId = C.Id
  INNER JOIN Turmas AS T ON P.TurmaId = T.Id
  INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
  INNER JOIN Meses AS M ON P.MesId = M.Id
  WHERE Situacao = 1 AND T.NomeTurma ='HM'
  GROUP BY P.ClasseId,A.Nome,T.NomeTurma,C.Nome,P.PrecoPropina
  
