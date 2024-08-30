# TaskManager - Sistema para gestão de tarefas

# Sobre o projeto
TaskManager é uma API responsavel pela gestão de tarefas.
Conta com cadastro e login de usuários assim como, notificações que auxiliam o usuário na execução das tarefas.

# Tecnologias utilizadas
Visual Studio 2022 com ASP.NET Core (Sdk .NET 6.0)<br>
xUnit para testes unitários<br>
Bogus para auxilio nos testes unitários<br>
Entity Framework Core<br>
Banco de dados InMemory<br>
API RESTful com autenticação JWT.<br>
Quartz para implementação de job
Swagger para documentação<br>

# Arquitetura
Arquitetura CQRS separado nas camadas<br>
-API
-Domain
-Repository

# Funcionalidades
Usuário
- Registro de novo usuário
- Login

Tarefas
- Criação de nova tarefas
- Listagem das tarefas do usuário
- Definição da tarefa como concluída
- Exclusão de tarefa

Notificações
- Criação de notificações quando uma nova tarefa é criada
- Criação automática de notificações informando que determinada tarefa está pendente
- Definição da notificação como lida

# Job para notificação de tarefas com prazo 
É gerado uma notificação para o usuário caso tenha alguma tarefa em aberto, e com sua data de conclusão nas próximas 24 horas.
O sistema faz essa verificação em um intervalo configurável. Por default temos 60 minutos mas pode ser configurado no appSettings em "IntervalMinutesToNotificationJob".

# Como rodar
Clonar este repositório e abri-lo com visual studio. Definir o projeto "Api" como projeto de inicialização.
Para rodar os testes unitários, ir no menu "Testes > Rodar todos os testes". Esperado que todos os testes sejam executados com sucesso.

# Como testar
Ao pressionar F5 o sistema irá compilar e subir no navegador a pagina com swagger carregado.
Poderá utilizar qualquer outra ferramenta para envio de requisições HTTP como por exemplo, Postman.

Para tornar a experiencia mais simples e direta, usaremos aqui o próprio swagger.

A seguir um guia de como testar todas as funcionalidades.

* Fazer o registro de um usuário
post: ​/api​/Users​/register

* Com o usuário criado, fazer a autenticação utilizando "username" e "password"
post: ​/api​/Users​/login


* Criar 5 tarefas para termos massa de teste. Para facilitar a compreenção, criar as tarefas com os títulos "Tarefa 1", "Tarefa 2", "Tarefa 3", "Tarefa 4", "Tarefa 5"
post: ​/api​/Tasks

* Para conferir a criação, basta consultar  
get: ​/api​/Tasks​/{userId}

* Deletar a "Tarefa 4" passando o Id da tarefa
delete: ​/api​/Tasks​/{id}

* Marcar a tarefa como concluída basta passar o Id. Faremos isso com o Id da "Tarefa 2" e "Tarefa 3"  
put: ​/api/Tasks/{id}/complete

* Executando novamente a consulta de tarefas teremos a seguinte situação
"Tarefa 1" e "Tarefa 5": Pendentes, status observado pelo campo "Concluded = false"  
"Tarefa 2" e "Tarefa 3": Concluídas, status observado pelo campo "Concluded = true"  
"Tarefa 4": Não lista pois foi deletada


* Observaremos agora as notificações. Teremos 5 notificações, uma para cada tarefa criada com a mensagem "Nova tarefa criada"
get: ​/api​/Notifications​/{userId}

* Fazer a leitura de uma das notificações passando o Id da notificação
put: ​/api​/Notifications​/{id}​/read

* Executando novamente a consulta de notificação 
Agora teremos apenas 4 notificações pois uma delas já foi marcada como lida


* Testando a job de noticação automática
Criar 3 tarefas em datas diferentes, sugestão criar D0, D1 e D2.
Na próxima rodada da job será criada duas notificações para as tarefas D0 e D1 informando "Tarefa pendente".
Por default essa verificação é feita a cada 60 minutos, para esse teste ´pode-se alterar a configuração "IntervalMinutesToNotificationJob" para 2 ou 3 minutos.
Com isso, dará tempo para criar as tarefas e observar a execução.



# Melhorias futuras no projeto
* Utilização do Mediator 
* Criação do Ioc para tratar da injeções e limpar o Program.cs
* Melhorar os métodos GetAll para retornar no controller apenas o necessário sem expor a classe de domínio
* Algumas validações podem ser adicionadas em todos os handlers pois foram construidos de forma otimista
* Otimizar a estrutura dos repositórios para não precisar injeção em cada classe de repositório
* Migrar a SecurityKey para um local mais adequado por se tratar de uma informação sensível
