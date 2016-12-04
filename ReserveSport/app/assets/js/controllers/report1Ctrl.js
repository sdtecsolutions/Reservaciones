'use strict';

app.controller('Report1Ctrl', ["$scope", "$rootScope", "$http", function ($scope, $rootScope, $http) {
    $rootScope.app.layout.titleoption = 'Reporte de Reservaciones';
    $scope.hgt = $('#app').height() - 55;

    Date.prototype.yyyymmdd = function () {
        var yyyy = this.getFullYear().toString();
        var mm = (this.getMonth() + 1).toString();
        var dd = this.getDate().toString();
        return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
    };

    Date.prototype.compare = function (b) {
        if (b.constructor !== Date) {
            throw "invalid_date";
        }

        return (isFinite(this.valueOf()) && isFinite(b.valueOf()) ?
                 (this > b) - (this < b) : NaN
               );
    };

    function IsNullOrWhiteSpace(value) {
        if (typeof value === 'undefined' || value === null || value === '') return true;
        return value.toString().replace(/\s/g, '').length < 1;
    };

    $scope.list_Report1 = function () {
        var result = DevExpress.validationEngine.validateGroup('ValReport');
        if (result.isValid) {
            var FEC_INIC = new Date($('#ddbFEC_INIC').dxDateBox('instance').option('value'));
            var FEC_FINA = new Date($('#ddbFEC_FINA').dxDateBox('instance').option('value'));
            var ind = FEC_INIC.compare(FEC_FINA);
            if (ind === 1) {
                DevExpress.ui.notify('Rango de fechas incorrecto.', 'warning', 3000);
            }
            else {
                $('#gdvReport1').dxDataGrid({ loadPanel: { enabled: false } });
                $('#dlpcustomLoad').dxLoadPanel({ position: { of: $('#gdvReport1'), at: 'center' } });
                $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', true);

                $http({ method: 'GET', url: 'http://localhost:2588/ServiceApp/Reserva.svc/reservas/' + FEC_INIC.yyyymmdd() + '/' + FEC_FINA.yyyymmdd(), headers: { 'Content-Type': 'application/json' } })
                .success(function (data) {
                    $('#gdvReport1').dxDataGrid({ dataSource: data, loadPanel: { enabled: false } });
                    $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
                })
                .error(function (ex) {
                    $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
                    DevExpress.ui.notify(ex.Message, 'error', 3000);
                    console.clear();
                });
            }
        }
    };
}]);