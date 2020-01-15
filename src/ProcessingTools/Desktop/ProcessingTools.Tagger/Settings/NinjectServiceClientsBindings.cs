﻿namespace ProcessingTools.Tagger.Settings
{
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Modules;

    /// <summary>
    /// NinjectModule to bind external service client objects.
    /// </summary>
    public class NinjectServiceClientsBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(typeof(ProcessingTools.Clients.Bio.ExtractHcmr.ExtractHcmrDataRequester).Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}