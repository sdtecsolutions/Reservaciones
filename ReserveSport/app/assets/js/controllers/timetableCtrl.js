'use strict';

app.controller('TimeTableCtrl', ["$scope", "$rootScope", "$http", function ($scope, $rootScope, $http) {
    $rootScope.app.layout.titleoption = 'Horarios';
    $scope.hgt = $('#app').height() - 55;

    Date.prototype.yyyymmdd = function () {
        var yyyy = this.getFullYear().toString();
        var mm = (this.getMonth() + 1).toString();
        var dd = this.getDate().toString();
        return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
    };

    function IsNullOrWhiteSpace(value) {
        if (typeof value === 'undefined' || value === null || value === '') return true;
        return value.toString().replace(/\s/g, '').length < 1;
    };

    $scope.list_TimeTable = function () {
        $('#gdvTimeTable').dxDataGrid({ loadPanel: { enabled: false }, height: $scope.hgt });
        $('#dlpcustomLoad').dxLoadPanel({ position: { of: $('#gdvTimeTable'), at: 'center' } });
        $('#dlpcustomLoad').dxLoadPanel('instance').option('visible', true);

        $http({ method: 'GET', url: 'http://localhost:2588/ServiceApp/Horario.svc/horarios', headers: { 'Content-Type': 'application/json' } })
        .success(function (data) {
            $('#gdvTimeTable').dxDataGrid({ dataSource: data, loadPanel: { enabled: false } });
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
        var rowsCount = $('#gdvTimeTable').dxDataGrid('instance').totalCount();
        if (rowsCount === 0) {
            DevExpress.ui.notify('No existen registros para exportar.', 'warning', 3000);
            return;
        }
        $('#gdvTimeTable').dxDataGrid({ loadPanel: { enabled: true, text: 'Exportando...' } });
        $('#gdvTimeTable').dxDataGrid('instance').exportToExcel(false);
    };

    $scope.gdvTimeTable_cellTemplate = function (container, options) {
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

    $scope.gdvTimeTable_headerCellTemplate = function (container) {
        $('<span title="Refrescar" />')
            .attr('class', 'fa fa-refresh fa-lg')
            .attr('style', 'cursor: pointer')
            .on('dxclick', function () { $scope.list_TimeTable(); })
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
        $scope.COD_HORA = '';
        $scope.HOR_INIC = null;
        $scope.HOR_FINA = null;

        $('#ddbHOR_INIC').dxValidator('instance').reset();
        $('#ddbHOR_FINA').dxValidator('instance').reset();
    };

    $scope.ShowEdit = function (row) {
        $('#dpp_Maintenance').dxPopup('instance').show();
        setTimeout(function () {
            $http({
                method: 'GET', url: 'http://localhost:2588/ServiceApp/Horario.svc/horarios/' + row.COD_HORA, headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.COD_HORA = data.COD_HORA;
                var today = new Date().yyyymmdd();
                $scope.HOR_INIC = new Date(today + ' ' + data.HOR_INIC);
                $scope.HOR_FINA = new Date(today + ' ' + data.HOR_FINA);
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
            var uri = 'http://localhost:2588/ServiceApp/Horario.svc/horarios';
            var method = 'POST';
            var COD_HORA = $('#dtbCOD_HORA').dxTextBox('instance').option('value');
            if (!IsNullOrWhiteSpace(COD_HORA)) {
                method = 'PUT';
            }
            else {
                COD_HORA = 0;
            }

            var HOR_INIC = $('#ddbHOR_INIC').dxDateBox('instance').option('text');
            var HOR_FINA = $('#ddbHOR_FINA').dxDateBox('instance').option('text');
            var obj = {
                COD_HORA: COD_HORA,
                HOR_INIC: HOR_INIC,
                HOR_FINA: HOR_FINA
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
                    $scope.list_TimeTable();
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
        var result = DevExpress.ui.dialog.confirm('Desea eliminar el horario (' + row.ALF_HORA + ')?', 'Confirmar Operación');
        result.done(function (dialogResult) {
            if (dialogResult) {
                var uri = 'http://localhost:2588/ServiceApp/Horario.svc/horarios/' + row.COD_HORA;
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
                        $scope.list_TimeTable();
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