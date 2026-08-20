Namespace Dominio

    Public Class Transacao

        Public Property IdTransacao As Integer
        Public Property NumeroCartao As String
        Public Property ValorTransacao As Decimal

        ''' <summary>Derivada de ValorTransacao pelo banco (fn_CategoriaTransacao). Só vem preenchida
        ''' quando a transação é lida via consulta/obtenção; não se aplica a um objeto recém-criado
        ''' para inserção.</summary>
        Public Property Categoria As Categoria?

        Public Property DataTransacao As DateTime
        Public Property Descricao As String
        Public Property StatusTransacao As StatusTransacao

    End Class

End Namespace
