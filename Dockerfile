FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY published/ ./
ENTRYPOINT ["dotnet", "SimpleTodo.Api.dll"]