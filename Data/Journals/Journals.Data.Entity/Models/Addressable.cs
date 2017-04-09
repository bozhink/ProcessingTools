namespace ProcessingTools.Journals.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ProcessingTools.Common.Models;
    using ProcessingTools.Contracts.Data.Journals.Models;

    public abstract class Addressable : ModelWithUserInformation, IAddressable
    {
        private ICollection<Address> addresses;

        public Addressable()
            : base()
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
        ICollection<IAddress> IAddressable.Addresses => this.Addresses.ToList<IAddress>();
    }
}
