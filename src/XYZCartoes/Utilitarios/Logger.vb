Imports System.IO

Namespace Utilitarios

    ''' <summary>Log de erros técnicos em arquivo de texto, um por dia, em Logs\ ao lado do executável.</summary>
    Public NotInheritable Class Logger

        Private Shared ReadOnly TravaEscrita As New Object()

        Private Sub New()
        End Sub

        Private Shared ReadOnly Property PastaLogs As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")
            End Get
        End Property

        Public Shared Sub RegistrarErro(excecao As Exception, contexto As String)
            RegistrarLinha("ERRO", contexto, excecao.ToString())
        End Sub

        Public Shared Sub RegistrarAviso(mensagem As String, contexto As String)
            RegistrarLinha("AVISO", contexto, mensagem)
        End Sub

        Private Shared Sub RegistrarLinha(nivel As String, contexto As String, detalhe As String)
            Try
                SyncLock TravaEscrita
                    If Not Directory.Exists(PastaLogs) Then
                        Directory.CreateDirectory(PastaLogs)
                    End If

                    Dim caminhoArquivo As String = Path.Combine(PastaLogs, $"log_{Date.Now:yyyyMMdd}.txt")
                    Dim linha As String =
                        $"[{Date.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}] [{contexto}]{Environment.NewLine}{detalhe}{Environment.NewLine}{New String("-"c, 80)}{Environment.NewLine}"

                    File.AppendAllText(caminhoArquivo, linha)
                End SyncLock
            Catch
                ' Falha ao gravar log não pode derrubar a aplicação nem virar um novo erro para o usuário.
            End Try
        End Sub

    End Class

End Namespace
