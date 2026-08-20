/*
    05_dados_exemplo.sql
    XYZ Administradora de Cartões de Crédito
    Popula dbo.Transacoes com uma massa de dados de teste (~8.000 linhas),
    toda gerada de forma set-based (sem WHILE/cursor), para demonstrar a
    paginação, os filtros e os relatórios com um volume realista de dados.

    As datas são geradas como offset de SYSDATETIME(), então sempre haverá
    transações no "mês anterior completo" (usado pela exportação Excel),
    não importa quando este script seja executado.
*/

USE XYZCartoesDB;
GO

IF EXISTS (SELECT 1 FROM dbo.Transacoes)
BEGIN
    TRUNCATE TABLE dbo.Transacoes;
END
GO

IF OBJECT_ID(N'tempdb..#Cartoes') IS NOT NULL DROP TABLE #Cartoes;
IF OBJECT_ID(N'tempdb..#Descricoes') IS NOT NULL DROP TABLE #Descricoes;
GO

-- Pool de ~200 números de cartão fictícios (16 dígitos, prefixo '4' + sequência
-- zero-padded), reutilizados entre as transações para que os agrupamentos por
-- cartão (SP de totais, view consolidada) façam sentido.
;WITH L0 AS (SELECT 1 AS c UNION ALL SELECT 1),
L1 AS (SELECT 1 AS c FROM L0 A CROSS JOIN L0 B),
L2 AS (SELECT 1 AS c FROM L1 A CROSS JOIN L1 B),
L3 AS (SELECT 1 AS c FROM L2 A CROSS JOIN L2 B),
Numeros AS (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS N FROM L3)
SELECT TOP (200)
       N AS Id,
       CONCAT('4', RIGHT(REPLICATE('0', 15) + CAST(N AS VARCHAR(15)), 15)) AS Numero_Cartao
INTO #Cartoes
FROM Numeros;

-- Pool pequeno de descrições de exemplo
SELECT Id = ROW_NUMBER() OVER (ORDER BY (SELECT NULL)), Texto
INTO #Descricoes
FROM (VALUES
    (N'Compra em loja de departamento'),
    (N'Assinatura de streaming'),
    (N'Pagamento de fatura de energia'),
    (N'Compra em supermercado'),
    (N'Restaurante'),
    (N'Posto de combustível'),
    (N'Farmácia'),
    (N'Compra on-line - eletrônicos'),
    (N'Mensalidade de academia'),
    (N'Passagem aérea'),
    (N'Hospedagem - hotel'),
    (N'Compra em livraria'),
    (N'Serviço de telefonia'),
    (N'Compra em loja de roupas'),
    (N'Assinatura de software')
) AS v(Texto);
GO

DECLARE @TotalCartoes INT = (SELECT COUNT(*) FROM #Cartoes);
DECLARE @TotalDescricoes INT = (SELECT COUNT(*) FROM #Descricoes);

;WITH L0 AS (SELECT 1 AS c UNION ALL SELECT 1),
L1 AS (SELECT 1 AS c FROM L0 A CROSS JOIN L0 B),
L2 AS (SELECT 1 AS c FROM L1 A CROSS JOIN L1 B),
L3 AS (SELECT 1 AS c FROM L2 A CROSS JOIN L2 B),
L4 AS (SELECT 1 AS c FROM L3 A CROSS JOIN L3 B),
Numeros AS (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS N FROM L4),
Sorteio AS (
    SELECT
        N,
        -- índice pseudo-aleatório de cartão/descrição, valor e status por linha
        ABS(CHECKSUM(NEWID())) % @TotalCartoes + 1                 AS CartaoIdx,
        ABS(CHECKSUM(NEWID())) % @TotalDescricoes + 1               AS DescricaoIdx,
        CAST(10 + (ABS(CHECKSUM(NEWID())) % 499000) / 100.0 AS DECIMAL(18,2)) AS Valor,
        -- distribui as datas nos últimos ~18 meses (18 * 30 * 24 * 60 minutos)
        DATEADD(MINUTE, -(ABS(CHECKSUM(NEWID())) % 777600), SYSDATETIME()) AS DataTransacao,
        ABS(CHECKSUM(NEWID())) % 100                                 AS SorteioStatus
    FROM Numeros
)
INSERT INTO dbo.Transacoes (Numero_Cartao, Valor_Transacao, Data_Transacao, Descricao, Status_Transacao)
SELECT TOP (8000)
    c.Numero_Cartao,
    s.Valor,
    s.DataTransacao,
    d.Texto,
    CASE
        WHEN s.SorteioStatus < 70 THEN 'Aprovada'
        WHEN s.SorteioStatus < 90 THEN 'Pendente'
        ELSE 'Cancelada'
    END AS Status_Transacao
FROM Sorteio s
JOIN #Cartoes c ON c.Id = s.CartaoIdx
JOIN #Descricoes d ON d.Id = s.DescricaoIdx;
GO

DROP TABLE IF EXISTS #Cartoes;
DROP TABLE IF EXISTS #Descricoes;
GO

SELECT COUNT(*) AS Total_Transacoes_Geradas FROM dbo.Transacoes;
GO
