Namespace Utilitarios

    Public Module FormatadorCartao

        ''' <summary>
        ''' Mascara o número do cartão exibindo apenas os 4 últimos dígitos (usado na grid e no Excel exportado).
        ''' </summary>
        ''' <param name="numeroCartao"></param>
        ''' <returns></returns>
        Public Function Mascarar(numeroCartao As String) As String
            If String.IsNullOrEmpty(numeroCartao) OrElse numeroCartao.Length < 4 Then
                Return numeroCartao
            End If
            Return "**** **** **** " & numeroCartao.Substring(numeroCartao.Length - 4)
        End Function

    End Module

End Namespace
