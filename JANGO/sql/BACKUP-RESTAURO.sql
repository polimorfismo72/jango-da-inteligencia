

-- Script para criar ou restaurar um banco de Dados
-- Exemplo completo com criação do banco de dados

--Cria um banco de Dados simples
create database MeuBancoDeDadosLegal

--Seleciona o Banco de Dados
use MeuBancoDeDadosLegal


--Cria uma tabela de Clientes
create table Clientes(
	Id int identity not null primary key,
	Nome Varchar(50) not null default(''),
	Endereco Varchar(150) not null default(''),
	Numero int
)

--Insere dados na tabela de clientes
insert into Clientes (Nome, Endereco,Numero) values ('Pedro Juan', 'Rua da Esmeraldas',10)
insert into Clientes (Nome, Endereco,Numero) values ('Marcio Takara', 'Av das Orquideas',20)
insert into Clientes (Nome, Endereco,Numero) values ('Carolina Lopes', 'Estrada Velha do Paiol S/N',null)
insert into Clientes (Nome, Endereco,Numero) values ('Livia Machado', 'Alameda dos Canarinhos',1500)

--Seleciona o banco Master
use master

--Realiza o banckup do Banco de dados com o compressão 
backup database MeuBancoDeDadosLegal to disk = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\MeuBackupSeguro.bak'
With Compression
 
--Exclui o banco de dados 
drop database MeuBancoDeDadosLegal

--Restaura o banco de dados
restore database MeuBancoDeDadosLegal from disk= 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\MeuBackupSeguro.bkk'