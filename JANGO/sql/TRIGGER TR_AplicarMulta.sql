  USE TESTE
GO

IF EXISTS(SELECT 1 FROM sys.triggers WHERE NAME = 'TR_AplicarMulta' AND TYPE = 'TR')
	 BEGIN
	   DROP TRIGGER TR_AplicarMulta
	 END
GO

CREATE TRIGGER TR_AplicarMulta
   ON  dbo.TESTE
	FOR INSERT
AS 
BEGIN
 EXEC dbo.AplicarMulta
END
GO


