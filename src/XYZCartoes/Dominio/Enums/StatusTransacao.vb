Namespace Dominio

    Public Enum StatusTransacao
        Pendente
        Aprovada
        Cancelada
    End Enum

    ''' <summary>Conversão entre o enum usado na aplicação e o VARCHAR gravado no banco.</summary>
    Public Module StatusTransacaoConversor

        Public Function ParaTexto(status As StatusTransacao) As String
            Return status.ToString()
        End Function

        Public Function ParaEnum(texto As String) As StatusTransacao
            Return CType([Enum].Parse(GetType(StatusTransacao), texto, ignoreCase:=True), StatusTransacao)
        End Function

    End Module

End Namespace
