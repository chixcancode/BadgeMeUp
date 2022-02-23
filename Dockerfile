#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILDPROFILE=Release

WORKDIR /src
COPY ["BadgeMeUp.csproj", "."]
RUN dotnet restore "./BadgeMeUp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BadgeMeUp.csproj" -c ${BUILDPROFILE} -o /app/build

FROM build AS publish
RUN dotnet publish "BadgeMeUp.csproj" -c ${BUILDPROFILE} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BadgeMeUp.dll"]