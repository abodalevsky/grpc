FROM mcr.microsoft.com/dotnet/core/runtime:2.1.15-bionic
LABEL maintainer "abodalevsky@hotmail.com"
LABEL description="Run gRPC netCore 2.11 client container"

RUN mkdir /usr/local/gclient

COPY GreeterClient/bin/Release/netcoreapp2.1/publish/*.dll /usr/local/gclient/
COPY GreeterClient/bin/Release/netcoreapp2.1/publish/*.json /usr/local/gclient/
COPY GreeterClient/bin/Release/netcoreapp2.1/publish/runtimes/linux/native/*.* /usr/local/gclient/runtimes/linux/native/

WORKDIR /usr/local/gclient

ENTRYPOINT ["dotnet", "GreeterClient.dll"]
