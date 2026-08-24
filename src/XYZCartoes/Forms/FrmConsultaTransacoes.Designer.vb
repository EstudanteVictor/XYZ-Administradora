Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmConsultaTransacoes
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.dgvTransacoes = New System.Windows.Forms.DataGridView()
            Me.colId = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCartao = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colValor = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCategoria = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colData = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colDescricao = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlPaginacao = New System.Windows.Forms.Panel()
            Me.cboTamanhoPagina = New System.Windows.Forms.ComboBox()
            Me.lblTamanho = New System.Windows.Forms.Label()
            Me.btnUltima = New System.Windows.Forms.Button()
            Me.btnProxima = New System.Windows.Forms.Button()
            Me.lblPaginaInfo = New System.Windows.Forms.Label()
            Me.btnAnterior = New System.Windows.Forms.Button()
            Me.btnPrimeira = New System.Windows.Forms.Button()
            Me.pnlAcoes = New System.Windows.Forms.Panel()
            Me.btnRelatorioTotais = New System.Windows.Forms.Button()
            Me.btnExportarUltimoMes = New System.Windows.Forms.Button()
            Me.btnExcluir = New System.Windows.Forms.Button()
            Me.btnEditar = New System.Windows.Forms.Button()
            Me.btnNovo = New System.Windows.Forms.Button()
            Me.pnlFiltros = New System.Windows.Forms.Panel()
            Me.btnLimparFiltros = New System.Windows.Forms.Button()
            Me.btnFiltrar = New System.Windows.Forms.Button()
            Me.dtpDataFinal = New System.Windows.Forms.DateTimePicker()
            Me.chkDataFinal = New System.Windows.Forms.CheckBox()
            Me.dtpDataInicial = New System.Windows.Forms.DateTimePicker()
            Me.chkDataInicial = New System.Windows.Forms.CheckBox()
            Me.txtValorMaximo = New System.Windows.Forms.TextBox()
            Me.lblValorMax = New System.Windows.Forms.Label()
            Me.txtValorMinimo = New System.Windows.Forms.TextBox()
            Me.lblValorMin = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.txtNumeroCartao = New System.Windows.Forms.TextBox()
            Me.lblCartao = New System.Windows.Forms.Label()
            Me.txtId = New System.Windows.Forms.TextBox()
            Me.lblId = New System.Windows.Forms.Label()
            Me.cboCategoriaFiltro = New System.Windows.Forms.ComboBox()
            Me.lblCategoriaFiltro = New System.Windows.Forms.Label()
            Me.chkMascararCartao = New System.Windows.Forms.CheckBox()
            CType(Me.dgvTransacoes, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlPaginacao.SuspendLayout()
            Me.pnlAcoes.SuspendLayout()
            Me.pnlFiltros.SuspendLayout()
            Me.SuspendLayout()
            '
            'dgvTransacoes
            '
            Me.dgvTransacoes.AllowUserToAddRows = False
            Me.dgvTransacoes.AllowUserToDeleteRows = False
            Me.dgvTransacoes.AutoGenerateColumns = False
            Me.dgvTransacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvTransacoes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colId, Me.colCartao, Me.colValor, Me.colCategoria, Me.colData, Me.colDescricao, Me.colStatus})
            Me.dgvTransacoes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvTransacoes.Location = New System.Drawing.Point(0, 0)
            Me.dgvTransacoes.MultiSelect = False
            Me.dgvTransacoes.Name = "dgvTransacoes"
            Me.dgvTransacoes.ReadOnly = True
            Me.dgvTransacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvTransacoes.Size = New System.Drawing.Size(1050, 638)
            Me.dgvTransacoes.TabIndex = 3
            '
            'colId
            '
            Me.colId.DataPropertyName = "IdTransacao"
            Me.colId.HeaderText = "Id"
            Me.colId.Name = "colId"
            Me.colId.ReadOnly = True
            Me.colId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
            Me.colId.Width = 60
            '
            'colCartao
            '
            Me.colCartao.DataPropertyName = "NumeroCartao"
            Me.colCartao.HeaderText = "Número do Cartão"
            Me.colCartao.Name = "colCartao"
            Me.colCartao.ReadOnly = True
            Me.colCartao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
            Me.colCartao.Width = 170
            '
            'colValor
            '
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
            DataGridViewCellStyle1.Format = "C2"
            DataGridViewCellStyle1.FormatProvider = New System.Globalization.CultureInfo("pt-BR")
            Me.colValor.DefaultCellStyle = DataGridViewCellStyle1
            Me.colValor.DataPropertyName = "ValorTransacao"
            Me.colValor.HeaderText = "Valor"
            Me.colValor.Name = "colValor"
            Me.colValor.ReadOnly = True
            Me.colValor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
            Me.colValor.Width = 110
            '
            'colCategoria
            '
            Me.colCategoria.DataPropertyName = "Categoria"
            Me.colCategoria.HeaderText = "Categoria"
            Me.colCategoria.Name = "colCategoria"
            Me.colCategoria.ReadOnly = True
            Me.colCategoria.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
            Me.colCategoria.Width = 90
            '
            'colData
            '
            DataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss"
            Me.colData.DefaultCellStyle = DataGridViewCellStyle2
            Me.colData.DataPropertyName = "DataTransacao"
            Me.colData.HeaderText = "Data"
            Me.colData.Name = "colData"
            Me.colData.ReadOnly = True
            Me.colData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
            Me.colData.Width = 140
            '
            'colDescricao
            '
            Me.colDescricao.DataPropertyName = "Descricao"
            Me.colDescricao.HeaderText = "Descrição"
            Me.colDescricao.Name = "colDescricao"
            Me.colDescricao.ReadOnly = True
            Me.colDescricao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
            Me.colDescricao.Width = 260
            '
            'colStatus
            '
            Me.colStatus.DataPropertyName = "StatusTransacao"
            Me.colStatus.HeaderText = "Status"
            Me.colStatus.Name = "colStatus"
            Me.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
            Me.colStatus.ReadOnly = True
            Me.colStatus.Width = 100
            '
            'pnlPaginacao
            '
            Me.pnlPaginacao.Controls.Add(Me.cboTamanhoPagina)
            Me.pnlPaginacao.Controls.Add(Me.lblTamanho)
            Me.pnlPaginacao.Controls.Add(Me.btnUltima)
            Me.pnlPaginacao.Controls.Add(Me.btnProxima)
            Me.pnlPaginacao.Controls.Add(Me.lblPaginaInfo)
            Me.pnlPaginacao.Controls.Add(Me.btnAnterior)
            Me.pnlPaginacao.Controls.Add(Me.btnPrimeira)
            Me.pnlPaginacao.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlPaginacao.Location = New System.Drawing.Point(0, 638)
            Me.pnlPaginacao.Name = "pnlPaginacao"
            Me.pnlPaginacao.Size = New System.Drawing.Size(1050, 42)
            Me.pnlPaginacao.TabIndex = 2
            '
            'cboTamanhoPagina
            '
            Me.cboTamanhoPagina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboTamanhoPagina.Items.AddRange(New Object() {25, 50, 100})
            Me.cboTamanhoPagina.Location = New System.Drawing.Point(645, 8)
            Me.cboTamanhoPagina.Name = "cboTamanhoPagina"
            Me.cboTamanhoPagina.Size = New System.Drawing.Size(70, 21)
            Me.cboTamanhoPagina.TabIndex = 6
            '
            'lblTamanho
            '
            Me.lblTamanho.Location = New System.Drawing.Point(510, 12)
            Me.lblTamanho.Name = "lblTamanho"
            Me.lblTamanho.Size = New System.Drawing.Size(130, 20)
            Me.lblTamanho.TabIndex = 5
            Me.lblTamanho.Text = "Registros por página:"
            '
            'btnUltima
            '
            Me.btnUltima.Location = New System.Drawing.Point(446, 7)
            Me.btnUltima.Name = "btnUltima"
            Me.btnUltima.Size = New System.Drawing.Size(32, 26)
            Me.btnUltima.TabIndex = 4
            Me.btnUltima.Text = ">|"
            Me.btnUltima.UseVisualStyleBackColor = True
            '
            'btnProxima
            '
            Me.btnProxima.Location = New System.Drawing.Point(410, 7)
            Me.btnProxima.Name = "btnProxima"
            Me.btnProxima.Size = New System.Drawing.Size(32, 26)
            Me.btnProxima.TabIndex = 3
            Me.btnProxima.Text = ">"
            Me.btnProxima.UseVisualStyleBackColor = True
            '
            'lblPaginaInfo
            '
            Me.lblPaginaInfo.Location = New System.Drawing.Point(85, 12)
            Me.lblPaginaInfo.Name = "lblPaginaInfo"
            Me.lblPaginaInfo.Size = New System.Drawing.Size(320, 20)
            Me.lblPaginaInfo.TabIndex = 2
            Me.lblPaginaInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnAnterior
            '
            Me.btnAnterior.Location = New System.Drawing.Point(46, 7)
            Me.btnAnterior.Name = "btnAnterior"
            Me.btnAnterior.Size = New System.Drawing.Size(32, 26)
            Me.btnAnterior.TabIndex = 1
            Me.btnAnterior.Text = "<"
            Me.btnAnterior.UseVisualStyleBackColor = True
            '
            'btnPrimeira
            '
            Me.btnPrimeira.Location = New System.Drawing.Point(10, 7)
            Me.btnPrimeira.Name = "btnPrimeira"
            Me.btnPrimeira.Size = New System.Drawing.Size(32, 26)
            Me.btnPrimeira.TabIndex = 0
            Me.btnPrimeira.Text = "|<"
            Me.btnPrimeira.UseVisualStyleBackColor = True
            '
            'pnlAcoes
            '
            Me.pnlAcoes.Controls.Add(Me.btnRelatorioTotais)
            Me.pnlAcoes.Controls.Add(Me.btnExportarUltimoMes)
            Me.pnlAcoes.Controls.Add(Me.btnExcluir)
            Me.pnlAcoes.Controls.Add(Me.btnEditar)
            Me.pnlAcoes.Controls.Add(Me.btnNovo)
            Me.pnlAcoes.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlAcoes.Location = New System.Drawing.Point(0, 80)
            Me.pnlAcoes.Name = "pnlAcoes"
            Me.pnlAcoes.Size = New System.Drawing.Size(1050, 42)
            Me.pnlAcoes.TabIndex = 1
            '
            'btnRelatorioTotais
            '
            Me.btnRelatorioTotais.Location = New System.Drawing.Point(510, 6)
            Me.btnRelatorioTotais.Name = "btnRelatorioTotais"
            Me.btnRelatorioTotais.Size = New System.Drawing.Size(150, 28)
            Me.btnRelatorioTotais.TabIndex = 4
            Me.btnRelatorioTotais.Text = "Relatório de Totais"
            Me.btnRelatorioTotais.UseVisualStyleBackColor = True
            '
            'btnExportarUltimoMes
            '
            Me.btnExportarUltimoMes.Location = New System.Drawing.Point(330, 6)
            Me.btnExportarUltimoMes.Name = "btnExportarUltimoMes"
            Me.btnExportarUltimoMes.Size = New System.Drawing.Size(170, 28)
            Me.btnExportarUltimoMes.TabIndex = 3
            Me.btnExportarUltimoMes.Text = "Exportar Último Mês"
            Me.btnExportarUltimoMes.UseVisualStyleBackColor = True
            '
            'btnExcluir
            '
            Me.btnExcluir.Location = New System.Drawing.Point(210, 6)
            Me.btnExcluir.Name = "btnExcluir"
            Me.btnExcluir.Size = New System.Drawing.Size(90, 28)
            Me.btnExcluir.TabIndex = 2
            Me.btnExcluir.Text = "Excluir"
            Me.btnExcluir.UseVisualStyleBackColor = True
            '
            'btnEditar
            '
            Me.btnEditar.Location = New System.Drawing.Point(110, 6)
            Me.btnEditar.Name = "btnEditar"
            Me.btnEditar.Size = New System.Drawing.Size(90, 28)
            Me.btnEditar.TabIndex = 1
            Me.btnEditar.Text = "Editar"
            Me.btnEditar.UseVisualStyleBackColor = True
            '
            'btnNovo
            '
            Me.btnNovo.Location = New System.Drawing.Point(10, 6)
            Me.btnNovo.Name = "btnNovo"
            Me.btnNovo.Size = New System.Drawing.Size(90, 28)
            Me.btnNovo.TabIndex = 0
            Me.btnNovo.Text = "Novo"
            Me.btnNovo.UseVisualStyleBackColor = True
            '
            'pnlFiltros
            '
            Me.pnlFiltros.Controls.Add(Me.btnLimparFiltros)
            Me.pnlFiltros.Controls.Add(Me.btnFiltrar)
            Me.pnlFiltros.Controls.Add(Me.dtpDataFinal)
            Me.pnlFiltros.Controls.Add(Me.chkDataFinal)
            Me.pnlFiltros.Controls.Add(Me.dtpDataInicial)
            Me.pnlFiltros.Controls.Add(Me.chkDataInicial)
            Me.pnlFiltros.Controls.Add(Me.txtValorMaximo)
            Me.pnlFiltros.Controls.Add(Me.lblValorMax)
            Me.pnlFiltros.Controls.Add(Me.txtValorMinimo)
            Me.pnlFiltros.Controls.Add(Me.lblValorMin)
            Me.pnlFiltros.Controls.Add(Me.cboStatus)
            Me.pnlFiltros.Controls.Add(Me.lblStatus)
            Me.pnlFiltros.Controls.Add(Me.txtNumeroCartao)
            Me.pnlFiltros.Controls.Add(Me.lblCartao)
            Me.pnlFiltros.Controls.Add(Me.txtId)
            Me.pnlFiltros.Controls.Add(Me.lblId)
            Me.pnlFiltros.Controls.Add(Me.cboCategoriaFiltro)
            Me.pnlFiltros.Controls.Add(Me.lblCategoriaFiltro)
            Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltros.Location = New System.Drawing.Point(0, 0)
            Me.pnlFiltros.Name = "pnlFiltros"
            Me.pnlFiltros.Size = New System.Drawing.Size(1050, 80)
            Me.pnlFiltros.TabIndex = 0
            '
            'btnLimparFiltros
            '
            Me.btnLimparFiltros.Location = New System.Drawing.Point(893, 4)
            Me.btnLimparFiltros.Name = "btnLimparFiltros"
            Me.btnLimparFiltros.Size = New System.Drawing.Size(110, 24)
            Me.btnLimparFiltros.TabIndex = 13
            Me.btnLimparFiltros.Text = "Limpar Filtros"
            Me.btnLimparFiltros.UseVisualStyleBackColor = True
            '
            'btnFiltrar
            '
            Me.btnFiltrar.Location = New System.Drawing.Point(803, 4)
            Me.btnFiltrar.Name = "btnFiltrar"
            Me.btnFiltrar.Size = New System.Drawing.Size(85, 24)
            Me.btnFiltrar.TabIndex = 12
            Me.btnFiltrar.Text = "Filtrar"
            Me.btnFiltrar.UseVisualStyleBackColor = True
            '
            'dtpDataFinal
            '
            Me.dtpDataFinal.Enabled = False
            Me.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpDataFinal.Location = New System.Drawing.Point(315, 41)
            Me.dtpDataFinal.Name = "dtpDataFinal"
            Me.dtpDataFinal.Size = New System.Drawing.Size(110, 20)
            Me.dtpDataFinal.TabIndex = 11
            '
            'chkDataFinal
            '
            Me.chkDataFinal.Location = New System.Drawing.Point(230, 44)
            Me.chkDataFinal.Name = "chkDataFinal"
            Me.chkDataFinal.Size = New System.Drawing.Size(85, 20)
            Me.chkDataFinal.TabIndex = 10
            Me.chkDataFinal.Text = "Data final:"
            Me.chkDataFinal.UseVisualStyleBackColor = True
            '
            'cboCategoriaFiltro
            '
            Me.cboCategoriaFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCategoriaFiltro.FormattingEnabled = True
            Me.cboCategoriaFiltro.Location = New System.Drawing.Point(520, 41)
            Me.cboCategoriaFiltro.Name = "cboCategoriaFiltro"
            Me.cboCategoriaFiltro.Size = New System.Drawing.Size(110, 21)
            Me.cboCategoriaFiltro.TabIndex = 16
            '
            'lblCategoriaFiltro
            '
            Me.lblCategoriaFiltro.Location = New System.Drawing.Point(450, 44)
            Me.lblCategoriaFiltro.Name = "lblCategoriaFiltro"
            Me.lblCategoriaFiltro.Size = New System.Drawing.Size(65, 20)
            Me.lblCategoriaFiltro.TabIndex = 17
            Me.lblCategoriaFiltro.Text = "Categoria:"
            '
            'dtpDataInicial
            '
            Me.dtpDataInicial.Enabled = False
            Me.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpDataInicial.Location = New System.Drawing.Point(105, 41)
            Me.dtpDataInicial.Name = "dtpDataInicial"
            Me.dtpDataInicial.Size = New System.Drawing.Size(110, 20)
            Me.dtpDataInicial.TabIndex = 9
            '
            'chkDataInicial
            '
            Me.chkDataInicial.Location = New System.Drawing.Point(10, 44)
            Me.chkDataInicial.Name = "chkDataInicial"
            Me.chkDataInicial.Size = New System.Drawing.Size(95, 20)
            Me.chkDataInicial.TabIndex = 8
            Me.chkDataInicial.Text = "Data inicial:"
            Me.chkDataInicial.UseVisualStyleBackColor = True
            '
            'txtValorMaximo
            '
            Me.txtValorMaximo.Location = New System.Drawing.Point(718, 6)
            Me.txtValorMaximo.Name = "txtValorMaximo"
            Me.txtValorMaximo.Size = New System.Drawing.Size(75, 20)
            Me.txtValorMaximo.TabIndex = 7
            '
            'lblValorMax
            '
            Me.lblValorMax.Location = New System.Drawing.Point(653, 9)
            Me.lblValorMax.Name = "lblValorMax"
            Me.lblValorMax.Size = New System.Drawing.Size(60, 20)
            Me.lblValorMax.TabIndex = 6
            Me.lblValorMax.Text = "Valor máx.:"
            '
            'txtValorMinimo
            '
            Me.txtValorMinimo.Location = New System.Drawing.Point(568, 6)
            Me.txtValorMinimo.Name = "txtValorMinimo"
            Me.txtValorMinimo.Size = New System.Drawing.Size(75, 20)
            Me.txtValorMinimo.TabIndex = 5
            '
            'lblValorMin
            '
            Me.lblValorMin.Location = New System.Drawing.Point(503, 9)
            Me.lblValorMin.Name = "lblValorMin"
            Me.lblValorMin.Size = New System.Drawing.Size(60, 20)
            Me.lblValorMin.TabIndex = 4
            Me.lblValorMin.Text = "Valor mín.:"
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Location = New System.Drawing.Point(368, 6)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(120, 21)
            Me.cboStatus.TabIndex = 3
            '
            'lblStatus
            '
            Me.lblStatus.Location = New System.Drawing.Point(323, 9)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(45, 20)
            Me.lblStatus.TabIndex = 2
            Me.lblStatus.Text = "Status:"
            '
            'txtNumeroCartao
            '
            Me.txtNumeroCartao.Location = New System.Drawing.Point(168, 6)
            Me.txtNumeroCartao.MaxLength = 19
            Me.txtNumeroCartao.Name = "txtNumeroCartao"
            Me.txtNumeroCartao.Size = New System.Drawing.Size(140, 20)
            Me.txtNumeroCartao.TabIndex = 1
            '
            'lblCartao
            '
            Me.lblCartao.Location = New System.Drawing.Point(118, 9)
            Me.lblCartao.Name = "lblCartao"
            Me.lblCartao.Size = New System.Drawing.Size(50, 20)
            Me.lblCartao.TabIndex = 0
            Me.lblCartao.Text = "Cartão:"
            '
            'txtId
            '
            Me.txtId.Location = New System.Drawing.Point(38, 6)
            Me.txtId.MaxLength = 9
            Me.txtId.Name = "txtId"
            Me.txtId.Size = New System.Drawing.Size(70, 20)
            Me.txtId.TabIndex = 0
            '
            'lblId
            '
            Me.lblId.Location = New System.Drawing.Point(10, 9)
            Me.lblId.Name = "lblId"
            Me.lblId.Size = New System.Drawing.Size(25, 20)
            Me.lblId.TabIndex = 15
            Me.lblId.Text = "Id:"
            '
            'chkMascararCartao
            '
            Me.chkMascararCartao.Checked = True
            Me.chkMascararCartao.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkMascararCartao.Dock = System.Windows.Forms.DockStyle.Top
            Me.chkMascararCartao.Location = New System.Drawing.Point(0, 0)
            Me.chkMascararCartao.Name = "chkMascararCartao"
            Me.chkMascararCartao.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
            Me.chkMascararCartao.Size = New System.Drawing.Size(1050, 26)
            Me.chkMascararCartao.TabIndex = 20
            Me.chkMascararCartao.Text = "Mascarar número do cartão"
            Me.chkMascararCartao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.chkMascararCartao.UseVisualStyleBackColor = True
            '
            'FrmConsultaTransacoes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1050, 706)
            Me.Controls.Add(Me.dgvTransacoes)
            Me.Controls.Add(Me.pnlPaginacao)
            Me.Controls.Add(Me.pnlAcoes)
            Me.Controls.Add(Me.pnlFiltros)
            Me.Controls.Add(Me.chkMascararCartao)
            Me.MinimumSize = New System.Drawing.Size(1030, 526)
            Me.Name = "FrmConsultaTransacoes"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "XYZ Administradora de Cartões de Crédito - Transações"
            CType(Me.dgvTransacoes, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlPaginacao.ResumeLayout(False)
            Me.pnlAcoes.ResumeLayout(False)
            Me.pnlFiltros.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents dgvTransacoes As System.Windows.Forms.DataGridView
        Friend WithEvents colId As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCartao As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colValor As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCategoria As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colData As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colDescricao As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents pnlFiltros As System.Windows.Forms.Panel
        Friend WithEvents lblId As System.Windows.Forms.Label
        Friend WithEvents txtId As System.Windows.Forms.TextBox
        Friend WithEvents lblCartao As System.Windows.Forms.Label
        Friend WithEvents txtNumeroCartao As System.Windows.Forms.TextBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblValorMin As System.Windows.Forms.Label
        Friend WithEvents txtValorMinimo As System.Windows.Forms.TextBox
        Friend WithEvents lblValorMax As System.Windows.Forms.Label
        Friend WithEvents txtValorMaximo As System.Windows.Forms.TextBox
        Friend WithEvents chkDataInicial As System.Windows.Forms.CheckBox
        Friend WithEvents dtpDataInicial As System.Windows.Forms.DateTimePicker
        Friend WithEvents chkDataFinal As System.Windows.Forms.CheckBox
        Friend WithEvents dtpDataFinal As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblCategoriaFiltro As System.Windows.Forms.Label
        Friend WithEvents cboCategoriaFiltro As System.Windows.Forms.ComboBox
        Friend WithEvents btnFiltrar As System.Windows.Forms.Button
        Friend WithEvents btnLimparFiltros As System.Windows.Forms.Button
        Friend WithEvents pnlAcoes As System.Windows.Forms.Panel
        Friend WithEvents btnNovo As System.Windows.Forms.Button
        Friend WithEvents btnEditar As System.Windows.Forms.Button
        Friend WithEvents btnExcluir As System.Windows.Forms.Button
        Friend WithEvents btnExportarUltimoMes As System.Windows.Forms.Button
        Friend WithEvents btnRelatorioTotais As System.Windows.Forms.Button
        Friend WithEvents pnlPaginacao As System.Windows.Forms.Panel
        Friend WithEvents btnPrimeira As System.Windows.Forms.Button
        Friend WithEvents btnAnterior As System.Windows.Forms.Button
        Friend WithEvents lblPaginaInfo As System.Windows.Forms.Label
        Friend WithEvents btnProxima As System.Windows.Forms.Button
        Friend WithEvents btnUltima As System.Windows.Forms.Button
        Friend WithEvents lblTamanho As System.Windows.Forms.Label
        Friend WithEvents cboTamanhoPagina As System.Windows.Forms.ComboBox
        Friend WithEvents chkMascararCartao As System.Windows.Forms.CheckBox

    End Class

End Namespace
