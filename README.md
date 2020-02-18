## Prepare
### Create sub network
<pre>docker network create --subnet=172.18.0.0/16 testnet</pre>
_testnet_ is the name of sub-network that is used to test.

## Server
location: ./server

### Dev configuration

#### Prepare image
Dockerfile: ./server/dev.Dockerfile
<pre>docker build -t ubuntu-vs . -f dev.Dockerfile</pre>

#### Run docker container
<pre>docker run -p 5001:22 -d --name vs-build-env ubuntu-vs /bin/bash</pre>
It starts image exposes SSH port to host ann will use early created network testnet for communication.

#### Initialization
1) On first run of the image it should be added user that will be used by Visual Studio for connection.
<pre>
docker run -p 5001:22 -it --name vs-build-env ubuntu-vs /bin/bash
$ useradd -m -d /home/&lt;user-name&gt; -s /bin/bash -G sudo &lt;user-name&gt;
$ passwd &lt;user-name&gt;
</pre>
where &lt;user-name&gt; should be replaced to user name, like _develop_ for example

2) Should be ran _init.bat_ file that creates symb-link for PROTO folder. PROTO manifest is shared between server and all clients.
<pre>
./init.bat
</pre>

3) Visual Studio should have installed C++ Dev Tools for Linux.

#### Development
Run Visual Studio, from startup window select "Open local folder".
Select ./server/server folder
Once the IDE has loaded, you can add a SSH connection to your Linux docker container the same way you would add any other remote connection. Navigate to the Connection Manager (Tools > Options > Cross Platform > Connection Manager) and select “Add” to add a new remote connection.

Your host name should be “localhost”, the port should be whatever you are using for your SSH connection (in this example we’re using 5001), and your username and password should match the user account that you just created for your container.


### Stage configuration
Run server for tests

#### Prepare image
Dockerfile: ./server/stage.Dockerfile
<pre>docker build -t grpc/server . -f stage.Dockerfile</pre>

#### Run Server
<pre>docker run -p 5050:5050 -d --net testnet --name grpc-server grpc/server</pre>

## Clients
Sources ./clients

### NetStandard
Sources ./clients/netstandard

#### Build
<pre>dotnet publish .\Greeter.sln --configuration Release</pre>

#### Prepare image
<pre>docker build -t grpc/client/netcore:1.0 .</pre>

#### Run client
<pre>docker run -d --net testnet --name grpc-client-netcore grpc/client/netcore:1.0 &lt;ip-server:port&gt;</pre>

##### Test run
<pre>docker run -it --rm --net testnet --name grpc-client-netcore grpc/client/netcore:1.21 172.18.0.2:5050</pre>