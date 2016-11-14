namespace ProcessingTools.NlmArchiveConsoleManager.Settings
{
    using System.IO;
    using System.Reflection;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ProcessingTools.Interceptors;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileReader>()
                .To<ProcessingTools.FileSystem.IO.XmlFileReader>();

            this.Bind<ProcessingTools.Contracts.Files.IO.IXmlFileWriter>()
                .To<ProcessingTools.FileSystem.IO.XmlFileWriter>()
                .Intercept()
                .With<FileExistsRaiseWarningInterceptor>();

            this.Bind<ProcessingTools.Contracts.Files.Generators.IFileNameGenerator>()
                .To<ProcessingTools.FileSystem.Generators.SequentialFileNameGenerator>()
                .InSingletonScope();
        }
    }
}
