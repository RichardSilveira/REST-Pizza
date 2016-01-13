# REST Pizza
Aplicação fictícia que aborda um estudo de caso prático através da implementação de REST.

Essa aplicação é parte integrante do artigo "REST, CONCEITUANDO ATRAVÉS DE UMA ABORDAGEM PRÁTICA E OBJETIVA", para conclusão do MBA em Arquitetura de Software no instituto Newton Paiva.


## Contextualização da aplicação
REST Pizza é uma aplicação fictícia cujo foco é gerenciar os pedidos realizados em uma pizzaria, onde o usuário, através de uma interface qualquer que ainda será construída, faz a reserva e recebe como resposta o tempo médio em que seu pedido ficará pronto para que o mesmo possa retirá-lo e fazer o pagamento no local.

Para que o foco seja mantido em como REST pode ser implementado na prática, questões relacionadas com validações e regras de negócio complexas foram ignoradas, mesmo com o código-fonte bem organizado e estruturado para atender o problema dessa aplicação. 

A aplicação conta com duas entidades de domínio principais, que são “Pizza” e “Pedido” e no próprio pedido vão os dados inerentes ao cliente associado a ele.

## Pré-requisitos
Aplicativo que auxilie na execução de requisições REST, de preferência o POSTMAN, pois com ele será permitido importar esses passos que podem ser acessados através do arquivo “RESTPizza.json.postman_collection” no diretório raíz do repositório git.

Visual Studio Express 2013 (software livre) ou posterior.

## Como executar a aplicação
Por tratar-se de uma aplicação "self-hosted", que utiliza o padrão "Owin", basta compilar e executar o projeto "RESTPizza", mesmo ele sendo do tipo "Console Application" que um servidor web local será iniciado na porta "1234" pronto para ser acessado através de requisições REST.
