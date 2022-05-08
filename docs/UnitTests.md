# Unit tests

Unit tests are enabled for debug and watch docker-compose.yml files.
For unit tests [xUnit](https://xunit.net/) is used.

## Local development:

### Run watcher, runs source code on file change:
```sh
dotnet watch test
```

### Run source code:
```sh
dotnet test
```

## Docker development:

### Run watcher, run source code on file change:
1. Build and compose tests container
    ```sh
    docker-compose -f docker-compose.yml -f docker-compose.watch.yml up -d --force-recreate --build tests
    ```
2. Get a bash shell in the container running container: 
    ```sh
    docker-compose -f docker-compose.yml -f docker-compose.watch.yml exec tests /bin/bash
    ```
3. Run watcher:
    ```sh
    dotnet watch test
    ```
### Debug configuration:
1. Build and compose tests container
    ```sh
    docker-compose -f docker-compose.yml -f docker-compose.debug.yml up -d --force-recreate --build tests
    ```
2. Get a bash shell in the container running container: 
    ```sh
    docker-compose -f docker-compose.yml -f docker-compose.debug.yml exec tests /bin/bash
    ```
3. Run assembly:
    ```sh
    dotnet test
    ```

## Filter tests:
Part:
```sh
dotnet test --filter "FullyQualifiedName~Tests.Tasks.Year2020.Day20.Part1"
```

Day:
```sh
dotnet test --filter "FullyQualifiedName~Tests.Tasks.Year2020.Day20"
```

Year:
```sh
dotnet test --filter "FullyQualifiedName~Tests.Tasks.Year2020"
```
