# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["Moto.Api/Moto.Api.csproj", "Moto.Api/"]
COPY ["Moto.BackgroundTasks/Moto.BackgroundTasks.csproj", "BackgroundTasks/"]
COPY ["Moto.Infraestructure/Moto.Infraestructure.csproj", "Moto.Infraestructure/"]
COPY ["Moto.Application/Moto.Application.csproj", "Moto.Application/"]
COPY ["Moto.Domain/Moto.Domain.csproj", "Moto.Domain/"]
COPY ["Moto.Persistence/Moto.Persistence.csproj", "Moto.Persistence/"]
RUN dotnet restore "./Moto.Api/Moto.Api.csproj"
COPY . .
WORKDIR "/src/Moto.Api"
RUN dotnet build "./Moto.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./Moto.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Moto.Api.dll"]