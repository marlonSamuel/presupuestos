console.log("Define departamento controller ");


model.monedaController = {

    moneda: {
        Id: ko.observable(""),
        moneda: ko.observable(""),
        nombre: ko.observable(""),
        iso: ko.observable(""),
        simbolo: ko.observable(""),
        estado: ko.observable(""),
    },

    monedas: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (moneda) {
        model.monedaController.disable(false);
        var eMoneda = model.monedaController.moneda;
        eMoneda.Id(moneda.Id);
        eMoneda.moneda(moneda.moneda);
        eMoneda.nombre(moneda.nombre);
        eMoneda.iso(moneda.iso);
        eMoneda.estado(moneda.estado);
    },

    nuevo: function () {
        var moneda = {
            Id: "",
            moneda: "",
            nombre: "",
            iso: "",
            estado: "",
        };


        model.monedaController.map(moneda);

        model.monedaController.insertMode(true);
        model.monedaController.gridMode(false);
    },


    editar: function (moneda) {

        model.monedaController.map(moneda);

        model.monedaController.gridMode(false);
        model.monedaController.insertMode(true);
    },

    guardar: function () {

        var moneda = model.monedaController.moneda;

        var monedaParam = ko.toJS(moneda);

        if (!model.validateForm('#monedaForm')) {
            model.monedaController.disable(false);
            return;
        }
        model.monedaController.disable(true);


        //call api save
        Moneda.Guardar(monedaParam, function (data) {
            model.monedaController.initialize();
            toastr.info(data);
            model.monedaController.insertMode(false);
            model.monedaController.gridMode(true);
        });
    },

    remover: function (moneda) {
        bootbox.confirm("¿Esta seguro que quiere remover moneda " + moneda.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Moneda.Remover(moneda, function (data) {
                    model.monedaController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.monedaController.insertMode(false);
        model.monedaController.gridMode(true);

        model.clearErrorMessage('#monedaForm');
    },


    initialize: function () {
        Moneda.Listar(function (data) {
            var monedas = data;
            model.monedaController.monedas(monedas);
        });
    }
};