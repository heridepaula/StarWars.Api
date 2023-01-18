# StarWars.Api

Esta Api tem o propósito de disponibilizar os dados dos planetas da franquia Star Wars, estes obtidos pela Api pública [https://swapi.dev/](https://swapi.dev/) e salvos em banco local SQLite.

## Como executar?
Antes de mais nada, valide se você possui o SDK .Net 6 instalado em seu computador

 - Abra o Windows PowerShell
 - Execute o comando abaixo
 
 ``` dotnet --list-sdks```

![image](https://user-images.githubusercontent.com/1659281/213291014-3eb5091f-2c61-4674-97d0-8276279e8f84.png)

Caso não possua a versão _**6.x**_, você poderá obte-la pelo link oficial da Mirosofit [clicando aqui](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

 - Vá até a pasta _**StarWars.WebApi**_ da aplicação no Windows PowerShell
 - Execute o comando abaixo para iniciar a aplicação
 ``` dotnet run --urls http://localhost:8076 ```
 
 ![image](https://user-images.githubusercontent.com/1659281/213294445-67ac99ca-2405-4c22-a486-ef810021c5f7.png)

Pronto! Agora é só acessar o link no browser de sua preferencia.
 [http://localhost:8076/swagger](http://localhost:8076/swagger)
 
![image](https://user-images.githubusercontent.com/1659281/213305311-c3284b67-f332-4c00-86be-3ee892d93d71.png)

## Estrutura

![map](https://user-images.githubusercontent.com/1659281/213304475-75052c57-b382-40cd-ad77-44ffeeb9b61f.jpg)

### Base de dados
 Foi utilizado o banco SQLite, um banco de dados simple que atende todos os requisitos propostos na aplicação. Foi utilizado o ORM Entityframework 6 para tratamento de todos os dados.
 
 ![map (1)](https://user-images.githubusercontent.com/1659281/213307895-4bbaf443-50c3-40f7-8ac3-bddf25a2e2a4.jpg)

 
 ### Logs
  Para a geração dos logs, foi utilizado o Serilog, uma das bibliotecas mais utilizadas para geração de logs, por ser fácil de se configurar e sua alta flexibilidade.
  Todos os logs gerados podem ser encontrados na pasta _**"\StarWars.Api\StarWars.WebApi\logs\"**_. É gerado um arquivo de logs para cada dia, no formato _**StarWarsApi{yyyymmdd}.txt**_.
  
  ## Testes
  Foram adicionados testes unitários para a aplicação utilizando o XUnit. para executá-los, execute os passos abaixo:
   - Abra o Windows PowerShell
   - Vá até a aplicação e abra a pasta _**StarWars.Api.UnitTests**_
   - Execute o comando
   ``` dotnet test --collect:"XPlat Code Coverage" ```
   - Ao final da execução, será criado um arquivo xml na pasta com o resultado dos testes _**TestResults\{guid}\coverage.cobertura.xml**_.
   - Para gerar o resultado da cobertura dos testes, primeiramente precisamos instalar o ReportGenerator utilizando o comando abaixo:
   ``` dotnet tool install -g dotnet-reportgenerator-globaltool ```
   - Agora podemos gerar o arquivo html que conterá o report completo sobre a cobertura do código, executando o código abaixo:
   ``` reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage_report ```
   - Será criado a pasta _**coverage_report**_, onde poderemos acessar o arquivo index.html, que conterá as informações de cobertura de código.
   
   ![image](https://user-images.githubusercontent.com/1659281/213311592-46b648b3-34fa-448a-be17-7a73bdce8cb6.png)

