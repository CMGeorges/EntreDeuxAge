#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlazorEntre2Ages/BlazorEntre2Ages.csproj", "BlazorEntre2Ages/"]
RUN dotnet restore "BlazorEntre2Ages/BlazorEntre2Ages.csproj"
COPY . .
WORKDIR "/src/BlazorEntre2Ages"
RUN dotnet build "BlazorEntre2Ages.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorEntre2Ages.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorEntre2Ages.dll"]