Imports System.Globalization
Imports System.Windows.Forms
Imports XYZCartoes.Dominio
Imports XYZCartoes.Negocio
Imports XYZCartoes.Utilitarios

Namespace Forms

    Public Class FrmConsultaTransacoes

        Private ReadOnly _service As TransacaoService
        Private _paginaAtual As Integer = 1
        Private _resultadoAtual As ResultadoPaginado(Of Transacao)
        Private _layoutPronto As Boolean = False
        Private _ordenarPor As String = "Data_Transacao"
        Private _ordenarDirecao As String = "DESC"

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

            cboCategoriaFiltro.Items.Add("(Todos)")
            For Each valor As Categoria In [Enum].GetValues(GetType(Categoria))
                cboCategoriaFiltro.Items.Add(valor)
            Next
            cboCategoriaFiltro.SelectedIndex = 0

            dtpDataInicial.Value = Date.Today.AddMonths(-1)
            dtpDataFinal.Value = Date.Today

            cboTamanhoPagina.SelectedItem = 50

            _layoutPronto = True
        End Sub

        ''' <summary>A primeira consulta só roda aqui (não no construtor): antes de a janela ter um
        ''' handle do Windows, ajustes de rolagem na grid (ver CarregarPagina) não têm efeito permanente.</summary>
        Private Sub FrmConsultaTransacoes_Shown(sender As Object, e As EventArgs) Handles Me.Shown
            AtualizarGlifosOrdenacao(colData)
            CarregarPagina()
        End Sub

        ''' <summary>O enum Categoria não tem acento (Media); isso só ajusta o texto exibido no combo
        ''' para "Média", sem mudar o item selecionado de verdade.</summary>
        Private Sub cboCategoriaFiltro_Format(sender As Object, e As ListControlConvertEventArgs) Handles cboCategoriaFiltro.Format
            If TypeOf e.ListItem Is Categoria Then
                e.Value = CategoriaConversor.ParaTexto(CType(e.ListItem, Categoria))
            End If
        End Sub

#Region "Consulta e paginação"

        Private Sub CarregarPagina()
            Try
                Dim filtro As New FiltroConsultaTransacoes With {
                    .Id = ParseIntOuNulo(txtId.Text),
                    .NumeroCartao = If(txtNumeroCartao.Text.Trim().Length = 16, txtNumeroCartao.Text.Trim(), Nothing),
                    .DataInicial = If(chkDataInicial.Checked, CType(dtpDataInicial.Value.Date, Date?), Nothing),
                    .DataFinal = If(chkDataFinal.Checked, CType(dtpDataFinal.Value.Date.AddDays(1).AddSeconds(-1), Date?), Nothing),
                    .ValorMinimo = ParseDecimalOuNulo(txtValorMinimo.Text),
                    .ValorMaximo = ParseDecimalOuNulo(txtValorMaximo.Text),
                    .Status = If(cboStatus.SelectedIndex > 0, CType(CType(cboStatus.SelectedItem, StatusTransacao), StatusTransacao?), Nothing),
                    .Categoria = If(cboCategoriaFiltro.SelectedIndex > 0, CType(CType(cboCategoriaFiltro.SelectedItem, Categoria), Categoria?), Nothing),
                    .OrdenarPor = _ordenarPor,
                    .OrdenarDirecao = _ordenarDirecao,
                    .NumeroPagina = _paginaAtual,
                    .TamanhoPagina = CInt(cboTamanhoPagina.SelectedItem)
                }

                _resultadoAtual = _service.ConsultarTransacoes(filtro)

                Dim origem As New BindingSource With {.DataSource = _resultadoAtual.Itens}
                dgvTransacoes.DataSource = origem

                If dgvTransacoes.Rows.Count > 0 Then
                    dgvTransacoes.FirstDisplayedScrollingRowIndex = 0
                End If

                AtualizarInfoPaginacao()

            Catch ex As Exception
                Logger.RegistrarErro(ex, "Consultar transações")
                MessageBox.Show("Ocorreu um erro inesperado ao consultar as transações. Os detalhes foram registrados no log.",
                                 "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub AtualizarInfoPaginacao()
            Dim totalPaginas As Integer = Math.Max(_resultadoAtual.TotalPaginas, 1)
            lblPaginaInfo.Text = $"Página {_paginaAtual} de {totalPaginas} — {_resultadoAtual.TotalRegistros} registro(s)"

            btnPrimeira.Enabled = _paginaAtual > 1
            btnAnterior.Enabled = _paginaAtual > 1
            btnProxima.Enabled = _paginaAtual < totalPaginas
            btnUltima.Enabled = _paginaAtual < totalPaginas
        End Sub

        Private Shared Function ParseDecimalOuNulo(texto As String) As Decimal?
            Dim valor As Decimal
            If Decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, valor) Then
                Return valor
            End If
            Return Nothing
        End Function

        Private Shared Function ParseIntOuNulo(texto As String) As Integer?
            Dim valor As Integer
            If Integer.TryParse(texto, valor) Then
                Return valor
            End If
            Return Nothing
        End Function

        Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
            _paginaAtual = 1
            CarregarPagina()
        End Sub

        Private Sub btnLimparFiltros_Click(sender As Object, e As EventArgs) Handles btnLimparFiltros.Click
            txtId.Clear()
            txtNumeroCartao.Clear()
            chkDataInicial.Checked = False
            chkDataFinal.Checked = False
            dtpDataInicial.Value = Date.Today.AddMonths(-1)
            dtpDataFinal.Value = Date.Today
            txtValorMinimo.Clear()
            txtValorMaximo.Clear()
            cboStatus.SelectedIndex = 0
            cboCategoriaFiltro.SelectedIndex = 0
            _ordenarPor = "Data_Transacao"
            _ordenarDirecao = "DESC"
            AtualizarGlifosOrdenacao(colData)
            _paginaAtual = 1
            CarregarPagina()
        End Sub

        Private Sub chkDataInicial_CheckedChanged(sender As Object, e As EventArgs) Handles chkDataInicial.CheckedChanged
            dtpDataInicial.Enabled = chkDataInicial.Checked
        End Sub

        Private Sub chkDataFinal_CheckedChanged(sender As Object, e As EventArgs) Handles chkDataFinal.CheckedChanged
            dtpDataFinal.Enabled = chkDataFinal.Checked
        End Sub

        Private Sub txtNumeroCartao_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumeroCartao.KeyPress
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

        Private Sub txtId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtId.KeyPress
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

        Private Sub btnPrimeira_Click(sender As Object, e As EventArgs) Handles btnPrimeira.Click
            _paginaAtual = 1
            CarregarPagina()
        End Sub

        Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
            If _paginaAtual > 1 Then
                _paginaAtual -= 1
                CarregarPagina()
            End If
        End Sub

        Private Sub btnProxima_Click(sender As Object, e As EventArgs) Handles btnProxima.Click
            _paginaAtual += 1
            CarregarPagina()
        End Sub

        Private Sub btnUltima_Click(sender As Object, e As EventArgs) Handles btnUltima.Click
            _paginaAtual = Math.Max(_resultadoAtual.TotalPaginas, 1)
            CarregarPagina()
        End Sub

        Private Sub cboTamanhoPagina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTamanhoPagina.SelectedIndexChanged
            If Not _layoutPronto Then Return
            _paginaAtual = 1
            CarregarPagina()
        End Sub

#End Region

#Region "Grid: formatação e seleção"

        Private Sub dgvTransacoes_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvTransacoes.CellFormatting
            If dgvTransacoes.Columns(e.ColumnIndex).Name = "colCartao" AndAlso e.Value IsNot Nothing Then
                e.Value = FormatadorCartao.Mascarar(CStr(e.Value))
                e.FormattingApplied = True
            ElseIf dgvTransacoes.Columns(e.ColumnIndex).Name = "colCategoria" AndAlso TypeOf e.Value Is Categoria Then
                e.Value = CategoriaConversor.ParaTexto(CType(e.Value, Categoria))
                e.FormattingApplied = True
            End If
        End Sub

        Private Sub dgvTransacoes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvTransacoes.DataBindingComplete
            For Each linha As DataGridViewRow In dgvTransacoes.Rows
                Dim transacao As Transacao = TryCast(linha.DataBoundItem, Transacao)
                If transacao Is Nothing Then Continue For

                Select Case transacao.StatusTransacao
                    Case StatusTransacao.Aprovada
                        linha.DefaultCellStyle.BackColor = Drawing.Color.Honeydew
                    Case StatusTransacao.Cancelada
                        linha.DefaultCellStyle.BackColor = Drawing.Color.WhiteSmoke
                    Case StatusTransacao.Pendente
                        linha.DefaultCellStyle.BackColor = Drawing.Color.LightYellow
                End Select
            Next
        End Sub

        Private Function ObterTransacaoSelecionada() As Transacao
            If dgvTransacoes.CurrentRow Is Nothing Then Return Nothing
            Return TryCast(dgvTransacoes.CurrentRow.DataBoundItem, Transacao)
        End Function

        ''' <summary>Ordenação roda no servidor (sp_ConsultarTransacoesPaginado), não só na página
        ''' carregada — clicar num header refaz a consulta contra o conjunto filtrado inteiro, não
        ''' só as ~50 linhas visíveis. Colunas com SortMode=Programmatic (ver Designer) para o grid
        ''' não tentar uma ordenação local por conta própria.</summary>
        Private Sub dgvTransacoes_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvTransacoes.ColumnHeaderMouseClick
            Dim coluna As DataGridViewColumn = dgvTransacoes.Columns(e.ColumnIndex)
            Dim colunaOrdenacao As String = ObterColunaOrdenacao(coluna.Name)

            If _ordenarPor = colunaOrdenacao Then
                _ordenarDirecao = If(_ordenarDirecao = "ASC", "DESC", "ASC")
            Else
                _ordenarPor = colunaOrdenacao
                _ordenarDirecao = "ASC"
            End If

            AtualizarGlifosOrdenacao(coluna)

            _paginaAtual = 1
            CarregarPagina()
        End Sub

        ''' <summary>Categoria não é uma coluna real (é calculada a partir de Valor_Transacao), e a
        ''' faixa de categoria é estritamente crescente com o valor — então ordenar por Categoria dá
        ''' exatamente a mesma ordem que ordenar por Valor_Transacao, sem precisar de um caso à parte
        ''' na stored procedure.</summary>
        Private Shared Function ObterColunaOrdenacao(nomeColuna As String) As String
            Select Case nomeColuna
                Case "colId" : Return "Id_Transacao"
                Case "colCartao" : Return "Numero_Cartao"
                Case "colValor", "colCategoria" : Return "Valor_Transacao"
                Case "colDescricao" : Return "Descricao"
                Case "colStatus" : Return "Status_Transacao"
                Case Else : Return "Data_Transacao"
            End Select
        End Function

        Private Sub AtualizarGlifosOrdenacao(colunaAtiva As DataGridViewColumn)
            For Each coluna As DataGridViewColumn In dgvTransacoes.Columns
                coluna.HeaderCell.SortGlyphDirection = SortOrder.None
            Next
            colunaAtiva.HeaderCell.SortGlyphDirection = If(_ordenarDirecao = "ASC", SortOrder.Ascending, SortOrder.Descending)
        End Sub

#End Region

#Region "Ações: novo, editar, excluir, exportar, relatório"

        Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
            Using formulario As New FrmTransacaoCadastro(_service)
                If formulario.ShowDialog(Me) = DialogResult.OK Then
                    CarregarPagina()
                End If
            End Using
        End Sub

        Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
            Dim transacaoSelecionada As Transacao = ObterTransacaoSelecionada()
            If transacaoSelecionada Is Nothing Then
                MessageBox.Show("Selecione uma transação na lista para editar.", "Nenhuma transação selecionada",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Try
                Dim transacaoAtualizada As Transacao = _service.ObterPorId(transacaoSelecionada.IdTransacao)
                If transacaoAtualizada Is Nothing Then
                    MessageBox.Show("Esta transação não existe mais. A lista será atualizada.", "Transação não encontrada",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CarregarPagina()
                    Return
                End If

                If transacaoAtualizada.StatusTransacao = StatusTransacao.Aprovada Then
                    MessageBox.Show("Esta transação está com status 'Aprovada' e não pode ser editada.",
                                     "Edição não permitida", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                Using formulario As New FrmTransacaoCadastro(_service, transacaoAtualizada)
                    If formulario.ShowDialog(Me) = DialogResult.OK Then
                        CarregarPagina()
                    End If
                End Using

            Catch ex As Exception
                Logger.RegistrarErro(ex, $"Abrir transação para edição (Id={transacaoSelecionada.IdTransacao})")
                MessageBox.Show("Ocorreu um erro inesperado ao abrir a transação. Os detalhes foram registrados no log.",
                                 "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
            Dim transacaoSelecionada As Transacao = ObterTransacaoSelecionada()
            If transacaoSelecionada Is Nothing Then
                MessageBox.Show("Selecione uma transação na lista para excluir.", "Nenhuma transação selecionada",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim confirmacao As DialogResult = MessageBox.Show(
                $"Confirma a exclusão da transação Id {transacaoSelecionada.IdTransacao} (cartão {FormatadorCartao.Mascarar(transacaoSelecionada.NumeroCartao)})?",
                "Confirmar exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If confirmacao <> DialogResult.Yes Then Return

            Try
                _service.ExcluirTransacao(transacaoSelecionada.IdTransacao)

                If _resultadoAtual.Itens.Count = 1 AndAlso _paginaAtual > 1 Then
                    _paginaAtual -= 1
                End If
                CarregarPagina()

            Catch ex As RegraNegocioException
                MessageBox.Show(ex.Message, "Não foi possível excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CarregarPagina()

            Catch ex As Exception
                Logger.RegistrarErro(ex, $"Excluir transação (Id={transacaoSelecionada.IdTransacao})")
                MessageBox.Show("Ocorreu um erro ao excluir a transação. Os detalhes foram registrados no log.",
                                 "Erro ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnExportarUltimoMes_Click(sender As Object, e As EventArgs) Handles btnExportarUltimoMes.Click
            Try
                Dim transacoes As List(Of Transacao) = _service.ObterTransacoesUltimoMes()

                If transacoes.Count = 0 Then
                    MessageBox.Show("Não há transações no último mês para exportar.", "Nada para exportar",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                Using dialogo As New SaveFileDialog With {
                    .Filter = "Planilha do Excel (*.xlsx)|*.xlsx",
                    .FileName = $"Transacoes_UltimoMes_{Date.Now:yyyyMMdd_HHmmss}.xlsx"
                }
                    If dialogo.ShowDialog(Me) <> DialogResult.OK Then Return

                    ExportadorExcelService.Exportar(transacoes, dialogo.FileName)

                    MessageBox.Show(
                        $"{transacoes.Count} transação(ões) exportada(s) com sucesso para:{Environment.NewLine}{dialogo.FileName}",
                        "Exportação concluída", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using

            Catch ex As Exception
                Logger.RegistrarErro(ex, "Exportar transações do último mês para Excel")
                MessageBox.Show("Ocorreu um erro ao gerar o relatório Excel. Os detalhes foram registrados no log.",
                                 "Erro na exportação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnRelatorioTotais_Click(sender As Object, e As EventArgs) Handles btnRelatorioTotais.Click
            Using formulario As New FrmRelatorioTotais(_service)
                formulario.ShowDialog(Me)
            End Using
        End Sub

#End Region

    End Class

End Namespace
