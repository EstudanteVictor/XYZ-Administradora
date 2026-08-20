Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmRelatorioTotais
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
            Me.pnlFiltros = New System.Windows.Forms.Panel()
            Me.btnBuscar = New System.Windows.Forms.Button()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.dtpDataFinal = New System.Windows.Forms.DateTimePicker()
            Me.lblAte = New System.Windows.Forms.Label()
            Me.dtpDataInicial = New System.Windows.Forms.DateTimePicker()
            Me.lblDe = New System.Windows.Forms.Label()
            Me.pnlResumo = New System.Windows.Forms.Panel()
            Me.lblResumo = New System.Windows.Forms.Label()
            Me.dgvTotais = New System.Windows.Forms.DataGridView()
            Me.colCartao = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colValorTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colQuantidade = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlFiltros.SuspendLayout()
            Me.pnlResumo.SuspendLayout()
            CType(Me.dgvTotais, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'pnlFiltros
            '
            Me.pnlFiltros.Controls.Add(Me.btnBuscar)
            Me.pnlFiltros.Controls.Add(Me.cboStatus)
            Me.pnlFiltros.Controls.Add(Me.lblStatus)
            Me.pnlFiltros.Controls.Add(Me.dtpDataFinal)
            Me.pnlFiltros.Controls.Add(Me.lblAte)
            Me.pnlFiltros.Controls.Add(Me.dtpDataInicial)
            Me.pnlFiltros.Controls.Add(Me.lblDe)
            Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltros.Location = New System.Drawing.Point(0, 0)
            Me.pnlFiltros.Name = "pnlFiltros"
            Me.pnlFiltros.Size = New System.Drawing.Size(720, 50)
            Me.pnlFiltros.TabIndex = 0
            '
            'btnBuscar
            '
            Me.btnBuscar.Location = New System.Drawing.Point(550, 10)
            Me.btnBuscar.Name = "btnBuscar"
            Me.btnBuscar.Size = New System.Drawing.Size(90, 26)
            Me.btnBuscar.TabIndex = 6
            Me.btnBuscar.Text = "Buscar"
            Me.btnBuscar.UseVisualStyleBackColor = True
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Location = New System.Drawing.Point(415, 12)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(120, 21)
            Me.cboStatus.TabIndex = 5
            '
            'lblStatus
            '
            Me.lblStatus.Location = New System.Drawing.Point(365, 16)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(45, 20)
            Me.lblStatus.TabIndex = 4
            Me.lblStatus.Text = "Status:"
            '
            'dtpDataFinal
            '
            Me.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpDataFinal.Location = New System.Drawing.Point(220, 12)
            Me.dtpDataFinal.Name = "dtpDataFinal"
            Me.dtpDataFinal.Size = New System.Drawing.Size(130, 20)
            Me.dtpDataFinal.TabIndex = 3
            '
            'lblAte
            '
            Me.lblAte.Location = New System.Drawing.Point(185, 16)
            Me.lblAte.Name = "lblAte"
            Me.lblAte.Size = New System.Drawing.Size(30, 20)
            Me.lblAte.TabIndex = 2
            Me.lblAte.Text = "Até:"
            '
            'dtpDataInicial
            '
            Me.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpDataInicial.Location = New System.Drawing.Point(40, 12)
            Me.dtpDataInicial.Name = "dtpDataInicial"
            Me.dtpDataInicial.Size = New System.Drawing.Size(130, 20)
            Me.dtpDataInicial.TabIndex = 1
            '
            'lblDe
            '
            Me.lblDe.Location = New System.Drawing.Point(10, 16)
            Me.lblDe.Name = "lblDe"
            Me.lblDe.Size = New System.Drawing.Size(25, 20)
            Me.lblDe.TabIndex = 0
            Me.lblDe.Text = "De:"
            '
            'pnlResumo
            '
            Me.pnlResumo.Controls.Add(Me.lblResumo)
            Me.pnlResumo.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlResumo.Location = New System.Drawing.Point(0, 470)
            Me.pnlResumo.Name = "pnlResumo"
            Me.pnlResumo.Size = New System.Drawing.Size(720, 30)
            Me.pnlResumo.TabIndex = 1
            '
            'lblResumo
            '
            Me.lblResumo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblResumo.Location = New System.Drawing.Point(0, 0)
            Me.lblResumo.Name = "lblResumo"
            Me.lblResumo.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
            Me.lblResumo.Size = New System.Drawing.Size(720, 30)
            Me.lblResumo.TabIndex = 0
            Me.lblResumo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'dgvTotais
            '
            Me.dgvTotais.AllowUserToAddRows = False
            Me.dgvTotais.AllowUserToDeleteRows = False
            Me.dgvTotais.AllowUserToOrderColumns = True
            Me.dgvTotais.AutoGenerateColumns = False
            Me.dgvTotais.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCartao, Me.colValorTotal, Me.colQuantidade, Me.colStatus})
            Me.dgvTotais.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvTotais.Location = New System.Drawing.Point(0, 50)
            Me.dgvTotais.MultiSelect = False
            Me.dgvTotais.Name = "dgvTotais"
            Me.dgvTotais.ReadOnly = True
            Me.dgvTotais.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvTotais.Size = New System.Drawing.Size(720, 420)
            Me.dgvTotais.TabIndex = 2
            '
            'colCartao
            '
            Me.colCartao.HeaderText = "Número do Cartão"
            Me.colCartao.Name = "colCartao"
            Me.colCartao.ReadOnly = True
            Me.colCartao.Width = 200
            '
            'colValorTotal
            '
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
            DataGridViewCellStyle1.Format = "C2"
            DataGridViewCellStyle1.FormatProvider = New System.Globalization.CultureInfo("pt-BR")
            Me.colValorTotal.DefaultCellStyle = DataGridViewCellStyle1
            Me.colValorTotal.HeaderText = "Valor Total"
            Me.colValorTotal.Name = "colValorTotal"
            Me.colValorTotal.ReadOnly = True
            Me.colValorTotal.Width = 130
            '
            'colQuantidade
            '
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
            Me.colQuantidade.DefaultCellStyle = DataGridViewCellStyle2
            Me.colQuantidade.HeaderText = "Quantidade"
            Me.colQuantidade.Name = "colQuantidade"
            Me.colQuantidade.ReadOnly = True
            Me.colQuantidade.Width = 100
            '
            'colStatus
            '
            Me.colStatus.HeaderText = "Status"
            Me.colStatus.Name = "colStatus"
            Me.colStatus.ReadOnly = True
            Me.colStatus.Width = 120
            '
            'FrmRelatorioTotais
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(720, 500)
            Me.Controls.Add(Me.dgvTotais)
            Me.Controls.Add(Me.pnlResumo)
            Me.Controls.Add(Me.pnlFiltros)
            Me.MinimumSize = New System.Drawing.Size(600, 350)
            Me.Name = "FrmRelatorioTotais"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Relatório de Totais por Período"
            Me.pnlFiltros.ResumeLayout(False)
            Me.pnlResumo.ResumeLayout(False)
            CType(Me.dgvTotais, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlFiltros As System.Windows.Forms.Panel
        Friend WithEvents lblDe As System.Windows.Forms.Label
        Friend WithEvents dtpDataInicial As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblAte As System.Windows.Forms.Label
        Friend WithEvents dtpDataFinal As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents btnBuscar As System.Windows.Forms.Button
        Friend WithEvents pnlResumo As System.Windows.Forms.Panel
        Friend WithEvents lblResumo As System.Windows.Forms.Label
        Friend WithEvents dgvTotais As System.Windows.Forms.DataGridView
        Friend WithEvents colCartao As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colValorTotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colQuantidade As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn

    End Class

End Namespace
