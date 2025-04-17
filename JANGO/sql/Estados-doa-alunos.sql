SELECT [Codigo]
      ,[Nome]
      ,[NumDocumento]
      ,[ValorDaInscricao],estado
  FROM [JANGOBD_Nova].[dbo].[AlunoInscritos]
  --where estado=0 and ValorDaInscricao=0 -- Não devem estar inscritos porque ainda não pagaram o valor da inscrição
  --where estado=1 and ValorDaInscricao=0  -- Já estão inscritos mas ainda não pagaram o valor da inscrição
  --where estado=0 and ValorDaInscricao>0  -- Já pagaram o valor da inscrição mas estão com o estado pendente 
