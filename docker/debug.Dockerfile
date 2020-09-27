FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS sdk
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Debug -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS runtime

# VSDBG debugger
RUN apt-get update \
    && apt-get install -y --no-install-recommends unzip \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

WORKDIR /app
COPY --from=sdk /app/out .
COPY ./src/Tasks ./src/Tasks
RUN find ./src/Tasks -type f ! -name 'input.txt' -delete

# ENTRYPOINT ["dotnet", "AdventOfCode.dll"]
