Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmTransacaoCadastro
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
            Me.lblCartao = New System.Windows.Forms.Label()
            Me.txtNumeroCartao = New System.Windows.Forms.TextBox()
            Me.btnValidarCartao = New System.Windows.Forms.Button()
            Me.lblValor = New System.Windows.Forms.Label()
            Me.txtValor = New System.Windows.Forms.TextBox()
            Me.lblDataRotulo = New System.Windows.Forms.Label()
            Me.lblDataValor = New System.Windows.Forms.Label()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblDescricao = New System.Windows.Forms.Label()
            Me.txtDescricao = New System.Windows.Forms.TextBox()
            Me.lblContadorDescricao = New System.Windows.Forms.Label()
            Me.btnSalvar = New System.Windows.Forms.Button()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'lblCartao
            '
            Me.lblCartao.Location = New System.Drawing.Point(15, 20)
            Me.lblCartao.Name = "lblCartao"
            Me.lblCartao.Size = New System.Drawing.Size(140, 20)
            Me.lblCartao.TabIndex = 0
            Me.lblCartao.Text = "Número do Cartão:"
            '
            'txtNumeroCartao
            '
            Me.txtNumeroCartao.Location = New System.Drawing.Point(165, 17)
            Me.txtNumeroCartao.MaxLength = 16
            Me.txtNumeroCartao.Name = "txtNumeroCartao"
            Me.txtNumeroCartao.Size = New System.Drawing.Size(150, 20)
            Me.txtNumeroCartao.TabIndex = 1
            '
            'btnValidarCartao
            '
            Me.btnValidarCartao.Location = New System.Drawing.Point(320, 16)
            Me.btnValidarCartao.Name = "btnValidarCartao"
            Me.btnValidarCartao.Size = New System.Drawing.Size(85, 23)
            Me.btnValidarCartao.TabIndex = 2
            Me.btnValidarCartao.Text = "Validar"
            Me.btnValidarCartao.UseVisualStyleBackColor = True
            '
            'lblValor
            '
            Me.lblValor.Location = New System.Drawing.Point(15, 55)
            Me.lblValor.Name = "lblValor"
            Me.lblValor.Size = New System.Drawing.Size(140, 20)
            Me.lblValor.TabIndex = 3
            Me.lblValor.Text = "Valor (R$):"
            '
            'txtValor
            '
            Me.txtValor.ForeColor = System.Drawing.SystemColors.WindowText
            Me.txtValor.Location = New System.Drawing.Point(165, 52)
            Me.txtValor.Name = "txtValor"
            Me.txtValor.Size = New System.Drawing.Size(100, 20)
            Me.txtValor.TabIndex = 4
            '
            'lblDataRotulo
            '
            Me.lblDataRotulo.Location = New System.Drawing.Point(15, 90)
            Me.lblDataRotulo.Name = "lblDataRotulo"
            Me.lblDataRotulo.Size = New System.Drawing.Size(140, 20)
            Me.lblDataRotulo.TabIndex = 5
            Me.lblDataRotulo.Text = "Data:"
            '
            'lblDataValor
            '
            Me.lblDataValor.ForeColor = System.Drawing.SystemColors.GrayText
            Me.lblDataValor.Location = New System.Drawing.Point(165, 90)
            Me.lblDataValor.Name = "lblDataValor"
            Me.lblDataValor.Size = New System.Drawing.Size(240, 20)
            Me.lblDataValor.TabIndex = 6
            '
            'lblStatus
            '
            Me.lblStatus.Location = New System.Drawing.Point(15, 125)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(140, 20)
            Me.lblStatus.TabIndex = 7
            Me.lblStatus.Text = "Status:"
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Location = New System.Drawing.Point(165, 122)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(150, 21)
            Me.cboStatus.TabIndex = 8
            '
            'lblDescricao
            '
            Me.lblDescricao.Location = New System.Drawing.Point(15, 160)
            Me.lblDescricao.Name = "lblDescricao"
            Me.lblDescricao.Size = New System.Drawing.Size(140, 20)
            Me.lblDescricao.TabIndex = 9
            Me.lblDescricao.Text = "Descrição:"
            '
            'txtDescricao
            '
            Me.txtDescricao.Location = New System.Drawing.Point(15, 180)
            Me.txtDescricao.MaxLength = 255
            Me.txtDescricao.Multiline = True
            Me.txtDescricao.Name = "txtDescricao"
            Me.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtDescricao.Size = New System.Drawing.Size(390, 70)
            Me.txtDescricao.TabIndex = 10
            '
            'lblContadorDescricao
            '
            Me.lblContadorDescricao.Font = New System.Drawing.Font("Segoe UI", 8.0!)
            Me.lblContadorDescricao.ForeColor = System.Drawing.SystemColors.GrayText
            Me.lblContadorDescricao.Location = New System.Drawing.Point(330, 253)
            Me.lblContadorDescricao.Name = "lblContadorDescricao"
            Me.lblContadorDescricao.Size = New System.Drawing.Size(75, 15)
            Me.lblContadorDescricao.TabIndex = 11
            Me.lblContadorDescricao.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'btnSalvar
            '
            Me.btnSalvar.Location = New System.Drawing.Point(220, 330)
            Me.btnSalvar.Name = "btnSalvar"
            Me.btnSalvar.Size = New System.Drawing.Size(90, 28)
            Me.btnSalvar.TabIndex = 12
            Me.btnSalvar.Text = "Salvar"
            Me.btnSalvar.UseVisualStyleBackColor = True
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Location = New System.Drawing.Point(320, 330)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(90, 28)
            Me.btnCancelar.TabIndex = 13
            Me.btnCancelar.Text = "Cancelar"
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'FrmTransacaoCadastro
            '
            Me.AcceptButton = Me.btnSalvar
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.btnCancelar
            Me.ClientSize = New System.Drawing.Size(430, 400)
            Me.Controls.Add(Me.lblCartao)
            Me.Controls.Add(Me.txtNumeroCartao)
            Me.Controls.Add(Me.btnValidarCartao)
            Me.Controls.Add(Me.lblValor)
            Me.Controls.Add(Me.txtValor)
            Me.Controls.Add(Me.lblDataRotulo)
            Me.Controls.Add(Me.lblDataValor)
            Me.Controls.Add(Me.lblStatus)
            Me.Controls.Add(Me.cboStatus)
            Me.Controls.Add(Me.lblDescricao)
            Me.Controls.Add(Me.txtDescricao)
            Me.Controls.Add(Me.lblContadorDescricao)
            Me.Controls.Add(Me.btnSalvar)
            Me.Controls.Add(Me.btnCancelar)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmTransacaoCadastro"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "FrmTransacaoCadastro"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents lblCartao As System.Windows.Forms.Label
        Friend WithEvents txtNumeroCartao As System.Windows.Forms.TextBox
        Friend WithEvents btnValidarCartao As System.Windows.Forms.Button
        Friend WithEvents lblValor As System.Windows.Forms.Label
        Friend WithEvents txtValor As System.Windows.Forms.TextBox
        Friend WithEvents lblDataRotulo As System.Windows.Forms.Label
        Friend WithEvents lblDataValor As System.Windows.Forms.Label
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblDescricao As System.Windows.Forms.Label
        Friend WithEvents txtDescricao As System.Windows.Forms.TextBox
        Friend WithEvents lblContadorDescricao As System.Windows.Forms.Label
        Friend WithEvents btnSalvar As System.Windows.Forms.Button
        Friend WithEvents btnCancelar As System.Windows.Forms.Button

    End Class

End Namespace
