#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app
ENV CLUSTER_SEEDS "[]"
ENV CLUSTER_IP ""
ENV CLUSTER_PORT "5054"
EXPOSE 5054

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Engine/Engine.csproj", "Engine/"]
RUN dotnet restore "Engine/Engine.csproj"
COPY . .
WORKDIR "/src/Engine"
RUN dotnet build "Engine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Engine.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Engine.dll"]