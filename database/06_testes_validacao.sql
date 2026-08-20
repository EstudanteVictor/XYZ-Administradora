/*
    06_testes_validacao.sql
    XYZ Administradora de Cartões de Crédito
    Bateria de consultas/execuções demonstrando cada objeto criado pelos
    scripts anteriores. Não é uma suíte de testes automatizada (fora do
    escopo pedido no desafio) - é um roteiro manual para validar, via
    SSMS/sqlcmd, que tabela, procedures, functions e view funcionam.
    Rode 01 a 05 antes deste script.
*/

USE XYZCartoesDB;
GO

PRINT '--- 1) sp_InserirTransacao ---';
DECLARE @NovoId INT;
EXEC dbo.sp_InserirTransacao
    @Numero_Cartao    = '4000000000000001',
    @Valor_Transacao  = 1234.56,
    @Descricao        = N'Transação de teste - inserção',
    @Status_Transacao = 'Pendente',
    @Id_Transacao     = @NovoId OUTPUT;

SELECT @NovoId AS Id_Transacao_Inserido;
EXEC dbo.sp_ObterTransacaoPorId @Id_Transacao = @NovoId;
GO

PRINT '--- 2) sp_AtualizarTransacao (caso válido: status Pendente) ---';
DECLARE @Id INT = (SELECT TOP 1 Id_Transacao FROM dbo.Transacoes WHERE Numero_Cartao = '4000000000000001' AND Status_Transacao = 'Pendente' ORDER BY Id_Transacao DESC);
EXEC dbo.sp_AtualizarTransacao
    @Id_Transacao     = @Id,
    @Numero_Cartao    = '4000000000000001',
    @Valor_Transacao  = 999.90,
    @Descricao        = N'Transação de teste - atualizada',
    @Status_Transacao = 'Aprovada';

EXEC dbo.sp_ObterTransacaoPorId @Id_Transacao = @Id;
GO

PRINT '--- 3) sp_AtualizarTransacao (caso inválido: transação já Aprovada -> deve lançar erro 50002) ---';
BEGIN TRY
    DECLARE @IdAprovada INT = (SELECT TOP 1 Id_Transacao FROM dbo.Transacoes WHERE Numero_Cartao = '4000000000000001' AND Status_Transacao = 'Aprovada' ORDER BY Id_Transacao DESC);
    EXEC dbo.sp_AtualizarTransacao
        @Id_Transacao     = @IdAprovada,
        @Numero_Cartao    = '4000000000000001',
        @Valor_Transacao  = 1.00,
        @Descricao        = N'Não deveria conseguir salvar',
        @Status_Transacao = 'Cancelada';
END TRY
BEGIN CATCH
    PRINT 'Erro esperado -> Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10)) + ' | Mensagem: ' + ERROR_MESSAGE();
END CATCH
GO

PRINT '--- 4) sp_ExcluirTransacao ---';
DECLARE @IdParaExcluir INT;
EXEC dbo.sp_InserirTransacao
    @Numero_Cartao = '4000000000000002', @Valor_Transacao = 50.00,
    @Descricao = N'Transação de teste - será excluída', @Status_Transacao = 'Pendente',
    @Id_Transacao = @IdParaExcluir OUTPUT;

EXEC dbo.sp_ExcluirTransacao @Id_Transacao = @IdParaExcluir;

SELECT COUNT(*) AS Deve_Ser_Zero FROM dbo.Transacoes WHERE Id_Transacao = @IdParaExcluir;
GO

PRINT '--- 4b) sp_ExcluirTransacao com Id inexistente -> deve lançar erro 50001 ---';
BEGIN TRY
    EXEC dbo.sp_ExcluirTransacao @Id_Transacao = -1;
END TRY
BEGIN CATCH
    PRINT 'Erro esperado -> Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10)) + ' | Mensagem: ' + ERROR_MESSAGE();
END CATCH
GO

PRINT '--- 5) sp_ConsultarTransacoesPaginado (página 1, tamanho 10, filtro por status) ---';
DECLARE @Total INT;
EXEC dbo.sp_ConsultarTransacoesPaginado
    @Status_Transacao = 'Aprovada',
    @Numero_Pagina    = 1,
    @Tamanho_Pagina   = 10,
    @Total_Registros  = @Total OUTPUT;
PRINT 'Total de registros aprovados: ' + CAST(@Total AS VARCHAR(10));
GO

PRINT '--- 5b) sp_ConsultarTransacoesPaginado sem paginação (usada na exportação Excel) ---';
DECLARE @TotalExport INT;
EXEC dbo.sp_ConsultarTransacoesPaginado
    @Data_Inicial     = '2000-01-01',
    @Data_Final       = '2100-01-01',
    @Total_Registros  = @TotalExport OUTPUT;
PRINT 'Total no período (sem paginação): ' + CAST(@TotalExport AS VARCHAR(10));
GO

PRINT '--- 6) sp_TotalTransacoesPorPeriodo (últimos 6 meses) ---';
EXEC dbo.sp_TotalTransacoesPorPeriodo
    @Data_Inicial = '2000-01-01',
    @Data_Final   = '2100-01-01';
GO

PRINT '--- 7) fn_CategoriaTransacao (casos de fronteira) ---';
SELECT v.Valor, dbo.fn_CategoriaTransacao(v.Valor) AS Categoria
FROM (VALUES (100.00), (499.99), (500.00), (999.99), (1000.00), (2000.00), (2000.01), (5000.00)) AS v(Valor);
GO

PRINT '--- 8) fn_TransacoesCategorizadas (últimos 30 dias) ---';
SELECT TOP 20 *
FROM dbo.fn_TransacoesCategorizadas(DATEADD(DAY, -30, SYSDATETIME()), SYSDATETIME())
ORDER BY Data_Transacao DESC;
GO

PRINT '--- 9) vw_ConsolidadoFinanceiro (top 20 por Valor_Total) ---';
SELECT TOP 20 *
FROM dbo.vw_ConsolidadoFinanceiro
ORDER BY Valor_Total DESC;
GO
