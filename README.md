# Biblioteca API

Esta é uma API para gerenciar uma biblioteca, permitindo operações CRUD para livros, gêneros, amigos, donos e empréstimos. Além da API, existe também uma aplicação console que permite interagir com a biblioteca de maneira mais simples e intuitiva.

## Tecnologias Utilizadas

- .NET 6
- Entity Framework Core
- C#
- Swagger para documentação da API

## Funcionalidades da API

A API oferece os seguintes endpoints:

### Livros (Books)

- `GET /Book`: Lista todos os livros.
- `GET /Book/{title}`: Obtém um livro pelo título.
- `POST /Book`: Adiciona um novo livro.
- `PUT /Book/{id}`: Atualiza um livro existente.
- `DELETE /Book/{id}`: Remove um livro pelo ID.

### Donos (Owners)

- `GET /Owner`: Lista todos os donos.
- `GET /Owner/{id}`: Obtém um dono pelo ID.
- `POST /Owner`: Adiciona um novo dono.
- `PUT /Owner/{id}`: Atualiza um dono existente.
- `DELETE /Owner/{id}`: Remove um dono pelo ID.

### Amigos (Friends)

- `GET /Friend`: Lista todos os amigos.
- `GET /Friend/{id}`: Obtém um amigo pelo ID.
- `POST /Friend`: Adiciona um novo amigo.
- `PUT /Friend/{id}`: Atualiza um amigo existente.
- `DELETE /Friend/{id}`: Remove um amigo pelo ID.

### Empréstimos (Loans)

- `GET /Loan`: Lista todos os empréstimos.
- `GET /Loan/{id}`: Obtém um empréstimo pelo ID.
- `POST /Loan`: Adiciona um novo empréstimo.
- `PUT /Loan/{id}`: Atualiza um empréstimo existente.
- `DELETE /Loan/{id}`: Remove um empréstimo pelo ID.

### Gêneros (Genders)

- `GET /Gender`: Lista todos os gêneros.
- `GET /Gender/{id}`: Obtém um gênero pelo ID.
- `POST /Gender`: Adiciona um novo gênero.
- `PUT /Gender/{id}`: Atualiza um gênero existente.
- `DELETE /Gender/{id}`: Remove um gênero pelo ID.

## Funcionalidades da Aplicação Console

A aplicação console fornece um menu interativo para gerenciar a biblioteca. As principais funcionalidades incluem:

1. **Adicionar Livro**: Permite inserir novos livros no sistema.
2. **Listar Livros**: Exibe todos os livros cadastrados.
3. **Atualizar Livro**: Permite atualizar as informações de um livro existente.
4. **Remover Livro**: Remove um livro pelo ID.
5. **Gerenciar Donos**: Inclui opções para adicionar, listar, atualizar e remover donos.
6. **Gerenciar Amigos**: Inclui opções para adicionar, listar, atualizar e remover amigos.
7. **Gerenciar Empréstimos**: Permite adicionar, listar, atualizar e remover empréstimos.
8. **Gerenciar Gêneros**: Inclui opções para adicionar, listar, atualizar e remover gêneros.

