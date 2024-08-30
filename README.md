# TaskManager - Sistema para Gestão de Tarefas

## Sobre o Projeto
**TaskManager** é uma API responsável pela gestão de tarefas. Ela inclui funcionalidades de cadastro e login de usuários, além de notificações que auxiliam os usuários na execução de suas tarefas diárias.

## Tecnologias Utilizadas
- **Visual Studio 2022** com **ASP.NET Core (SDK .NET 6.0)**
- **xUnit** para testes unitários
- **Bogus** para geração de dados nos testes unitários
- **FluentAssertions** para melhor compreensão dos testes
- **Entity Framework Core** com banco de dados **InMemory**
- API **RESTful** com autenticação **JWT**
- **Quartz.NET** para implementação de jobs agendados
- **Swagger** para testes e documentação da API

## Arquitetura
O projeto segue a arquitetura **CQRS** (Command Query Responsibility Segregation), organizada em três camadas principais:
- **API**
- **Domain**
- **Repository**

## Funcionalidades

### Usuário
- Registro de novos usuários
- Autenticação (Login)

### Tarefas
- Criação de novas tarefas
- Listagem de tarefas associadas a um usuário
- Marcação de tarefas como concluídas
- Exclusão de tarefas

### Notificações
- Notificações automáticas ao criar uma nova tarefa
- Notificações automáticas para tarefas pendentes
- Marcação de notificações como lidas

## Job para Notificação de Tarefas com Prazo
O sistema gera uma notificação para o usuário caso uma tarefa esteja em aberto e tenha a data de conclusão nas próximas 24 horas. Essa verificação é realizada em um intervalo configurável. Por padrão, 60 minutos, mas pode ser alterada no `appSettings` pela chave **"IntervalMinutesToNotificationJob"**.

## Como Rodar o Projeto
1. Clone este repositório e abra-o no Visual Studio.
2. Defina o projeto **API** como o projeto de inicialização.
3. Para rodar os testes unitários, acesse o menu **Testes > Rodar todos os testes**. Todos os testes devem ser executados com sucesso.

## Como Testar a API
Ao pressionar **F5**, o sistema será compilado e o Swagger será carregado automaticamente no navegador. Você pode testar as funcionalidades da API diretamente pelo Swagger ou utilizar outra ferramenta, como o **Postman**.

### Guia de Teste Rápido no Swagger
As funcionalidades do **TaskManager** podem ser conferidas aqui:
[https://www.youtube.com/watch?v=9ybtyd_Vwwk](https://www.youtube.com/watch?v=9ybtyd_Vwwk)

1. **Registrar um Usuário**
   - Endpoint: `POST /api/Users/register`

2. **Autenticar o Usuário**
   - Endpoint: `POST /api/Users/login`
   - Use o "username" e "password" cadastrados para obter o token JWT.
   - Na parte superior direita, clique em **Authorize**. Será aberta a tela onde deve-se colar o token retornado no passo anterior. 

3. **Criar 5 Tarefas para Teste**
   - Sugestão: Utilize os títulos **"Tarefa 1"**, **"Tarefa 2"**, **"Tarefa 3"**, **"Tarefa 4"**, **"Tarefa 5"**.
   - Endpoint: `POST /api/Tasks`

4. **Conferir as Tarefas Criadas**
   - Endpoint: `GET /api/Tasks/{userId}`

5. **Deletar a "Tarefa 4"**
   - Endpoint: `DELETE /api/Tasks/{id}`

6. **Marcar Tarefas como Concluídas**
   - Marque **"Tarefa 2"** e **"Tarefa 3"** como concluídas.
   - Endpoint: `PUT /api/Tasks/{id}/complete`

7. **Conferir o Status das Tarefas (etapa 4)**
   - Após as operações, as tarefas ficarão assim:
     - **"Tarefa 1"** e **"Tarefa 5"**: Pendentes (`Concluded = false`)
     - **"Tarefa 2"** e **"Tarefa 3"**: Concluídas (`Concluded = true`)
     - **"Tarefa 4"**: Não será listada, pois foi deletada.

8. **Conferir as Notificações**
   - Haverá 5 notificações, uma para cada tarefa criada com a mensagem **"Nova tarefa criada"**.
   - Endpoint: `GET /api/Notifications/{userId}`

9. **Marcar Notificação como Lida**
   - Endpoint: `PUT /api/Notifications/{id}/read`

10. **Conferir Notificações Após Leitura**
    - Agora haverá 4 notificações, pois uma foi marcada como lida.
    - Endpoint: `GET /api/Notifications/{userId}`

11. **Testar o Job de Notificação Automática**
    - Crie 3 tarefas em datas diferentes **(LimitToComplete)** (sugestão: **D0**, **D1** e **D2**).
    - Na próxima execução do job, serão criadas notificações para as tarefas **D0** e **D1** informando **"Tarefa pendente + Título da tarefa"**.
    - Por padrão, essa verificação ocorre a cada 60 minutos. Para testar rapidamente, altere a configuração **"IntervalMinutesToNotificationJob"** para 2 ou 3 minutos.
    - Isso dará tempo para criar as tarefas e observar a execução.

## Melhorias Futuras
- Implementação do **Mediator** para mediar comandos e queries.
- Criação de um **IoC container** para melhorar a injeção de dependências e limpar o `Program.cs`.
- Refatoração dos métodos `GetAll` para retornar apenas os dados necessários, evitando a exposição direta das classes de domínio.
- Adição de validações em todos os handlers, atualmente construídos de forma otimista.
- Otimização da estrutura dos repositórios para reduzir a necessidade de injeção do contexto em cada classe de repositório.
- Migração da **SecurityKey** para um local mais seguro, devido à sua natureza sensível.
