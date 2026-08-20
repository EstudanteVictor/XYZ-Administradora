/*
    01_tabela_transacoes.sql
    XYZ Administradora de Cartões de Crédito
    Cria o banco de dados (se necessário) e a tabela principal Transacoes,
    com as constraints de domínio e os índices que sustentam os filtros
    e a paginação usados pela aplicação.
*/

IF DB_ID(N'XYZCartoesDB') IS NULL
BEGIN
    CREATE DATABASE XYZCartoesDB;
END
GO

USE XYZCartoesDB;
GO

IF OBJECT_ID(N'dbo.Transacoes', N'U') IS NOT NULL
    DROP TABLE dbo.Transacoes;
GO

CREATE TABLE dbo.Transacoes
(
    Id_Transacao        INT             IDENTITY(1,1)   NOT NULL,
    Numero_Cartao       CHAR(16)                        NOT NULL,
    Valor_Transacao     DECIMAL(18,2)                   NOT NULL,
    Data_Transacao      DATETIME2(0)                    NOT NULL CONSTRAINT DF_Transacoes_Data_Transacao DEFAULT (SYSDATETIME()),
    Descricao           NVARCHAR(255)                   NULL,
    Status_Transacao    VARCHAR(10)                     NOT NULL CONSTRAINT DF_Transacoes_Status_Transacao DEFAULT ('Pendente'),

    CONSTRAINT PK_Transacoes PRIMARY KEY CLUSTERED (Id_Transacao),

    CONSTRAINT CK_Transacoes_Numero_Cartao
        CHECK (LEN(Numero_Cartao) = 16 AND Numero_Cartao NOT LIKE '%[^0-9]%'),

    CONSTRAINT CK_Transacoes_Valor_Transacao
        CHECK (Valor_Transacao > 0),

    CONSTRAINT CK_Transacoes_Status_Transacao
        CHECK (Status_Transacao IN ('Aprovada', 'Pendente', 'Cancelada'))
);
GO

-- Suporta filtro/agrupamento por cartão (ex.: sp_TotalTransacoesPorPeriodo)
CREATE NONCLUSTERED INDEX IX_Transacoes_NumeroCartao
    ON dbo.Transacoes (Numero_Cartao);
GO

-- Suporta a consulta paginada: a grid ordena por Data_Transacao DESC e filtra
-- por Status_Transacao/Numero_Cartao; INCLUDE evita lookups extras na tabela
-- para os campos exibidos na grid.
CREATE NONCLUSTERED INDEX IX_Transacoes_Consulta
    ON dbo.Transacoes (Data_Transacao DESC, Status_Transacao, Numero_Cartao)
    INCLUDE (Valor_Transacao, Descricao);
GO
