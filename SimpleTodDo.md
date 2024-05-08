# Simple Todo List

## 1. Build and deploy manually

Build and publish manually
```
dotnet build
dotnet publish -c Release -o published
```

Run the application locally from publised sources. 
```
 dotnet .\published\SimpleTodo.Api.dll 
```
Browse to http://localhost:5000/tasks to see the results.

The application is now running smoothly. We can proceed to create an image from the published sources and deploy our application within a container.

### Dockerfile

```docker
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY published/ ./
ENTRYPOINT ["dotnet", "SimpleTodo.Api.dll"]
```
### Docker image
Now, to create our image, we can build the Dockerfile using the following command:

```bash
podman build -t simple-todo:4.0 .
```
Although I'm using Podman instead of Docker, the instructions remain the same.

```bash
 > podman images
REPOSITORY               TAG     IMAGE ID      CREATED         SIZE
localhost/simple-todo    4.0     b083ce73a02d  26 minutes ago  224 MB
```

### Docker container

This is the command to run a container using Podman.

```bash
 podman run --name todo-api -p 5000:8080 -d simple-todo:4.0
```

This command wille create and start a container named "todo-api" (--name option) using the "simple-todo:4.0" image, mapping port 5000 on the host to port 8080 in the container, and running the container in detached mode (-d option).

Browse to http://localhost:5000/tasks to see the results.

Enjoy! 😊