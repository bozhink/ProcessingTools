namespace ProcessingTools.Web.Settings
{
    using System.Configuration;
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Configuration;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(configure =>
            {
                configure.FromAssembliesMatching(
                    $"{nameof(ProcessingTools)}.*.{FileConstants.DllFileExtension}")
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.History.Data.Entity.Contracts.IHistoryDbContext>()
                .To<ProcessingTools.History.Data.Entity.HistoryDbContext>()
                .WhenInjectedInto<ProcessingTools.History.Data.Entity.Repositories.EntityHistoryRepository>()
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.HistoryDatabaseConnection].ConnectionString);

            this.Bind<ProcessingTools.Contracts.Data.History.Repositories.IHistoryRepository>()
                .To<ProcessingTools.History.Data.Entity.Repositories.EntityHistoryRepository>()
                .InRequestScope();

            this.Bind<ProcessingTools.Journals.Data.Entity.Contracts.IJournalsDbContext>()
                .To<ProcessingTools.Journals.Data.Entity.JournalsDbContext>()
                .WhenInjectedInto(typeof(ProcessingTools.Data.Common.Entity.Repositories.GenericRepository<,>))
                .InRequestScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.JournalsDatabaseConnection].ConnectionString);

            this.Bind<ProcessingTools.Contracts.Data.Journals.Repositories.IPublishersRepository>()
                .To<ProcessingTools.Journals.Data.Entity.Repositories.EntityPublishersRepository>()
                .InRequestScope();
        }
    }
}
