 --Habilitar o Agent XPs 
 1-Executar para Ver se o Agent está habilitado 
 EXEC SP_CONFIGURE 'Agent XPs';
 2-Habilitar
 EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO

3-Executar 
EXEC SP_CONFIGURE 'Agent XPs';

4-Ativar o Agent XPs.
EXEC SP_CONFIGURE 'Agent XPs', 1;
GO
RECONFIGURE;
GO

5-Now we can check the setting again.
EXEC SP_CONFIGURE 'Agent XPs';