## .net 5 - Testes
Projeto criado como demo para testes na versao .net5, utilizando:
- xUnit
- MoQ
- FluentAssertions
- Bogus

Utilizado como base o curso Dominando os testes de software, disponivel em: https://desenvolvedor.io/curso-online-dominando-os-testes-de-software

## SonarQube
O CodeCoverage foi feito atraves do SonarQube

Instalacao do Coverlet e do SonarScanner:
```
dotnet tool install --global coverlet.console
dotnet tool install — global dotnet-sonarscanner
```

Docker Run:
```
docker run -d --name sonarqube -p 9000:9000 -p 9092:9092 sonarqube
```

Instalacao do pacote Coverlet no projeto .net
```
dotnet add package coverlet.msbuild
```

Comandos para geracao do arquivo em formato OpenCover e importacao para Docker SonarQube
- Project Key: net5_testes
- Token: bce7fa718c13dd154234ac746885a05d57418108

```
dotnet test Features.Tests/Features.Tests.csproj -p:CollectCoverage=true -p:CoverletOutputFormat=opencover

dotnet sonarscanner begin -k:"net5_testes" -d:sonar.host.url="http://localhost:9000" \
     -d:sonar.login="bce7fa718c13dd154234ac746885a05d57418108" \
     -d:sonar.cs.opencover.reportsPaths=Features.Tests/coverage.opencover.xml
     
dotnet build

dotnet sonarscanner end -d:sonar.login="bce7fa718c13dd154234ac746885a05d57418108"
```