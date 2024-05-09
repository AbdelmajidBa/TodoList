# Simple Todo List

## 1. Build and deploy manually

Go to SimpleTodo.Api folder and run the following command: 
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

ENV ASPNETCORE_HTTP_PORTS 5000

COPY published/ ./
ENTRYPOINT ["dotnet", "SimpleTodo.Api.dll"]
```
Let’s break down the Dockerfile step by step:

1. **FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime**
    - This line specifies the base image for the Docker container. It uses the official .NET ASP runtime image with version 8.0.
    - The `AS runtime` part assigns a stage name to this image. This alias can be referenced later in the Dockerfile.

2. **WORKDIR /app**
    - This line sets the working directory inside the container to `/app`. Any subsequent commands will be executed relative to this directory.

3. **ENV ASPNETCORE_HTTP_PORTS 5000**
    - This line sets the environment variable named `ASPNETCORE_HTTP_PORTS` with the value `5000`.
    - Environment variables play a crucial role in configuring application behavior within the container.
    - In this specific case, our application will listen on port 5000.

4. **COPY published/ ./**
    - This line copies files from the host machine (outside the container) into the container.
    - It copies the contents of the `published/` directory (relative to the Dockerfile location) into the current working directory (`/app`) inside the container.

5. **ENTRYPOINT ["dotnet", "SimpleTodo.Api.dll"]**
    - The `ENTRYPOINT` specifies the command that will be executed when the container starts.
    - In this case, it runs the `dotnet SimpleTodo.Api.dll` command, which likely starts an ASP.NET Core web application (assuming `SimpleTodo.Api.dll` is the main application assembly).

### Docker image
Now, to create our image, we can build the Dockerfile using the following command:

```bash
podman build --rm -t simple-todo:1.0 .
```
Although I'm using Podman instead of Docker, the instructions remain the same.

```bash
 > podman images
REPOSITORY               TAG     IMAGE ID      CREATED         SIZE
localhost/simple-todo    1.0     b083ce73a02d  26 minutes ago  224 MB
```

### Docker container

This is the command to run a container using Podman.

```bash
 podman run --name todo-api -p 5000:5000 -d simple-todo:1.0
```

This command wille create and start a container named "todo-api" (--name option) using the "simple-todo:1.0" image, mapping port 5000 on the host to port 5000 in the container, and running the container in detached mode (-d option).

Browse to http://localhost:5000/tasks to see the results.


## 2. Build and deploy with Docker

Now, I will attempt to build and deploy my application's source code within a container. To achieve this, let's create our new Dockerfile.

### Dockerfile
```Docker
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

ENV ASPNETCORE_HTTP_PORTS 5000

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "SimpleTodo.Api.dll"]
```

### Docker image
Create a new image from Dockerfile:
```bash
podman build -t simple-todo:5.0 -f Dockerfile-build-and-publish .
```

### Docker container

To run our container, use the following command  
```bash
 podman run --name todo-api-2 -p 5001:8080 -d simple-todo:5.0.
```

Browse to http://localhost:5001/tasks to see the results.


Enjoy! 😊
