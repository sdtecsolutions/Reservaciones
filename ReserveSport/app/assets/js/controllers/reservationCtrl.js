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

    function IsNullOrWhiteSpace(value) {
        if (typeof value === 'undefined' || value === null || value === '') return true;
        return value.toString().replace(/\s/g, '').length < 1;
    };

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

            $('<span>&nbsp;</span>')
               .appendTo(container);

            $('<span title="Cancelar" />')
               .attr('class', 'fa fa-ban fa-lg')
               .attr('style', 'cursor: pointer;color: #c9302c')
               .on('dxclick', function () { $scope.Cancel(options.data); })
               .appendTo(container);
        }
    };

    $scope.ClearControls = function () {
        $scope.COD_PEDI = '';
        $scope.MON_PAGA = '';
        $scope.MON_PAGO = '';
        $scope.MON_DEUD = ''
        $scope.MON_SALD = '';
        $('#dtbMON_SALD').dxValidator('instance').reset();
    };

    $scope.ShowConfirm = function (row) {
        rService.sharedVar.COD_RESE = row.COD_RESE
        $('#dpp_Confirm').dxPopup('instance').show();
        setTimeout(function () {            
            $http({
                method: 'GET', url: 'http://localhost:2588/ServiceApp/Reserva.svc/pedidos/' + row.COD_PEDI, headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.COD_PEDI = data.COD_PEDI;
                $scope.MON_PAGA = data.MON_PAGA;
                $scope.MON_PAGO = data.MON_PAGO;
                $scope.MON_DEUD = data.MON_DEUD;
            }).error(function (ex) {
                DevExpress.ui.notify(ex.Message, 'error', 4000);
                console.clear();
            });            
        }, 0);
    };

    $scope.SaveConfirm = function () {
        var result = DevExpress.validationEngine.validateGroup('ValReserva');
        if (result.isValid) {
            var uri = 'http://localhost:2588/ServiceApp/Reserva.svc/reservas';
            var method = 'POST';
            if (rService.sharedVar.COD_RESE !== 0) {
                method = 'PUT';
            }

            var COD_PEDI = $('#dtbCOD_PEDI').dxTextBox('instance').option('value');
            var MON_PAGA = $('#dtbMON_PAGA').dxTextBox('instance').option('value');
            var MON_SALD = $('#dtbMON_SALD').dxNumberBox('instance').option('value');

            var obj = {
                COD_PEDI: COD_PEDI,
                MON_PAGA: MON_PAGA,
                MON_PAGO: MON_SALD,
                IND_CANC: false
            };

            var pars = JSON.stringify(obj);
            $http({
                method: method,
                url: uri,
                data: pars,
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .success(function (data) {
                if (!IsNullOrWhiteSpace(data.ALF_MNSG_ERRO)) {
                    DevExpress.ui.notify(data.ALF_MNSG_ERRO, 'error', 4000);
                }
                else {
                    $scope.ClearControls();
                    $('#dpp_Confirm').dxPopup('instance').hide();
                    $scope.list_Reservation();
                    DevExpress.ui.notify('Operación concretada con éxito.', 'success', 4000);
                }
            })
            .error(function () {
                DevExpress.ui.notify('Error interno.', 'error', 4000);
                console.clear();
            });
        }
    };

    $scope.Cancel = function (row) {
        var result = DevExpress.ui.dialog.confirm('Desea cancelar la reserva ' + row.ALF_NUME_PEDI + '?', 'Confirmar Operación');
        result.done(function (dialogResult) {
            if (dialogResult) {
                var uri = 'http://localhost:2588/ServiceApp/Reserva.svc/reservas';
                var method = 'PUT';

                var obj = {
                    COD_PEDI: row.COD_PEDI,
                    IND_CANC: true
                };

                var pars = JSON.stringify(obj);
                $http({
                    method: method,
                    url: uri,
                    data: pars,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .success(function (data) {
                    if (!IsNullOrWhiteSpace(data.ALF_MNSG_ERRO)) {
                        DevExpress.ui.notify(data.ALF_MNSG_ERRO, 'error', 4000);
                    }
                    else {
                        $scope.list_Reservation();
                        DevExpress.ui.notify('Operación concretada con éxito.', 'success', 4000);
                    }                    
                })
                .error(function () {
                    DevExpress.ui.notify('Error interno.', 'error', 4000);
                    console.clear();
                });
            }
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