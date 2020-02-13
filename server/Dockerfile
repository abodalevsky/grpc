FROM ubuntu:19.10 as build

# https://devblogs.microsoft.com/cppblog/build-c-applications-in-a-linux-docker-container-with-visual-studio/
LABEL description="Build container for gRPC test"

# install build dependencies
RUN apt-get update && apt-get install -y g++ gdb rsync zip openssh-server make git golang vim

# configure SSH for communication with Visual Studio
RUN mkdir -p /var/run/sshd
 
RUN echo 'PasswordAuthentication yes' >> /etc/ssh/sshd_config && \
   ssh-keygen -A
 
# expose port 22
EXPOSE 22