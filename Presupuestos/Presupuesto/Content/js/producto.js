console.log("Define producto controller ");


model.productoController = {

    producto: {
        productoId: ko.observable(""),
        nombre: ko.observable(""),
        codigo: ko.observable(""),
        cultivoId: ko.observable(""),
        categoriaId: ko.observable(""),
        tipo: ko.observable(""),
        detalle: ko.observableArray([])
    },

    detalle: {
        detalleId: ko.observable(""),
        cultivoId: ko.observable(""),
        presentacionId: ko.observable("")
    },

    categoriaId: ko.observable(""),
    name: ko.observable(""),
    productos: ko.observableArray([]),
    categorias: ko.observableArray([]),
    cultivos: ko.observableArray([]),
    presentaciones: ko.observableArray([]),
    addPresentaciones: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    aplica: ko.observable(true),
    tipoOption: [{ nombre: 'EM', valor: 'EM' }, { nombre: 'RC', valor: 'RC' }],



    map: function (producto) {
        model.productoController.addPresentaciones([]);
        model.productoController.producto.detalle([]);

        var eProducto = model.productoController.producto;
        eProducto.productoId(producto.productoId);
        eProducto.codigo(producto.codigo);
        eProducto.nombre(producto.nombre);
        eProducto.categoriaId(producto.categoriaId);
        eProducto.cultivoId(producto.cultivoId);
        eProducto.tipo(producto.tipo);
    },

    nuevo: function () {
        var producto = {
            productoId: "",
            codigo: "",
            nombre: "",
            categoriaId: "",
            cultivoId: "",
            tipo: "",
        };
        producto.categoriaId = model.productoController.categoriaId();
        model.productoController.map(producto);

        model.productoController.insertMode(true);
        model.productoController.gridMode(false);
    },


    editar: function (producto) {
       

        model.productoController.map(producto);

        Producto.ListarD(producto.productoId, function (data) {
            var presentaciones = data;
            for (var i = 0; i < presentaciones.length; i++) {
                model.productoController.addPresentaciones.push(presentaciones[i].presentacionId);
            }
        });

        model.productoController.gridMode(false);
        model.productoController.insertMode(true);
    },

    guardar: function () {

        var producto = model.productoController.producto;

        var presentaciones = model.productoController.addPresentaciones();
       

        for (var i = 0; i < presentaciones.length; i++) {

            var detalle = Object();
            detalle.detalleId = 0;
            detalle.presentacionId = presentaciones[i];

            model.productoController.producto.detalle.push(detalle);
        }

        var productoParam = ko.toJS(producto);

        if (!model.validateForm('#productoForm')) {
            return;
        }
        //call api save
        Producto.Guardar(productoParam, function (data) {
            model.productoController.seleccionar(productoParam);
            toastr.info(data);
            model.productoController.insertMode(false);
            model.productoController.gridMode(true);     
        });
    },

    remover: function (producto) {
        bootbox.confirm("¿Esta seguro que quiere remover producto " + producto.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Producto.Remover(producto,function (data) {
                    model.productoController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.productoController.insertMode(false);
        model.productoController.gridMode(true);
        model.productoController.addPresentaciones([]);

        model.clearErrorMessage('#productoForm');
    },

    seleccionar: function (categoria) {
        model.productoController.categoriaId(categoria.categoriaId);
        model.productoController.name(categoria.nombre);

        Bi.ListarProAgricola(categoria.codigo,function(data) {
            var productos = data;
            model.productoController.productos(productos);
        });
    },

    noAplica: function (codigo) {
        console.log(codigo);
        if (codigo == 'N    ') {
            model.productoController.aplica(false);
            model.productoController.addPresentaciones([]);
        } else {
            model.productoController.aplica(true);
        }
        
    },


    initialize: function (tipo) {
        if (tipo === "A") {
            Bi.ListarCategorias(function (data) {
                var categorias = data;
                model.productoController.seleccionar(categorias[0]);
                model.productoController.categorias(categorias);
            });
        } else {
            Bi.ListarProVenta(function (data) {
                var productos = data;
                model.productoController.productos(productos);
            });
        }


        //Cultivo.ListarA(function (data) {
        //    var cultivos = data;
        //    model.productoController.cultivos(cultivos);
        //});

        //Presentacion.Listar(function (data) {
        //    var presentaciones = data;
        //    model.productoController.presentaciones(presentaciones);
        //});
    }
};