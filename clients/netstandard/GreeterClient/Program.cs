// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;

namespace GreeterClient
{
    class Program
    {
        static bool shouldExit = false;
        
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            try
            {
                Console.WriteLine("GRPC demo netstandard client v 1.0");

                if (args.Length < 1)
                {
                    throw new ArgumentException("IP addres of server is not passed");
                }
                var address = args[0];
                Console.WriteLine($"Connecting to: {address}");

                var count = 0;
                do
                {
                    Channel channel = new Channel(address, ChannelCredentials.Insecure);

                    var client = new Greeter.GreeterClient(channel);
                    String user = $"netstandard [{++count}]";

                    var reply = client.SayHello(new HelloRequest { Name = user });
                    Console.WriteLine(">> " + reply.Message);

                    channel.ShutdownAsync().Wait();

                    Task.Delay(500).Wait();
                } while (!shouldExit);

                Console.WriteLine("Terminating...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Work is interrupted: {e}");
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            shouldExit = true;
        }
    }
}
