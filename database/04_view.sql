/*
    04_view.sql
    XYZ Administradora de Cartões de Crédito
    vw_ConsolidadoFinanceiro: resumo financeiro mensal por cartão, para
    consultas gerenciais rápidas sem precisar repetir a agregação em cada
    relatório. Detalhe linha a linha já é coberto por fn_TransacoesCategorizadas;
    esta view complementa com a visão agregada por Numero_Cartao + mês.
*/

USE XYZCartoesDB;
GO

CREATE OR ALTER VIEW dbo.vw_ConsolidadoFinanceiro
AS
SELECT
    Numero_Cartao,
    CONVERT(CHAR(7), Data_Transacao, 120)                              AS Ano_Mes,
    COUNT(*)                                                           AS Quantidade_Transacoes,
    SUM(Valor_Transacao)                                               AS Valor_Total,
    CAST(AVG(Valor_Transacao) AS DECIMAL(18,2))                        AS Valor_Medio,
    SUM(CASE WHEN Status_Transacao = 'Aprovada'  THEN 1 ELSE 0 END)    AS Quantidade_Aprovadas,
    SUM(CASE WHEN Status_Transacao = 'Pendente'  THEN 1 ELSE 0 END)    AS Quantidade_Pendentes,
    SUM(CASE WHEN Status_Transacao = 'Cancelada' THEN 1 ELSE 0 END)    AS Quantidade_Canceladas,
    dbo.fn_CategoriaTransacao(SUM(Valor_Transacao))                    AS Categoria_Valor_Total
FROM dbo.Transacoes
GROUP BY Numero_Cartao, CONVERT(CHAR(7), Data_Transacao, 120);
GO
