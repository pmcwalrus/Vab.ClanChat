FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["src/ClanChat.ServiceHost/ClanChat.ServiceHost.csproj", "src/ClanChat.ServiceHost/"]
COPY ["src/ClanChat.Migrations/ClanChat.Migrations.csproj", "src/ClanChat.Migrations/"]
COPY ["src/ClanChat.Client/ClanChat.Client.csproj", "src/ClanChat.Client/"]
COPY ["src/ClanChat.Integration.EntityFramework/ClanChat.Integration.EntityFramework.csproj", "src/ClanChat.Integration.EntityFramework/"]
COPY ["src/ClanChat.Application/ClanChat.Application.csproj", "src/ClanChat.Application/"]
COPY ["src/ClanChat.Integration.HttpApi/ClanChat.Integration.HttpApi.csproj", "src/ClanChat.Integration.HttpApi/"]
COPY ["src/ClanChat.Integration.SignalR/ClanChat.Integration.SignalR.csproj", "src/ClanChat.Integration.SignalR/"]
RUN dotnet restore "src/ClanChat.ServiceHost/ClanChat.ServiceHost.csproj"
RUN dotnet restore "src/ClanChat.Migrations/ClanChat.Migrations.csproj"
RUN dotnet restore "src/ClanChat.Client/ClanChat.Client.csproj"
COPY . .
RUN dotnet build "src/ClanChat.ServiceHost/ClanChat.ServiceHost.csproj" -c Release -o /app/build
RUN dotnet build "src/ClanChat.Migrations/ClanChat.Migrations.csproj" -c Release -o /app/build
RUN dotnet build "src/ClanChat.Client/ClanChat.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/ClanChat.ServiceHost/ClanChat.ServiceHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClanChat.ServiceHost.dll"]

FROM build AS publish_ef
RUN dotnet publish "src/ClanChat.Migrations/ClanChat.Migrations.csproj" -c Release -o /app/publish
COPY ["src/ClanChat.Migrations/wait-for-it.sh", "/app/publish"]

FROM base AS final_ef
WORKDIR /app
COPY --from=publish_ef /app/publish .
ENTRYPOINT ["dotnet", "ClanChat.Migrations.dll"]

FROM build AS publish_client
RUN dotnet publish "src/ClanChat.Client/ClanChat.Client.csproj" -c Release -o /app/publish

FROM base AS final_client
WORKDIR /app
EXPOSE 5200
ENV ASPNETCORE_URLS=http://*:5200
COPY --from=publish_client /app/publish .
ENTRYPOINT ["dotnet", "ClanChat.Client.dll"]

