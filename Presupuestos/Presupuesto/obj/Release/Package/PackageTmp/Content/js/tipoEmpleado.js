console.log("Define tipo empleado controller ");


model.tipoEmpleadoController = {

    tipo: {
        tipoId: ko.observable(""),
        nombre: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
    },

    tipos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),


    map: function (tipo) {
        var eTipo = model.tipoEmpleadoController.tipo;
        eTipo.tipoId(tipo.tipoId);
        eTipo.nombre(tipo.nombre);
        eTipo.descripcion(tipo.descripcion);
        eTipo.estado(tipo.estado);
    },

    nuevo: function () {

        var tipo = {
            tipoId: "",
            nombre: "",
            descripcion: "",
            estado: "",
        };


        model.tipoEmpleadoController.map(tipo);

        model.tipoEmpleadoController.insertMode(true);
        model.tipoEmpleadoController.gridMode(false);
    },


    editar: function (tipo) {

        model.tipoEmpleadoController.map(tipo);

        model.tipoEmpleadoController.gridMode(false);
        model.tipoEmpleadoController.insertMode(true);
    },

    guardar: function () {

        var tipo = model.tipoEmpleadoController.tipo;

        var tipoParam = ko.toJS(tipo);

        if (!model.validateForm('#tipoEmpleadoForm')) {
            return;
        }


        //call api save
        TipoEmpleado.Guardar(tipoParam, function (data) {
            model.tipoEmpleadoController.initialize();
            toastr.info(data);
            model.tipoEmpleadoController.insertMode(false);
            model.tipoEmpleadoController.gridMode(true);
        });
    },

    remover: function (tipo) {
        bootbox.confirm("¿Esta seguro que quiere remover tipo empleado " + tipo.nombre + "?", function (result) {
            if (result) {
                //call api remove
                TipoEmpleado.Remover(tipo, function (data) {
                    model.tipoEmpleadoController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.tipoEmpleadoController.insertMode(false);
        model.tipoEmpleadoController.gridMode(true);

        model.clearErrorMessage('#tipoEmpleadoForm');
    },


    initialize: function () {
        TipoEmpleado.Listar(function (data) {
            var tipos = data;
            model.tipoEmpleadoController.tipos(tipos);
        });
    }
};