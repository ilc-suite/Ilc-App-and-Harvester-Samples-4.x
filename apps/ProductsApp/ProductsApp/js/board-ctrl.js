angular.module('ilc.framework.angular')
    .controller('com-ilc-technologies-productsBoardCtrl', ['$scope', '$filter', '$timeout', '$rootScope', 'InformationService',
        function ($scope, $filter, $timeout, $rootScope, InformationService) {
        //------------------------------------------------------------------------
        // Init
        //------------------------------------------------------------------------

        function reset() {
            $scope.items = [];
            $scope.renderItemLimit = 30;
            $scope.selectedItem = null;
            $scope.selectFirstItemFired = false;
            $scope.model.filterQuery = '';
            $scope.model.orderBy = 'Value.Name';
        }

        function init() {
            reset();

            var unsubscribe = InformationService.subscribeInformationObjects($scope.appId, function (res) {

                res.on('new', function (informationObject) {
                    //This method is called for every new information object that is sent to the app.
                    $scope.items.push(informationObject);

                    if (!$scope.selectFirstItemFired) {

                        $timeout(function () {
                            $scope.selectFirstItem();
                        }, 500);

                        $scope.selectFirstItemFired = true;
                    }
                });

                res.on('changed', function (informationObject) {
                    //This method is called when an information object that was already sent to the app as new information has changed.
                    var found = _.find($scope.items, function (x) { return x.Id === informationObject.Id; });

                    if (found) {
                        found.Value = informationObject.Value;
                    }
                });

                res.on('removed', function (informationObject) {
                    //This method is called when an information object gets removed.
                    _.remove($scope.items, function (x) {
                        return x.Id === informationObject.Id;
                    });
                });

                res.on('reset', function () {
                    //This method is called when the current information objects should be cleared, e.g when a new infopoint is selected.
                    reset();
                });

            });

            $scope.$on('$destroy', function () {
                unsubscribe();
            });
        }

        init();

        //------------------------------------------------------------------------
        // Scope Methods
        //------------------------------------------------------------------------

        $scope.loadMore = function () {
            $scope.renderItemLimit += 30;
        };

        $scope.canLoadMore = function () {
            return $scope.renderItemLimit < $scope.items.length;
        };

        $scope.isSelected = function (item) {
            if ($scope.selectedItem) {
                return $scope.selectedItem.Id === item.Id;
            }

            return false;
        };

        $scope.selectItem = function (item) {
            $scope.selectedItem = item;
            $rootScope.$broadcast('ilc.notifyItemSelected', $scope.appId, item);
        };

        $scope.selectFirstItem = function () {
            if ($scope.items.length > 0 && !$scope.isDashboardHosted) {
                var firstItem = ($filter('orderBy')($scope.items, $scope.model.orderBy, false))[0];
                $scope.selectItem(firstItem);
            }
        };
    }]);