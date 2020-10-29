FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS sdk
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish -c $BUILD_CONFIGURATION -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS runtime

ARG BUILD_CONFIGURATION=Debug
RUN if [ "$BUILD_CONFIGURATION" = "Debug" ]; then \
        # Install VSDBG debugger
        apt-get update && apt-get install -y --no-install-recommends unzip \
        && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg ; \
    fi

WORKDIR /app
COPY --from=sdk /app/out .
COPY ./src/Tasks ./src/Tasks
RUN find ./src/Tasks -type f ! -name 'input.txt' -delete

# ENTRYPOINT ["dotnet", "AdventOfCode.dll"]
