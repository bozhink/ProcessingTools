namespace ProcessingTools.MainProgram
{
    using System.Reflection;

    using Ninject;
    using Ninject.Extensions.Conventions;

    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Load(Assembly.GetExecutingAssembly());

                kernel.Bind(x =>
                {
                    x.FromThisAssembly()
                     .SelectAllClasses()
                     .BindDefaultInterface();
                });
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