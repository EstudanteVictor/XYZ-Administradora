Imports System.Globalization
Imports System.Windows.Forms
Imports XYZCartoes.Dominio
Imports XYZCartoes.Negocio
Imports XYZCartoes.Utilitarios

Namespace Forms

    Public Class FrmTransacaoCadastro

        Private Enum ModoFormulario
            Novo
            Edicao
        End Enum

        Private ReadOnly _service As TransacaoService
        Private ReadOnly _transacaoOriginal As Transacao
        Private _modoAtual As ModoFormulario

        Public Sub New()
            InitializeComponent()
        End Sub

        ''' <summary>O chamador é responsável por não passar uma transação com status 'Aprovada' — a edição
        ''' dela é barrada com um aviso antes de esta tela ser aberta (ver FrmConsultaTransacoes.btnEditar_Click).</summary>
        Public Sub New(service As TransacaoService, Optional transacaoExistente As Transacao = Nothing)
            Me.New()
            _service = service
            _transacaoOriginal = transacaoExistente

            cboStatus.DataSource = [Enum].GetValues(GetType(StatusTransacao))

            If _transacaoOriginal Is Nothing Then
                _modoAtual = ModoFormulario.Novo
                Me.Text = "Nova Transação"
                cboStatus.SelectedItem = StatusTransacao.Pendente
                lblDataValor.Text = "(gerada automaticamente ao salvar)"
            Else
                _modoAtual = ModoFormulario.Edicao
                Me.Text = "Editar Transação"
                txtNumeroCartao.Text = _transacaoOriginal.NumeroCartao
                txtValor.Text = _transacaoOriginal.ValorTransacao.ToString("N2", CultureInfo.CurrentCulture)
                txtDescricao.Text = _transacaoOriginal.Descricao
                cboStatus.SelectedItem = _transacaoOriginal.StatusTransacao
                lblDataValor.Text = _transacaoOriginal.DataTransacao.ToString("dd/MM/yyyy HH:mm:ss")
            End If

            AtualizarContadorDescricao()
        End Sub

        Private Sub txtNumeroCartao_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumeroCartao.KeyPress
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

        Private Sub txtValor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValor.KeyPress
            If Char.IsControl(e.KeyChar) OrElse Char.IsDigit(e.KeyChar) Then Return

            Dim separador As String = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
            If separador.Length = 1 AndAlso e.KeyChar = separador(0) AndAlso Not txtValor.Text.Contains(separador) Then
                Return
            End If

            e.Handled = True
        End Sub

        ''' <summary>Ao sair do campo (Tab ou clique em outro lugar), arredonda para exatamente 2 casas decimais.</summary>
        Private Sub txtValor_Leave(sender As Object, e As EventArgs) Handles txtValor.Leave
            Dim valor As Decimal
            If Decimal.TryParse(txtValor.Text, NumberStyles.Number, CultureInfo.CurrentCulture, valor) Then
                txtValor.Text = valor.ToString("N2", CultureInfo.CurrentCulture)
            End If
        End Sub

        Private Sub btnValidarCartao_Click(sender As Object, e As EventArgs) Handles btnValidarCartao.Click
            Dim numeroCartao As String = txtNumeroCartao.Text.Trim()

            If Not ValidadorTransacao.ValidarNumeroCartao(numeroCartao) Then
                MessageBox.Show("Informe os 16 dígitos do número do cartão antes de validar.", "Número incompleto",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If ValidadorTransacao.ValidarLuhn(numeroCartao) Then
                MessageBox.Show("Número de cartão válido.", "Validação de cartão",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Número de cartão inválido (não passa na validação de Luhn).", "Validação de cartão",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Private Sub txtDescricao_TextChanged(sender As Object, e As EventArgs) Handles txtDescricao.TextChanged
            AtualizarContadorDescricao()
        End Sub

        Private Sub AtualizarContadorDescricao()
            lblContadorDescricao.Text = $"{txtDescricao.Text.Length}/255"
        End Sub

        Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
            Dim valor As Decimal
            If Not Decimal.TryParse(txtValor.Text, NumberStyles.Number, CultureInfo.CurrentCulture, valor) Then
                MessageBox.Show("Informe um valor numérico válido para a transação.", "Valor inválido",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtValor.Focus()
                Return
            End If

            Dim transacao As New Transacao With {
                .IdTransacao = If(_transacaoOriginal IsNot Nothing, _transacaoOriginal.IdTransacao, 0),
                .NumeroCartao = txtNumeroCartao.Text.Trim(),
                .ValorTransacao = valor,
                .Descricao = txtDescricao.Text.Trim(),
                .StatusTransacao = CType(cboStatus.SelectedItem, StatusTransacao)
            }

            Try
                If _modoAtual = ModoFormulario.Novo Then
                    _service.CadastrarTransacao(transacao)
                Else
                    _service.AtualizarTransacao(transacao)
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As ValidacaoException
                MessageBox.Show(String.Join(Environment.NewLine, ex.Erros), "Não foi possível salvar",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Catch ex As RegraNegocioException
                MessageBox.Show(ex.Message, "Não foi possível salvar",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Catch ex As Exception
                Logger.RegistrarErro(ex, "Salvar transação")
                MessageBox.Show("Ocorreu um erro inesperado ao salvar a transação. Os detalhes foram registrados no log.",
                                 "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class

End Namespace
