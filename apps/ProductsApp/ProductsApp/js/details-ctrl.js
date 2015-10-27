angular.module('ilc.framework.angular')

    .controller('com-ilc-technologies-productsDetailsCtrl', ['$scope', '$debouncedApplyProvider', function ($scope, $debouncedApplyProvider) {

        //The $debouncedApplyProvider wraps a function that executes a debounced $scope.$apply()
        var debouncedApply = $debouncedApplyProvider();

        $scope.model = {
            loadingPhoto: false,
        };

        function formatPhotoObject(obj) {
            //add a source string for img tag
            obj.ImgSrcString = 'data:image/' + getImageType(obj.LargePhotoFileName) + ';base64,' + obj.LargePhotoB64;
        }

        function getImageType(name) {
            var arr = name.split('.');
            return arr[arr.length - 1];
        }


        //The init function requests the details for a detail link when the item is opend in the detail view
        (function init() {
            //check if details have already been collected
            if (typeof $scope.item.Value.ProductPhoto == "undefined") {

                //extracts the detail link by name. This example uses the lodah libraray (see: https://lodash.com/)
                var link = _.find($scope.item.Details, function (x) { return x.Name === "ProductPhoto"; });

                if (typeof (link) != "undefined" && typeof (link) != null) {
                    //display a loading spinner
                    $scope.model.loadingPhoto = true;

                    //gets details for the detail link
                    ilc.information.getDetails(link, function (detailData) {

                        //check if detailData is null (e.g. if a exception occurs in the harvester)
                        if (detailData != null) {
                            formatPhotoObject(detailData);
                            $scope.item.Value.ProductPhoto = detailData;
                        }
                        $scope.model.loadingPhoto = false;

                        //Do an apply to initiate the rendering of the view
                        debouncedApply();

                    });

                }
            }
        })();
    }]);