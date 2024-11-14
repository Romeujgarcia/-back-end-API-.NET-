# Usar a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho
WORKDIR /app

# Copiar o csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o restante da aplicação
COPY . ./

# Compilar a aplicação
RUN dotnet publish -c Release -o out

# Criar a imagem final usando a imagem do runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Definir o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "TaskManagerAPI.dll"]




















