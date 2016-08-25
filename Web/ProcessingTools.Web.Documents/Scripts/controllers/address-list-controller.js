(function (angular) {
    'use strict';

    var id = 0;

    function getId() {
        return id += 1;
    }

    angular.module('addressApp', [])
        .controller('AddressListController', function AddressListController() {
            var addressList = this;
            addressList.addresses = [];

            addressList.addAddress = function () {
                addressList.addresses.push({
                    id: getId(),
                    addressString: addressList.addressString,
                    cityId: addressList.cityId,
                    countryId: addressList.countryId
                });
                addressList.addressString = '';
                addressList.cityId = '';
                addressList.countryId = '';
            };

            addressList.removeAddress = function (id) {
                var i, len, addresses = addressList.addresses;

                if (id) {
                    for (i = 0, len = addresses.length; i < len; i += 1) {
                        if (addresses[i].id === id) {
                            addresses.splice(i, 1);
                            break;
                        }
                    }
                }
            };
        });
}(window.angular));