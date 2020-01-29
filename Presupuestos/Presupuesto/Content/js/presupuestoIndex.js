console.log("Define define presupuestoIndex controller");


model.pindexController = {
    presupuestos: {
        agricola: ko.observable(""),
        costosygastos: ko.observable(""),
        ventas: ko.observable(""),
        manoObra: ko.observable("")
    },

    ccostos: ko.observableArray([]),
    tipos: ko.observableArray([]),
    ccostosMode: ko.observable(false),
    indexMode: ko.observable(true),
    tipoId: ko.observable(""),


    cancelar: function () {
        model.pindexController.ccostosMode(false);
        model.pindexController.indexMode(true);
    },

    seleccionar: function (tipo) {
        model.pindexController.ccostosMode(true);
        model.pindexController.indexMode(false);
        model.pindexController.tipoId(tipo.tipoId);
    },

    irA: function (tipo) {
        var url = route + "/presupuesto/" + tipo.url + "?id=" + tipo.tipoId;
        $(location).attr('href', url);
    },


    initialize: function () {
        var self = this;
        var ccostos = this.ccostos();

        console.log(model.pindexController.presupuestos);

        //CCosto.Listar(function (data) {
        //    ccostos = data;
        //    model.pindexController.ccostos(ccostos);
        //});
        var userId = window.User.Id;
        Presupuesto.ListarByUser(userId,function (data) {
            tipos = data;
            model.pindexController.tipos(tipos);
        });
    }
};