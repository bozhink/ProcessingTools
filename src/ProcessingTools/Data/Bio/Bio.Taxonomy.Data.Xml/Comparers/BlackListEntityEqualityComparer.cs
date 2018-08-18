namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Comparers
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class BlackListEntityEqualityComparer : EqualityComparer<IBlackListItem>
    {
        public override bool Equals(IBlackListItem x, IBlackListItem y) => (x?.Content ?? string.Empty) == (y?.Content ?? string.Empty);

        public override int GetHashCode(IBlackListItem obj) => obj?.Content.GetHashCode() ?? -1;
    }
}
