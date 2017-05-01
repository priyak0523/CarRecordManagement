app.value('pageMethods', PageMethods);

app.factory('cars', function (pageMethods, $rootScope) {
    var result = [];
    pageMethods.GetCars(function (data) {
        data.forEach(function (item) {
            result.push({ mileage: item.mileage, name: item.name, model: item.model, engine: item.engine, color: item.color });
        });
        $rootScope.$apply();
    });
    return result;
});