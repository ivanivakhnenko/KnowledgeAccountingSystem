angular.module("KAS").controller("TeamsView", ["$scope", "$http", "mvcModel", function ($scope, $http, mvcModel) {
    
    $scope.model = mvcModel;
    $scope.editMode = [];

    $scope.users = [];
    $scope.shownUsers = [];
    $scope.noUsersError = [];

    $("#dialog-error").dialog({
        autoOpen: false,
        resizable: false,
        height: 180,
        modal: true,
        dialogClass: "no-close",
        buttons: {
            "Reload": function () {
                $scope.reload();
                $(this).dialog("close");
            }
        }
    });

    $scope.reload = function () {
        $http.post("/TeamUp/GetTeams/", "")
            .success(function (data) {
                $scope.model = data;
            });

        $scope.editMode = [];
        $scope.users = [];
        $scope.shownUsers = [];
        $scope.noUsersError = [];
    };

    $scope.renameTeam = function (index) {
        $http.post("/TeamUp/RenameTeam", JSON.stringify($scope.model[index]))
            .success(function () {
                $scope.reload();
            }).error(function (data) {
                $("#dialog-error p").append(data);
                $("#dialog-error").dialog("open");
            });
    };

    $scope.addTeam = function () {

        var newteam = { Name: "New Team", Users: {} };
        $scope.model.push(newteam);
        
        $http.post("/TeamUp/AddTeam", JSON.stringify($scope.model[$scope.model.length - 1]))
            .success(function () {
                $scope.reload();
                $scope.editMode[$scope.model.length - 1] = true;
            });
    };

    $scope.triggerEditMode = function (index) {
        $scope.editMode = [];
        $scope.editMode[index] = true;
    };


    $scope.remove = function (index) {
        $http.post("/TeamUp/RemoveTeam", JSON.stringify($scope.model[index]))
            .success(function () {
                $scope.reload();
            });
    };

    $scope.getUsers = function (team, teamId) {
        $scope.noUsersError = [];
        $http.post("/TeamUp/GetContent", JSON.stringify(team))
            .success(function (data) {
                $scope.users[teamId] = data;
                if (data.length !== 0)
                    $scope.shownUsers[teamId] = true;
                else {
                    $scope.noUsersError[teamId] = true;
                }
            });
    };

    $scope.hideUsers = function(teamId) {
        $scope.shownUsers[teamId] = false;
    };
}]);