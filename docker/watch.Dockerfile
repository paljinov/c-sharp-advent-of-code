FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS sdk

# VSDBG debugger
RUN apt-get update \
    && apt-get install -y --no-install-recommends unzip \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

WORKDIR /app
# ENTRYPOINT ["dotnet", "watch", "run"]
