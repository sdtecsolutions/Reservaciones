'use strict';

app.controller('TypesSportCtrl', ["$scope", "$rootScope", "$http", function ($scope, $rootScope, $http) {
    $rootScope.app.layout.titleoption = 'Tipos de Deporte';
    $scope.hgt = $('#app').height() - 55;

    function IsNullOrWhiteSpace(value) {
        if (typeof value === 'undefined' || value === null || value === '') return true;
        return value.toString().replace(/\s/g, '').length < 1;
    };

    $scope.list_TypesSport = function () {
        $('#gdvTypesSport').dxDataGrid({ loadPanel: { enabled: false }, height: $scope.hgt });
        $('#dlpcustomLoad').dxLoadPanel({ position: { of: $('#gdvTypesSport'), at: 'center' } });
        $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', true);

        $http({ method: 'GET', url: 'http://localhost:2588/ServiceApp/TipoDeporte.svc/tipodeportes', headers: { 'Content-Type': 'application/json' } })
        .success(function (data) {
            $('#gdvTypesSport').dxDataGrid({ dataSource: data, loadPanel: { enabled: false } });
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
        })
        .error(function (ex) {
            $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', false);
            DevExpress.ui.notify(ex.Message, 'error', 3000);
            console.clear();
        });
    };

    // MÉTODO PARA EXPORTAR A EXCEL
    $scope.ExportTo = function () {
        var rowsCount = $('#gdvTypesSport').dxDataGrid('instance').totalCount();
        if (rowsCount === 0) {
            DevExpress.ui.notify('No existen registros para exportar.', 'warning', 3000);
            return;
        }
        $('#gdvTypesSport').dxDataGrid({ loadPanel: { enabled: true, text: 'Exportando...' } });
        $('#gdvTypesSport').dxDataGrid('instance').exportToExcel(false);
    };

    $scope.gdvTypesSport_cellTemplate = function (container, options) {
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

    $scope.gdvTypesSport_headerCellTemplate = function (container) {
        $('<span title="Refrescar" />')
            .attr('class', 'fa fa-refresh fa-lg')
            .attr('style', 'cursor: pointer')
            .on('dxclick', function () { $scope.list_TypesSport(); })
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
        $scope.COD_TIPO_DEPO = '';
        $scope.ALF_TIPO_DEPO = '';

        $('#dtbALF_TIPO_DEPO').dxValidator('instance').reset();
    };

    $scope.ShowEdit = function (row) {
        $('#dpp_Maintenance').dxPopup('instance').show();
        setTimeout(function () {
            $http({
                method: 'GET', url: 'http://localhost:2588/ServiceApp/TipoDeporte.svc/tipodeportes/' + row.COD_TIPO_DEPO, headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.COD_TIPO_DEPO = data.COD_TIPO_DEPO;
                $scope.ALF_TIPO_DEPO = data.ALF_TIPO_DEPO;
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
            var uri = 'http://localhost:2588/ServiceApp/TipoDeporte.svc/tipodeportes';
            var method = 'POST';
            var COD_TIPO_DEPO = $('#dtbCOD_TIPO_DEPO').dxTextBox('instance').option('value');
            if (!IsNullOrWhiteSpace(COD_TIPO_DEPO)) {
                method = 'PUT';
            }
            else {
                COD_TIPO_DEPO = 0;
            }

            var ALF_TIPO_DEPO = $('#dtbALF_TIPO_DEPO').dxTextBox('instance').option('value');
            var obj = {
                COD_TIPO_DEPO: COD_TIPO_DEPO,
                ALF_TIPO_DEPO: ALF_TIPO_DEPO
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
                    $scope.list_TypesSport();
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
        var result = DevExpress.ui.dialog.confirm('Desea eliminar el tipo de deporte (' + row.ALF_TIPO_DEPO + ')?', 'Confirmar Operación');
        result.done(function (dialogResult) {
            if (dialogResult) {
                var uri = 'http://localhost:2588/ServiceApp/TipoDeporte.svc/tipodeportes/' + row.COD_TIPO_DEPO;
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
                        $scope.list_TypesSport();
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