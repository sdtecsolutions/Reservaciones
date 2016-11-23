'use strict';

app.controller('ReservationCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    $rootScope.app.layout.titleoption = 'Reservaciones Pendientes';
    $scope.hgt = $('#app').height() - 55;

    $scope.list_Reservation = function () {
        $('#gdvReservation').dxDataGrid({ height: $scope.hgt });
        $('#dlpcustomLoad').dxLoadPanel({ position: { of: $('#gdvReservation'), at: 'center' } });
        $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', true);

        $http({ method: 'POST', url: 'http://localhost:28665/listarreservaciones', headers: { 'Content-Type': 'application/json' } })
        .success(function (data) {
            $('#gdvReservation').dxDataGrid({ dataSource: data, loadPanel: { enabled: false } });
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
        })
        .error(function (ex) {
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
            DevExpress.ui.notify(ex.Message, 'error', 3000);
            console.clear();
        });
    };

    $scope.gdvReservation_headerCellTemplate = function (container) {
        $('<span title="Refrescar" />')
            .attr('class', 'fa fa-refresh fa-lg')
            .attr('style', 'cursor: pointer')
            .on('dxclick', function () { $scope.list_Reservation(); })
            .appendTo(container);
    };

    $scope.gdvReservation_cellTemplate = function (container, options) {
        if (options.data.IND_ESTA !== 'Pendiente') { return; }

        $('<span title="Confirmar" />')
           .attr('class', 'fa fa-check fa-lg')
           .attr('style', 'cursor: pointer;color: #4cae4c')
           .on('dxclick', function () { })
           .appendTo(container);

        $('<span>&nbsp;</span>')
           .appendTo(container);

        $('<span title="Visualizar" />')
           .attr('class', 'fa fa-binoculars fa-lg')
           .attr('style', 'cursor: pointer;color: #337ab7')
           .on('dxclick', function () { })
           .appendTo(container);

        $('<span>&nbsp;</span>')
           .appendTo(container);

        $('<span title="Cancelar" />')
           .attr('class', 'fa fa-ban fa-lg')
           .attr('style', 'cursor: pointer;color: #c9302c')
           .on('dxclick', function () { })
           .appendTo(container);
    };
}]);