'use strict';

app.controller('OrderCtrl', ["$scope", "$http", function ($scope, $http) {    
    Date.prototype.yyyymmdd = function () {
        var yyyy = this.getFullYear().toString();
        var mm = (this.getMonth() + 1).toString();
        var dd = this.getDate().toString();
        return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
    };

    $scope.tiposdocumentos = [
        { COD_TIPO_DOCU: 'DNI', ALF_TIPO_DOCU: 'DNI' },
        { COD_TIPO_DOCU: 'RUC', ALF_TIPO_DOCU: 'RUC' }
    ];

    $scope.ListTypesSport = function () {
        $http({ method: 'POST', url: 'http://localhost:28665/listardeportes', headers: { 'Content-Type': 'application/json; charset=utf-8' } })
        .success(function (data) {
            $('#dsbCOD_TIPO_DEPO').dxSelectBox({ dataSource: data });
        });
    };

    $scope.ListTypeCourts = function (COD_TIPO_DEPO) {
        var par = JSON.stringify(COD_TIPO_DEPO);
        $http({
            method: 'POST',
            url: 'http://localhost:28665/listarcanchas',
            data: par,
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        })
        .success(function (data) {
            $('#dsbCOD_TIPO_CANC').dxSelectBox({ dataSource: data });
        });
    };

    $scope.dsbCOD_TIPO_DEPO_onValueChanged = function (e) {
        $scope.ListTypeCourts(parseInt(e.value));
    };

    $scope.ListTimeTable = function () {
        $http({ method: 'POST', url: 'http://localhost:28665/listarhorarios', headers: { 'Content-Type': 'application/json; charset=utf-8' } })
        .success(function (data) {
            $('#dsbCOD_HORA').dxSelectBox({ dataSource: data });
        });
    };

    $scope.ClearControls = function () {
        $('#dsbALF_TIPO_DOCU').dxSelectBox('instance').option('value', null);
        $('#dtbALF_NUME_DOCU').dxTextBox('instance').option('value', '');
        $('#dtbALF_NOMB').dxTextBox('instance').option('value', '');
        $('#dtbALF_CORR').dxTextBox('instance').option('value', '');
        $('#dtbALF_NUME_TELE').dxTextBox('instance').option('value', null);
        $('#dsbCOD_TIPO_DEPO').dxSelectBox('instance').option('value', null);
        $('#dsbCOD_TIPO_CANC').dxSelectBox('instance').option('value', null);
        $('#ddbFEC_HORA_RESE').dxDateBox('instance').option('value', null);
        $('#dsbCOD_HORA').dxSelectBox('instance').option('value', null);        

        $('#dsbALF_TIPO_DOCU').dxValidator('instance').reset();
        $('#dtbALF_NUME_DOCU').dxValidator('instance').reset();
        $('#dtbALF_NOMB').dxValidator('instance').reset();
        $('#dtbALF_CORR').dxValidator('instance').reset();
        $('#dtbALF_NUME_TELE').dxValidator('instance').reset();
        $('#dsbCOD_TIPO_DEPO').dxValidator('instance').reset();
        $('#dsbCOD_TIPO_CANC').dxValidator('instance').reset();
        $('#ddbFEC_HORA_RESE').dxValidator('instance').reset();
        $('#dsbCOD_HORA').dxValidator('instance').reset();

        $('#dsbCOD_TIPO_CANC').dxSelectBox({ dataSource: new Array() });
        $('#dsbALF_TIPO_DOCU').dxSelectBox('instance').focus();
    };

    $scope.SaveOrder = function () {
        var result = DevExpress.validationEngine.validateGroup('ValOrdr');
        if (result.isValid) {
            $('#dlpProcess').dxLoadPanel({ position: { of: $('#ordrContent'), at: 'center' } });
            $('#dlpProcess').dxLoadPanel('instance').option('visible', true);

            var ALF_TIPO_DOCU = $('#dsbALF_TIPO_DOCU').dxSelectBox('instance').option('value');
            var ALF_NUME_DOCU = $('#dtbALF_NUME_DOCU').dxTextBox('instance').option('value');
            var ALF_NOMB = $('#dtbALF_NOMB').dxTextBox('instance').option('value');
            var ALF_CORR = $('#dtbALF_CORR').dxTextBox('instance').option('value');
            var ALF_NUME_TELE = $('#dtbALF_NUME_TELE').dxTextBox('instance').option('value');
            var COD_TIPO_DEPO = $('#dsbCOD_TIPO_DEPO').dxSelectBox('instance').option('value');
            var COD_TIPO_CANC = $('#dsbCOD_TIPO_CANC').dxSelectBox('instance').option('value');
            var FEC_HORA_RESE = new Date($('#ddbFEC_HORA_RESE').dxDateBox('instance').option('value')).yyyymmdd();
            var COD_HORA = $('#dsbCOD_HORA').dxSelectBox('instance').option('value');
            var rowhr = $('#dsbCOD_HORA').dxSelectBox('instance').option('selectedItem');

            var obj = {
                ALF_TIPO_DOCU: ALF_TIPO_DOCU,
                ALF_NUME_DOCU: ALF_NUME_DOCU,
                ALF_NOMB: ALF_NOMB,
                ALF_CORR: ALF_CORR,
                ALF_NUME_TELE: ALF_NUME_TELE,
                COD_TIPO_DEPO: COD_TIPO_DEPO,
                COD_TIPO_CANC: COD_TIPO_CANC,
                FEC_HORA_RESE: FEC_HORA_RESE,
                COD_HORA: COD_HORA,
                HOR_INIC: rowhr.HOR_INIC
            };

            var pars = JSON.stringify(obj);
            $http({ method: 'POST', url: 'http://localhost:28665/registrarsolicitud', data: pars, headers: { 'Content-Type': 'application/json' } })
            .success(function () {
                $('#dlpProcess').dxLoadPanel('instance').option('visible', false);
                $scope.ClearControls();
                DevExpress.ui.notify('Operación concretada con éxito.', 'success', 4000);
            })
            .error(function (ex) {
                $('#dlpProcess').dxLoadPanel('instance').option('visible', false);
                DevExpress.ui.notify(ex.Message, 'error', 4000);
                console.clear();
            });
        }
        else {
            DevExpress.ui.notify(msgval, 'warning', 4000);
        }
    };
}]);