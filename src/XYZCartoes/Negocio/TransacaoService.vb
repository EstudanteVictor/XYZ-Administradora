Imports XYZCartoes.Dados
Imports XYZCartoes.Dominio

Namespace Negocio

    Public Class TransacaoService

        Private ReadOnly _repositorio As ITransacaoRepositorio

        Public Sub New(repositorio As ITransacaoRepositorio)
            _repositorio = repositorio
        End Sub

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="transacao"></param>
        ''' <returns></returns>
        Public Function CadastrarTransacao(transacao As Transacao) As Integer
            ValidarOuLancar(transacao)
            Return _repositorio.Inserir(transacao)
        End Function

        ''' <summary>
        ''' Repete a checagem de status 'Aprovada' já feita na UI: protege contra uma grid desatualizada (a transação pode ter sido aprovada por outra sessão entre a listagem e o salvamento).
        ''' </summary>
        ''' <param name="transacao"></param>
        Public Sub AtualizarTransacao(transacao As Transacao)
            Dim atual As Transacao = _repositorio.ObterPorId(transacao.IdTransacao)
            If atual Is Nothing Then
                Throw New RegraNegocioException("Transação não encontrada. Ela pode ter sido excluída por outro usuário.")
            End If
            If atual.StatusTransacao = StatusTransacao.Aprovada Then
                Throw New RegraNegocioException("Não é possível editar uma transação com status 'Aprovada'.")
            End If

            ValidarOuLancar(transacao)
            _repositorio.Atualizar(transacao)
        End Sub

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="idTransacao"></param>
        Public Sub ExcluirTransacao(idTransacao As Integer)
            _repositorio.Excluir(idTransacao)
        End Sub

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="idTransacao"></param>
        ''' <returns></returns>
        Public Function ObterPorId(idTransacao As Integer) As Transacao
            Return _repositorio.ObterPorId(idTransacao)
        End Function

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="filtro"></param>
        ''' <returns></returns>
        Public Function ConsultarTransacoes(filtro As FiltroConsultaTransacoes) As ResultadoPaginado(Of Transacao)
            Return _repositorio.ConsultarPaginado(filtro)
        End Function

        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Function ObterTransacoesUltimoMes() As List(Of Transacao)
            Dim periodo = ObterPeriodoUltimoMes()
            Return _repositorio.ObterTransacoesPorPeriodo(periodo.Inicio, periodo.Fim)
        End Function
        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="dataInicial"></param>
        ''' <param name="dataFinal"></param>
        ''' <param name="status"></param>
        ''' <returns></returns>
        Public Function ObterTotalPorPeriodo(dataInicial As Date, dataFinal As Date, status As StatusTransacao?) As List(Of TotalTransacaoPorCartao)
            Return _repositorio.ObterTotalPorPeriodo(dataInicial, dataFinal, status)
        End Function

        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObterPeriodoUltimoMes() As (Inicio As Date, Fim As Date)
            Dim primeiroDiaMesAtual As New Date(Date.Today.Year, Date.Today.Month, 1)
            Dim primeiroDiaMesAnterior As Date = primeiroDiaMesAtual.AddMonths(-1)
            Dim fimDoMesAnterior As Date = primeiroDiaMesAtual.AddSeconds(-1)
            Return (primeiroDiaMesAnterior, fimDoMesAnterior)
        End Function

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="transacao"></param>
        Private Shared Sub ValidarOuLancar(transacao As Transacao)
            Dim erros As List(Of String) = ValidadorTransacao.Validar(transacao)
            If erros.Count > 0 Then
                Throw New ValidacaoException(erros)
            End If
        End Sub

    End Class

End Namespace
