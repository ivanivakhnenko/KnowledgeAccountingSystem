angular.module("KAS").controller("EditUser", ["$scope", "$http", "mvcModel", function ($scope, $http, mvcModel) {
    $scope.model = mvcModel;

    $scope.editMode = false;

    $scope.save = function (valid) {

        if (valid) {
            $http.post("", JSON.stringify($scope.model))
                .success(function (data) {
                    $scope.model = data;
                });
            $scope.editMode = false;
        }
    };

    $scope.cancel = function () {
        $scope.model = mvcModel;
        $scope.editMode = false;
    };

    $scope.edit = function (bool) {
        $scope.editMode = bool;
    };
}]);
