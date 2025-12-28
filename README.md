# API - Sistema de cadastro de pessoas

## 📘 Sobre o Projeto

Esta API tem como finalidade expor um CRUD para cadastro de pessoas

O endpoint de cadastro conta com algumas regras:

- O Email informado não pode existir no banco de dados
- A pessoa precisa ter no mínimo 18 anos
- Validação de formato dos demais campos(telefone,nome e etc)

## ⚙️ Tecnologias/Libs e Padrões utilizados

- .NET 8
- Result Pattern (fluent results)
- FluentValidation para os requests recebidos pela API
- Inversão de Dependência
- Repository Pattern com EntityFramework
- Banco de dados: SQL Server
- Moq para testes unitários

## ❓ Como executar o projeto

Requisitos: possuir o Docker instalado na máquina.

Para executar a aplicação vá até a pasta raiz do projeto e execute o seguinte comando:
**docker compose up --build**

Após isso, o comando deve criar um container com a API e o SQL Server prontos para funcionamento.

API estará na porta 8080, para acessar o swagger:

- **http://localhost:8080/swagger/index.html**
  (Observe que a API está exposta como **HTTP**, não HTTPS)

Caso queira acessar o banco, as credências são as seguintes:

- server = localhost,1433
- User = sa
- Senha = 1q2w3e4r@#$

## 🧱 Arquitetura

Tentei ponderar entre a utilização de boas práticas, mas sem causar overengineering, o projeto possui uma separação de responsabilidades por pastas e não por camadas(projetos/assemblies), visto que o objetivo era oferecer um CRUD sem muitas regras de negócio.

- Controllers

  - Responsável por receber as requests validá-las, fornecer os dados para as services e então devolver um retorno contendo o status code e body de acordo com o resultado da operação dentro da service.)

- Services

  - Responsável por orquestrar todos os pontos da aplicação para concluir o caso de uso solicitado, fazendo o mapeamento dos requests para entidade, efetuando a camunicação com a repository e utilizando das operações disponíveis dentro da entitidade.

- Repository

  - Responsável por realizar a comunicação com o banco de dados, efetuando as operações solicitadas pela service.

- Entity
  - Responsável por garantir que somente sejam criadas instâncias com estado válido, por meio das validações internas, e também responsável por fornecer métodos para que a service possa mutar o estado da entidade de forma segura.
