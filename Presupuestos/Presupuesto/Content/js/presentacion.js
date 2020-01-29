console.log("Define centro de costo ");


model.presentacionController = {

    presentacion: {
        codigo: ko.observable(""),
        descripcion: ko.observable(""),
    },

    presentaciones: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (presentacion) {
        model.presentacionController.disable(false);
        var ePresentacion = model.presentacionController.presentacion;
        ePresentacion.codigo(presentacion.codigo);
        ePresentacion.descripcion(presentacion.descripcion);
    },

    nuevo: function () {
        var presentacion = {
            codigo: "",
            descripcion: "",
        };


        model.presentacionController.map(presentacion);

        model.presentacionController.insertMode(true);
        model.presentacionController.gridMode(false);
    },


    editar: function (presentacion) {

        model.presentacionController.map(presentacion);

        model.presentacionController.gridMode(false);
        model.presentacionController.insertMode(false);
    },

    guardar: function () {
        var presentacion = model.presentacionController.presentacion;
        var presentacionParam = ko.toJS(presentacion);

        if (!model.validateForm('#presentacionParam')) {
            return;
        }
        model.presentacionController.disable(true);
        //call api save
        Presentacion.Guardar(presentacionParam, function (data) {

            console.log(data);
            toastr.info(data);
        });
    },

    remover: function (presentacion) {
        bootbox.confirm("¿Esta seguro que quiere remover presentacion " + presentacion.Descripcion + "?", function (result) {
            if (result) {
                //call api remove
                presentacion.Remover(presentacion, function (data) {
                    model.presentacion.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.presentacionController.insertMode(false);
        model.presentacionController.gridMode(true);

        model.clearErrorMessage('#presentacionForm');
    },


    initialize: function () {

        Presentacion.Listar(function (data) {
            presentaciones = data;
            model.presentacionController.presentaciones(presentaciones);
        });
    }
};