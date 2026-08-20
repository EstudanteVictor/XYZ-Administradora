Imports System.Configuration
Imports System.Data.SqlClient

Namespace Dados

    ''' <summary>Único ponto que lê a connection string do App.config e cria conexões com o banco.</summary>
    Public Module ConexaoFactory

        Private Const NomeConexao As String = "XYZCartoesConnection"

        Public Function ObterConexao() As SqlConnection
            Dim configuracao As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(NomeConexao)

            If configuracao Is Nothing Then
                Throw New ConfigurationErrorsException(
                    $"Connection string '{NomeConexao}' não encontrada em App.config.")
            End If

            Return New SqlConnection(configuracao.ConnectionString)
        End Function

    End Module

End Namespace
