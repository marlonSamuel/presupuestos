console.log("Define departamento controller ");


model.departamentoController = {

    departamento: {
        departamentoId: ko.observable(""),
        nombre: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
    },

    departamentos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),


    map: function (departamento) {
        var eDepartamento = model.departamentoController.departamento;
        eDepartamento.departamentoId(departamento.departamentoId);
        eDepartamento.nombre(departamento.nombre);
        eDepartamento.descripcion(departamento.descripcion);
        eDepartamento.estado(departamento.estado);
    },

    nuevo: function () {
        var departamento = {
            departamentoID: "",
            nombre: "",
            descripcion: "",
            estado: "",
        };


        model.departamentoController.map(departamento);

        model.departamentoController.insertMode(true);
        model.departamentoController.gridMode(false);
    },


    editar: function (departamento) {

        model.departamentoController.map(departamento);

        model.departamentoController.gridMode(false);
        model.departamentoController.insertMode(true);
    },

    guardar: function () {

        var departamento = model.departamentoController.departamento;

        var departamentoParam = ko.toJS(departamento);

        if (!model.validateForm('#departamentoForm')) {
            return;
        }


        //call api save
        Departamento.Guardar(departamentoParam, function (data) {
            model.departamentoController.initialize();
            toastr.info(data);
            model.departamentoController.insertMode(false);
            model.departamentoController.gridMode(true);
        });
    },

    remover: function (departamento) {
        bootbox.confirm("¿Esta seguro que quiere remover departamento " + departamento.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Departamento.Remover(departamento, function (data) {
                    model.departamentoController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.departamentoController.insertMode(false);
        model.departamentoController.gridMode(true);

        model.clearErrorMessage('#departamentoForm');
    },


    initialize: function () {
        Departamento.Listar(function (data) {
            var departamentos = data;
            model.departamentoController.departamentos(departamentos);
        });
    }
};