angular.module("KAS").controller("EditRoles", ["$scope", "$http", "mvcModel", function ($scope, $http, mvcModel) {
    $scope.model = mvcModel;
    $scope.editMode = [];
    $scope.newRole = [];

    $http.post("/Account/GetRoles/", "")
        .success(function(data) {
            $scope.roles = data;
            for (var i = 0; i < data.length; i++) {
                if(data[i].Name === "admin")
                    $scope.roles.splice(i, 1);
            }
        });

    $scope.EnterEditMode = function(userid) {
        $scope.editMode = [];
        $scope.editMode[userid] = true;
    };

    $scope.AssignUser = function(user, role) {

        user.Role = role;

        $http.post("Assign", JSON.stringify(user))
            .success(function(data) {
                user = data;
            });

        $scope.editMode = [];
    };

    $scope.Update = function() {
        $http.post("/Account/GetUsers/", "")
        .success(function (data) {
            $scope.model = data;
        });
    };

    $scope.Cancel = function () {

        $scope.Update();

        $scope.editMode = [];
    };

}]);