namespace ProcessingTools.Tagger.Core
{
    using System;
    using Ninject;

    public static class DI
    {
        private static IKernel kernel;

        public static IKernel Kernel
        {
            get
            {
                if (kernel == null)
                {
                    throw new ApplicationException("Kernel is not set.");
                }

                if (kernel.IsDisposed)
                {
                    throw new ApplicationException("Kernel is disposed.");
                }

                return kernel;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Kernel should not be null.", nameof(Kernel));
                }

                if (value.IsDisposed)
                {
                    throw new ArgumentException("Kernel should not be disposed.", nameof(Kernel));
                }

                kernel = value;
            }
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static object Get(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Kernel.Get(type);
        }
    }
}
