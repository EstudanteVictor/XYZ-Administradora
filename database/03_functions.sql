/*
    03_functions.sql
    XYZ Administradora de Cartões de Crédito
    fn_CategoriaTransacao: scalar function de categorização por faixa de valor.
    fn_TransacoesCategorizadas: table-valued function (inline) que reaproveita
    a scalar function para categorizar todas as transações de um período.
*/

USE XYZCartoesDB;
GO

/*
    fn_CategoriaTransacao
    Faixas (sem sobreposição/lacuna nos limites 500, 1000 e 2000):
        > 2000            -> Premium
        1000 <= v <= 2000  -> Alta
        500  <= v < 1000   -> Média
        < 500             -> Baixa
*/
CREATE OR ALTER FUNCTION dbo.fn_CategoriaTransacao (@Valor DECIMAL(18,2))
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @Categoria VARCHAR(10);

    IF @Valor IS NULL
        SET @Categoria = NULL;
    ELSE IF @Valor > 2000
        SET @Categoria = 'Premium';
    ELSE IF @Valor >= 1000
        SET @Categoria = 'Alta';
    ELSE IF @Valor >= 500
        SET @Categoria = 'Média';
    ELSE
        SET @Categoria = 'Baixa';

    RETURN @Categoria;
END
GO

/*
    fn_TransacoesCategorizadas
    TVF inline (não multi-statement): o corpo é uma única instrução SELECT,
    o que permite ao otimizador "enxergar através" da função e usar os
    índices de dbo.Transacoes normalmente, em vez de materializar um
    resultado intermediário como uma multi-statement TVF faria.
*/
CREATE OR ALTER FUNCTION dbo.fn_TransacoesCategorizadas
(
    @Data_Inicial DATETIME,
    @Data_Final   DATETIME
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        t.Id_Transacao,
        t.Numero_Cartao,
        t.Valor_Transacao,
        t.Data_Transacao,
        t.Descricao,
        t.Status_Transacao,
        dbo.fn_CategoriaTransacao(t.Valor_Transacao) AS Categoria
    FROM dbo.Transacoes t
    WHERE t.Data_Transacao >= @Data_Inicial
      AND t.Data_Transacao <= @Data_Final
);
GO
