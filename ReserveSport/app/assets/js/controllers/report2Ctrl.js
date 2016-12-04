'use strict';

app.controller('Report2Ctrl', ["$scope", "$rootScope", "$http", function ($scope, $rootScope, $http) {
    $rootScope.app.layout.titleoption = 'Reporte de Mensajes';
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

    $scope.gdvReport2_headerCellTemplate = function (container) {
        $('<span title="Refrescar" />')
            .attr('class', 'fa fa-refresh fa-lg')
            .attr('style', 'cursor: pointer')
            .on('dxclick', function () { $scope.list_Report2(); })
            .appendTo(container);
    };

    $scope.list_Report2 = function () {
        $('#gdvReport2').dxDataGrid({ loadPanel: { enabled: false } });
        $('#dlpcustomLoad').dxLoadPanel({ position: { of: $('#gdvReport2'), at: 'center' } });
        $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', true);

        $http({ method: 'POST', url: 'http://localhost:28665/listarcolamensajes', headers: { 'Content-Type': 'application/json' } })
        .success(function (data) {
            $('#gdvReport2').dxDataGrid({ dataSource: data, loadPanel: { enabled: false } });
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
        })
        .error(function (ex) {
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
            DevExpress.ui.notify(ex.Message, 'error', 3000);
            console.clear();
        });
    };
}]);