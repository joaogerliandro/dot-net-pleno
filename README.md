# **dot-net-pleno**

Este repositório tem por finalidade, disponibilizar conteúdo para execução da avaliação para desenvolvedor pleno utilizando .Net 8 e SQL Server. 
A descrição da avaliação está descrita em nosso [Wiki](https://github.com/StallosTecnologia/dot-net-pleno/wiki "Wiki").

# **Observações**

## Sobre o banco de dados
O arquivo `SetupDatabase.sql` contem o script de criação do banco de dados, das tabelas e relacionamentos necessários para a API.

O script tambem contem a criação de um usuario padrão para realização das operações no banco de dados. O username e password padrões são `user` e `user`;

## Sobre o arquivo de configurações
O arquivo de configurações `appsettings.json` contem as chaves e constantes de configuração usadas pela applicação, abaixo um exemplo de configuração deste arquivo:

```javascript 
{
  "ConnectionStrings": {
    "DatabaseConnection": "Server=hostname;Database=project_database;User Id=user;Password=user;TrustServerCertificate=True;" // String de conexão do banco de dados
  },
  "Basic": {
    "BasicAuthUsername": "user", // Usuário utilizado na autenticação básica
    "BasicAuthPassword": "user" // Senha utilizada na autenticação básica
  },
  "Jwt": {
    "ApiSecret": "Stallos-Dotnet-Pleno-API-SECRET-2024" // String secret para geração do Token JWT para Autenticação Baerer da API principal
  },
  "Appsettings": {
    "RosterOAuthHostname": "https://stallosopendata.auth.us-east-2.amazoncognito.com/oauth2/", // Hostname da API de Autenticação do Roster
    "RosterApiHostname": "https://x5hn0kjhpl.execute-api.us-east-2.amazonaws.com/prd/roster/v2/", // Hostname da API de Processamento do Roster
    "RosterClientId": "", // Api Roster ClientId
    "RosterSecret": "", // Api Roster ClientSecret
    "RosterXApi": "" // Api Roster X-Api-Key
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
````
# Sobre a autenticação
Existem dois padrões de autenticação na aplicação: `Basic` e `Baerer Token`

## Basic:
Será utilizada somente para Geração de Token na API de Autenticação. Os campos de autenticação a serem informados devem ser:

`Username` => Deve ser informado o mesmo que está no `appsetting.json` </br>
`Password` => Deve ser informado o mesmo que está no `appsetting.json` </br>

## Baerer Token
Será utilizada durante a execução dos métodos da API de Pessoas. Os campos de autenticação a serem informados devem ser:

`BaererToken` => Token gerado pela API de Autenticação.

# Sobre os métodos da API
Os métodos possuem os caminhos de roteamento configurados de acordo com os informados na Wiki. Aqui temos uma breve descrição sobre a execução destes metodos:

## API de Autenticação

> GenerateToken (POST): `/auth/token` </br>
#### Esse é o método responsavel por gerar o Bearer Token para a Autenticação dos métodos da API de Pessoas.

## API de Pessoas

> GetAllPersons (GET): `/pessoa` <br>
#### Esse método retorna todas as pessoas registradas pela API no banco de dados
> GetPersonById (GET): `/pessoa/{id}` <br>
#### Esse método retorna uma pessoa registrada pela API no banco de dados. Os campos a serem informados devem ser:
`id` => Identificador unico da pessoa registrada no banco de dados (Parametro da Rota)
> UpdatePerson (PUT): `/pessoa/{id}` <br>
#### Esse método que atualiza uma pessoa registrada pela API no banco de dados. Os campos a serem informados devem ser:
`id` => Identificador unico da pessoa registrada no banco de dados (Parametro da Rota)
Além de que o seguinte corpo de requisição seve ser informado:
```javascript
{
    "Name": "João Gerliandro Silva Martins", // Nome completo a ser atualizado
    "Document": "077.357.653-39", // Numero do CPF/CNPJ com ou sem pontuação a ser atualizado
    "Type": "PF", // Tipo da Pessoa ("PF" => Pessoa Fisica, "PJ" => Pessoa Juridica) a ser atualizado
    "Addresses": [
        {
            "ZipCode": "62930000", // CEP a ser atualizado
            "Street": "Rua Rosa Tereza de Jesus", // Logradouro a ser atualizado
            "Number": "2263", // Numero da residencia a ser atualizado
            "District": "Antônio Holanda", // Bairro a ser atualizado
            "City": "Limoeiro do Norte", // Cidade a ser atualizado
            "StateCode": "CE" // Codigo do Estado (UF) a ser atualizado
        }
    ]
}
```
#### OBS: Esse método atualiza os campos de Person e as entidades relacionadas. A lista de endereços é substituida pela nova informada na atualização. 
> DeletePerson (DELETE): `/pessoa/{id}` <br>
#### Esse método que deleta uma pessoa registrada pela API no banco de dados. Os campos a serem informados devem ser:
`id` => Identificador unico da pessoa registrada no banco de dados (Parametro da Rota)
#### OBS: Esse método também deleta as entidades relacionadas a pessoa
> CreatePerson (POST): `/pessoa` <br>
#### O seguinte corpo de requisição seve ser informado:
```javascript
{
    "Name": "João Gerliandro Silva Martins", // Nome completo
    "Document": "077.357.653-39", // Numero do CPF/CNPJ com ou sem pontuação
    "Type": "PF", // Tipo da Pessoa ("PF" => Pessoa Fisica, "PJ" => Pessoa Juridica)
    "Addresses": [
        {
            "ZipCode": "62930000", // CEP
            "Street": "Rua Rosa Tereza de Jesus", // Logradouro
            "Number": "2263", // Numero da residencia
            "District": "Antônio Holanda", // Bairro
            "City": "Limoeiro do Norte", // Cidade
            "StateCode": "CE" // Codigo do Estado (UF)
        }
    ]
}
```

