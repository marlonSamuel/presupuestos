console.log("Define cargo controller ");


model.cargoController = {

    cargo: {
        cargoId: ko.observable(""),
        nombre: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
    },

    cargos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (cargo) {
        model.cargoController.disable(false);
        var ecargo = model.cargoController.cargo;
        ecargo.cargoId(cargo.cargoId);
        ecargo.nombre(cargo.nombre);
        ecargo.descripcion(cargo.descripcion);
        ecargo.estado(cargo.estado);
    },

    nuevo: function () {
        var cargo = {
            cargoId: "",
            nombre: "",
            descripcion: "",
            estado: "",
        };


        model.cargoController.map(cargo);

        model.cargoController.insertMode(true);
        model.cargoController.gridMode(false);
    },


    editar: function (cargo) {

        model.cargoController.map(cargo);

        model.cargoController.gridMode(false);
        model.cargoController.insertMode(true);
    },

    guardar: function () {

        model.cargoController.disable(true);
        var cargo = model.cargoController.cargo;

        var cargoParam = ko.toJS(cargo);

        if (!model.validateForm('#cargoForm')) {
            model.cargoController.disable(false);
            return;
        }


        //call api save
        Cargo.Guardar(cargoParam, function (data) {
            model.cargoController.initialize();
            toastr.info(data);
            model.cargoController.insertMode(false);
            model.cargoController.gridMode(true);
        });
    },

    remover: function (cargo) {
        bootbox.confirm("¿Esta seguro que quiere remover cargo " + cargo.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Cargo.Remover(cargo, function (data) {
                    model.cargoController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.cargoController.insertMode(false);
        model.cargoController.gridMode(true);

        model.clearErrorMessage('#cargoForm');
    },


    initialize: function () {
        Cargo.Listar(function (data) {
            var cargos = data;
            model.cargoController.cargos(cargos);
        });
    }
};