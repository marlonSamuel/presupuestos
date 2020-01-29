console.log("Define departamento controller ");


model.categoriaController = {

    categoria: {
        categoriaId: ko.observable(""),
        nombre: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
    },

    categorias: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),


    map: function (categoria) {
        model.categoriaController.disable(false);
        var eCategoria = model.categoriaController.categoria;
        eCategoria.categoriaId(categoria.categoriaId);
        eCategoria.nombre(categoria.nombre);
        eCategoria.descripcion(categoria.descripcion);
        eCategoria.estado(categoria.estado);
    },

    nuevo: function () {
        var categoria = {
            categoriaId: "",
            nombre: "",
            descripcion: "",
            estado: "",
        };


        model.categoriaController.map(categoria);

        model.categoriaController.insertMode(true);
        model.categoriaController.gridMode(false);
    },


    editar: function (categoria) {

        model.categoriaController.map(categoria);

        model.categoriaController.gridMode(false);
        model.categoriaController.insertMode(true);
    },

    guardar: function () {

        var categoria = model.categoriaController.categoria;

        var categoriaParam = ko.toJS(categoria);

        if (!model.validateForm('#categoriaForm')) {
            model.categoriaController.disable(false);
            return;      
        }

        model.categoriaController.disable(true);


        //call api save
        Categoria.Guardar(categoriaParam, function (data) {
            model.categoriaController.initialize();
            toastr.info(data);
            model.categoriaController.insertMode(false);
            model.categoriaController.gridMode(true);
        });
    },

    remover: function (categoria) {
        bootbox.confirm("¿Esta seguro que quiere remover categoria " + categoria.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Categoria.Remover(categoria, function (data) {
                    model.categoriaController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.categoriaController.insertMode(false);
        model.categoriaController.gridMode(true);

        model.clearErrorMessage('#categoriaForm');
    },


    initialize: function () {
        Bi.ListarCategorias(function (data) {
            var categorias = data;
            model.categoriaController.categorias(categorias);
        });
    }
};