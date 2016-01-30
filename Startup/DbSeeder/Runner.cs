namespace ProcessingTools.DbSeeder
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Contracts;

    internal class Runner
    {
        private readonly string defaultSeederInterfaceName = typeof(IDbSeeder).FullName;

        public async Task Run()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var seederTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i => i.FullName == this.defaultSeederInterfaceName));

            foreach (var seederType in seederTypes)
            {
                try
                {
                    Console.WriteLine(seederType.FullName);
                    var seeder = Activator.CreateInstance(seederType) as IDbSeeder;
                    await seeder.Seed();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.Source);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}