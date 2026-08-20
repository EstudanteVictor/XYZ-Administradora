Imports System.Data.SqlClient
Imports XYZCartoes.Dominio

Namespace Dados

    ''' <summary>
    ''' Resolve os ordinais de coluna uma única vez por leitura e mapeia cada linha do SqlDataReader para Transacao.
    ''' </summary>
    Friend Module TransacaoMapper

        Friend Structure Ordinais
            Public Id As Integer
            Public Cartao As Integer
            Public Valor As Integer
            Public Categoria As Integer
            Public Data As Integer
            Public Descricao As Integer
            Public Status As Integer
        End Structure

        Friend Function ObterOrdinais(leitor As SqlDataReader) As Ordinais
            Return New Ordinais With {
                .Id = leitor.GetOrdinal("Id_Transacao"),
                .Cartao = leitor.GetOrdinal("Numero_Cartao"),
                .Valor = leitor.GetOrdinal("Valor_Transacao"),
                .Categoria = leitor.GetOrdinal("Categoria"),
                .Data = leitor.GetOrdinal("Data_Transacao"),
                .Descricao = leitor.GetOrdinal("Descricao"),
                .Status = leitor.GetOrdinal("Status_Transacao")
            }
        End Function

        Friend Function Mapear(leitor As SqlDataReader, ordinais As Ordinais) As Transacao
            Return New Transacao With {
                .IdTransacao = leitor.GetInt32(ordinais.Id),
                .NumeroCartao = leitor.GetString(ordinais.Cartao).Trim(),
                .ValorTransacao = leitor.GetDecimal(ordinais.Valor),
                .Categoria = If(leitor.IsDBNull(ordinais.Categoria), CType(Nothing, Categoria?), CategoriaConversor.ParaEnum(leitor.GetString(ordinais.Categoria))),
                .DataTransacao = leitor.GetDateTime(ordinais.Data),
                .Descricao = If(leitor.IsDBNull(ordinais.Descricao), String.Empty, leitor.GetString(ordinais.Descricao)),
                .StatusTransacao = StatusTransacaoConversor.ParaEnum(leitor.GetString(ordinais.Status))
            }
        End Function

    End Module

End Namespace
