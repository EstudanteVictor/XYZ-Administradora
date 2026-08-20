Namespace Dominio

    Public Class ResultadoPaginado(Of T)

        Public Property Itens As List(Of T)
        Public Property TotalRegistros As Integer
        Public Property NumeroPagina As Integer
        Public Property TamanhoPagina As Integer

        Public ReadOnly Property TotalPaginas As Integer
            Get
                If TamanhoPagina <= 0 Then Return 0
                Return CInt(Math.Ceiling(TotalRegistros / CDbl(TamanhoPagina)))
            End Get
        End Property

    End Class

End Namespace
