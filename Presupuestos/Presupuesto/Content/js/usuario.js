console.log("Define departamento controller ");


model.usuarioController = {

    usuario: {
        Id: ko.observable(""),
        usuario: ko.observable(""),
        email: ko.observable(""),
        password: ko.observable(""),
        estado: ko.observable(""),
        permisos: ko.observableArray([]),
        paises: ko.observableArray([]),
        presupuestos: ko.observableArray([]),
    },

    usuarios: ko.observableArray([]),
    permisos: ko.observableArray([]),
    paises: ko.observableArray([]),
    presupuestos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),
    isSelected: ko.observable(false),
    loadingVisible: ko.observable(false),
    loadingVisibleImport: ko.observable(false),


    setArrays: function (usuario) {
        //seteamos los permisos asignados a los usuarios
        if (usuario.Id != "") {
            Usuario.ListarPermisos(usuario.Id, function (data) {
                var permisos = data;
                model.usuarioController.usuario.permisos([]);
                for (var i = 0; i < permisos.length; i++) {
                    model.usuarioController.usuario.permisos.push(permisos[i].permisoId);
                }
            });

        //seteamos los paises asignados a los usuarios
            Usuario.ListarPaises(usuario.Id, function (data) {
                var paises = data;
                model.usuarioController.usuario.paises([]);
                for (var i = 0; i < paises.length; i++) {
                    model.usuarioController.usuario.paises.push(paises[i].paisId);
                }
            });

            //seteamos los presupuestos a los usuarios
            Presupuesto.ListarByUser(usuario.Id, function (data) {
                var presupuestos = data;
                model.usuarioController.usuario.presupuestos([]);
                for (var i = 0; i < presupuestos.length; i++) {
                    model.usuarioController.usuario.presupuestos.push(presupuestos[i].tipoId);
                }
            });
        }
    },



    map: function (usuario) {
        model.usuarioController.setArrays(usuario);
        model.usuarioController.disable(false);
        var eUsuario = model.usuarioController.usuario;
        eUsuario.Id(usuario.Id);
        eUsuario.usuario(usuario.usuario);
        eUsuario.password(usuario.password);
        eUsuario.email(usuario.email);
        eUsuario.estado(usuario.estado);
    },

    nuevo: function () {
        var usuario = {
            Id: "",
            usuario: "",
            email: "",
            password: "",
            estado: "",
        };


        model.usuarioController.map(usuario);

        model.usuarioController.insertMode(true);
        model.usuarioController.gridMode(false);
    },


    editar: function (usuario) {

        model.usuarioController.map(usuario);

        model.usuarioController.gridMode(false);
        model.usuarioController.insertMode(true);
    },

    guardar: function () {

        var usuario = model.usuarioController.usuario;

        var usuarioParam = ko.toJS(usuario);

        if (!model.validateForm('#usuarioForm')) {
            model.usuarioController.disable(false);
            return;
        }

        model.usuarioController.disable(true);

        //call api save
        Usuario.Guardar(usuarioParam, function (data) {
            if (data) {
                toastr.info("usuario registrado/actualizado con exito");
            } else {
                toastr.error("usuario no se pudo registrar/actualizar");
            }
            model.usuarioController.initialize();
            model.usuarioController.insertMode(false);
            model.usuarioController.gridMode(true);
        });
    },

    remover: function (usuario) {
        bootbox.confirm("¿Esta seguro que quiere remover usuario " + usuario.usuario + "?", function (result) {
            if (result) {
                //call api remove
                Usuario.Remover(usuario, function (data) {
                    model.usuarioController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.usuarioController.insertMode(false);
        model.usuarioController.gridMode(true);

        model.clearErrorMessage('#usuarioForm');
        model.usuarioController.usuario.permisos([]);
    },

    selectAll: function () {
        var permisos = model.usuarioController.permisos();
        var checked = false;

        var isSelected = model.usuarioController.isSelected();

        if (isSelected) {
            checked = false;
        } else {
            checked = true;
        }

        if (checked) {
            for (var i = 0; i < permisos.length; i++) {
                model.usuarioController.usuario.permisos.push(permisos[i].Id);
            }
        } else {
            model.usuarioController.usuario.permisos([]);
        }

        model.usuarioController.isSelected(checked);
    },


    initialize: function () {
        Usuario.Listar(function (data) {
            var usuarios = data;
            model.usuarioController.usuarios(usuarios);
        });

        Permiso.Listar(function (data) {
            var permisos = data;
            model.usuarioController.permisos(permisos);
        });

        Paises.Listar(function (data) {
            var paises = data;
            model.usuarioController.paises(paises);
        });

        Presupuesto.ListarTipos(function (data) {
            var presupuestos = data;
            model.usuarioController.presupuestos(presupuestos);
        });

    }
};