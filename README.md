Containers
  - Rabbitmq = 15672, 5672
  - Postgres = 5432
  - PgAdmin = 8082
  - Seq = 5341, 8083
  - Aplicação = 8080, 8081

Para o sistema de logs da aplicação foi utilizado o [Serilog](https://serilog.net), 
integrado ao [Seq](https://datalust.co/seq) para visualização dos logs 

Como biblioteca de pub/sub foi utlizado a biblioteca RabbitMQ.Client.
Foi utilizado o conceito de EDA, aonde o dominio registra os eventos que depois serão disparados no commit. 
Para manter a resiliência das entidades com seus eventos, foi utilizado a resiliência de conexão, através do CreateExecutionStrategy.

Para validações tanto das entidades como dos value objects foi utilizado o [FluentValidation](https://docs.fluentvalidation.net/en/latest/).
Em conjunto deste, foi utilizado o Result pattern para retorno do resultado da api.

Para os testes de integração foi implementado o uso de [TestContainer](https://testcontainers.com/), aonde é possivel rodar um 
container do Postgre durante os testes. Para cada classe de teste, um container é criado e ao final o mesmo é removido.
