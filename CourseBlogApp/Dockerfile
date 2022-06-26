#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CourseBlogApp/BlogAppAPI.csproj", "CourseBlogApp/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
RUN dotnet restore "CourseBlogApp/BlogAppAPI.csproj"
COPY . .
WORKDIR "/src/CourseBlogApp"
RUN dotnet build "BlogAppAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogAppAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogAppAPI.dll"]