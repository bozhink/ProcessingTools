namespace ProcessingTools.Web.Areas.Data.Models.Shared
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.Shared.Models_Strings;

    public class SynonymisableViewModel
    {
        [Display(Name = nameof(Strings.Synonyms), ResourceType = typeof(Strings))]
        public IEnumerable<SynonymViewModel> Synonyms { get; set; }
    }
}
