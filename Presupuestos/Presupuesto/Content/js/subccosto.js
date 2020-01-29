console.log("Define centro de costo ");


model.SubCCostoController = {

    subCCosto: {
        Id: ko.observable(""),
        fincaId: ko.observable(""),
        codigo_nav: ko.observable(""),
        nombre: ko.observable(""),
        codigo: ko.observable(""),
        actividades: ko.observableArray("")
    },

    search: ko.observable(""),
    SubCCostos: ko.observableArray([]),
    actividades: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(false),
    editMode: ko.observable(false),
    disable: ko.observable(false),

    map: function (subCCosto) {
        model.SubCCostoController.disable(false);
        model.SubCCostoController.getDetalle(subCCosto.Id);
        var eSubCCosto = model.SubCCostoController.subCCosto;
        eSubCCosto.Id(subCCosto.Id);
        eSubCCosto.nombre(subCCosto.nombre);
        eSubCCosto.codigo_nav(subCCosto.codigo_nav);
        eSubCCosto.codigo(subCCosto.codigo);
        eSubCCosto.fincaId(subCCosto.fincaId);
    },

    getDetalle: function(Id) {
        if (Id !== "") {
            SubCentro.ListarA(Id, function (data) {
                var actividades = data;
                model.SubCCostoController.subCCosto.actividades([]);
                for (var i = 0; i < actividades.length; i++) {
                    model.SubCCostoController.subCCosto.actividades.push(actividades[i].actividadId);
                }
            });
        }
    },

    nuevo: function () {
        var SubCCosto = {
            Id: "",
            nombre: "",
            fincaId: "",
            codigo: "",
            codigo_nav: ""
        };

        SubCCosto.fincaId = model.SubCCostoController.subCCosto.fincaId();
        model.SubCCostoController.map(SubCCosto);

        model.SubCCostoController.insertMode(true);
        model.SubCCostoController.gridMode(false);
        model.SubCCostoController.editMode(false);
        model.SubCCostoController.subCCosto.actividades([]);
    },


    editar: function (SubCCosto) {
        model.SubCCostoController.map(SubCCosto);

        model.SubCCostoController.gridMode(false);
        model.SubCCostoController.insertMode(true);
    },

    guardar: function () {
        var SubCCosto = model.SubCCostoController.subCCosto;
        var SubCCostoParam = ko.toJS(SubCCosto);

        if (!model.validateForm('#SubCCostoForm')) {
            return;
        }
        model.SubCCostoController.disable(true);
        //call api save
        SubCentro.Guardar(SubCCostoParam, function (data) {

            toastr.info(data);
            model.SubCCostoController.insertMode(false);
            model.SubCCostoController.gridMode(true);
            model.SubCCostoController.initialize(SubCCostoParam.fincaId);
        });
    },


    remover: function (SubCCosto) {
        bootbox.confirm("¿Esta seguro que quiere remover centro de costo " + SubCCosto.nombre + "?", function (result) {
            if (result) {
                //call api remove
                SubCentro.Remover(SubCCosto, function (data) {
                    model.SubCCostoController.initialize(SubCCosto.fincaId);
                });
            }
        });
    },

    cancelar: function () {
        model.SubCCostoController.insertMode(false);
        model.SubCCostoController.gridMode(true);
        model.SubCCostoController.editMode(false);

        model.clearErrorMessage('#SubCCostoForm');
    },


    initialize: function (fincaId) {
        var self = this;
        model.SubCCostoController.subCCosto.fincaId(fincaId);
        model.SubCCostoController.gridMode(true);

        SubCentro.Listar(fincaId, function (data) {
            SubCCostos = data;
            model.SubCCostoController.SubCCostos(SubCCostos);
        });

        Actividad.Listar(function (data) {
            var actividades = data;
            model.SubCCostoController.actividades(actividades);
        });
    }
};

  model.SubCCostoController.filterSearch = ko.computed(function () {
    var q = model.SubCCostoController.search();

    return model.SubCCostoController.actividades().filter(function (i) {
        return i.nombre.toLowerCase().indexOf(q) >= 0;
    });
});