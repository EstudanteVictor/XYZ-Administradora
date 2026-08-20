/*
    02_stored_procedures.sql
    XYZ Administradora de Cartões de Crédito
    Stored procedures de CRUD, consulta paginada e totais por período.

    Convenção de erros de negócio sinalizados via THROW com número customizado
    (>= 50000), para que a camada de dados em VB.NET consiga distinguir uma
    falha de regra de negócio (mostrada ao usuário) de uma falha técnica
    (logada em arquivo):
        50001 - Transação não encontrada
        50002 - Transação com status 'Aprovada' não pode ser editada
*/

USE XYZCartoesDB;
GO

CREATE OR ALTER PROCEDURE dbo.sp_InserirTransacao
    @Numero_Cartao      CHAR(16),
    @Valor_Transacao    DECIMAL(18,2),
    @Descricao          NVARCHAR(255) = NULL,
    @Status_Transacao   VARCHAR(10)   = 'Pendente',
    @Id_Transacao       INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Transacoes (Numero_Cartao, Valor_Transacao, Descricao, Status_Transacao)
    VALUES (@Numero_Cartao, @Valor_Transacao, @Descricao, @Status_Transacao);

    SET @Id_Transacao = CAST(SCOPE_IDENTITY() AS INT);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_AtualizarTransacao
    @Id_Transacao       INT,
    @Numero_Cartao      CHAR(16),
    @Valor_Transacao    DECIMAL(18,2),
    @Descricao          NVARCHAR(255) = NULL,
    @Status_Transacao   VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StatusAtual VARCHAR(10);

    SELECT @StatusAtual = Status_Transacao
    FROM dbo.Transacoes
    WHERE Id_Transacao = @Id_Transacao;

    IF @StatusAtual IS NULL
    BEGIN
        THROW 50001, N'Transação não encontrada.', 1;
    END

    IF @StatusAtual = 'Aprovada'
    BEGIN
        THROW 50002, N'Não é possível editar uma transação com status ''Aprovada''.', 1;
    END

    UPDATE dbo.Transacoes
    SET Numero_Cartao    = @Numero_Cartao,
        Valor_Transacao  = @Valor_Transacao,
        Descricao        = @Descricao,
        Status_Transacao = @Status_Transacao
    WHERE Id_Transacao = @Id_Transacao;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_ExcluirTransacao
    @Id_Transacao INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Transacoes
    WHERE Id_Transacao = @Id_Transacao;

    IF @@ROWCOUNT = 0
    BEGIN
        THROW 50001, N'Transação não encontrada.', 1;
    END
END
GO

-- Observação: esta e a sp_ConsultarTransacoesPaginado (abaixo) chamam dbo.fn_CategoriaTransacao,
-- criada em 03_functions.sql. Resolução de nomes adiada permite CREATE aqui antes de a function
-- existir; só é obrigatório rodar 03_functions.sql antes de EXECUTAR estas procedures.
CREATE OR ALTER PROCEDURE dbo.sp_ObterTransacaoPorId
    @Id_Transacao INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id_Transacao,
        Numero_Cartao,
        Valor_Transacao,
        dbo.fn_CategoriaTransacao(Valor_Transacao) AS Categoria,
        Data_Transacao,
        Descricao,
        Status_Transacao
    FROM dbo.Transacoes
    WHERE Id_Transacao = @Id_Transacao;
END
GO

/*
    sp_ConsultarTransacoesPaginado
    Consulta única usada tanto pela tela de listagem (com paginação) quanto
    pela exportação Excel (sem paginação, passando @Numero_Pagina/@Tamanho_Pagina = NULL).
    Todos os filtros são opcionais (NULL = sem filtro nesse campo).

    Duas leituras leves e independentes, ambas cobertas por
    IX_Transacoes_Consulta (Data_Transacao, Status_Transacao, Numero_Cartao):
    uma conta o total de linhas que atendem ao filtro (para a UI montar
    "Página X de Y"), a outra busca apenas a janela de linhas da página atual.
*/
CREATE OR ALTER PROCEDURE dbo.sp_ConsultarTransacoesPaginado
    @Id_Transacao       INT             = NULL,
    @Numero_Cartao      CHAR(16)        = NULL,
    @Data_Inicial       DATETIME2(0)    = NULL,
    @Data_Final         DATETIME2(0)    = NULL,
    @Valor_Minimo       DECIMAL(18,2)   = NULL,
    @Valor_Maximo       DECIMAL(18,2)   = NULL,
    @Status_Transacao   VARCHAR(10)     = NULL,
    @Categoria           VARCHAR(10)    = NULL,
    @Ordenar_Por         VARCHAR(20)    = 'Data_Transacao',
    @Ordenar_Direcao     VARCHAR(4)     = 'DESC',
    @Numero_Pagina       INT            = NULL,
    @Tamanho_Pagina      INT            = NULL,
    @Total_Registros     INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT  = ISNULL((@Numero_Pagina - 1) * @Tamanho_Pagina, 0);
    DECLARE @Limite INT  = ISNULL(@Tamanho_Pagina, 2147483647);

    -- @Categoria é derivada de Valor_Transacao via fn_CategoriaTransacao, então este predicado não é
    -- sargável (não usa índice) — aceitável aqui porque os outros filtros (data/status/cartão) já
    -- reduzem o conjunto via IX_Transacoes_Consulta antes deste filtro residual ser avaliado, e manter
    -- a categorização centralizada numa única function evita duplicar as faixas de valor em dois lugares.
    SELECT @Total_Registros = COUNT(*)
    FROM dbo.Transacoes
    WHERE (@Id_Transacao IS NULL OR Id_Transacao = @Id_Transacao)
      AND (@Numero_Cartao IS NULL OR Numero_Cartao = @Numero_Cartao)
      AND (@Data_Inicial IS NULL OR Data_Transacao >= @Data_Inicial)
      AND (@Data_Final IS NULL OR Data_Transacao <= @Data_Final)
      AND (@Valor_Minimo IS NULL OR Valor_Transacao >= @Valor_Minimo)
      AND (@Valor_Maximo IS NULL OR Valor_Transacao <= @Valor_Maximo)
      AND (@Status_Transacao IS NULL OR Status_Transacao = @Status_Transacao)
      AND (@Categoria IS NULL OR dbo.fn_CategoriaTransacao(Valor_Transacao) = @Categoria);

    SELECT
        Id_Transacao,
        Numero_Cartao,
        Valor_Transacao,
        dbo.fn_CategoriaTransacao(Valor_Transacao) AS Categoria,
        Data_Transacao,
        Descricao,
        Status_Transacao
    FROM dbo.Transacoes
    WHERE (@Id_Transacao IS NULL OR Id_Transacao = @Id_Transacao)
      AND (@Numero_Cartao IS NULL OR Numero_Cartao = @Numero_Cartao)
      AND (@Data_Inicial IS NULL OR Data_Transacao >= @Data_Inicial)
      AND (@Data_Final IS NULL OR Data_Transacao <= @Data_Final)
      AND (@Valor_Minimo IS NULL OR Valor_Transacao >= @Valor_Minimo)
      AND (@Valor_Maximo IS NULL OR Valor_Transacao <= @Valor_Maximo)
      AND (@Status_Transacao IS NULL OR Status_Transacao = @Status_Transacao)
      AND (@Categoria IS NULL OR dbo.fn_CategoriaTransacao(Valor_Transacao) = @Categoria)
    -- ORDER BY dinâmico sem SQL dinâmico: cada coluna clicável vira um par de CASE (um para ASC,
    -- um para DESC), cada um com um único tipo de dado (evita o problema de unificar tipos
    -- diferentes num único CASE). Só o par que bate com @Ordenar_Por/@Ordenar_Direcao "participa"
    -- da ordenação em cada chamada; os demais avaliam sempre NULL e não afetam o resultado.
    -- Fallback (Data_Transacao DESC, Id_Transacao DESC) garante ordem determinística quando
    -- @Ordenar_Por não bate com nenhuma coluna conhecida, e desempata dentro da coluna escolhida.
    ORDER BY
        CASE WHEN @Ordenar_Por = 'Id_Transacao' AND @Ordenar_Direcao = 'ASC' THEN Id_Transacao END ASC,
        CASE WHEN @Ordenar_Por = 'Id_Transacao' AND @Ordenar_Direcao = 'DESC' THEN Id_Transacao END DESC,
        CASE WHEN @Ordenar_Por = 'Numero_Cartao' AND @Ordenar_Direcao = 'ASC' THEN Numero_Cartao END ASC,
        CASE WHEN @Ordenar_Por = 'Numero_Cartao' AND @Ordenar_Direcao = 'DESC' THEN Numero_Cartao END DESC,
        CASE WHEN @Ordenar_Por = 'Valor_Transacao' AND @Ordenar_Direcao = 'ASC' THEN Valor_Transacao END ASC,
        CASE WHEN @Ordenar_Por = 'Valor_Transacao' AND @Ordenar_Direcao = 'DESC' THEN Valor_Transacao END DESC,
        CASE WHEN @Ordenar_Por = 'Data_Transacao' AND @Ordenar_Direcao = 'ASC' THEN Data_Transacao END ASC,
        CASE WHEN @Ordenar_Por = 'Data_Transacao' AND @Ordenar_Direcao = 'DESC' THEN Data_Transacao END DESC,
        CASE WHEN @Ordenar_Por = 'Descricao' AND @Ordenar_Direcao = 'ASC' THEN Descricao END ASC,
        CASE WHEN @Ordenar_Por = 'Descricao' AND @Ordenar_Direcao = 'DESC' THEN Descricao END DESC,
        CASE WHEN @Ordenar_Por = 'Status_Transacao' AND @Ordenar_Direcao = 'ASC' THEN Status_Transacao END ASC,
        CASE WHEN @Ordenar_Por = 'Status_Transacao' AND @Ordenar_Direcao = 'DESC' THEN Status_Transacao END DESC,
        Data_Transacao DESC,
        Id_Transacao DESC
    OFFSET @Offset ROWS
    FETCH NEXT @Limite ROWS ONLY;
END
GO

/*
    sp_TotalTransacoesPorPeriodo (item 2 do desafio)
    Total de transações por cartão dentro de um período, com o SUM/COUNT
    pedidos no enunciado. @Data_Inicial/@Data_Final são tratados como
    limites inclusivos exatos (quem chama decide a granularidade -
    ex.: passar 23:59:59 no final para incluir o dia inteiro).
*/
CREATE OR ALTER PROCEDURE dbo.sp_TotalTransacoesPorPeriodo
    @Data_Inicial       DATETIME,
    @Data_Final         DATETIME,
    @Status_Transacao   VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Numero_Cartao,
        SUM(Valor_Transacao)   AS Valor_Total,
        COUNT(*)               AS Quantidade_Transacoes,
        Status_Transacao
    FROM dbo.Transacoes
    WHERE Data_Transacao >= @Data_Inicial
      AND Data_Transacao <= @Data_Final
      AND (@Status_Transacao IS NULL OR Status_Transacao = @Status_Transacao)
    GROUP BY Numero_Cartao, Status_Transacao
    ORDER BY Numero_Cartao, Status_Transacao;
END
GO
