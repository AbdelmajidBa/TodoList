FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS 5000

COPY published/ ./
ENTRYPOINT ["dotnet", "SimpleTodo.Api.dll"]