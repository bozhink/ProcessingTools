namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Models.Abstractions;
    using ProcessingTools.Contracts.Models.Documents;

    public abstract class AddressableEntity : ModelWithUserInformation, IAddressable
    {
        private ICollection<Address> addresses;

        protected AddressableEntity()
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
        IEnumerable<IAddress> IAddressable.Addresses => this.Addresses;
    }
}
