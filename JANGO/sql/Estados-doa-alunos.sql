SELECT [Codigo]
      ,[Nome]
      ,[NumDocumento]
      ,[ValorDaInscricao],estado
  FROM [JANGOBD_Nova].[dbo].[AlunoInscritos]
  --where estado=0 and ValorDaInscricao=0 -- N�o devem estar inscritos porque ainda n�o pagaram o valor da inscri��o
  --where estado=1 and ValorDaInscricao=0  -- J� est�o inscritos mas ainda n�o pagaram o valor da inscri��o
  --where estado=0 and ValorDaInscricao>0  -- J� pagaram o valor da inscri��o mas est�o com o estado pendente 
