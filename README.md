# 🎨 lookbook dotnet api

### 📖Descrição do Projeto

A Lookbook API é uma aplicação web desenvolvida com .NET 8.0 para gerenciar lookbooks e produtos, permitindo criar, listar, atualizar e excluir lookbooks e produtos. A API permite a associação de produtos a lookbooks e suporta uma relação N:N, onde um lookbook pode conter vários produtos, e um produto pode estar em vários lookbooks.

A aplicação utiliza um banco de dados Oracle para armazenamento dos dados e segue o padrão Repository para a separação da lógica de acesso aos dados.

### 🏛️ Arquitetura

Este projeto adota uma arquitetura monolítica com camadas bem definidas:

- **Camada de Apresentação (Controllers):** Responsável por expor os endpoints da API e tratar as requisições HTTP. 🌐
- **Camada de Repositório:** Realiza o acesso aos dados e implementa os métodos de CRUD utilizando o padrão Repository. 📂
- **Camada de Modelos:** Define as entidades que representam os dados manipulados pela API (como Lookbook e Produto). 📊

#### Escolha da Arquitetura

A arquitetura escolhida para este projeto é **monolítica**. Optou-se por uma abordagem monolítica em vez de microservices devido a vários fatores:

1. **Simplicidade e Facilidade de Desenvolvimento:** Em um projeto com um escopo relativamente pequeno e um conjunto limitado de funcionalidades, uma arquitetura monolítica simplifica o desenvolvimento, o teste e a manutenção. Não há a necessidade de gerenciar múltiplos serviços, o que reduz a complexidade.

2. **Menor Overhead Operacional:** Uma aplicação monolítica pode ser mais fácil de gerenciar em termos de deployment e escalabilidade inicial. Não há necessidade de orquestração de containers ou comunicação entre serviços, o que diminui o overhead operacional.

3. **Cohesão dos Componentes:** Em um projeto onde as funcionalidades estão fortemente interligadas, uma arquitetura monolítica pode facilitar a integração e a comunicação entre os diferentes componentes da aplicação.

### 🔧 Padrões de Design Utilizados

1. **Repository Pattern:**
   O padrão Repository é usado para isolar a lógica de acesso ao banco de dados da lógica de negócios. Isso facilita a manutenção, testes e futuras alterações no acesso aos dados.

### 🛠️ Tecnologias Utilizadas

- .NET 8.0: Framework de desenvolvimento.
- Entity Framework Core: ORM para mapear as entidades com o banco de dados Oracle.
- Oracle: Banco de dados relacional utilizado para armazenar os dados.
- Swashbuckle/Swagger: Ferramenta de documentação interativa da API.

### 🛠️ Implementação da API

A API foi implementada seguindo a arquitetura monolítica, com as seguintes características:

1. **Camadas e Responsabilidades:**

   - **Camada de Apresentação:** Os controllers são responsáveis por expor os endpoints da API e processar as requisições HTTP. Eles utilizam serviços da camada de repositório para realizar operações de CRUD.
   - **Camada de Repositório:** Implementa o padrão Repository, que separa a lógica de acesso aos dados da lógica de negócios. Os repositórios fornecem métodos para interagir com o banco de dados de forma genérica e reutilizável.
   - **Camada de Modelos:** Define as entidades do domínio, como `Lookbook` e `Produto`, que são mapeadas para tabelas no banco de dados Oracle.

2. **Diferenças em Relação à Arquitetura de Microservices:**

   - **Escalabilidade:** Em uma arquitetura monolítica, toda a aplicação é escalada como um único bloco, o que pode ser menos eficiente para aplicações com altos volumes de tráfego. Em contraste, a arquitetura de microservices permite a escalabilidade independente de cada serviço.
   - **Desenvolvimento e Deploy:** Em uma arquitetura monolítica, a aplicação é desenvolvida e implantada como uma unidade única. Isso pode simplificar o desenvolvimento inicial, mas torna o deployment e a manutenção mais complexos à medida que a aplicação cresce. Microservices permitem deploys independentes e mais flexíveis.
   - **Complexidade:** Microservices podem reduzir a complexidade de um sistema grande, dividindo-o em serviços menores e mais manejáveis. No entanto, eles introduzem complexidade adicional na comunicação entre serviços e na orquestração.

3. **Implementação da API:**
   - **Controllers:** Os controllers expõem endpoints RESTful para operações CRUD em `Lookbook` e `Produto`.
   - **Serviços e Repositórios:** A camada de serviços coordena a lógica de negócios e usa repositórios para acessar o banco de dados.
   - **Database Context:** O `ApplicationDbContext` gerencia as entidades e interage com o banco de dados Oracle.

## Endpoints Disponíveis

[Baixar o conjunto de json teste do postman]()

### Lookbooks

- `GET /api/Lookbooks` - Retorna todos os lookbooks.
- `POST /api/Lookbooks` - Cria um novo lookbook.
- `PUT /api/Lookbooks/{id}` - Atualiza um lookbook existente.
- `DELETE /api/Lookbooks/{id}` - Exclui um lookbook.

### Produtos

- `GET /api/Produtos` - Retorna todos os produtos.
- `POST /api/Produtos` - Cria um novo produto.
- `PUT /api/Produtos/{id}` - Atualiza um produto existente.
- `DELETE /api/Produtos/{id}` - Exclui um produto.

## Passo a Passo

### Clone o repositório:

```bash
git clone https://github.com/seu-usuario/projeto-lookbook-api.git
cd projeto-lookbook-api
```

Instale as dependências: Execute o comando para restaurar os pacotes do .NET:

```bash
dotnet restore
```

### Configurar a Conexão com o Banco de Dados:

Atualize a string de conexão para o banco de dados Oracle no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=seu_oracle_db"
  }
}
```

Rodar as Migrações:

```bash
dotnet ef database update
```

Rodar a Aplicação:

```bash
dotnet build
dotnet run
```

Acessar a Documentação acesse a interface do Swagger para testar os endpoints:

```bash
https://localhost:<porta>/index.html
```

### 👥 Integrantes do grupo

<table>
  <tr>
        <td align="center">
      <a href="https://github.com/biancaroman">
        <img src="https://avatars.githubusercontent.com/u/128830935?v=4" width="100px;" border-radius='50%' alt="Bianca Román's photo on GitHub"/><br>
        <sub>
          <b>Bianca Román</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/charlenefialho">
        <img src="https://avatars.githubusercontent.com/u/94643076?v=4" width="100px;" border-radius='50%' alt="Charlene Aparecida's photo on GitHub"/><br>
        <sub>
          <b>Charlene Aparecida</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/laiscrz">
        <img src="https://avatars.githubusercontent.com/u/133046134?v=4" width="100px;" alt="Lais Alves's photo on GitHub"/><br>
        <sub>
          <b>Lais Alves</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/LuccaRaphael">
        <img src="https://avatars.githubusercontent.com/u/127765063?v=4" width="100px;" border-radius='50%' alt="Lucca Raphael's photo on GitHub"/><br>
        <sub>
          <b>Lucca Raphael</b>
        </sub>
      </a>
    </td>
     <td align="center">
      <a href="https://github.com/Fabs0602">
        <img src="https://avatars.githubusercontent.com/u/111320639?v=4" width="100px;" border-radius='50%' alt="Fabricio Torres's photo on GitHub"/><br>
        <sub>
          <b>Fabricio Torres</b>
        </sub>
      </a>
    </td>
  </tr>
</table>
