﻿namespace ProcessingTools.Data.Models.Entity.Documents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Models.Contracts.Documents;

    public abstract class AddressableEntity : ProcessingTools.Models.Contracts.ModelWithUserInformation, IAddressable
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