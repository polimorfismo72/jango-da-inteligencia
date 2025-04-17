SELECT DISTINCT Pdc.ProfessorId 
,Pdc.NomeClasse AS NomeClasse ,P.Nome AS Nome,
COUNT(Pdc.ProfessorId) AS DisciplinasAssociadas
  FROM [JANGOBD_Nova].[dbo].[ProfessorDisciplinaClasses]
  AS Pdc
  INNER JOIN Professores AS P ON Pdc.ProfessorId = P.Id
  group by ProfessorId,NomeClasse,Nome
 

