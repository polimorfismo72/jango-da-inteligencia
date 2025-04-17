/*
ANICP- Alunos Não Inscritos Com Pagamentos
ANISP- Alunos Não Inscritos Sem Pagamentos
AISP- Alunos Inscritos Sem Pagamentos
AICP- Alunos Inscritos Com Pagamentos
*/
SELECT
 COUNT(Id) AS 'Todos'
--,'AISP Enisno Primario'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='7320579E-2461-4E10-A0EE-BF402DB1E64E')
,(SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='A18C2496-279D-4107-B673-7F5DB870A855') AS 'AISP Inicição'
,'AICP Inicição'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='A18C2496-279D-4107-B673-7F5DB870A855')
,'ANISP Inicição'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='A18C2496-279D-4107-B673-7F5DB870A855')
,'ANICP Inicição'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='A18C2496-279D-4107-B673-7F5DB870A855')

,(SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='7320579E-2461-4E10-A0EE-BF402DB1E64E') AS 'AISP Enisno Primario'
,'AICP Enisno Primario'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='7320579E-2461-4E10-A0EE-BF402DB1E64E')
,'ANISP Enisno Primario'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='7320579E-2461-4E10-A0EE-BF402DB1E64E')
,'ANICP Enisno Primario'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='7320579E-2461-4E10-A0EE-BF402DB1E64E')

,(SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F') AS 'AISP Etapa I'
,'AICP Etapa I'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F')
,'ANISP Etapa I'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F')
,'ANICP Etapa I'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F')
    ,(SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='BD6C00A0-533D-4636-8A57-046FBBDB365B') AS 'AISP Etapa II'
,'AICP Etapa II'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='BD6C00A0-533D-4636-8A57-046FBBDB365B')
,'ANISP Etapa II'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='BD6C00A0-533D-4636-8A57-046FBBDB365B')
,'ANICP Etapa II'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='BD6C00A0-533D-4636-8A57-046FBBDB365B')

,(SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='293567AC-4832-4A6B-BEB5-EE2B147FBAFA') AS 'AISP Etapa III'
,'AICP Etapa III'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='293567AC-4832-4A6B-BEB5-EE2B147FBAFA')
,'ANISP Etapa III'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='293567AC-4832-4A6B-BEB5-EE2B147FBAFA')
,'ANICP Etapa III'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='293567AC-4832-4A6B-BEB5-EE2B147FBAFA')
 
 ,(SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='3BD50DA4-7435-4034-A5C3-2D0E8DD897C7') AS 'AISP I Ciclo'
,'AICP I Ciclo'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='3BD50DA4-7435-4034-A5C3-2D0E8DD897C7')
,'ANISP I Ciclo'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='3BD50DA4-7435-4034-A5C3-2D0E8DD897C7')
,'ANICP I Ciclo'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='3BD50DA4-7435-4034-A5C3-2D0E8DD897C7')
  
   ,(SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='B3FB841A-06FC-4EB1-948A-BB1A6FA03FB7') AS 'AISP II Ciclo'
,'AICP II Ciclo'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 1 AND NiveisDeEnsinoId='B3FB841A-06FC-4EB1-948A-BB1A6FA03FB7')
,'ANISP II Ciclo'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao = 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='B3FB841A-06FC-4EB1-948A-BB1A6FA03FB7')
,'ANICP II Ciclo'= (SELECT COUNT(Id)  FROM [JANGOBD].[dbo].[AlunoInscritos] where ValorDaInscricao > 0 AND AlunoInterno = 0 AND NiveisDeEnsinoId='B3FB841A-06FC-4EB1-948A-BB1A6FA03FB7')
  FROM [JANGOBD].[dbo].[AlunoInscritos]
 
