console.log("Define lote controller ");


model.loteController = {

    lote: {
        Id: ko.observable(""),
        nombre: ko.observable(""),
        fincaId: ko.observable(""),
        codigo: ko.observable(""),
        descripcion: ko.observable("")
    },

    sublote: {
        Id: ko.observable(""),
        nombre: ko.observable(""),
        codigo: ko.observable(""),
        loteId: ko.observable(""),
        metros: ko.observable(""),
        altura: ko.observable(""),
        ancho: ko.observable(""),
        densidad: ko.observable(""),
        descripcion: ko.observable(""),
        cultivoId: ko.observable(""),
        factor_manzana: ko.observable(""),
        padre: ko.observable(""),
        techado: ko.observable(""),
        fecha_inicio: ko.observable(""),
        fecha_fin: ko.observable(""),
        variedadId: ko.observable(""),
        region: ko.observable(""),
    },

    detalle: {
        detalleId: ko.observable(""),
        cultivoId: ko.observable(""),
        variedadId: ko.observable("")
    },

    GrupoCultivo: ko.observable(""),
    lotes: ko.observableArray([]),
    sublotes: ko.observableArray([]),
    ccostos: ko.observableArray([]),
    cultivos: ko.observableArray([]),
    variedades: ko.observableArray([]),
    regiones: ko.observableArray([]),
    isTechado: [{ valor: 'S', nombre: 'Si' }, {valor: 'N', nombre: 'No'}],
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    insertModeSublote: ko.observable(false),
    gridModeSublote: ko.observable(false),
    disable: ko.observable(false),
    codigoLote: ko.observable(""),



    map: function (lote) {
        model.loteController.disable(false);
        var eLote = model.loteController.lote;
        eLote.Id(lote.Id);
        eLote.codigo(lote.codigo);
        eLote.nombre(lote.nombre);
        eLote.fincaId(lote.fincaId);
        eLote.descripcion(lote.descripcion);
    },

    mapSublote: function (lote) {
        model.loteController.disable(false);
        var eLote = model.loteController.sublote;
        eLote.Id(lote.Id);
        eLote.nombre(lote.nombre);
        eLote.codigo(lote.codigo);
        eLote.loteId(lote.loteId);
        eLote.metros(lote.metros);
        eLote.altura(lote.altura);
        eLote.ancho(lote.ancho);
        eLote.densidad(lote.densidad);
        eLote.techado(lote.techado);
        eLote.fecha_inicio(lote.fecha_inicio);
        eLote.fecha_fin(lote.fecha_fin);
        eLote.factor_manzana(lote.factor_manzana);
        eLote.variedadId(lote.variedadId);
        eLote.fecha_inicio(lote.fecha_inicio);
        eLote.fecha_fin(lote.fecha_fin);
        eLote.region(lote.region);
    },

    nuevo: function () {
        var lote = {
            Id: "",
            codigo: "",
            nombre: "",
            fincaId: "",
            descripcion: "",
        };
        model.loteController.map(lote);

        model.loteController.insertMode(true);
        model.loteController.gridMode(false);
    },

    nuevoSublote: function () {
        var sublote = {
            Id: "",
            nombre: "",
            codigo: "",
            loteId: "",
            metros: "",
            altura: "",
            ancho: "",
            techado: "",
            factor_manzana: "",
            fecha_inicio: "",
            densidad: "",
            variedadId: "",
            fecha_fin: "",
            region: ""
        }

        sublote.loteId = model.loteController.lote.Id();

        model.loteController.mapSublote(sublote);
        model.loteController.insertModeSublote(true);
        model.loteController.gridModeSublote(false);      
    },

    validateSublote: function (loteId) {
        var lotes = model.loteController.lotes();
        for (var i = 0; i < lotes.length; i++) {
            if (loteId === lotes[i].Id) {
                model.loteController.codigoLote(lotes[i].codigo);
            }
        }
    },


    editar: function (lote) {
        model.loteController.nuevo();
        model.loteController.map(lote);

        model.loteController.gridMode(false);
        model.loteController.insertMode(true);

        model.loteController.initializeSublote(lote.Id);
        model.loteController.validateSublote(lote.Id);
    },

    editarSubLote: function (sublote) {

        model.loteController.mapSublote(sublote);
        //model.loteController.seleccionar();
        model.loteController.gridModeSublote(false);
        model.loteController.insertModeSublote(true);
    },

    guardar: function () {
        var lote = model.loteController.lote;
        var loteParam = ko.toJS(lote);

        if (!model.validateForm('#loteForm')) {
            return;
        }

        model.loteController.disable(true);
        //call api save
        Lote.Guardar(loteParam, function (data) {

            model.loteController.initialize();
            toastr.info(data);
            model.loteController.insertMode(false);
            model.loteController.gridMode(true);
        });
    },


    guardarSublote: function () {

        var sublote = model.loteController.sublote;

        var subloteParam = ko.toJS(sublote);

        if (!model.validateForm('#loteForm')) {
            return;
        }

        model.loteController.disable(true);
        //call api save
        SubLote.Guardar(subloteParam, function (data) {
            model.loteController.initializeSublote(subloteParam.loteId);
            toastr.info(data);
            model.loteController.insertModeSublote(false);
            model.loteController.gridModeSublote(true);
        });
    },

    remover: function (lote) {
        bootbox.confirm("¿Esta seguro que quiere desactivar lote " + lote.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Lote.ActivarODesactivar(lote,"I", function (data) {
                    model.loteController.initialize();
                });
            }
        })
    },

    removerSublote: function (sublote) {
        bootbox.confirm("¿Esta seguro que quiere desactivar lote " + sublote.nombre + "?", function (result) {
            if (result) {
                //call api remove
                SubLote.ActivarODesactivar(sublote, "I", function (data) {
                    model.loteController.initializeSublote(sublote.loteId);
                });
            }
        })
    },

    activar: function (lote) {
        bootbox.confirm("¿Esta seguro que quiere activar lote " + lote.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Lote.ActivarODesactivar(lote, "A", function (data) {
                    model.loteController.initialize();
                });
            }
        })
    },

    activarSublote: function (sublote) {
        bootbox.confirm("¿Esta seguro que quiere activar sublote " + sublote.nombre + "?", function (result) {
            if (result) {
                //call api remove
                SubLote.ActivarODesactivar(sublote, "A", function (data) {
                    model.loteController.initializeSublote(sublote.loteId);
                });
            }
        })
    },

    seleccionar: function () {

        var GrupoCultivo = model.loteController.GrupoCultivo();
        model.loteController.sublote.cultivoId(GrupoCultivo);

        Variedad.ListarG(GrupoCultivo, function (data) {
            var variedades = data;
            model.loteController.variedades(variedades);
        });
    },

    cancelar: function () {
        model.loteController.insertMode(false);
        model.loteController.gridMode(true);
        model.loteController.insertModeSublote(false);
        model.loteController.gridModeSublote(false);
        //model.loteController.addVariedades([]);

        model.clearErrorMessage('#loteForm');
    },

    cancelarSublote: function () {
        model.loteController.insertModeSublote(false);
        model.loteController.gridModeSublote(true);
        //model.loteController.addVariedades([]);

        model.clearErrorMessage('#subloteForm');
    },

    initializeSublote: function (loteId) {
        var paisId = window.Pais.Id;
        model.loteController.gridModeSublote(true);

        SubLote.Listar(loteId, function (data) {
            var sublotes = data;
            model.loteController.sublotes(sublotes);
        });

        Cultivo.ListarA(function (data) {
            var cultivos = data;
            model.loteController.cultivos(cultivos);
        });
    },


    initialize: function () {
        var paisId = window.Pais.Id;

        Lote.Listar(paisId,function (data) {
            var lotes = data;
            model.loteController.lotes(lotes);
        });

        CCosto.Listar(paisId, function (data) {
            var ccostos = data;
            model.loteController.ccostos(ccostos);
        });

        Region.Listar(function (data) {
            regiones = data;
            model.ccostoController.regiones(regiones);
        });
    }
};


            //bootbox.confirm({
            //    message: data + " desea seguir agregando cultivos al lote?",
            //    buttons: {
            //        confirm: {
            //            label: 'Si',
            //            className: 'btn-success'
            //        },
            //        cancel: {
            //            label: 'No',
            //            className: 'btn-danger'
            //        }
            //    },
            //    callback: function (result) {
            //        if (result) {

            //        } else {
            //            model.loteController.initialize();
            //            toastr.info(data);
            //            model.loteController.insertMode(false);
            //            model.loteController.gridMode(true);
            //            model.loteController.addVariedades([]);
            //        }
            //    }
            //});