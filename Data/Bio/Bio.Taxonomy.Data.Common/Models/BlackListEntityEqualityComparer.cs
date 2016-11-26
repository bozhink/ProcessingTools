namespace ProcessingTools.Bio.Taxonomy.Data.Common.Models
{
    using System.Collections.Generic;
    using Contracts;

    public class BlackListEntityEqualityComparer : EqualityComparer<IBlackListEntity>
    {
        public override bool Equals(IBlackListEntity x, IBlackListEntity y) => (x?.Content ?? string.Empty) == (y?.Content ?? string.Empty);

        public override int GetHashCode(IBlackListEntity obj) => obj?.Content.GetHashCode() ?? -1;
    }
}
