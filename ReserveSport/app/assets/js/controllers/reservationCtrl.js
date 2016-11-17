'use strict';

app.factory('rService', ["$http", function ($http) {
    return {
        sharedVar: {
            COD_RESE: 0
        }
    };
}]);


app.controller('ReservationCtrl', ["$scope", "$http", "$rootScope", "rService", function ($scope, $http, $rootScope, rService) {
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
        if (options.data.IND_ESTA === 'Pendiente') {
            $('<span title="Confirmar" />')
               .attr('class', 'fa fa-check fa-lg')
               .attr('style', 'cursor: pointer;color: #4cae4c')
               .on('dxclick', function () { $scope.ShowConfirm(options.data); })
               .appendTo(container);

            //$('<span>&nbsp;</span>')
            //   .appendTo(container);

            //$('<span title="Visualizar" />')
            //   .attr('class', 'fa fa-binoculars fa-lg')
            //   .attr('style', 'cursor: pointer;color: #337ab7')
            //   .on('dxclick', function () { })
            //   .appendTo(container);

            $('<span>&nbsp;</span>')
               .appendTo(container);

            $('<span title="Cancelar" />')
               .attr('class', 'fa fa-ban fa-lg')
               .attr('style', 'cursor: pointer;color: #c9302c')
               .on('dxclick', function () { })
               .appendTo(container);
        }
    };

    $scope.ShowConfirm = function (row) {
        rService.sharedVar.COD_RESE = row.COD_RESE
        $('#dpp_Confirm').dxPopup('instance').show();
        setTimeout(function () {
            $http({
                method: 'GET', url: 'http://localhost:2588/ServiceApp/Reserva.svc/Buscar_Pedido/' + row.COD_PEDI, headers: { 'Content-Type': 'application/json; charset=utf-8' }
            }).success(function (data) {
                $scope.COD_PEDI = data.Buscar_PedidoResult.COD_PEDI;
                $scope.MON_PAGA = data.Buscar_PedidoResult.MON_PAGA;
                $scope.MON_PAGO = data.Buscar_PedidoResult.MON_PAGO;
                $scope.MON_DEUD = data.Buscar_PedidoResult.MON_DEUD;
            }).error(function (ex) {
                DevExpress.ui.notify(ex.Message, 'error', 4000);
                console.clear();
            });
        }, 0);
    };

    $scope.SaveConfirm = function () {
        $('#dlpcustomLoad1').dxLoadPanel({ message: 'Guardando...', position: { of: $('#dpp_Confirm'), at: 'center' } });
        $('#dlpcustomLoad1').dxLoadPanel('instance').option('visible', true);

        var uri = 'http://localhost:2588/ServiceApp/Reserva.svc/Registrar_Reserva';
        var method = 'POST';
        if (rService.sharedVar.COD_RESE !== 0) {
            uri = 'http://localhost:2588/ServiceApp/Reserva.svc/Actualizar_Reserva';
            method = 'PUT';
        }

        var COD_PEDI = $('#dtbCOD_PEDI').dxTextBox('instance').option('value');
        var MON_PAGA = $('#dtbMON_PAGA').dxTextBox('instance').option('value');
        var MON_SALD = $('#dtbMON_SALD').dxNumberBox('instance').option('value');

        var obj = {
            COD_PEDI: COD_PEDI,
            MON_PAGA: MON_PAGA,
            MON_PAGO: MON_SALD
        };

        var pars = JSON.stringify(obj);
        $http({ method: method, url: uri, data: pars, headers: { 'Content-Type': 'application/json; charset=utf-8' } })
        .success(function (data) {
            $('#dlpcustomLoad1').dxLoadPanel('instance').option('visible', false);
            $('#dpp_Confirm').dxPopup('instance').hide();
            $scope.ClearControls();
            DevExpress.ui.notify('Operación concretada con éxito.', 'success', 4000);
        })
        .error(function (ex) {
            $('#dlpcustomLoad1').dxLoadPanel('instance').option('visible', false);
            DevExpress.ui.notify(ex.Message, 'error', 4000);
            console.clear();
        });
    };

    $scope.dpp_Confirm_toolbarItems = [
        {
            location: 'after', toolbar: 'bottom', widget: 'dxButton', options: {
                hint: 'Guardar', icon: 'save', width: 50, onClick: $scope.SaveConfirm
            }
        }
    ];

}]);