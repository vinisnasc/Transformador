# Gerenciamento de relatórios de testes de transformadores  

Esta web API foi desenvolvida para o processo seletivo da empresa HVEX.  

## Tecnologias  

- .NET 6.0;  
- ASP .NET Web API;  
- MongoDB;  
- Swagger;  
- Automapper;  
- FluentValidation;  
- Git;  
- Docker.  

## Detalhes
Esta API é estruturada com base no padrão DDD com projetos do tipo biblioteca de classe, além do projeto do tipo API. Apesar de não estar totalmente conforme a Clean Architecture e o Domain Driven Design, buscou-se seguir seus princípios na medida do possível. Além disso, a API conta com um repositório genérico.  
Mapeamentos foram feitos com auxílio da biblioteca Automapper e as validações com uso da biblioteca FluentValidation.

## User Stories

### Épico: GU - Gerenciamento de usuários  

GU1: É necessário o cadastro de usuários, contendo nome e e-mail.  
Critérios de aceite: Ao cadastrar um novo usuário, suas informações deverão persistir no banco de dados.  

GU2: É necessário atualizar os dados de um usuário, para poder corrigir eventuais erros de digitação.  
Critérios de aceite: Ao editar as informações de um usuário, seus dados atualizados devem persistir no banco de dados.

GU3: É necessário poder consultar os usuários cadastrados.  
Critérios de aceite: Deve haver uma listagem geral de todos os usuários e a possibilidade de busca de um usuário específico, com informações mais detalhadas.  

### Épico: GT - Gerenciamento de transformadores  

GT1: É necessário o cadastro de transformadores, contendo nome, numeração interna, classe de tensão, potencia e corrente. Deve-se levar em consideração também que um transformador deve estar relacionado a um usuário.  
Critérios de aceite: Ao cadastrar um novo transformador, suas informações deverão persistir no banco de dados.  

GT2: É necessário atualizar os dados de um transformador, para poder corrigir eventuais erros de digitação.  
Critérios de aceite: Ao editar as informações de um transformador, seus dados atualizados devem persistir no banco de dados.

GT3: É necessário poder consultar os transformadores cadastrados.  
Critérios de aceite: Deve haver uma listagem geral de todos os transformadores e a possibilidade de busca de um transformador específico, com informações mais detalhadas.  

### Épico: GTt - Gerenciamento de testes  

GTt1: É necessário o cadastro de testes, contendo nome, status e duração em segundos. Deve-se levar em consideração também que um teste deve estar relacionado a um transformador.  
Critérios de aceite: Ao cadastrar um novo teste, suas informações deverão persistir no banco de dados.  

GTt2: É necessário atualizar os dados de um teste, para poder corrigir eventuais erros de digitação.  
Critérios de aceite: Ao editar as informações de um teste, seus dados atualizados devem persistir no banco de dados.

GTt3: É necessário poder consultar os testes cadastrados.  
Critérios de aceite: Deve haver uma listagem geral de todos os testes, uma listagem geral com todos os testes ativos e a possibilidade de busca de um teste específico, com informações mais detalhadas.  

GTt4: É necessário a inativação de um teste.  
Critérios de aceite: Deve haver a possibilidade de desativar um teste. Ao ser desativado, caso haja algum relatório relacionado a ele, também será desativado.  

### Épico: GR - Gerenciamento de relatórios  

GR1: É necessário o cadastro de relatórios, contendo nome e status. Deve-se levar em consideração também que um relatório deve estar relacionado a um teste ativo.  
Critérios de aceite: Ao cadastrar um novo relatório, suas informações deverão persistir no banco de dados.  

GR2: É necessário atualizar os dados de um relatório, para poder corrigir eventuais erros de digitação.  
Critérios de aceite: Ao editar as informações de um relatório, seus dados atualizados devem persistir no banco de dados.

GR3: É necessário poder consultar os relatórios cadastrados.  
Critérios de aceite: Deve haver uma listagem geral de todos os relatórios, uma listagem geral com todos os relatórios ativos e a possibilidade de busca de um relatório específico, com informações mais detalhadas.  

GR4: É necessário a inativação de um relatório.  
Critérios de aceite: Deve haver a possibilidade de desativar um relatório.  

## Instruções de Utilização:

### Docker:

Caso tenha o Docker desktop instalado e em execução, execute os comandos:  
```console
$ docker-compose build  
$ docker-compose up  
```

### Visual Studio

Caso tenha o `git` instalado, é possível clonar este repositório com os seguintes comandos:

```console
$ git clone https://github.com/viniciusnasc/Transformador
```

Também é possível baixar o conteúdo em formato `.zip` clicando no botão "Clone" na [homepage](https://github.com/viniciusnasc/Transformador) do repositório.

Certifique-se de ter o .NET instalado, caso não tenha, é possível fazer download do .NET 6 [na página oficial](https://dotnet.microsoft.com/download/dotnet) da Microsoft.  
Também é preciso estar com o banco de dados MongoDB instalado, é possível fazer download do mongoDB [na página oficial](https://www.mongodb.com/)

A string de conexão com o banco deve ser definida. Vá até o arquivo `appsettings.json`, que se encontra em Transformador.API e edite a ConnectionString da chave MongoDbSettings.  

**Compilar e executar com CLI do .NET:**

A seguir, abra o prompt de comando no diretório do projeto Anuncios.API e digite:

```console
$ dotnet run
```

Abra o navegador e acesse `https://localhost:7003/swagger/index.html`

**Compilar e executar com Visual Studio:**

Abra o diretório do projeto contendo o arquivo `Transformador.sln` e clique duas vezes. Isso abrirá a IDE Visual Studio com todos os arquivos pertencentes ao projeto. Aperte a tecla F5 ou então clique no botão com ícone verde contendo o nome Transformador.API ou IIS Express, na barra de ferramentas. O navegador será automaticamente aberto com o URL `https://localhost:7003/swagger/index.html`

**Exemplo de utilização da API:**

Com o Swagger aberto, basta interagir com a API. Para cadastrar um novo usuario (POST), siga o exemplo de request, informando os dados solicitados.  
Para exibir os dados do usuário, o método GET trará informações completas de todos os usuários, conforme o Id informado.  
Para alterar os dados de um usuário, o método PUT pode ser utilizado, basta informar o ID e os dados novos do usuário.
