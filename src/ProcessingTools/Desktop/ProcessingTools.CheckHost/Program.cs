// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CheckHost
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Main program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            string portArgument = null;

            TcpClient tcpClient = null;

            try
            {
                string addressArgument = args[0];
                portArgument = args[1];

                int portNumber;
                portNumber = int.Parse(portArgument);

                tcpClient = new TcpClient();
                tcpClient.ReceiveTimeout = tcpClient.SendTimeout = 2000;

                if (IPAddress.TryParse(args[0], out IPAddress address))
                {
                    var endPoint = new IPEndPoint(address, portNumber);
                    tcpClient.Connect(endPoint);
                }
                else
                {
                    tcpClient.Connect(addressArgument, portNumber);
                }

                Console.WriteLine("Port {0} is listening.", portArgument);
            }
            catch (Exception e)
            {
                if (e is SocketException || e is TimeoutException)
                {
                    Console.WriteLine("Not listening on port {0}.", portArgument);
                }
                else
                {
                    Console.WriteLine("Usage:");
                    Console.WriteLine("CheckHost [host|ip] [port]");
                }
            }
            finally
            {
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
            }
        }
    }
}
