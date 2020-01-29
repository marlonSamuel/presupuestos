console.log("Define departamento controller ");


model.dimensionController = {

    dimension: {
        Id: ko.observable(""),
        nombre: ko.observable(""),
        cuenta_contable: ko.observable(""),
        nombre_cuenta: ko.observable(""),
        factor: ko.observable(""),
        grupoId: ko.observable(""),
        estado: ko.observable(""),
    },

    dimensiones: ko.observableArray([]),
    grupos: ko.observableArray([]),
    cuentas: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),
    search: ko.observable(""),


    map: function (dimension) {
        model.dimensionController.disable(false);
        var edimension = model.dimensionController.dimension;
        edimension.Id(dimension.Id);
        edimension.grupoId(dimension.grupoId);
        $('#grupoId').selectpicker('refresh');
        edimension.nombre(dimension.nombre);
        edimension.cuenta_contable(dimension.cuenta_contable);
        $('#cuenta_contable').selectpicker('refresh');
        edimension.factor(dimension.factor);
        edimension.estado(dimension.estado);
    },

    nuevo: function () {
        var dimension = {
            Id: "",
            nombre: "",
            cuenta_contable: "",
            fator: "",
            grupoId: "",
            estado: "",
        };


        model.dimensionController.map(dimension);

        model.dimensionController.insertMode(true);
        model.dimensionController.gridMode(false);
    },


    editar: function (dimension) {
        model.dimensionController.dimension.factor("");
        model.dimensionController.dimension.grupoId("");
        model.dimensionController
        model.dimensionController.map(dimension);

        model.dimensionController.gridMode(false);
        model.dimensionController.insertMode(true);
    },

    setNameCuenta: function (cuenta) {
        var cuentas = model.dimensionController.cuentas();

        for (var i = 0; i < cuentas.length; i++) {
            if (cuentas[i].numero === cuenta) {
                model.dimensionController.dimension.nombre_cuenta(cuentas[i].nombre);
            }
        }
    },

    guardar: function () {

        var dimension = model.dimensionController.dimension;

        model.dimensionController.setNameCuenta(dimension.cuenta_contable());

        var dimensionParam = ko.toJS(dimension);

        if (!model.validateForm('#dimensionForm')) {
            model.dimensionController.disable(false);
            return;
        }

        model.dimensionController.disable(true);


        //call api save
        Dimension.Guardar(dimensionParam, function (data) {
            model.dimensionController.initialize();
            toastr.info(data);
            model.dimensionController.insertMode(false);
            model.dimensionController.gridMode(true);
        });
    },

    remover: function (dimension) {
        bootbox.confirm("¿Esta seguro que quiere remover dimension " + dimension.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Dimension.Remover(dimension, function (data) {
                    model.dimensionController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.dimensionController.insertMode(false);
        model.dimensionController.gridMode(true);

        model.clearErrorMessage('#dimensionForm');
    },


    initialize: function () {
        Dimension.Listar(function (data) {
            var dimensiones = data;
            model.dimensionController.dimensiones(dimensiones);
        });

        GrupoDimension.Listar(function (data) {
            var grupos = data;
            model.dimensionController.grupos(grupos);
        });

        Dimension.ListarCuentas(function (data) {
            var cuentas = data;
            model.dimensionController.cuentas(cuentas);
        });
    }
};