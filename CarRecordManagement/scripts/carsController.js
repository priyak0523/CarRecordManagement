//Register controller with module 
app.controller('carsController', function ($scope, cars) {
    $scope.cars = cars;
    $scope.sortColumn = "mileage";
    $scope.selectedcolumn = 'mileage';
    $scope.revertColumn = false;

    $scope.sortData = function (column) {
        $scope.revertColumn = ($scope.sortColumn == column) ? !$scope.revertColumn : false;
        $scope.sortColumn = column;
    }

    $scope.getSort = function (column) {
        if ($scope.sortColumn == column) {
            return $scope.revertColumn ? 'arrow-down' : 'arrow-up'
        }
        return '';
    }

});