Imports ClosedXML.Excel
Imports XYZCartoes.Dominio

Namespace Utilitarios

    Public Module ExportadorExcelService

        Public Sub Exportar(transacoes As List(Of Transacao), caminhoArquivo As String)
            Using pasta As New XLWorkbook()
                Dim planilha = pasta.Worksheets.Add("Transações")

                planilha.Cell(1, 1).Value = "Id"
                planilha.Cell(1, 2).Value = "Número do Cartão"
                planilha.Cell(1, 3).Value = "Valor"
                planilha.Cell(1, 4).Value = "Data"
                planilha.Cell(1, 5).Value = "Descrição"
                planilha.Cell(1, 6).Value = "Status"
                planilha.Row(1).Style.Font.Bold = True

                Dim linha As Integer = 2
                For Each transacao As Transacao In transacoes
                    planilha.Cell(linha, 1).Value = transacao.IdTransacao
                    planilha.Cell(linha, 2).Value = FormatadorCartao.Mascarar(transacao.NumeroCartao)

                    planilha.Cell(linha, 3).Value = transacao.ValorTransacao
                    planilha.Cell(linha, 3).Style.NumberFormat.Format = "R$ #,##0.00"

                    planilha.Cell(linha, 4).Value = transacao.DataTransacao
                    planilha.Cell(linha, 4).Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss"

                    planilha.Cell(linha, 5).Value = transacao.Descricao
                    planilha.Cell(linha, 6).Value = transacao.StatusTransacao.ToString()

                    linha += 1
                Next

                planilha.Columns().AdjustToContents()
                planilha.SheetView.FreezeRows(1)

                pasta.SaveAs(caminhoArquivo)
            End Using
        End Sub

    End Module

End Namespace
