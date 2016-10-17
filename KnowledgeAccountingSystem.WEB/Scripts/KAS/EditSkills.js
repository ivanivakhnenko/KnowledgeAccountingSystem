angular.module("KAS").controller("EditSkills", ["$scope", "$http", "mvcModel", function ($scope, $http, mvcModel) {
    $scope.model = mvcModel;
    $scope.editSkill = [];
    $scope.editCategory = [];

    $("#dialog-error").dialog({
        autoOpen: false,
        resizable: false,
        height: 180,
        modal: true,
        dialogClass: "no-close",
        buttons: {
            "Reload": function () {
                $scope.reload();
                $("#dialog-error p").empty();
                $(this).dialog("close");
            }
        }
    });

    $scope.reload = function () {
        $http.post("/EditSkills/GetSkills/", "")
            .success(function (data) {
                $scope.model = data;
            });

        $scope.editSkill = [];
        $scope.editCategory = [];
    };

    $scope.cancel = function () {
        $scope.editSkill = {};

        $scope.reload();
    };

    $scope.addSkill = function (id) {
        $http.post("/EditSkills/AddSkill/", {
            Name: "New Skill",
            Category: { Name: $scope.model[id].Name, Id: $scope.model[id].Id }
        })
            .success(function () {
                $scope.reload();
            });
    };

    $scope.triggerEditMode = function (category, skill) {
        $scope.editSkill = [];
        $scope.editCategory = [];
        if (skill === undefined) {
            $scope.editCategory[category] = true;
        } else {
            $scope.editSkill[category] = [];
            $scope.editSkill[category][skill] = true;
        }
    };

    $scope.addCategory = function() {
        $scope.model[$scope.model.length] = { Name: "New Category" };
        $scope.addSkill($scope.model.length - 1);
    };

    $scope.removeCategory = function(index) {
        $http.post("/EditSkills/RemoveCategory/", $scope.model[index])
            .success(function () {
                $scope.reload();
            });
    };

    $scope.removeSkill = function(parent, index) {
        $http.post("/EditSkills/RemoveSkill/", $scope.model[parent].Skills[index])
            .success(function () {
                $scope.reload();
            });
    };

    $scope.renameSkill = function (parent, index) {
        $http.post("/EditSkills/RenameSkill/", $scope.model[parent].Skills[index])
            .success(function () {
                $scope.reload();
            }).error(function (data) {
                $("#dialog-error p").append(data);
                $("#dialog-error").dialog("open");
            });
    };

    $scope.renameCategory = function (parent) {
        $http.post("/EditSkills/RenameCategory/", $scope.model[parent])
            .success(function () {
                $scope.reload();
            }).error(function (data) {
                $("#dialog-error p").append(data);
                $("#dialog-error").dialog("open");
            });
    };

}]);