FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProjectServ.WebApi/ProjectServ.WebApi.csproj", "ProjectServ.WebApi/"]
COPY ["ProjectServ.Application/ProjectServ.Application.csproj", "ProjectServ.Application/"]
COPY ["ProjectServ.Domain/ProjectServ.Domain.csproj", "ProjectServ.Domain/"]
COPY ["ProjectServ.Infrastructure/ProjectServ.Infrastructure.csproj", "ProjectServ.Infrastructure/"]

RUN dotnet restore "ProjectServ.WebApi/ProjectServ.WebApi.csproj"
COPY . .
WORKDIR "/src/ProjectServ.WebApi"
RUN dotnet build "ProjectServ.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectServ.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

#RUN dotnet tool install --global dotnet-ef --version 7.0.13
#ENV PATH="${PATH}:/root/.dotnet/tools"
#RUN dotnet ef database update

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectServ.WebApi.dll"]
