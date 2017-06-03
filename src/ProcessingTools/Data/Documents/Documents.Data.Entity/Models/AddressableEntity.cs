namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Models.Abstractions;

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
