function Address(id, addressString, cityId, countryId, status) {
    var self = this;

    self.id = id;
    self.addressString = ko.observable(addressString).extend({ required: true, minLength: 3 });
    self.cityId = ko.observable(cityId).extend({ required: true });
    self.countryId = ko.observable(countryId).extend({ required: true });
    self.status = ko.observable(status);
}
