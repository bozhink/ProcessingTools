namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public class HelpProvider : IHelpProvider
    {
        private readonly IReporter reporter;

        public HelpProvider(IReporter reporter)
        {
            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            this.reporter = reporter;
        }

        public Task GetHelp()
        {
            throw new NotImplementedException();
        }
    }
}
