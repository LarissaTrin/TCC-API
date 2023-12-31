#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/nightly/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 7024
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/nightly/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Project.API/Project.API.csproj", "Project.API/"]
COPY ["src/Project.Application/Project.Application.csproj", "Project.Application/"]
COPY ["src/Project.Domain/Project.Domain.csproj", "Project.Domain/"]
COPY ["src/Project.Persistence/Project.Persistence.csproj", "Project.Persistence/"]
RUN dotnet restore "Project.API/Project.API.csproj"

WORKDIR "/src/Project.API"
COPY . .
RUN dotnet build "Project.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/nightly/aspnet:7.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.API.dll"]
