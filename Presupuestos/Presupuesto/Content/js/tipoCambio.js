console.log("Define tipoCambio controller ");


model.tipoCambioController = {

    tipoCambio: {
        Id: ko.observable(""),
        fecha: ko.observable(""),
        paisId: ko.observable(""),
        tipo_cambio: ko.observable(""),
        estado: ko.observable(""),
    },


    tipoCambios: ko.observableArray([]),
    paises: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),



    map: function (tipoCambio) {
        model.tipoCambioController.disable(false);
        var etipoCambio = model.tipoCambioController.tipoCambio;
        etipoCambio.Id(tipoCambio.Id);
        etipoCambio.fecha(tipoCambio.fecha);
        etipoCambio.paisId(tipoCambio.paisId);
        etipoCambio.estado(tipoCambio.estado);
        etipoCambio.tipo_cambio(tipoCambio.tipo_cambio);
    },

    nuevo: function () {
        var tipoCambio = {
            Id: "",
            fecha: "",
            paisId: "",
            tipo_cambio: "",
            estado: "",

        };

        model.tipoCambioController.map(tipoCambio);

        model.tipoCambioController.insertMode(true);
        model.tipoCambioController.gridMode(false);
    },


    editar: function (tipoCambio) {

        model.tipoCambioController.disable(true);
        model.tipoCambioController.map(tipoCambio);

        model.tipoCambioController.gridMode(false);
        model.tipoCambioController.insertMode(true);
    },

    guardar: function () {
        var tipoCambio = model.tipoCambioController.tipoCambio;

        var tipoCambioParam = ko.toJS(tipoCambio);

        if (!model.validateForm('#tipoCambioForm')) {
            return;
        }
        model.tipoCambioController.disable(true);
        //call api save
        TipoCambio.Guardar(tipoCambioParam, function (data) {
            toastr.info(data);
            model.tipoCambioController.initialize();
            model.tipoCambioController.insertMode(false);
            model.tipoCambioController.gridMode(true);
        });
    },

    remover: function (tipoCambio) {
        bootbox.confirm("¿Esta seguro que quiere remover tipoCambio ?", function (result) {
            if (result) {
                //call api remove
                TipoCambio.Remover(tipoCambio, function (data) {
                    model.tipoCambioController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.tipoCambioController.insertMode(false);
        model.tipoCambioController.gridMode(true);

        model.clearErrorMessage('#tipoCambioForm');
    },

    initialize: function () {
        TipoCambio.Listar(function (data) {
            var tipos = data;
            model.tipoCambioController.tipoCambios(tipos);
        });

        Paises.Listar(function (data) {
            var paises = data;
            model.tipoCambioController.paises(paises);
        });
    }
};