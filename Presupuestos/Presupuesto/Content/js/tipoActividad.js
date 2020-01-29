console.log("Define departamento controller ");


model.tipoActividadController = {

    tipoActividad: {
        Id: ko.observable(""),
        nombre: ko.observable(""),
        descripcion: ko.observable(""),
        estado: ko.observable(""),
        actividad_principal: ko.observable(""),
    },

    tipoActividades: ko.observableArray([]),
    principales: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    disable: ko.observable(false),
    isClasificacion: ko.observable(false),
    name: ko.observable(""),
    tipos: [{ nombre: 'Principales', valor: 'P' }, { nombre: 'Clasificaciones', valor: 'C' }],


    map: function (tipoActividad) {
        model.tipoActividadController.disable(false);
        var etipoActividad = model.tipoActividadController.tipoActividad;
        etipoActividad.Id(tipoActividad.Id);
        etipoActividad.nombre(tipoActividad.nombre);
        etipoActividad.descripcion(tipoActividad.descripcion);
        etipoActividad.estado(tipoActividad.estado);
        etipoActividad.actividad_principal(tipoActividad.actividad_principal);
    },

    nuevo: function () {
        var tipoActividad = {
            tipoActividadId: "",
            nombre: "",
            descripcion: "",
            estado: "",
            actividad_principal: "",
        };


        model.tipoActividadController.map(tipoActividad);

        model.tipoActividadController.insertMode(true);
        model.tipoActividadController.gridMode(false);
    },


    editar: function (tipoActividad) {

        model.tipoActividadController.map(tipoActividad);

        model.tipoActividadController.gridMode(false);
        model.tipoActividadController.insertMode(true);
    },

    guardar: function () {

        var tipoActividad = model.tipoActividadController.tipoActividad;

        var tipoActividadParam = ko.toJS(tipoActividad);

        if (!model.validateForm('#tipoActividadForm')) {
            model.tipoActividadController.disable(false);
            return;
        }
        model.tipoActividadController.disable(true);


        //call api save
        TipoActividad.Guardar(tipoActividadParam, function (data) {
            model.tipoActividadController.initialize();
            toastr.info(data);
            model.tipoActividadController.insertMode(false);
            model.tipoActividadController.gridMode(true);
        });
    },

    remover: function (tipoActividad) {
        bootbox.confirm("¿Esta seguro que quiere remover tipoActividad " + tipoActividad.nombre + "?", function (result) {
            if (result) {
                //call api remove
                TipoActividad.Remover(tipoActividad, function (data) {
                    model.tipoActividadController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.tipoActividadController.insertMode(false);
        model.tipoActividadController.gridMode(true);

        model.clearErrorMessage('#tipoActividadForm');
    },

    selectTipo: function (tipo) {
        model.tipoActividadController.name(tipo.nombre);
        if (tipo.valor === 'P') {
            model.tipoActividadController.isClasificacion(false);
            TipoActividad.ListarPrincipales(function (data) {
                var tipoActividades = data;
                model.tipoActividadController.tipoActividades(tipoActividades);
                model.tipoActividadController.principales(tipoActividades);
            });
        } else {
            model.tipoActividadController.isClasificacion(true);
            TipoActividad.Listar(function (data) {
                var tipoActividades = data;
                model.tipoActividadController.tipoActividades(tipoActividades);
            });
        }
    },


    initialize: function () {
        var tipos = model.tipoActividadController.tipos;
        model.tipoActividadController.selectTipo(tipos[0]);
    }
};