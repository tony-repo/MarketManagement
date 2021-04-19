# FAQs(Frequently Asked Questions)

[TOC]
1. [Face SSL connection and remote certificate problem](#Server)

## Docker

1. Q: Face SSL connection and remote certificate problem

    > [build 4/7] RUN dotnet restore "MarketManagement/MarketManagement.csproj":
        #12 0.894   Determining projects to restore...
        #12 4.731 /usr/share/dotnet/sdk/5.0.202/NuGet.targets(131,5): error : Unable to load the service index for source https://api.nuget.org/v3/index.json. [/src/MarketManagement/MarketManagement.csproj]
        #12 4.731 /usr/share/dotnet/sdk/5.0.202/NuGet.targets(131,5): error :   The SSL connection could not be established, see inner exception. [/src/MarketManagement/MarketManagement.csproj]
        #12 4.731 /usr/share/dotnet/sdk/5.0.202/NuGet.targets(131,5): error :   The remote certificate is invalid because of errors in the certificate chain: PartialChain [/src/MarketManagement/MarketManagement.csproj]

        A: Follow below step to fix this problem

            1. Open https://api.nuget.org/v3/index.json and then download the certificates

            2. Run below command to fix this problem

            ```
                COPY ./support/certs/*.crt /usr/local/share/ca-certificates/
                RUN update-ca-certificates --fresh --verbose    
            ```

2. Q: When build and run in local, it will throw **'No such image sha 256 ......'**

        A: The reason is caused by old cache, so we need to use below command to remove old cache

            ```
                docker-compose ps
                docker-compose rm
            ```

## Server