Namespace Dominio

    ''' <summary>Violação de uma regra de negócio (ex.: editar transação já Aprovada), distinta de uma falha técnica.</summary>
    Public Class RegraNegocioException
        Inherits Exception

        Public Sub New(mensagem As String)
            MyBase.New(mensagem)
        End Sub

        Public Sub New(mensagem As String, innerException As Exception)
            MyBase.New(mensagem, innerException)
        End Sub

    End Class

End Namespace
