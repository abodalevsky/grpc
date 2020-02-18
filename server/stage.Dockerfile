FROM ubuntu:19.10 as runtime
LABEL maintainer "abodalevsky@hotmail.com"
LABEL description="Run gRPC CPP server container"

RUN apt-get update

RUN mkdir /usr/local/gserver
COPY server/out/build/Linux-Debug/gServer /usr/local/gserver/gServer
WORKDIR /usr/local/gserver
CMD ./gServer
EXPOSE 5050