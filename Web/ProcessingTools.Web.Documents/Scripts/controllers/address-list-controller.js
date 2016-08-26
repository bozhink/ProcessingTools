(function (angular) {
    'use strict';

    var id = 0;

    function getId() {
        return id += 1;
    }

    function addToSet(array, item) {
        var i, len, currentItem;

        if (!item) {
            return;
        }

        item.addressString = item.addressString || '';
        item.cityId = item.cityId | 0 || NaN;
        item.countryId = item.countryId | 0 || NaN;
        if (item.addressString === '' || isNaN(item.cityId) || isNaN(item.countryId)) {
            return;
        }

        array = array || [];
        if (!Array.isArray(array)) {
            array = [];
        }

        for (i = 0, len = array.length; i < len; i += 1) {
            currentItem = array[i];
            if (currentItem.addressString === item.addressString &&
                currentItem.cityId === item.cityId &&
                currentItem.countryId === item.countryId) {
                return;
            }
        }

        array.push(item);
    }

    angular.module('addressApp', [])
        .controller('AddressListController', function AddressListController() {
            var addressList = this;
            addressList.addresses = [];

            addressList.addAddress = function () {
                addToSet(addressList.addresses, {
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