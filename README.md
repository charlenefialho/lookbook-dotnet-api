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

## Endpoints Disponíveis e testes de Sistema via postman

[Baixar o conjunto de json teste de endpoisn e sistema do postman](https://github.com/user-attachments/files/17608778/api-dotnet-lookbook.postman_collection-final.json)

#### Inserção de conteúdo significativo

- [Está configurado no DataSeeder](https://github.com/charlenefialho/lookbook-dotnet-api/blob/main/Data/DataSeeder.cs)

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
git clone https://github.com/charlenefialho/lookbook-dotnet-api.git
cd lookbook-dotnet-api
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

## 🧪 Testes Unitários
Os testes unitários são fundamentais para garantir que a lógica da aplicação funcione como esperado. Para a API Lookbook, foram implementados testes utilizando o framework xUnit e a biblioteca Moq para simulação de dependências.

Estrutura dos Testes
Os testes foram organizados em três classes principais, cada uma responsável por testar diferentes componentes da API:

LookbookServiceTests: Testa a lógica da camada de serviço relacionada aos lookbooks.

### Métodos de Teste Unitário:
- `GetAllLookbooksAsync_ShouldReturnAllLookbooks`: Verifica se todos os lookbooks são retornados corretamente.
- `CreateLookbookAsync_ShouldAddLookbookWithVerifiedProdutos`: Testa a criação de um novo lookbook e a verificação de produtos.
- `UpdateLookbookAsync_ShouldUpdateLookbookWithVerifiedProdutos`: Assegura que a atualização de um lookbook funcione como esperado.
- `DeleteLookbookAsync_ShouldCallDeleteMethod`: Testa se o método de exclusão é chamado corretamente.
- `CreateLookbookAsync_ShouldAddProdutosIfNotExistInRepository`: Verifica se produtos são adicionados corretamente ao criar um lookbook.
- `ProdutoServiceTests`: Foca na lógica da camada de serviço relacionada aos produtos.

### Métodos de Teste Unitário:
- `GetAllProdutosAsync_ShouldReturnAllProdutos`: Garante que todos os produtos sejam retornados.
- `CreateProdutoAsync_ShouldCallAddMethod`: Verifica se o método de adição é chamado ao criar um novo produto.
- `GetProdutoByIdAsync_ProdutoExists_ShouldReturnProduto`: Testa se um produto existente é retornado corretamente.
- `GetProdutoByIdAsync_ProdutoNotFound_ShouldReturnNull`: Assegura que, se o produto não existir, null seja retornado.
- `LookbooksControllerTests`: Testa os controladores da API para assegurar que as requisições HTTP sejam tratadas corretamente.

### Métodos de Teste Unitário:
- `GetLookbooks_ShouldReturnOkWithLookbooks`: Garante que a chamada para obter lookbooks retorne um resultado OK com os lookbooks.
- `CreateLookbook_ValidLookbook_ShouldReturnCreatedAtAction`: Verifica se a criação de um lookbook retorna a ação correta.
- `CreateLookbook_NullLookbook_ShouldReturnBadRequest`: Assegura que uma requisição com lookbook nulo retorne um Bad Request.
- `UpdateLookbook_ValidLookbook_ShouldReturnNoContent`: Testa se a atualização de um lookbook retorna o resultado correto.
- `DeleteLookbook_ValidId_ShouldReturnNoContent`: Verifica se a exclusão de um lookbook retorna o resultado esperado.

### Estrutura dos Testes de Integração
A classe ProdutoServiceIntegrationTests inclui os seguintes métodos de teste:

`CreateProdutoAsync_ShouldAddProdutoToDatabase`

- **Descrição:** Este teste verifica se um novo produto é corretamente adicionado ao banco de dados. Um produto de teste é criado e, em seguida, o método CreateProdutoAsync é chamado para adicioná-lo ao repositório. Após a operação, o teste valida se o produto foi realmente persistido no banco de dados ao consultar o contexto de dados e verificar se o produto existe.
- **Objetivo:** Garantir que a funcionalidade de criação de produtos funcione corretamente, confirmando que os dados são salvos no banco de dados.

`GetAllProdutosAsync_ShouldReturnProdutosFromDatabase`

- **Descrição:** Este teste assegura que todos os produtos armazenados no banco de dados sejam retornados corretamente pelo serviço. Inicialmente, dois produtos são adicionados diretamente ao contexto de dados, e o método GetAllProdutosAsync é chamado para recuperá-los. O teste verifica se a quantidade de produtos retornados é igual a dois.
- **Objetivo:** Validar que o método de recuperação de todos os produtos funciona conforme esperado, retornando todos os produtos que foram salvos no banco de dados.

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
