# Minimal API - Questões
Este projeto é uma minimal-api para gerenciamento de questões de múltipla escolha. Ele fornece endpoints para criar, listar, atualizar e deletar questões, tornando-o útil para projetos de estudos e práticas de desenvolvimento de APIs.

### Tecnologias Utilizadas
.NET 8: Framework principal utilizado para desenvolver a API.<br>
Entity Framework Core: Para mapeamento objeto-relacional e gerenciamento de banco de dados.<br>
MySQL: Banco de dados ideal para desenvolvimento local.<br>
Swagger: Interface para visualizar a API.<br>
Token JWT: Utilizado para autenticação e autorização para os cargos de administradores e editores. <br>
(Ao logar em uma conta valida a API te retorna o Token)

### Funcionalidades

#### A API permite:
Criar um novo administrador ou editor<br>
Logar como um administrador ou editor para adquirir o Token JWT<br>
Buscar os administradores caso esteja logado como administrador<br>
Criar uma nova questão<br>
Listar todas as questões<br>
Obter uma questão específica pelo ID<br>
Obter questões de especificas materias<br>
Atualizar uma questão existente<br>
Deletar uma questão pelo ID<br>

----

### EndPoints

#### Administradores

POST
/administradores/login

POST
/administradores

GET
/administradores

GET
/administradores/{id}

#### Questoes

POST
/questoes

GET
/questoes

GET
/questoes/{id}

PUT
/questoes/{id}

DELETE
/questoes/{id}

GET
/materia/{materia}

----

## Swagger
![image](https://github.com/user-attachments/assets/32dbdd74-9156-4e33-811c-31568e39fe21)

----

## Bearer para autenticar
![image](https://github.com/user-attachments/assets/48e9fbe2-d7d7-4ee8-a400-8257a9369179)

----

## Exemplo sem TokenJWT
![image](https://github.com/user-attachments/assets/dc6b0086-4598-4091-87c2-43b5a10b253f)

----

## Exemplo ao utilizar TokenJWT
![image](https://github.com/user-attachments/assets/2d9a82b1-3056-4151-835e-2530cdd58a16)


