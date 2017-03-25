function AddressesViewModel(addresses) {
    var self = this, count = 0;

    self.isModified = ko.observable(false);
    self.count = ko.observable(count);
    self.addresses = ko.observableArray([]);

    function rebind(addresses) {
        var i, len, address;

        self.addresses.removeAll();
        count = 0;
        self.count(count);

        if (Array.isArray(addresses)) {
            len = addresses.length;
            for (i = 0; i < len; i += 1) {
                address = addresses[i];
                self.addresses.push(new Address(address.Id, address.AddressString, address.CityId, address.CountryId, 0));
                count += 1;
            }

            self.count(count);
        }

        self.isModified(false);
    }

    rebind(addresses);

    self.addAddress = function () {
        self.addresses.push(new Address(-1, '', '', '', 2));
        count = self.count() + 1;
        self.count(count);
        self.isModified(true);
    };

    self.removeAddress = function (address) {
        if (address) {
            self.addresses.remove(address);
            count = self.count() - 1;
            self.count(count);
            if (address.status() < 2) {
                self.addresses.push(new Address(address.id, address.addressString(), address.cityId(), address.countryId(), 3));
                self.isModified(true);
            }
        }
    };

    self.json = ko.computed(function () {
        var i, len, isModified, address, data = [];

        len = self.addresses().length;
        for (i = 0; i < len; i += 1) {
            try {
                address = self.addresses()[i];
                isModified = address.addressString.isModified() || address.cityId.isModified() || address.countryId.isModified();
                data.push({
                    Id: address.id,
                    AddressString: address.addressString(),
                    CityId: address.cityId(),
                    CountryId: address.countryId(),
                    Status: address.status() === 0 ? isModified ? 1 : 0 : address.status()
                });

                self.isModified(isModified || self.isModified());
            } catch (e) {
                console.log(e);
            }
        }

        return JSON.stringify(data);
    }, self);

    self.save = function (url) {
        return {
            invoke: function () {
                $.ajax({
                    url: url,
                    method: 'POST',
                    contentType: 'application/json',
                    data: self.json(),
                    success: function (data) {
                        rebind(data);
                    },
                    error: function (error) {
                        alert(JSON.stringify(error));
                    }
                });
            }
        };
    };
}