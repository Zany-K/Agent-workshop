## Setup
### Nugets
* ``dotnet add package OllamaSharp``
* ``dotnet add package MailKit``
* ``dotnet add package Microsoft.Agents.AI``
* ``dotnet add package Microsoft.Extensions.Configuration``
* ``dotnet add package Microsoft.Extensions.Configuration.Json``
* ``dotnet add package Microsoft.Extensions.Configuration.UserSecrets``
* ``dotnet add package SmartReader``
* ``dotnet add package System.ServiceModel.Syndication``

### Credentials to send email
* ``dotnet user-secrets init``
* ``dotnet user-secrets set "Smtp:User" "abc@email.com"``
* ``dotnet user-secrets set "Smtp:Password" "password"``