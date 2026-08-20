Imports System.Linq
Imports XYZCartoes.Dominio

Namespace Negocio

    Public Module ValidadorTransacao

        Public Function Validar(transacao As Transacao) As List(Of String)
            Dim erros As New List(Of String)

            If Not ValidarNumeroCartao(transacao.NumeroCartao) Then
                erros.Add("O número do cartão deve conter exatamente 16 dígitos numéricos.")
            End If

            If Not ValidarValor(transacao.ValorTransacao) Then
                erros.Add("O valor da transação deve ser maior que zero.")
            End If

            If Not ValidarDescricao(transacao.Descricao) Then
                erros.Add("A descrição deve ter no máximo 255 caracteres.")
            End If

            Return erros
        End Function

        Public Function ValidarNumeroCartao(numeroCartao As String) As Boolean
            Return Not String.IsNullOrEmpty(numeroCartao) AndAlso
                   numeroCartao.Length = 16 AndAlso
                   numeroCartao.All(AddressOf Char.IsDigit)
        End Function

        Public Function ValidarValor(valor As Decimal) As Boolean
            Return valor > 0D
        End Function

        Public Function ValidarDescricao(descricao As String) As Boolean
            Return descricao Is Nothing OrElse descricao.Length <= 255
        End Function

        ''' <summary>Checksum mod 10 (algoritmo de Luhn) usado por bandeiras de cartão para detectar erro de
        ''' digitação. É uma checagem à parte, opcional — não bloqueia o cadastro/edição da transação.</summary>
        Public Function ValidarLuhn(numeroCartao As String) As Boolean
            If Not ValidarNumeroCartao(numeroCartao) Then Return False

            Dim soma As Integer = 0
            Dim dobrarDigito As Boolean = False

            For i As Integer = numeroCartao.Length - 1 To 0 Step -1
                Dim digito As Integer = Integer.Parse(numeroCartao(i).ToString())

                If dobrarDigito Then
                    digito *= 2
                    If digito > 9 Then digito -= 9
                End If

                soma += digito
                dobrarDigito = Not dobrarDigito
            Next

            Return soma Mod 10 = 0
        End Function

    End Module

End Namespace
