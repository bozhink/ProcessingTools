namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Comparers
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    public class BlackListEntityEqualityComparer : EqualityComparer<IBlackListEntity>
    {
        public override bool Equals(IBlackListEntity x, IBlackListEntity y) => (x?.Content ?? string.Empty) == (y?.Content ?? string.Empty);

        public override int GetHashCode(IBlackListEntity obj) => obj?.Content.GetHashCode() ?? -1;
    }
}
