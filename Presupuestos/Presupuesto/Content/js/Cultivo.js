console.log("Define cultivo ");


model.cultivoController = {

    cultivo: {
        codigo: ko.observable(""),
        descripcion: ko.observable(""),
    },

    cultivos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (cultivo) {
        model.cultivoController.disable(false);
        var eCultivo = model.cultivoController.cultivo;
        eCultivo.codigo(cultivo.codigo);
        eCultivo.descripcion(cultivo.descripcion);
    },

    nuevo: function () {
        var cultivo = {
            codigo: "",
            descripcion: "",
        };
        

        model.cultivoController.map(cultivo);

        model.cultivoController.insertMode(true);
        model.cultivoController.gridMode(false);
    },


    editar: function (cultivo) {

        model.cultivoController.map(cultivo);
        
        model.cursoController.gridMode(false);
        model.cursoController.insertMode(false);
    },

    guardar: function () {
        var cultivo = model.cultivoController.cultivo;
        var cultivoParam = ko.toJS(cultivo);

        if (!model.validateForm('#cultivoForm')) {
            return;
        }

        model.cultivoController.disable(true);
        //call api save
        Cultivo.Guardar(cultivoParam, function (data) {

            console.log(data);
            toastr.info(data);
        });
    },

    remover: function (cultivo) {
        bootbox.confirm("¿Esta seguro que quiere remover curso " + cultivo.descripcion + "?", function (result) {
            if (result) {
                //call api remove
                Cultivo.Remover(cultivo, function (data) {
                    model.cultivoController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.cultivoController.insertMode(false);
        model.cultivoController.gridMode(true);

        model.clearErrorMessage('#cultivoForm');
    },


    initialize: function () {
        var self = this;
        var cultivos = this.cultivos();

        Cultivo.Listar(function (data) {
            cultivos = data;
            model.cultivoController.cultivos(cultivos);
        });
    }
};