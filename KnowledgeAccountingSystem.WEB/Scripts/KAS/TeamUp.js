angular.module("KAS").controller("TeamUp", ["$scope", "$http", "mvcModel", function ($scope, $http, mvcModel) {
    $scope.model = mvcModel;

    $scope.selectedTeam = {};

    $("#dialog-error").dialog({
        autoOpen: false,
        resizable: false,
        height: 180,
        modal: true,
        dialogClass: "no-close",
        buttons: {
            "Ok": function () {
                $scope.cancel();
                $("#dialog-error p").empty();
                $(this).dialog("close");
            }
        }
    });

    $scope.save = function () {

        $scope.users = [];

        $http.post("/TeamUp/Index/", JSON.stringify($scope.model))
            .success(function (data) {
                $scope.users = data;

                $http.post("/TeamUp/GetTeams", "")
                .success(function (data) {
                    $scope.teams = data;
                });
            }).error(function (data) {
                $("#dialog-error p").append(data);

                if (!$scope.model.IncludeTeamed)
                    $("#dialog-error p").append(" Try to include users already in teams.");

                $("#dialog-error").dialog("open");
            });
    };

    $scope.cancel = function () {
        $scope.model = mvcModel;
        $scope.users = {};
    };

    $scope.AssignUser = function (user, team) {

        $http.post("/TeamUp/AddUserTeam", JSON.stringify({ UserId: user, TeamId: team }))
            .success(function () {
                $scope.save();
            });

    };

    $scope.removeTeam = function (user, team) {
        
        $http.post("/TeamUp/RemoveUserTeam", JSON.stringify({ UserId: user, TeamId: team }))
            .success(function () {
                $scope.save();
            });
    };

}]);