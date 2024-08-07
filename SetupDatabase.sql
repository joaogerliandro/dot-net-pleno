USE [master];
GO

CREATE DATABASE project_database;
GO

USE [project_database];
GO

-- USER CREATION BEGIN --
CREATE LOGIN [user] WITH PASSWORD = 'user';

CREATE USER [user] FOR LOGIN [user];

GRANT SELECT ON SCHEMA::dbo TO [user];
GRANT INSERT ON SCHEMA::dbo TO [user];
GRANT UPDATE ON SCHEMA::dbo TO [user];
GRANT DELETE ON SCHEMA::dbo TO [user];
GRANT EXECUTE ON SCHEMA::dbo TO [user];

-- USER CREATION END

-- TABLES CREATION BEGIN --
CREATE TABLE TB_TIPO_PESSOA(
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	TIPO TINYINT NOT NULL
);
GO

CREATE TABLE TB_PESSOA(
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	ID_TIPO_PESSOA BIGINT NOT NULL,
	NOME VARCHAR(255) NOT NULL,
	DOCUMENTO VARCHAR(14) UNIQUE NOT NULL,
	CONSTRAINT FK_TIPO_PESSOA FOREIGN KEY (ID_TIPO_PESSOA)
        REFERENCES TB_TIPO_PESSOA(ID)
);
GO

CREATE TABLE TB_ENDERECO(
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	CEP VARCHAR(8) NOT NULL,
	LOGRADOURO VARCHAR(255) NOT NULL,
	NUMERO VARCHAR(7),
	BAIRRO VARCHAR(30) NOT NULL,
	CIDADE VARCHAR(30) NOT NULL,
	UF VARCHAR(2) NOT NULL
);
GO

CREATE TABLE TB_PESSOA_ENDERECO(
    ID_PESSOA BIGINT NOT NULL,
    ID_ENDERECO BIGINT NOT NULL,
    CONSTRAINT PK_TB_PESSOA_ENDERECO PRIMARY KEY (ID_PESSOA, ID_ENDERECO),
    CONSTRAINT FK_TB_PESSOA_ENDERECO_PESSOA FOREIGN KEY (ID_PESSOA) REFERENCES TB_PESSOA(ID),
    CONSTRAINT FK_TB_PESSOA_ENDERECO_ENDERECO FOREIGN KEY (ID_ENDERECO) REFERENCES TB_ENDERECO(ID)
);
GO

CREATE TABLE TB_PESSOA_LISTA (
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
    ID_PESSOA BIGINT NOT NULL,
    LISTA VARCHAR(255) NOT NULL,
    CONSTRAINT FK_TB_PESSOA_LISTA_PESSOA FOREIGN KEY (ID_PESSOA) REFERENCES TB_PESSOA(ID)
);
GO
-- TABLES CREATION END --