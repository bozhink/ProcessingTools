namespace ProcessingTools.Documents.Startup
{
    using System;
    using Microsoft.Owin.Hosting;

    public class Program
    {
        private const string BaseAddress = "http://localhost:9324/";

        public static void Main(string[] args)
        {
            using (var server = WebApp.Start<ProcessingTools.Web.Documents.Startup>(BaseAddress))
            {
                Console.WriteLine("Server is running: {0}", BaseAddress);
                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();
            }
        }
    }
}
