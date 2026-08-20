Imports System.Linq
Imports System.Windows.Forms
Imports XYZCartoes.Dominio
Imports XYZCartoes.Negocio
Imports XYZCartoes.Utilitarios

Namespace Forms

    ''' <summary>Expõe visualmente o resultado de sp_TotalTransacoesPorPeriodo (item 2 do desafio).</summary>
    Public Class FrmRelatorioTotais

        Private ReadOnly _service As TransacaoService

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Sub New(service As TransacaoService)
            Me.New()
            _service = service

            cboStatus.Items.Add("(Todos)")
            For Each valor As StatusTransacao In [Enum].GetValues(GetType(StatusTransacao))
                cboStatus.Items.Add(valor)
            Next
            cboStatus.SelectedIndex = 0
            dtpDataInicial.Value = Date.Today.AddMonths(-6)
            dtpDataFinal.Value = Date.Today

            Buscar()
        End Sub

        Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
            Buscar()
        End Sub

        Private Sub Buscar()
            Try
                Dim statusFiltro As StatusTransacao? = Nothing
                If cboStatus.SelectedIndex > 0 Then
                    statusFiltro = CType(cboStatus.SelectedItem, StatusTransacao)
                End If

                Dim dataFinalComHora As Date = dtpDataFinal.Value.Date.AddDays(1).AddSeconds(-1)
                Dim totais As List(Of TotalTransacaoPorCartao) =
                    _service.ObterTotalPorPeriodo(dtpDataInicial.Value.Date, dataFinalComHora, statusFiltro)

                dgvTotais.Rows.Clear()
                For Each item As TotalTransacaoPorCartao In totais
                    dgvTotais.Rows.Add(FormatadorCartao.Mascarar(item.NumeroCartao), item.ValorTotal, item.QuantidadeTransacoes, item.StatusTransacao.ToString())
                Next

                lblResumo.Text = $"{totais.Count} linha(s) — Valor total geral: {totais.Sum(Function(t) t.ValorTotal):C2} — Transações: {totais.Sum(Function(t) t.QuantidadeTransacoes)}"

            Catch ex As Exception
                Logger.RegistrarErro(ex, "Consultar relatório de totais por período")
                MessageBox.Show("Ocorreu um erro inesperado ao consultar o relatório. Os detalhes foram registrados no log.",
                                 "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class

End Namespace
