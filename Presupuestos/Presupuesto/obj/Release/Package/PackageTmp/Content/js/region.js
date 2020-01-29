console.log("Define region controller ");


model.regionController = {

    region: {
        codigo: ko.observable(""),
        nombre: ko.observable(""),
    },

    regiones: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (region) {
        model.regionController.disable(false);
        var eregion = model.regionController.region;
        eregion.codigo(region.codigo);
        eregion.nombre(region.nombre);
    },

    nuevo: function () {
        var region = {
            codigo: "",
            nombre: "",
        };


        model.regionController.map(region);

        model.regionController.insertMode(true);
        model.regionController.gridMode(false);
    },


    editar: function (region) {

        model.regionController.map(region);

        model.regionController.gridMode(false);
        model.regionController.insertMode(true);
    },

    guardar: function () {

        var region = model.regionController.region;

        var regionParam = ko.toJS(region);

        if (!model.validateForm('#regionForm')) {
            model.regionController.disable(false);
            return;
        }
        model.regionController.disable(true);


        //call api save
        region.Guardar(regionParam, function (data) {
            model.regionController.initialize();
            toastr.info(data);
            model.regionController.insertMode(false);
            model.regionController.gridMode(true);
        });
    },

    remover: function (region) {
        bootbox.confirm("¿Esta seguro que quiere remover region " + region.nombre + "?", function (result) {
            if (result) {
                //call api remove
                region.Remover(region, function (data) {
                    model.regionController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.regionController.insertMode(false);
        model.regionController.gridMode(true);

        model.clearErrorMessage('#regionForm');
    },


    initialize: function () {
        region.Listar(function (data) {
            var regiones = data;
            model.regionController.regiones(regiones);
        });
    }
};