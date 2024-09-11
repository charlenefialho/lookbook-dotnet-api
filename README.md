# lookbook dotnet api


### Descrição do Projeto
A Lookbook API é uma aplicação web desenvolvida com .NET 8.0 para gerenciar lookbooks e produtos, permitindo criar, listar, atualizar e excluir lookbooks e produtos. A API permite a associação de produtos a lookbooks e suporta uma relação N:N, onde um lookbook pode conter vários produtos, e um produto pode estar em vários lookbooks.

A aplicação utiliza um banco de dados Oracle para armazenamento dos dados e segue o padrão Repository para a separação da lógica de acesso aos dados.


### Arquitetura
Este projeto adota uma arquitetura monolítica com camadas bem definidas:

- **Camada de Apresentação (Controllers):** Responsável por expor os endpoints da API e tratar as requisições HTTP.
- **Camada de Repositório:** Realiza o acesso aos dados e implementa os métodos de CRUD utilizando o padrão Repository.
- **Camada de Modelos:** Define as entidades que representam os dados manipulados pela API (como Lookbook e Produto).


### Padrões de Design Utilizados
1. **Repository Pattern:**
O padrão Repository é usado para isolar a lógica de acesso ao banco de dados da lógica de negócios. Isso facilita a manutenção, testes e futuras alterações no acesso aos dados.


### Tecnologias Utilizadas
- .NET 8.0: Framework de desenvolvimento.
- Entity Framework Core: ORM para mapear as entidades com o banco de dados Oracle.
- Oracle: Banco de dados relacional utilizado para armazenar os dados.
- Swashbuckle/Swagger: Ferramenta de documentação interativa da API.

## Endpoints Disponíveis

### Lookbooks
- `GET /api/lookbooks` - Retorna todos os lookbooks.
- `POST /api/lookbooks` - Cria um novo lookbook.
- `PUT /api/lookbooks/{id}` - Atualiza um lookbook existente.
- `DELETE /api/lookbooks/{id}` - Exclui um lookbook.

### Produtos
- `GET /api/produtos` - Retorna todos os produtos.
- `POST /api/produtos` - Cria um novo produto.
- `PUT /api/produtos/{id}` - Atualiza um produto existente.
- `DELETE /api/produtos/{id}` - Exclui um produto.


## Passo a Passo

### Clone o repositório:
``` bash
git clone https://github.com/seu-usuario/projeto-lookbook-api.git
cd projeto-lookbook-api
```

Instale as dependências: Execute o comando para restaurar os pacotes do .NET:

``` bash
dotnet restore
```

### Configurar a Conexão com o Banco de Dados: 
Atualize a string de conexão para o banco de dados Oracle no arquivo `appsettings.json`:

``` json
{
    "ConnectionStrings": {
        "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=seu_oracle_db"
    }
}
```

Rodar as Migrações: 

``` bash
dotnet ef database update
```

Rodar a Aplicação:

``` bash
dotnet run
```

Acessar a Documentação acesse a interface do Swagger para testar os endpoints:

``` bash
https://localhost:<porta>/swagger
```

### Integrantes do grupo
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
