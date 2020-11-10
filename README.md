# C# (C-Sharp) Advent of Code solutions

## Tasks solutions:
* [2015](./src/Tasks/2015)
* [2016](./src/Tasks/2016)

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
    docker exec -it c-sharp-advent-of-code_app_1 /bin/bash
    ```
3. Run watcher:
    ```sh
    dotnet watch run
    ```
### Run source code by publishing the application and its dependencies:
1. Build and compose app container
    ```sh
    docker-compose -f docker-compose.yml -f docker-compose.publish.yml up -d --force-recreate --build
    ```
2. Get a bash shell in the container running container: 
    ```sh
    docker exec -it c-sharp-advent-of-code_app_1 /bin/bash
    ```
3. Run assembly:
    ```sh
    dotnet AdventOfCode.dll
    ```

Default build configuration is "Debug", change "BUILD_CONFIGURATION" ARG to "Release" if needed.

### Check logs:
```sh
docker-compose logs -ft app
```
