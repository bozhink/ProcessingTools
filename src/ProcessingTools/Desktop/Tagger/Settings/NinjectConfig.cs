namespace ProcessingTools.Tagger.Settings
{
    using System.Reflection;
    using Ninject;

    internal static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Load(Assembly.GetExecutingAssembly());
            }
            catch
            {
                kernel.Dispose();
                throw;
            }

            return kernel;
        }
    }
}
