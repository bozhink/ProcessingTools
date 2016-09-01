namespace ProcessingTools.Documents.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using ProcessingTools.Common.Models;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;

    public abstract class AddressableEntity : ModelWithUserInformation, IAddressableEntity
    {
        private ICollection<Address> addresses;

        public AddressableEntity()
        {
            this.addresses = new HashSet<Address>();
        }

        public virtual ICollection<Address> Addresses
        {
            get
            {
                return this.addresses;
            }

            set
            {
                this.addresses = value;
            }
        }

        [NotMapped]
        ICollection<IAddressEntity> IAddressableEntity.Addresses => this.Addresses.ToList<IAddressEntity>();
    }
}
