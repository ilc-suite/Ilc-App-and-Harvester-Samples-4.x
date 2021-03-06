﻿//Register the controller to the ilc.framework.angular module
angular.module('ilc.framework.angular')

    .controller('com-ilc-technologies-companiesDetailsCtrl', ['$scope', function ($scope) {
        $scope.model = {
            getGoogleMapsUrl: function () {
                var url = 'http://maps.google.com/maps?q=';
                //the current detail item can be accessed via $scope.item;
                var address = $scope.item.Value.Addresses[0];
                url += address.Address + address.Zip + address.City;
                return url;
            }
        };
    }]);