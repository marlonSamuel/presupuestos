console.log("Define actividad controller ");


model.actividadController = {

    actividad: {
        id: ko.observable(""),
        nombre: ko.observable(""),
        costo: ko.observable(""),
        unidadId: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
        codigo: ko.observable(""),
        grupoId: ko.observable(""),
    },

    actividades: ko.observableArray([]),
    unidades: ko.observableArray([]),
    tipos: ko.observableArray([]),
    grupos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),
    unidadMedidaOption: [{ nombre: 'Semana', valor: 'S' }, { nombre: 'Dia', valor: 'D' }, { nombre: 'Hora', valor: 'H' }],


    map: function (actividad) {
        model.actividadController.disable(false);
        var eActividad = model.actividadController.actividad;
        eActividad.id(actividad.Id);
        eActividad.nombre(actividad.nombre);
        eActividad.costo(actividad.costo);
        eActividad.unidadId(actividad.unidadId);
        eActividad.descripcion(actividad.Descripcion);
        eActividad.estado(actividad.Estado);
        eActividad.codigo(actividad.codigo);
        eActividad.grupoId(actividad.grupoId);
    },

    nuevo: function () {
        var actividad = {
            Id: "",
            nombre: "",
            costo: "",
            unidadId: "",
            Descripcion: "",
            Estado: "",
            codigo: "",
            grupoId: "",
        };


        model.actividadController.map(actividad);

        model.actividadController.insertMode(true);
        model.actividadController.gridMode(false);
    },


    editar: function (actividad) {

        model.actividadController.map(actividad);

        model.actividadController.gridMode(false);
        model.actividadController.insertMode(true);
    },

    guardar: function () {

        var actividad = model.actividadController.actividad;

        var actividadParam = ko.toJS(actividad);

        if (!model.validateForm('#actividadForm')) {
            return;
        }

        if (actividad.id() === "" || actividad.id() === undefined) {
            var actividades = model.actividadController.actividades();
            for (var i = 0; i < actividades.length; i++) {
                if (actividad.codigo() === actividades[i].Codigo) {
                    toastr.error("actividad ya ha sido ingresada");
                    return;
                }
            }
        }
        model.actividadController.disable(true);
        //call api save
        Actividad.Guardar(actividadParam, function (data) {
            model.actividadController.initialize();
            toastr.info(data);
            model.actividadController.insertMode(false);
            model.actividadController.gridMode(true);
        });
    },

    remover: function (actividad) {
        bootbox.confirm("¿Esta seguro que quiere remover actividad " + actividad.Nombre + "?", function (result) {
            if (result) {
                //call api remove
                Actividad.Remover(actividad, function (data) {
                    model.actividadController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.actividadController.insertMode(false);
        model.actividadController.gridMode(true);

        model.clearErrorMessage('#actividadForm');
    },


    initialize: function () {
        Actividad.Listar(function (data) {
            var actividades = data;
            model.actividadController.actividades(actividades);
        });

        Unidad.Listar(function (data) {
            var unidades = data;
            model.actividadController.unidades(unidades);
        });

        GrupoDimension.Listar(function (data) {
            var grupos = data;
            model.actividadController.grupos(grupos);
        });
    }
};