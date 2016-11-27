namespace ProcessingTools.DbSeeder.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Contracts.Core;
    using Contracts.Seeders;
    using Settings;

    using Ninject;

    public class Engine : IEngine
    {
        private readonly string defaultSeederInterfaceName = typeof(IDbSeeder).FullName;

        public async Task Run(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var seederTypes = assembly.GetTypes()
                .Where(t => t.IsInterface && !t.IsGenericType && t.GetInterfaces().Any(i => i.FullName == this.defaultSeederInterfaceName));

            using (IKernel kernel = NinjectConfig.CreateKernel())
            {
                var tasks = new ConcurrentQueue<Task>();
                foreach (var seederType in seederTypes)
                {
                    tasks.Enqueue(this.RunSeeder(kernel, seederType));
                }

                await Task.WhenAll(tasks);
            }
        }

        private async Task RunSeeder(IKernel kernel, Type seederType)
        {
            {
                var foregroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n{0}\n", seederType.FullName);
                Console.ForegroundColor = foregroundColor;
            }

            try
            {
                var seeder = kernel.Get(seederType);

                if (seederType == null)
                {
                    throw new ApplicationException("Seeder is not initialized correctly.");
                }

                Console.WriteLine(seeder.GetType().FullName);

                var castedSeeder = (IDbSeeder)seeder;
                await castedSeeder.Seed();
            }
            catch (Exception e)
            {
                var foregroundColor = Console.ForegroundColor;
                Console.WriteLine("{0}", seederType.FullName);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(e.ToString());
                Console.ForegroundColor = foregroundColor;
            }
        }
    }
}