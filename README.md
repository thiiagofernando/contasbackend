# API de Cadastro de Contas

#
API de Cadastro de Contas a Pagar utilizando JWT para Autenticação e com as funções de

  - Cadastro de Usuários.
  - Login de Usuários Utilizando Autenticação com JWT.
  - Gerador de Token de Acesso.
  - Cadastro de Contas.
  - Listagem de Contas.
  - Cálculo de Juros e Multas por Atraso.

# Tecnologias  e Ferramentas Utilizados!

  - .NET Core
  - Entity Framework Core
  - Autenticação com JwtBearer
  - Banco de Dados SQL Server
  - Testes unitários com o xUnit
  - Swagger
  - Visual Studio 2019
  
  
# Requisitos para executar o Projeto

  - Altere a ConnectionStrings do Banco de Dados nos arquivos appsettings.json e appsettings.Development.json 
  - Execute um Clean e um Rebuild no Projeto para baixar o pacotes
  - Não é necessario gerar um novo migrations para cria o banco e as tabelas
  - No Statup.cs possui um InitializeDatabase que ao executar o projeto irá criar o banco e as tabelas se a ConnectionStrings estiver correta
  
  
**[Projeto de frontend](https://github.com/thiiagofernando/contasfrontend)**.<br>
