Namespace Dominio

    Public Class FiltroConsultaTransacoes

        Public Property Id As Integer?
        Public Property NumeroCartao As String
        Public Property DataInicial As Date?
        Public Property DataFinal As Date?
        Public Property ValorMinimo As Decimal?
        Public Property ValorMaximo As Decimal?
        Public Property Status As StatusTransacao?
        Public Property Categoria As Categoria?

        ''' <summary>Nome exato da coluna no banco (Id_Transacao, Numero_Cartao, Valor_Transacao,
        ''' Data_Transacao, Descricao ou Status_Transacao) — repassado direto para sp_ConsultarTransacoesPaginado.</summary>
        Public Property OrdenarPor As String = "Data_Transacao"

        ''' <summary>"ASC" ou "DESC".</summary>
        Public Property OrdenarDirecao As String = "DESC"

        Public Property NumeroPagina As Integer = 1
        Public Property TamanhoPagina As Integer = 50

    End Class

End Namespace
