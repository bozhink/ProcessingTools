namespace ProcessingTools.Journals.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Models.Contracts.Journals;
    using ProcessingTools.Models.Abstractions;

    public abstract class Addressable : ModelWithUserInformation, IAddressable
    {
        private ICollection<Address> addresses;

        protected Addressable()
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
