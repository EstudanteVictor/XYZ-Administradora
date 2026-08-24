Imports System.Linq
Imports System.Text

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

        ''' <summary>
        ''' Extrai apenas os dígitos de um texto, descartando espaços ou qualquer outro caractere de formatação.
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns></returns>
        Public Function ExtrairDigitos(texto As String) As String
            If texto Is Nothing Then Return String.Empty
            Return New String(texto.Where(AddressOf Char.IsDigit).ToArray())
        End Function

        ''' <summary>
        ''' Agrupa os dígitos do número do cartão em blocos de 4 separados por espaço (ex.: "4000 0000 0000 0042"),
        ''' usado para exibir o número completo de forma legível quando a máscara está desativada.
        ''' </summary>
        ''' <param name="numeroCartao"></param>
        ''' <returns></returns>
        Public Function Formatar(numeroCartao As String) As String
            Dim digitos As String = ExtrairDigitos(numeroCartao)
            If digitos.Length = 0 Then Return digitos

            Dim resultado As New StringBuilder()
            For i As Integer = 0 To digitos.Length - 1 Step 4
                If resultado.Length > 0 Then resultado.Append(" "c)
                resultado.Append(digitos.Substring(i, Math.Min(4, digitos.Length - i)))
            Next
            Return resultado.ToString()
        End Function

    End Module

End Namespace
