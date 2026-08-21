# XYZ Administradora de Cartões de Crédito

## Visão geral

- **Aplicação desktop**: VB.NET WinForms, .NET Framework 4.8, um único projeto organizado em pastas por camada (`Dominio`, `Dados`, `Negocio`, `Utilitarios`, `Forms`).
- **Acesso a dados**: ADO.NET puro (`SqlConnection`/`SqlCommand`), sempre via stored procedures parametrizadas.
- **Banco de dados**: SQL Server (testado em LocalDB), com tabela, stored procedures, functions, view e massa de dados de exemplo.
- **Exportação**: relatório Excel do último mês via [ClosedXML](https://github.com/ClosedXML/ClosedXML).

## Estrutura do repositório

```
xyz-cartoes-credito/
├── README.md
├── .gitignore
├── src/
│   └── XYZCartoes.sln
│       XYZCartoes/                  (projeto único WinForms)
│           Dominio/                 (entidades, enums, DTOs, exceções — sem dependências)
│           Dados/                   (ConexaoFactory, repositório, mapeador de SqlDataReader)
│           Negocio/                 (validação e regras de negócio)
│           Utilitarios/             (Logger, mascaramento de cartão, exportação Excel)
│           Forms/                   (telas WinForms, padrão Designer: FrmX.vb + FrmX.Designer.vb, editável no VS)
├── database/
│   ├── 01_tabela_transacoes.sql
│   ├── 02_stored_procedures.sql
│   ├── 03_functions.sql
│   ├── 04_view.sql
│   └── 05_dados_exemplo.sql
└── sample-output/
    └── Transacoes_UltimoMes_*.xlsx
```

## Pré-requisitos

- Visual Studio 2022+ com a carga de trabalho **.NET desktop development** (inclui o SDK do .NET Framework 4.8).
- SQL Server (LocalDB, Express ou completo). O projeto foi desenvolvido e testado contra `(localdb)\MSSQLLocalDB`.
- Acesso à internet na primeira compilação, para o NuGet restaurar o pacote `ClosedXML`.

## Como configurar o banco de dados

Execute os scripts em `database/` **nesta ordem**, contra a instância desejada (todos criam/usam o banco `XYZCartoesDB`):

1. `01_tabela_transacoes.sql` — cria o banco (se não existir) e a tabela `Transacoes` com constraints e índices.
2. `05_dados_exemplo.sql` — popula ~8.000 transações de teste (datas relativas a hoje, então sempre há dados no "mês anterior").
3. `02_stored_procedures.sql` — CRUD, consulta paginada e totais por período.
4. `03_functions.sql` — `fn_CategoriaTransacao` (scalar) e `fn_TransacoesCategorizadas` (TVF inline).
5. `04_view.sql` — `vw_ConsolidadoFinanceiro`.

Pode rodar via SSMS (abrir e executar cada arquivo) ou via `sqlcmd`:

```
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\01_tabela_transacoes.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\05_dados_exemplo.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\02_stored_procedures.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\03_functions.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\04_view.sql
```

Os scripts são salvos com BOM UTF-8 para que tanto o SSMS quanto o `sqlcmd` reconheçam automaticamente a acentuação (mensagens de erro, "Média", etc.) sem precisar de flags extras.

## Como configurar e rodar a aplicação

A connection string fica em `src/XYZCartoes/App.config`:

```xml
<connectionStrings>
  <add name="XYZCartoesConnection"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=XYZCartoesDB;Integrated Security=True;Connect Timeout=15;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Ajuste `Data Source`/autenticação conforme o seu ambiente, abra `src/XYZCartoes.sln` no Visual Studio, restaure os pacotes NuGet (automático ao compilar) e rode o projeto `XYZCartoes` (F5). A tela principal já abre com a consulta paginada carregada.

## Funcionalidades

- **Cadastro/edição/exclusão de transações**, com confirmação antes de excluir e mensagens amigáveis para erros de validação e de negócio.
- **Edição bloqueada para transações `Aprovada`**: ao clicar em Editar, é exibida uma mensagem informando que a transação não pode ser editada e a tela de cadastro não chega a abrir. A regra é reforçada em três pontos — UI, `TransacaoService` e a própria `sp_AtualizarTransacao` — para proteger contra uma grid desatualizada (a transação pode ter sido aprovada por outra sessão entre a listagem e o clique em Editar/Salvar).
- **Consulta com filtros opcionais** (Id, cartão, intervalo de data, intervalo de valor, status, categoria) e **paginação no servidor** via `OFFSET/FETCH`, testada com ~8.000 registros. A coluna Categoria (Baixa/Média/Alta/Premium) é calculada pelo banco via `fn_CategoriaTransacao` e exibida/filtrável direto na grid.
- **Ordenação por clique no header da grid** (Id, Cartão, Valor/Categoria, Data, Descrição, Status), também resolvida no servidor — reordena o conjunto filtrado inteiro, não só a página carregada; clique de novo na mesma coluna alterna entre crescente/decrescente.
- **Número do cartão mascarado** na grid e no Excel exportado (mostra só os 4 últimos dígitos); o valor completo só aparece no banco e na tela de cadastro/edição.
- **Relatório de Totais por Período**, tela dedicada que expõe `sp_TotalTransacoesPorPeriodo`.
- **Exportação para Excel** das transações do último mês calendário completo (via ClosedXML).
- **Log de erros técnicos** em `Logs\log_yyyyMMdd.txt`, ao lado do executável; erros de validação/regra de negócio não são logados (são entrada esperada do usuário), apenas mostrados ao usuário.

## Decisões técnicas

O enunciado do desafio deixa alguns pontos em aberto. As decisões abaixo foram tomadas conscientemente durante o desenvolvimento:

| Ponto em aberto | Decisão adotada | Racional |
|---|---|---|
| Fronteiras de `fn_CategoriaTransacao` | `> 2000` → Premium; `1000` a `2000` → Alta; `500` a `999,99` → Média; `< 500` → Baixa | Sem sobreposição/lacuna nos limites 500, 1000 e 2000 |
| `Data_Transacao` | Somente leitura na UI, preenchida pelo banco (`DEFAULT SYSDATETIME()`) | É "data/hora do registro", não um campo de negócio editável |
| "Último mês" na exportação | Mês calendário anterior completo (dia 1 ao último dia), relativo à data de execução | Interpretação mais natural de "último mês" |
| Exclusão de transação `Aprovada` | Permitida (o enunciado só pede bloqueio de **edição**) | Segue o enunciado literalmente |
| Mascaramento do cartão | Grid e Excel mostram só os 4 últimos dígitos; número completo só no banco e no formulário | Boa prática de segurança para dado de cartão |
| Estrutura do projeto | Um único projeto WinForms organizado em pastas/namespaces por camada, em vez de vários assemblies | Organização clara sem overhead de múltiplos projetos para o porte do desafio |
| `Id_Transacao` | `INT IDENTITY` | Volume do desafio (milhares de linhas) não justifica `BIGINT` |

## Stored procedures, functions e view

| Objeto | Descrição |
|---|---|
| `sp_InserirTransacao` | Insere uma transação, retorna o novo `Id_Transacao` via parâmetro `OUTPUT` |
| `sp_AtualizarTransacao` | Atualiza uma transação; lança erro 50001 (não encontrada) ou 50002 (status `Aprovada`) |
| `sp_ExcluirTransacao` | Exclui uma transação; lança erro 50001 se não encontrada |
| `sp_ObterTransacaoPorId` | Busca uma transação (usada para refrescar antes de abrir a tela de edição) |
| `sp_ConsultarTransacoesPaginado` | Filtros opcionais + ordenação (`@Ordenar_Por`/`@Ordenar_Direcao`) + paginação via `OFFSET/FETCH`; usada tanto pela grid quanto pela exportação (sem paginar/ordenar) |
| `sp_TotalTransacoesPorPeriodo` | SUM/COUNT por cartão e status em um período (item 2 do desafio) |
| `fn_CategoriaTransacao` | Scalar function: categoriza um valor (Baixa/Média/Alta/Premium) |
| `fn_TransacoesCategorizadas` | TVF inline: transações de um período já categorizadas |
| `vw_ConsolidadoFinanceiro` | View: resumo financeiro mensal por cartão |

## Relatório Excel de exemplo

Arquivo de exemplo gerado na pasta: sample-output
