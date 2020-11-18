# C# (C-Sharp) Advent of Code solutions

## Tasks solutions:
* [2015](./src/Tasks/2015)
* [2016](./src/Tasks/2016)

## Prerequisites for running project
Copy `env.example` to `.env` file and set year, day and part for task you wish to debug or run.

## Local development:

### Run watcher, runs source code on file change:
```sh
dotnet watch run
```

### Run source code:
```sh
dotnet run
```

## Docker development:

### Run watcher, run source code on file change:
1. Build and compose app container
    ```sh
    docker-compose -f docker-compose.yml -f docker-compose.watch.yml up -d --force-recreate --build
    ```
2. Get a bash shell in the container running container: 
    ```sh
    docker-compose exec app /bin/bash
    ```
3. Run watcher:
    ```sh
    dotnet watch run
    ```
### Debug configuration:
1. Build and compose app container
    ```sh
    docker-compose -f docker-compose.yml -f docker-compose.debug.yml up -d --force-recreate --build
    ```
2. Get a bash shell in the container running container: 
    ```sh
    docker-compose exec app /bin/bash
    ```
3. Run assembly:
    ```sh
    dotnet run
    ```

### Release configuration:
1. Build and compose app container
    ```sh
    docker-compose up -d --force-recreate --build
    ```
2. Get a bash shell in the container running container: 
    ```sh
    docker-compose exec app /bin/bash
    ```
3. Run assembly:
    ```sh
    dotnet AdventOfCode.dll
    ```

### Check logs:
```sh
docker-compose logs -ft app
```
