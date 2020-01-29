console.log("Define pais controller ");


model.paisController = {

    pais: {
        Id: ko.observable(""),
        nombre: ko.observable(""),
        monedaId: ko.observable(""),
        estado: ko.observable(""),
    },


    paises: ko.observableArray([]),
    monedas: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),



    map: function (pais) {
        model.paisController.disable(false);
        var ePais = model.paisController.pais;
        ePais.Id(pais.Id);
        ePais.nombre(pais.nombre);
        ePais.monedaId(pais.monedaId);
        ePais.estado(pais.estado);
    },

    nuevo: function () {
        var pais = {
            Id: "",
            nombre: "",
            monedId: "",
            estado: "",

        };

        model.paisController.map(pais);

        model.paisController.insertMode(true);
        model.paisController.gridMode(false);
    },


    editar: function (pais) {

        model.paisController.disable(true);
        model.paisController.map(pais);

        model.paisController.gridMode(false);
        model.paisController.insertMode(true);
    },

    guardar: function () {
        var pais = model.paisController.pais;

        var paisParam = ko.toJS(pais);

        if (!model.validateForm('#paisForm')) {
            return;
        }
        model.paisController.disable(true);
        //call api save
        Paises.Guardar(paisParam, function (data) {
            toastr.info(data);
            model.paisController.initialize();
            model.paisController.insertMode(false);
            model.paisController.gridMode(true);
        });
    },

    remover: function (pais) {
        bootbox.confirm("¿Esta seguro que quiere remover pais " + pais.codigo + "?", function (result) {
            if (result) {
                //call api remove
                Paises.Remover(pais, function (data) {
                    model.paisController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.paisController.insertMode(false);
        model.paisController.gridMode(true);
        model.paisController.addPresentaciones([]);

        model.clearErrorMessage('#paisForm');
    },

    initialize: function () {
        Paises.Listar(function (data) {
            var paises = data;
            model.paisController.paises(paises);
        });

        Moneda.Listar(function (data) {
            var monedas = data;
            model.paisController.monedas(monedas);
        });
    }
};