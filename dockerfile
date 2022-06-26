#.NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS dotnet

#Copy Projects
COPY /Application/Application.csproj ./Application/
COPY /BlogApi/BlogApi.csproj ./BlogApi/
COPY /Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY /Domain/Domain.csproj ./Domain/

#.NET Restore
RUN dotnet Restore 

#Copy All files
COPY / ./

#.NET Publish
RUN dotnet publish ./BlogApi/BlogApi.csproj -c Release -o /dist --no-restore

#.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=dotnet /dist .
ENTRYPOINT ["dotnet", "BlogApi.dll"]