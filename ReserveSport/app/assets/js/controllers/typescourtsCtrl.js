'use strict';

app.controller('TypesCourtsCtrl', ["$scope", "$rootScope", "$http", function ($scope, $rootScope, $http) {
    $rootScope.app.layout.titleoption = 'Tipos de Cancha';
    $scope.hgt = $('#app').height() - 55;

    function IsNullOrWhiteSpace(value) {
        if (typeof value === 'undefined' || value === null || value === '') return true;
        return value.toString().replace(/\s/g, '').length < 1;
    };

    $scope.list_TypesCourts = function () {
        $('#gdvTypesCourts').dxDataGrid({ loadPanel: { enabled: false } });
        $('#dlpcustomLoad').dxLoadPanel({ position: { of: $('#gdvTypesCourts'), at: 'center' } });
        $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', true);

        $http({ method: 'GET', url: 'http://localhost:2588/ServiceApp/TipoCancha.svc/tipocanchas', headers: { 'Content-Type': 'application/json' } })
        .success(function (data) {
            $('#gdvTypesCourts').dxDataGrid({ dataSource: data, loadPanel: { enabled: false } });
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
        })
        .error(function (ex) {
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
            DevExpress.ui.notify(ex.Message, 'error', 3000);
            //console.clear();
        });
    };

    $scope.list_TypesSport = function () {
        $http({ method: 'GET', url: 'http://localhost:2588/ServiceApp/TipoDeporte.svc/tipodeportes', headers: { 'Content-Type': 'application/json' } })
        .success(function (data) {
            $('#dsbCOD_TIPO_DEPO').dxSelectBox({ dataSource: data });
        })
        .error(function (ex) {
            DevExpress.ui.notify(ex.Message, 'error', 3000);
            console.clear();
        });
    };

    // MÉTODO PARA EXPORTAR A EXCEL
    $scope.ExportTo = function () {
        var rowsCount = $('#gdvTypesCourts').dxDataGrid('instance').totalCount();
        if (rowsCount === 0) {
            DevExpress.ui.notify('No existen registros para exportar.', 'warning', 3000);
            return;
        }
        $('#gdvTypesCourts').dxDataGrid({ loadPanel: { enabled: true, text: 'Exportando...' } });
        $('#gdvTypesCourts').dxDataGrid('instance').exportToExcel(false);
    };

    $scope.gdvTypesCourts_cellTemplate = function (container, options) {
        $('<span title="Editar" />')
           .attr('class', 'fa fa-pencil fa-lg')
           .attr('style', 'cursor: pointer;color: #4cae4c')
           .on('dxclick', function () { $scope.ShowEdit(options.data); })
           .appendTo(container);

        $('<span>&nbsp;</span>')
           .appendTo(container);

        $('<span title="Eliminar" />')
           .attr('class', 'fa fa-trash-o fa-lg')
           .attr('style', 'cursor: pointer;color: #c9302c')
           .on('dxclick', function () { $scope.Delete(options.data); })
           .appendTo(container);
    };

    $scope.gdvTypesCourts_headerCellTemplate = function (container) {
        $('<span title="Refrescar" />')
            .attr('class', 'fa fa-refresh fa-lg')
            .attr('style', 'cursor: pointer')
            .on('dxclick', function () { $scope.list_TypesCourts(); })
            .appendTo(container);

        $('<span>&nbsp;</span>')
           .appendTo(container);

        $('<span title="Nuevo" />')
            .attr('class', 'fa fa-plus-circle fa-lg')
            .attr('style', 'cursor: pointer')
            .on('dxclick', function () { $scope.ShowNew(); })
            .appendTo(container);

        $('<span>&nbsp;</span>')
           .appendTo(container);

        //$('<span title="Exportar a Excel" />')
        //    .attr('class', 'fa fa-file-excel-o fa-lg')
        //    .attr('style', 'cursor: pointer; height: 18px')
        //    .on('dxclick', function () { $scope.ExportTo(); })
        //    .appendTo(container);
    };

    $scope.ClearControls = function () {
        $scope.COD_TIPO_CANC = '';
        $scope.ALF_TIPO_CANC = '';
        $scope.COD_TIPO_DEPO = null;
        $scope.NUM_JUGA = 0;
        $scope.MON_PREC = 0;

        $('#dtbALF_TIPO_CANC').dxValidator('instance').reset();
        $('#dsbCOD_TIPO_DEPO').dxValidator('instance').reset();
        $('#dnbNUM_JUGA').dxValidator('instance').reset();
        $('#dnbMON_PREC').dxValidator('instance').reset();
    };

    $scope.ShowEdit = function (row) {
        $('#dpp_Maintenance').dxPopup('instance').show();
        setTimeout(function () {
            $http({
                method: 'GET', url: 'http://localhost:2588/ServiceApp/TipoCancha.svc/tipocanchas/' + row.COD_TIPO_CANC, headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.COD_TIPO_CANC = data.COD_TIPO_CANC;
                $scope.ALF_TIPO_CANC = data.ALF_TIPO_CANC;
                $scope.COD_TIPO_DEPO = data.COD_TIPO_DEPO;
                $scope.NUM_JUGA = data.NUM_JUGA;
                $scope.MON_PREC = data.MON_PREC;
            }).error(function (ex) {
                DevExpress.ui.notify(ex.Message, 'error', 4000);
                console.clear();
            });
        }, 0);
    };

    $scope.ShowNew = function () {
        $('#dpp_Maintenance').dxPopup('instance').show();
        setTimeout(function () {
            $scope.ClearControls();
        }, 0);
    };

    $scope.Save = function () {
        var result = DevExpress.validationEngine.validateGroup('ValMaintenance');
        if (result.isValid) {
            var uri = 'http://localhost:2588/ServiceApp/TipoCancha.svc/tipocanchas';
            var method = 'POST';
            var COD_TIPO_CANC = $('#dtbCOD_TIPO_CANC').dxTextBox('instance').option('value');
            if (!IsNullOrWhiteSpace(COD_TIPO_CANC)) {
                method = 'PUT';
            }
            else {
                COD_TIPO_CANC = 0;
            }

            var ALF_TIPO_CANC = $('#dtbALF_TIPO_CANC').dxTextBox('instance').option('value');
            var COD_TIPO_DEPO = $('#dsbCOD_TIPO_DEPO').dxSelectBox('instance').option('value');
            var NUM_JUGA = $('#dnbNUM_JUGA').dxNumberBox('instance').option('value');
            var MON_PREC = $('#dnbMON_PREC').dxNumberBox('instance').option('value');
            var obj = {
                COD_TIPO_CANC: COD_TIPO_CANC,
                ALF_TIPO_CANC: ALF_TIPO_CANC,
                COD_TIPO_DEPO: COD_TIPO_DEPO,
                NUM_JUGA: NUM_JUGA,
                MON_PREC: MON_PREC
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
                    $('#dpp_Maintenance').dxPopup('instance').hide();
                    $scope.list_TypesCourts();
                    DevExpress.ui.notify('Operación concretada con éxito.', 'success', 4000);
                }
            })
            .error(function () {
                DevExpress.ui.notify('Error interno.', 'error', 4000);
                console.clear();
            });
        }
    };

    $scope.Delete = function (row) {
        var result = DevExpress.ui.dialog.confirm('Desea eliminar el tipo de cancha (' + row.ALF_TIPO_CANC + ')?', 'Confirmar Operación');
        result.done(function (dialogResult) {
            if (dialogResult) {
                var uri = 'http://localhost:2588/ServiceApp/TipoCancha.svc/tipocanchas/' + row.COD_TIPO_CANC;
                var method = 'DELETE';

                $http({
                    method: method,
                    url: uri,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .success(function (data) {
                    if (!IsNullOrWhiteSpace(data.ALF_MNSG_ERRO)) {
                        DevExpress.ui.notify(data.ALF_MNSG_ERRO, 'error', 4000);
                    }
                    else {
                        $scope.list_TypesCourts();
                        DevExpress.ui.notify('Operación concretada con éxito.', 'success', 4000);
                    }
                })
                .error(function () {
                    DevExpress.ui.notify('Error interno.', 'error', 4000);
                    //console.clear();
                });
            }
        });
    };

    $scope.dpp_Maintenance_toolbarItems = [
        {
            location: 'after', toolbar: 'bottom', widget: 'dxButton', options: {
                hint: 'Guardar', icon: 'save', width: 50, onClick: $scope.Save
            }
        }
    ];
}]);