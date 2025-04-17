
-- Defina o nome do banco de dados que você deseja fazer backup  
    DECLARE @DatabaseName NVARCHAR(128) = 'JANGOBD'  
  
    -- Crie uma string com a data e hora atual para gerar um nome de arquivo exclusivo  
    DECLARE @Timestamp NVARCHAR(20)  
    SET @Timestamp = REPLACE(CONVERT(NVARCHAR, GETDATE(), 120), ':', '')  
    -- Defina o caminho para o arquivo de backup com base na data e hora  
    DECLARE @BackupPath NVARCHAR(260) = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\' + @DatabaseName + '_Backup_' + @Timestamp + '.bkk'  
  
    -- Crie o comando de backup  
    DECLARE @ComandoBackUp NVARCHAR(4000)  
    SET @ComandoBackUp =   
        'BACKUP DATABASE ' + @DatabaseName +   
        ' TO DISK = ''' + @BackupPath + ''' WITH FORMAT, INIT, NAME = N'''
       + @DatabaseName + '_Backup'', SKIP, NOREWIND, NOUNLOAD, STATS = 10' 

EXEC(@ComandoBackUp)

--------------------------------------------
-- copie a partir daqui 

-- Defina o nome do banco de dados que você deseja fazer backup  
    DECLARE @DatabaseName NVARCHAR(128) = 'AdventureWorks2019'  
  
    -- Crie uma string com a data e hora atual para gerar um nome de arquivo exclusivo  
    DECLARE @Timestamp NVARCHAR(20)  
    SET @Timestamp = REPLACE(CONVERT(NVARCHAR, GETDATE(), 120), ':', '')  
  
    -- Defina o caminho para o arquivo de backup com base na data e hora  
    DECLARE @BackupPath NVARCHAR(260) = 'C:\Backup\' + @DatabaseName + '_Backup_' + @Timestamp + '.bak'  
  
    -- Crie o comando de backup  
    DECLARE @BackupCommand NVARCHAR(4000)  
    SET @BackupCommand =   
        'BACKUP DATABASE ' + @DatabaseName +   
        ' TO DISK = ''' + @BackupPath + ''' WITH FORMAT, INIT, NAME = N''' + @DatabaseName + '_Backup'', SKIP, NOREWIND, NOUNLOAD, STATS = 10'  

-- final 


-- cole esta parte em outra sessão 
--criação da procedure

CREATE PROCEDURE dbo.BackupDatabase_AdventureWorks2019  
AS  
BEGIN  
    -- Defina o nome do banco de dados que você deseja fazer backup  
    DECLARE @DatabaseName NVARCHAR(128) = 'AdventureWorks2019'  
  
    -- Crie uma string com a data e hora atual para gerar um nome de arquivo exclusivo  
    DECLARE @Timestamp NVARCHAR(20)  
    SET @Timestamp = REPLACE(CONVERT(NVARCHAR, GETDATE(), 120), ':', '')  
  
    -- Defina o caminho para o arquivo de backup com base na data e hora  
    DECLARE @BackupPath NVARCHAR(260) = 'C:\Backup\' + @DatabaseName + '_Backup_' + @Timestamp + '.bak'  
  
    -- Crie o comando de backup  
    DECLARE @BackupCommand NVARCHAR(4000)  
    SET @BackupCommand =   
        'BACKUP DATABASE ' + @DatabaseName +   
        ' TO DISK = ''' + @BackupPath + ''' WITH FORMAT, INIT, NAME = N''' + @DatabaseName + '_Backup'', SKIP, NOREWIND, NOUNLOAD, STATS = 10'  
  
    -- Execute o comando de backup  
    EXEC(@BackupCommand)  
END

