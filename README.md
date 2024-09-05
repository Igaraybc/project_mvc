# Project_MVC

Esta aplicação web foi desenvolvida como parte do ["Curso de ASP .NET Core MVC - Criando um Site do Zero (NET 6)"](https://www.udemy.com/course/curso-de-asp-net-core-mvc-criando-um-site-do-zero/) do Marcoratti. O objetivo do projeto é criar um site funcional que demonstre as principais funcionalidades do ASP.NET Core MVC, incluindo autenticação e autorização com Identity, acesso a dados com Entity Framework Core, e design responsivo utilizando Bootstrap. Durante o desenvolvimento, foram feitas modificações na estrutura das telas para melhorar a usabilidade e a experiência do usuário, o que proporcionou uma valiosa experiência prática com ASP.NET MVC.

## Tecnologias Utilizadas

- **ASP.NET Core MVC**: Framework para construção de aplicações web.
- **Bootstrap**: Framework CSS para design responsivo e componentes de interface.
- **Entity Framework Core**: ORM para acesso a dados.
- **Identity**: Sistema de autenticação e autorização.
- **SQL Server e PostgreSQL**: Bancos de dados suportados (utilizada a versão 16.0).

## Pacotes Utilizados

- `Microsoft.AspNetCore.Identity` (2.2.0)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (8.0.6)
- `Microsoft.EntityFrameworkCore` (8.0.6)
- `Microsoft.EntityFrameworkCore.Design` (8.0.6)
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.6)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.6)
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` (8.0.0)
- `Npgsql.EntityFrameworkCore.PostgreSQL` (8.0.4)
- `ReflectionIT.Mvc.Paging` (8.0.0)

## Estrutura do Projeto

- **Controllers**: Contém os controladores que gerenciam as requisições HTTP.
- **Models**: Contém as classes de modelo que representam os dados da aplicação.
- **Views**: Contém as páginas de interface do usuário.
- **wwwroot**: Contém arquivos estáticos como CSS, JavaScript e imagens.

## Configuração e Execução

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/Igaraybc/project_mvc.git

2. **Navegue até o diretório do projeto**:
   ```bash
   cd project_mvc

3. **Instale as dependências**:
   ```bash
   dotnet restore

4. **Atualize o banco de dados**:
   ```bash
   dotnet ef database update

5. **Execute a aplicação**:
   ```bash
   dotnet run