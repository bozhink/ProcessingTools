namespace ProcessingTools.MainProgram
{
    using System.Reflection;

    using Ninject;

    public static class NinjectConfig
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