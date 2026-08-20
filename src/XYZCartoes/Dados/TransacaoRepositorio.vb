Imports System.Data
Imports System.Data.SqlClient
Imports XYZCartoes.Dominio

Namespace Dados

    Public Class TransacaoRepositorio
        Implements ITransacaoRepositorio

        Public Function Inserir(transacao As Transacao) As Integer Implements ITransacaoRepositorio.Inserir
            Using conexao As SqlConnection = ConexaoFactory.ObterConexao()
                Using comando As New SqlCommand("dbo.sp_InserirTransacao", conexao)
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add("@Numero_Cartao", SqlDbType.Char, 16).Value = transacao.NumeroCartao
                    AdicionarParametroDecimal(comando, "@Valor_Transacao", transacao.ValorTransacao)
                    AdicionarParametroTextoOuNulo(comando, "@Descricao", SqlDbType.NVarChar, 255, transacao.Descricao)
                    comando.Parameters.Add("@Status_Transacao", SqlDbType.VarChar, 10).Value =
                        StatusTransacaoConversor.ParaTexto(transacao.StatusTransacao)

                    Dim paramId As SqlParameter = comando.Parameters.Add("@Id_Transacao", SqlDbType.Int)
                    paramId.Direction = ParameterDirection.Output

                    conexao.Open()
                    comando.ExecuteNonQuery()

                    Return CInt(paramId.Value)
                End Using
            End Using
        End Function

        Public Sub Atualizar(transacao As Transacao) Implements ITransacaoRepositorio.Atualizar
            Using conexao As SqlConnection = ConexaoFactory.ObterConexao()
                Using comando As New SqlCommand("dbo.sp_AtualizarTransacao", conexao)
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add("@Id_Transacao", SqlDbType.Int).Value = transacao.IdTransacao
                    comando.Parameters.Add("@Numero_Cartao", SqlDbType.Char, 16).Value = transacao.NumeroCartao
                    AdicionarParametroDecimal(comando, "@Valor_Transacao", transacao.ValorTransacao)
                    AdicionarParametroTextoOuNulo(comando, "@Descricao", SqlDbType.NVarChar, 255, transacao.Descricao)
                    comando.Parameters.Add("@Status_Transacao", SqlDbType.VarChar, 10).Value =
                        StatusTransacaoConversor.ParaTexto(transacao.StatusTransacao)

                    conexao.Open()
                    Try
                        comando.ExecuteNonQuery()
                    Catch ex As SqlException When ex.Number = 50001
                        Throw New RegraNegocioException("Transação não encontrada. Ela pode ter sido excluída por outro usuário.", ex)
                    Catch ex As SqlException When ex.Number = 50002
                        Throw New RegraNegocioException("Não é possível editar uma transação com status 'Aprovada'.", ex)
                    End Try
                End Using
            End Using
        End Sub

        Public Sub Excluir(idTransacao As Integer) Implements ITransacaoRepositorio.Excluir
            Using conexao As SqlConnection = ConexaoFactory.ObterConexao()
                Using comando As New SqlCommand("dbo.sp_ExcluirTransacao", conexao)
                    comando.CommandType = CommandType.StoredProcedure
                    comando.Parameters.Add("@Id_Transacao", SqlDbType.Int).Value = idTransacao

                    conexao.Open()
                    Try
                        comando.ExecuteNonQuery()
                    Catch ex As SqlException When ex.Number = 50001
                        Throw New RegraNegocioException("Transação não encontrada. Ela pode já ter sido excluída por outro usuário.", ex)
                    End Try
                End Using
            End Using
        End Sub

        Public Function ObterPorId(idTransacao As Integer) As Transacao Implements ITransacaoRepositorio.ObterPorId
            Using conexao As SqlConnection = ConexaoFactory.ObterConexao()
                Using comando As New SqlCommand("dbo.sp_ObterTransacaoPorId", conexao)
                    comando.CommandType = CommandType.StoredProcedure
                    comando.Parameters.Add("@Id_Transacao", SqlDbType.Int).Value = idTransacao

                    conexao.Open()
                    Using leitor As SqlDataReader = comando.ExecuteReader()
                        If leitor.Read() Then
                            Dim ordinais = TransacaoMapper.ObterOrdinais(leitor)
                            Return TransacaoMapper.Mapear(leitor, ordinais)
                        End If
                        Return Nothing
                    End Using
                End Using
            End Using
        End Function

        Public Function ConsultarPaginado(filtro As FiltroConsultaTransacoes) As ResultadoPaginado(Of Transacao) Implements ITransacaoRepositorio.ConsultarPaginado
            Using conexao As SqlConnection = ConexaoFactory.ObterConexao()
                Using comando As New SqlCommand("dbo.sp_ConsultarTransacoesPaginado", conexao)
                    comando.CommandType = CommandType.StoredProcedure

                    AdicionarParametroIntOuNulo(comando, "@Id_Transacao", filtro.Id)
                    AdicionarParametroTextoOuNulo(comando, "@Numero_Cartao", SqlDbType.Char, 16, filtro.NumeroCartao)
                    AdicionarParametroDataOuNulo(comando, "@Data_Inicial", filtro.DataInicial)
                    AdicionarParametroDataOuNulo(comando, "@Data_Final", filtro.DataFinal)
                    AdicionarParametroDecimalOuNulo(comando, "@Valor_Minimo", filtro.ValorMinimo)
                    AdicionarParametroDecimalOuNulo(comando, "@Valor_Maximo", filtro.ValorMaximo)
                    AdicionarParametroTextoOuNulo(comando, "@Status_Transacao", SqlDbType.VarChar, 10,
                        If(filtro.Status.HasValue, StatusTransacaoConversor.ParaTexto(filtro.Status.Value), Nothing))
                    AdicionarParametroTextoOuNulo(comando, "@Categoria", SqlDbType.VarChar, 10,
                        If(filtro.Categoria.HasValue, CategoriaConversor.ParaTexto(filtro.Categoria.Value), Nothing))
                    comando.Parameters.Add("@Ordenar_Por", SqlDbType.VarChar, 20).Value = filtro.OrdenarPor
                    comando.Parameters.Add("@Ordenar_Direcao", SqlDbType.VarChar, 4).Value = filtro.OrdenarDirecao
                    comando.Parameters.Add("@Numero_Pagina", SqlDbType.Int).Value = filtro.NumeroPagina
                    comando.Parameters.Add("@Tamanho_Pagina", SqlDbType.Int).Value = filtro.TamanhoPagina

                    Dim paramTotal As SqlParameter = comando.Parameters.Add("@Total_Registros", SqlDbType.Int)
                    paramTotal.Direction = ParameterDirection.Output

                    Dim itens As New List(Of Transacao)

                    conexao.Open()
                    Using leitor As SqlDataReader = comando.ExecuteReader()
                        Dim ordinais = TransacaoMapper.ObterOrdinais(leitor)
                        While leitor.Read()
                            itens.Add(TransacaoMapper.Mapear(leitor, ordinais))
                        End While
                    End Using

                    Return New ResultadoPaginado(Of Transacao) With {
                        .Itens = itens,
                        .TotalRegistros = CInt(paramTotal.Value),
                        .NumeroPagina = filtro.NumeroPagina,
                        .TamanhoPagina = filtro.TamanhoPagina
                    }
                End Using
            End Using
        End Function

        Public Function ObterTransacoesPorPeriodo(dataInicial As Date, dataFinal As Date) As List(Of Transacao) Implements ITransacaoRepositorio.ObterTransacoesPorPeriodo
            Using conexao As SqlConnection = ConexaoFactory.ObterConexao()
                Using comando As New SqlCommand("dbo.sp_ConsultarTransacoesPaginado", conexao)
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add("@Data_Inicial", SqlDbType.DateTime2).Value = dataInicial
                    comando.Parameters.Add("@Data_Final", SqlDbType.DateTime2).Value = dataFinal
                    comando.Parameters.Add("@Numero_Pagina", SqlDbType.Int).Value = DBNull.Value
                    comando.Parameters.Add("@Tamanho_Pagina", SqlDbType.Int).Value = DBNull.Value

                    Dim paramTotal As SqlParameter = comando.Parameters.Add("@Total_Registros", SqlDbType.Int)
                    paramTotal.Direction = ParameterDirection.Output

                    Dim itens As New List(Of Transacao)

                    conexao.Open()
                    Using leitor As SqlDataReader = comando.ExecuteReader()
                        Dim ordinais = TransacaoMapper.ObterOrdinais(leitor)
                        While leitor.Read()
                            itens.Add(TransacaoMapper.Mapear(leitor, ordinais))
                        End While
                    End Using

                    Return itens
                End Using
            End Using
        End Function

        Public Function ObterTotalPorPeriodo(dataInicial As Date, dataFinal As Date, status As StatusTransacao?) As List(Of TotalTransacaoPorCartao) Implements ITransacaoRepositorio.ObterTotalPorPeriodo
            Using conexao As SqlConnection = ConexaoFactory.ObterConexao()
                Using comando As New SqlCommand("dbo.sp_TotalTransacoesPorPeriodo", conexao)
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add("@Data_Inicial", SqlDbType.DateTime).Value = dataInicial
                    comando.Parameters.Add("@Data_Final", SqlDbType.DateTime).Value = dataFinal
                    AdicionarParametroTextoOuNulo(comando, "@Status_Transacao", SqlDbType.VarChar, 20,
                        If(status.HasValue, StatusTransacaoConversor.ParaTexto(status.Value), Nothing))

                    Dim resultado As New List(Of TotalTransacaoPorCartao)

                    conexao.Open()
                    Using leitor As SqlDataReader = comando.ExecuteReader()
                        Dim idxCartao = leitor.GetOrdinal("Numero_Cartao")
                        Dim idxValor = leitor.GetOrdinal("Valor_Total")
                        Dim idxQtd = leitor.GetOrdinal("Quantidade_Transacoes")
                        Dim idxStatus = leitor.GetOrdinal("Status_Transacao")

                        While leitor.Read()
                            resultado.Add(New TotalTransacaoPorCartao With {
                                .NumeroCartao = leitor.GetString(idxCartao).Trim(),
                                .ValorTotal = leitor.GetDecimal(idxValor),
                                .QuantidadeTransacoes = leitor.GetInt32(idxQtd),
                                .StatusTransacao = StatusTransacaoConversor.ParaEnum(leitor.GetString(idxStatus))
                            })
                        End While
                    End Using

                    Return resultado
                End Using
            End Using
        End Function

        Private Shared Sub AdicionarParametroDecimal(comando As SqlCommand, nome As String, valor As Decimal)
            Dim parametro As SqlParameter = comando.Parameters.Add(nome, SqlDbType.Decimal)
            parametro.Precision = 18
            parametro.Scale = 2
            parametro.Value = valor
        End Sub

        Private Shared Sub AdicionarParametroDecimalOuNulo(comando As SqlCommand, nome As String, valor As Decimal?)
            Dim parametro As SqlParameter = comando.Parameters.Add(nome, SqlDbType.Decimal)
            parametro.Precision = 18
            parametro.Scale = 2
            parametro.Value = If(valor.HasValue, CType(valor.Value, Object), CType(DBNull.Value, Object))
        End Sub

        Private Shared Sub AdicionarParametroIntOuNulo(comando As SqlCommand, nome As String, valor As Integer?)
            Dim parametro As SqlParameter = comando.Parameters.Add(nome, SqlDbType.Int)
            parametro.Value = If(valor.HasValue, CType(valor.Value, Object), CType(DBNull.Value, Object))
        End Sub

        Private Shared Sub AdicionarParametroDataOuNulo(comando As SqlCommand, nome As String, valor As Date?)
            Dim parametro As SqlParameter = comando.Parameters.Add(nome, SqlDbType.DateTime2)
            parametro.Value = If(valor.HasValue, CType(valor.Value, Object), CType(DBNull.Value, Object))
        End Sub

        Private Shared Sub AdicionarParametroTextoOuNulo(comando As SqlCommand, nome As String, tipo As SqlDbType, tamanho As Integer, valor As String)
            Dim parametro As SqlParameter = comando.Parameters.Add(nome, tipo, tamanho)
            parametro.Value = If(String.IsNullOrEmpty(valor), CType(DBNull.Value, Object), CType(valor, Object))
        End Sub

    End Class

End Namespace
