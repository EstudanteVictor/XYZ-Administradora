Imports System.Windows.Forms
Imports XYZCartoes.Dados
Imports XYZCartoes.Forms
Imports XYZCartoes.Negocio
Imports XYZCartoes.Utilitarios

Friend Module Program

    <STAThread>
    Sub Main()
        AddHandler Application.ThreadException, AddressOf TratarExcecaoThread
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf TratarExcecaoDominio
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim repositorio As New TransacaoRepositorio()
        Dim service As New TransacaoService(repositorio)

        Application.Run(New FrmConsultaTransacoes(service))
    End Sub

    Private Sub TratarExcecaoThread(sender As Object, e As Threading.ThreadExceptionEventArgs)
        Logger.RegistrarErro(e.Exception, "Exceção não tratada (thread de UI)")
        MessageBox.Show("Ocorreu um erro inesperado. Os detalhes foram registrados no log da aplicação.",
                         "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub TratarExcecaoDominio(sender As Object, e As UnhandledExceptionEventArgs)
        Dim excecao As Exception = TryCast(e.ExceptionObject, Exception)
        If excecao IsNot Nothing Then
            Logger.RegistrarErro(excecao, "Exceção não tratada (AppDomain)")
        End If
        MessageBox.Show("Ocorreu um erro grave e a aplicação será encerrada. Os detalhes foram registrados no log.",
                         "Erro fatal", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Module
