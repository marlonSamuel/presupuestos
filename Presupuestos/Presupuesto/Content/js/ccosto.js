console.log("Define centro de costo ");


model.ccostoController = {

    ccosto: {
        codigo: ko.observable(""),
        descripcion: ko.observable(""),
        codigoNav: ko.observable(""),
        region: ko.observable(""),
        paisId: ko.observable("")
    },

    search: ko.observable(""),
    ccostos: ko.observableArray([]),
    actividades: ko.observableArray([]),
    regiones: ko.observableArray([]),
    addActividades: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    editMode: ko.observable(false),
    disable: ko.observable(false),


    map: function (ccosto) {
        //model.ccostoController.getDetalle(ccosto.codigo);
        model.ccostoController.disable(false);
        var eCcosto = model.ccostoController.ccosto;
        eCcosto.codigo(ccosto.codigo);
        eCcosto.descripcion(ccosto.descripcion);
        eCcosto.codigoNav(ccosto.CodigoNAV);
        eCcosto.paisId(ccosto.paisId);
    },

    nuevo: function () {
        var ccosto = {
            codigo: "",
            descripcion: "",
            codigoNav: "",
            paisId: ""
        };

        ccosto.paisId = window.Pais.Id;
        model.ccostoController.map(ccosto);

        model.ccostoController.insertMode(true);
        model.ccostoController.gridMode(false);
        model.ccostoController.editMode(false);
    },


    editar: function (ccosto) {
        model.ccostoController.map(ccosto);
        model.SubCCostoController.initialize(ccosto.codigo);

        model.ccostoController.gridMode(false);
        model.ccostoController.insertMode(false);
        model.ccostoController.editMode(true);
    },

    guardar: function () {
        var ccosto = model.ccostoController.ccosto;
        var ccostoParam = ko.toJS(ccosto);

        if (!model.validateForm('#ccostoForm')) {
            return;
        }

        model.ccostoController.disable(true);
        //call api save
        CCosto.Guardar(ccostoParam,'G', function (data) {
            
            toastr.info(data);
            model.ccostoController.insertMode(false);
            model.ccostoController.editMode(false);
            model.ccostoController.gridMode(true);
            model.ccostoController.initialize();
        });
    },

    actualizar: function () {
        model.presupuestoMController.disable(true);
        var ccosto = model.ccostoController.ccosto;
        var ccostoParam = ko.toJS(ccosto);

        if (!model.validateForm('#ccostoForm')) {
            return;
        }
        //call api save
        CCosto.Guardar(ccostoParam, 'A', function (data) {
            toastr.info(data);
            model.ccostoController.insertMode(false);
            model.ccostoController.editMode(false);
            model.ccostoController.gridMode(true);
            model.ccostoController.initialize();
        });
    },

    remover: function (ccosto) {
        bootbox.confirm("¿Esta seguro que quiere remover centro de costo " + ccosto.Descripcion + "?", function (result) {
            if (result) {
                //call api remove
                CCosto.Remover(ccosto, function (data) {
                    model.ccostoController.initialize();
                });
            }
        });
    },

    cancelar: function () {
        model.ccostoController.insertMode(false);
        model.ccostoController.gridMode(true);
        model.ccostoController.editMode(false);
        model.SubCCostoController.insertMode(false);
        model.SubCCostoController.gridMode(false);
        model.SubCCostoController.editMode(false);

        model.clearErrorMessage('#ccostoForm');
    },


    initialize: function () {
        var self = this;
        var ccostos = this.ccostos();
        var paisId = window.Pais.Id;

        CCosto.Listar(paisId,function (data) {
            ccostos = data;
            model.ccostoController.ccostos(ccostos);
        });
        
        Region.Listar(function (data) {
            regiones = data;
            model.ccostoController.regiones(regiones);
        });
        
    }
};
