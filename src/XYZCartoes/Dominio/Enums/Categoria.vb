Namespace Dominio

    ''' <summary>Espelha as faixas de valor de fn_CategoriaTransacao no banco de dados.</summary>
    Public Enum Categoria
        Baixa
        Media
        Alta
        Premium
    End Enum

    ''' <summary>Conversão entre o enum e o texto gravado/devolvido pelo banco ("Média" tem acento, o enum não).</summary>
    Public Module CategoriaConversor

        Public Function ParaTexto(categoria As Categoria) As String
            If categoria = Categoria.Media Then
                Return "Média"
            End If
            Return categoria.ToString()
        End Function

        Public Function ParaEnum(texto As String) As Categoria
            If String.Equals(texto, "Média", StringComparison.OrdinalIgnoreCase) Then
                Return Categoria.Media
            End If
            Return CType([Enum].Parse(GetType(Categoria), texto, ignoreCase:=True), Categoria)
        End Function

    End Module

End Namespace
