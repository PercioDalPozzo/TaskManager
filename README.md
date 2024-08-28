# TaskManager - Sistema para gestão de tarefas

# Sobre o projeto
TaskManager é uma API responsavel pela gestão de tarefas.
Conta com cadastro e login de usuários assim como, notificações que auxiliam o usuário na execução das tarefas.

# Tecnologias utilizadas
Visual Studio 2022 com ASP.NET Core (Sdk .NET 6.0)<br>
Entity Framework Core<br>
Banco de dados InMemory<br>
Arquitetura CQRS<br>
API RESTful com autenticação JWT.<br>
Swagger

# Funcionalidades
- Registro de novo usuário
- Login

- Criação de nova tarefas
- Listagem das tarefas do usuário
- Definição da tarefa como concluída
- Exclusão de tarefa

- Criação de notificações quando uma nova tarefa é criada
- Criação automática de notificações informando que determinada tarefa está pendente
- Definição da notificação como lida


# Melhorias futuras no projeto
* Utilização do Mediator 
* Criação do Ioc para tratar da injeções
* Melhorar os métodos GetAll para retornar apenas o necessário sem expor a classe de domínio
* Melhorar as validações em todos os handlers pois foram construidos de forma otimista 
