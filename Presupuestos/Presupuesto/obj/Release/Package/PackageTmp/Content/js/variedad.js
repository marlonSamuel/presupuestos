console.log("Define variedad controller ");


model.variedadController = {

    variedad: {
        codigo: ko.observable(""),
        descripcion: ko.observable(""),
        grupoCultivo: ko.observable(""),
    },

    variedades: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (variedad) {
        model.variedadController.disable(false);
        var eVariedad = model.variedadController.variedad;
        eVariedad.codigo(variedad.codigo);
        eVariedad.descripcion(variedad.descripcion);
        eVariedad.grupoCultivo(variedad.grupoCultivo);
    },

    nuevo: function () {
        var variedad = {
            codigo: "",
            descripcion: "",
            grupoCultivo: "",
        };


        model.variedadController.map(variedad);

        model.variedadController.insertMode(true);
        model.variedadController.gridMode(false);
    },


    editar: function (variedad) {

        model.variedadController.map(variedad);

        model.variedadController.gridMode(false);
        model.variedadController.insertMode(false);
    },

    guardar: function () {
        model.variedadController.disable(true);
        var variedad = model.variedadController.variedad;
        var variedadParam = ko.toJS(variedad);

        if (!model.validateForm('#variedadForm')) {
            return;
        }
        //call api save
        Variedad.Guardar(variedadParam, function (data) {

            console.log(data);
            toastr.info(data);
        });
    },

    remover: function (variedad) {
        bootbox.confirm("¿Esta seguro que quiere remover curso " + variedad.descripcion + "?", function (result) {
            if (result) {
                //call api remove
                Variedad.Remover(variedad, function (data) {
                    model.ccosto.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.variedadController.insertMode(false);
        model.variedadController.gridMode(true);

        model.clearErrorMessage('#variedadForm');
    },


    initialize: function () {
        var self = this;
        var variedades = this.variedades();

        Variedad.Listar(function (data) {
            variedades = data;
            model.variedadController.variedades(variedades);
        });
    }
};