Imports XYZCartoes.Dominio

Namespace Dados

    Public Interface ITransacaoRepositorio

        Function Inserir(transacao As Transacao) As Integer
        Sub Atualizar(transacao As Transacao)
        Sub Excluir(idTransacao As Integer)
        Function ObterPorId(idTransacao As Integer) As Transacao
        Function ConsultarPaginado(filtro As FiltroConsultaTransacoes) As ResultadoPaginado(Of Transacao)
        Function ObterTransacoesPorPeriodo(dataInicial As Date, dataFinal As Date) As List(Of Transacao)
        Function ObterTotalPorPeriodo(dataInicial As Date, dataFinal As Date, status As StatusTransacao?) As List(Of TotalTransacaoPorCartao)

    End Interface

End Namespace
