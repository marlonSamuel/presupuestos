console.log("Define actividad controller ");


model.unidadController = {

    unidad: {
        unidadId: ko.observable(""),
        nombre: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
    },

    unidades: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (unidad) {
        model.unidadController.disable(false);
        var eUnidad = model.unidadController.unidad;
        eUnidad.unidadId(unidad.unidadId);
        eUnidad.nombre(unidad.nombre);
        eUnidad.descripcion(unidad.descripcion);
        eUnidad.estado(unidad.estado);
    },

    nuevo: function () {
        var unidad = {
            unidadId: "",
            nombre: "",
            descripcion: "",
            estado: "",
        };


        model.unidadController.map(unidad);

        model.unidadController.insertMode(true);
        model.unidadController.gridMode(false);
    },


    editar: function (unidad) {

        model.unidadController.map(unidad);

        model.unidadController.gridMode(false);
        model.unidadController.insertMode(true);
    },

    guardar: function () {
        var unidad = model.unidadController.unidad;

        var unidadParam = ko.toJS(unidad);

        if (!model.validateForm('#unidadForm')) {
            return;
        }

        model.unidadController.disable(true);


        //call api save
        Unidad.Guardar(unidadParam, function (data) {
            model.unidadController.initialize();
            toastr.info(data);
            model.unidadController.insertMode(false);
            model.unidadController.gridMode(true);
        });
    },

    remover: function (unidad) {
        bootbox.confirm("¿Esta seguro que quiere remover unidad " + unidad.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Unidad.Remover(unidad, function (data) {
                    model.unidadController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.unidadController.insertMode(false);
        model.unidadController.gridMode(true);

        model.clearErrorMessage('#unidadForm');
    },


    initialize: function () {
        Unidad.Listar(function (data) {
            var unidades = data;
            model.unidadController.unidades(unidades);
        });
    }
};