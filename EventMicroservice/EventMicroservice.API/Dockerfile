#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EventMicroservice.API/EventMicroservice.API.csproj", "EventMicroservice.API/"]
COPY ["EventMicroservice.Domain/EventMicroservice.Domain.csproj", "EventMicroservice.Domain/"]
COPY ["EventMicroservice.EntityFramework/EventMicroservice.EntityFramework.csproj", "EventMicroservice.EntityFramework/"]
RUN dotnet restore "EventMicroservice.API/EventMicroservice.API.csproj"
COPY . .
WORKDIR "/src/EventMicroservice.API"
RUN dotnet build "EventMicroservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventMicroservice.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventMicroservice.API.dll"]