Imports System.Linq

Namespace Dominio

    ''' <summary>Uma ou mais violações de regras de validação de campo (entrada do usuário).</summary>
    Public Class ValidacaoException
        Inherits Exception

        Public ReadOnly Property Erros As IReadOnlyList(Of String)

        Public Sub New(erros As IEnumerable(Of String))
            MyBase.New(String.Join(Environment.NewLine, erros))
            Me.Erros = erros.ToList()
        End Sub

    End Class

End Namespace
