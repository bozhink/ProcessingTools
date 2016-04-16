namespace ProcessingTools.DbSeeder
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Contracts;

    using Ninject;

    internal class Runner
    {
        private readonly string defaultSeederInterfaceName = typeof(IDbSeeder).FullName;

        public async Task Run()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var seederTypes = assembly.GetTypes()
                .Where(t => t.IsInterface && !t.IsGenericType)
                .Where(t => t.GetInterfaces().Any(i => i.FullName == this.defaultSeederInterfaceName));

            using (IKernel kernel = NinjectConfig.CreateKernel())
            {
                foreach (var seederType in seederTypes)
                {
                    try
                    {
                        var seeder = kernel.Get(seederType);
                        Console.WriteLine(seeder.GetType().FullName);
                        await (seeder as IDbSeeder).Seed();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.Source);

                        var foregroundColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(e.ToString());
                        Console.ForegroundColor = foregroundColor;
                    }
                }
            }
        }
    }
}