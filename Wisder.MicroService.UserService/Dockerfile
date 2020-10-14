#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Wisder.MicroService.UserService/Wisder.MicroService.UserService.csproj", "Wisder.MicroService.UserService/"]
RUN dotnet restore "Wisder.MicroService.UserService/Wisder.MicroService.UserService.csproj"
COPY . .
WORKDIR "/src/Wisder.MicroService.UserService"
RUN dotnet build "Wisder.MicroService.UserService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wisder.MicroService.UserService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wisder.MicroService.UserService.dll"]