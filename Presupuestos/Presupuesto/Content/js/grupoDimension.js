console.log("Define departamento controller ");


model.grupoDimensionController = {

    grupoDimension: {
        Id: ko.observable(""),
        codigo: ko.observable(""),
        nombre: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
    },

    grupoDimensiones: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (grupoDimension) {
        model.grupoDimensionController.disable(false);
        var egrupoDimension = model.grupoDimensionController.grupoDimension;
        egrupoDimension.Id(grupoDimension.Id);
        egrupoDimension.nombre(grupoDimension.nombre);
        egrupoDimension.descripcion(grupoDimension.descripcion);
        egrupoDimension.estado(grupoDimension.estado);
        egrupoDimension.codigo(grupoDimension.codigo);
    },

    nuevo: function () {
        var grupoDimension = {
            Id: "",
            nombre: "",
            descripcion: "",
            estado: "",
            codigo: ""
        };


        model.grupoDimensionController.map(grupoDimension);

        model.grupoDimensionController.insertMode(true);
        model.grupoDimensionController.gridMode(false);
    },


    editar: function (grupoDimension) {

        model.grupoDimensionController.map(grupoDimension);

        model.grupoDimensionController.gridMode(false);
        model.grupoDimensionController.insertMode(true);
    },

    guardar: function () {

        var grupoDimension = model.grupoDimensionController.grupoDimension;

        var grupoDimensionParam = ko.toJS(grupoDimension);

        if (!model.validateForm('#grupoDimensionForm')) {
            model.grupoDimensionController.disable(false);
            return;
        }

        model.grupoDimensionController.disable(true);

        //call api save
        GrupoDimension.Guardar(grupoDimensionParam, function (data) {
            model.grupoDimensionController.initialize();
            toastr.info(data);
            model.grupoDimensionController.insertMode(false);
            model.grupoDimensionController.gridMode(true);
        });
    },

    remover: function (grupoDimension) {
        bootbox.confirm("¿Esta seguro que quiere remover grupoDimension " + grupoDimension.nombre + "?", function (result) {
            if (result) {
                //call api remove
                GrupoDimension.Remover(grupoDimension, function (data) {
                    model.grupoDimensionController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.grupoDimensionController.insertMode(false);
        model.grupoDimensionController.gridMode(true);

        model.clearErrorMessage('#grupoDimensionForm');
    },


    initialize: function () {
        GrupoDimension.Listar(function (data) {
            var grupoDimensiones = data;
            model.grupoDimensionController.grupoDimensiones(grupoDimensiones);
        });
    }
};