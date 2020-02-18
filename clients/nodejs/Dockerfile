FROM node:12.16.0-alpine3.11
LABEL maintainer "abodalevsky@hotmail.com"
LABEL description="Run gRPC NodeJS 12.16 client container"

WORKDIR /usr/src/app
COPY ./clients/nodejs/package.json ./
COPY ./clients/nodejs/greeter_client.js ./
COPY ./protos/*.* ./protos/

RUN npm i

ENTRYPOINT ["node", "greeter_client.js"]
