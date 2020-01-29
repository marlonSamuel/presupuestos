console.log("Define presupuesto controller ");


model.presupuestoMController = {

    presupuesto: {
        id: ko.observable(""),
        tipoId: ko.observable(""),
        tipo: ko.observable(""),
        paisId: ko.observable(""),
        semana: ko.observable(""),
        año: ko.observable(""),
        quincena: ko.observable(""),
        mes: ko.observable(""),
        total_estimado: ko.observable(0)
    },

    detalle: {
        id: ko.observable(""),
        presupuestoId: ko.observable(""),
        codigoCC: ko.observable(""),
        fincaId: ko.observable(""),
        loteId: ko.observable(""),
        referencia: ko.observable(""),
        referencia_producto: ko.observable(""),
        cantidad: ko.observable(""),
        total_ccosto: ko.observable(0),
        total_referencia: ko.observable(""),
        total_lote: ko.observable(0),
        costo_actividad: ko.observable(""),
        unidad_medida: ko.observable(""),
        estado: ko.observable(""),
        categoriaId: ko.observable(""),
        grupoId: ko.observable(""),
        tipo: ko.observable(""),
        cultivo: ko.observable(""),
        costo_by_p_cs: ko.observable(""),
        unidad_by_p_cs: ko.observable("")
    },

    detalleCarga: {
        presupuestoId: ko.observable(""),
        codigoTipo: ko.observable(""),
        detalle: ko.observableArray([]),
    },

    contable: {
        id: ko.observable(""),
        ccosto: ko.observable(""),
        subcentroId: ko.observable(""),
        presupuestoId: ko.observable(""),
        codigo_cuenta: ko.observable(""),
        cuenta_contable: ko.observable(""),
        loteId: ko.observable(""),
        enero: ko.observable(0),
        febrero: ko.observable(0),
        marzo: ko.observable(0),
        abril: ko.observable(0),
        mayo: ko.observable(0),
        junio: ko.observable(0),
        julio: ko.observable(0),
        agosto: ko.observable(0),
        septiembre: ko.observable(0),
        octubre: ko.observable(0),
        noviembre: ko.observable(0),
        diciembre: ko.observable(0),
        total: ko.observable(0),
    },

    presupuestos: ko.observableArray([]),
    presupuestoManoObra: ko.observableArray([]),
    allPresupuestos: ko.observableArray([]),
    lotes: ko.observableArray([]),
    actividades: ko.observableArray([]),
    groupByLotes: ko.observableArray([]),
    asignados: ko.observableArray([]),
    subccostos: ko.observableArray([]),
    categorias: ko.observableArray([]),
    productos: ko.observableArray([]),
    grupos: ko.observableArray([]),
    actividadesPrincipales: ko.observableArray([]),
    detalleGrupos: ko.observableArray([]),
    detalleCategorias: ko.observableArray([]),
    arrayMasivo: ko.observableArray([]),
    cuentas: ko.observableArray([]),
    detalleAllContable: ko.observableArray([]),
    totalFinca: ko.observable(0),
    totalLotes: ko.observable(0),
    total: ko.observable(0),
    totalContable: ko.observable(0),
    ccostos: ko.observableArray([]),
    detalleContable: ko.observableArray([]),
    unidades: ko.observableArray([]),
    insertMode: ko.observable(false),
    insertModeDetalle: ko.observable(false),
    gridMode: ko.observable(false),
    optionMode: ko.observable(true),
    detalleMode: ko.observable(false),
    viewMode: ko.observable(false),
    createMode: ko.observable(true),
    disable: ko.observable(false),
    isFile: ko.observable(false),
    buttonFile: ko.observable(false),
    isContable: ko.observable(false),
    isContableView: ko.observable(false),
    isCosecha: ko.observable(false),
    title: ko.observable(""),
    title2: ko.observable(""),
    namePresupuesto: ko.observable(""),
    nameFinca: ko.observable(""),
    nameCCosto: ko.observable(""),
    tipoName: ko.observable(""),
    codigoPresupuesto: ko.observable(""),
    referencia: ko.observable(""),
    codigoSubCentro: ko.observable(""),
    tipoOption: [{ nombre: 'SEMANAL', valor: 'S' }, { nombre: 'MENSUAL', valor: 'M' }],
    tipo: [{ nombre: 'Personal Fijo', valor: 'F' }, { nombre: 'Personal Temporal', valor: 'T' }],


    map: function (presupuesto) {
        model.presupuestoMController.disable(false);
        var ePresupuesto = model.presupuestoMController.presupuesto;
        ePresupuesto.id(presupuesto.Id);
        ePresupuesto.tipoId(presupuesto.tipoId);
        ePresupuesto.paisId(presupuesto.paisId);
        ePresupuesto.semana(presupuesto.semana);
        ePresupuesto.quincena(presupuesto.quincena);
        ePresupuesto.año(presupuesto.año);
        ePresupuesto.mes(presupuesto.mes);
        ePresupuesto.total_estimado(presupuesto.total_estimado);
    },

    mapDetalle: function (detalle_p) {
        var temRef = detalle_p.referencia;
        var eDetalle = model.presupuestoMController.detalle;
        eDetalle.id(detalle_p.id);
        eDetalle.presupuestoId(detalle_p.presupuestoId);
        eDetalle.referencia(detalle_p.referencia);
        $('#referencia').selectpicker('refresh');
        eDetalle.loteId(detalle_p.loteId);
        $('#subloteId').selectpicker('refresh');
        eDetalle.fincaId(detalle_p.fincaId);
        eDetalle.cantidad(detalle_p.cantidad);
        eDetalle.estado(detalle_p.estado);
        eDetalle.categoriaId(detalle_p.categoria);
        eDetalle.grupoId(detalle_p.grupo);
        $('#grupoId').selectpicker('refresh');
        eDetalle.tipo(detalle_p.tipo);
        eDetalle.costo_by_p_cs(detalle_p.costo_by_p_cs);
        eDetalle.unidad_by_p_cs(detalle_p.unidad_by_p_cs);

        //model.presupuestoMController.selectTipoActividad(detalle_p.tipo);

        model.presupuestoMController.selectCategoria(detalle_p.categoria);
        model.presupuestoMController.selectGrupo(detalle_p.grupo);

        var codigo_presupuesto = model.presupuestoMController.codigoPresupuesto();

        if (codigo_presupuesto === 'P_MO') {
            Actividad.ListarByFinca(detalle_p.fincaId, detalle_p.tipo, function (data) {
                var actividades = data;
                model.presupuestoMController.actividades(actividades);
                model.presupuestoMController.selectActividad(detalle_p.referencia);
            });
        }

        if (codigo_presupuesto == 'P_AG') {
            Bi.ListarByGrupo(parseInt(detalle_p.grupo), function (data) {
                var productos = data;
                model.presupuestoMController.productos(productos);
                model.presupuestoMController.selectProducto(detalle_p.referencia);
                eDetalle.referencia_producto(detalle_p.referencia);
            });
        } else if (codigo_presupuesto == 'P_VT') {
            Bi.ListarByGrupoVenta(parseInt(detalle_p.grupo), function (data) {
                var productos = data;
                model.presupuestoMController.productos(productos);
                model.presupuestoMController.selectProducto(detalle_p.referencia);
                eDetalle.referencia_producto(detalle_p.referencia);
            });
        }
        
    },

    nuevo: function () {
        var presupuesto = {
            Id: "",
            tipoId: "",
            paisId: "",
            semana: "",
            quincena: "",
            año: "",
            mes: "",
            total_estimado: 0
        };
        presupuesto.paisId = window.Pais.Id;
        presupuesto.tipoId = model.presupuestoMController.presupuesto.tipoId();
        model.presupuestoMController.map(presupuesto);

        model.presupuestoMController.gridMode(false);
        model.presupuestoMController.insertMode(true);
    },

    //setear titulo para el detalle 
    setTitle2: function (presupuesto) {
        var tipo = model.presupuestoMController.tipoName();
        if (presupuesto.tipo === "S") {
            if (presupuesto.semana < 10 ? presupuesto.semana = '0' + parseInt(presupuesto.semana) : presupuesto.semana);
            model.presupuestoMController.title2("Presupuesto " + tipo+" semanal " + presupuesto.semana + " del " + presupuesto.año);

        }else if (presupuesto.tipo === "M") {
            if (presupuesto.mes < 10 ? presupuesto.mes = '0' + parseInt(presupuesto.mes) : presupuesto.semana);
            model.presupuestoMController.title2("Presupuesto " + tipo +" mensual " + presupuesto.mes + " del " + presupuesto.año);
        }else{
            model.presupuestoMController.title2("Presupuesto Contable " + presupuesto.año);
        }
    },

    view: function (presupuesto) {
        model.presupuestoMController.totalFinca(0);
        model.presupuestoMController.viewMode(true);
        model.presupuestoMController.createMode(false);
        model.presupuestoMController.setTitle2(presupuesto);

        var codigoPresupuesto = model.presupuestoMController.codigoPresupuesto();
        if (codigoPresupuesto == "P_MO") {
            model.presupuestoMController.isContableView(false);
            model.presupuestoMController.referencia("Actividad");
        } else if (codigoPresupuesto == "P_CT") {
            model.presupuestoMController.isContableView(true);
        } else {
            model.presupuestoMController.isContableView(false);
            model.presupuestoMController.referencia("Producto");
        }

        model.presupuestoMController.map(presupuesto);
        var ccostos = model.presupuestoMController.ccostos();

        model.presupuestoMController.total(presupuesto.total_estimado);

        model.presupuestoMController.viewDetails(ccostos[0]);

    },

    viewStatus: function (presupuesto) {
        model.presupuestoMController.map(presupuesto);
        model.presupuestoMController.setTitle2(presupuesto);

        Detalle.ListarAsignados(presupuesto.Id, function (data) {
            var detalles = data;
            model.presupuestoMController.asignados(detalles);
        });
    },


    viewDetails: function (finca) {
        model.presupuestoMController.nameFinca(finca.descripcion);
        model.presupuestoMController.totalFinca(0);
        model.presupuestoMController.totalLotes(0);
        var presupuestoId = model.presupuestoMController.presupuesto.id();
        model.presupuestoMController.selectSubCentroCosto(finca.codigo);
        model.presupuestoMController.presupuestoManoObra([]);
        model.presupuestoMController.groupByLotes([]);

        var ccostos = model.presupuestoMController.subccostos();
    },

    viewInfoCCostos: function (centro_costo) {
        model.presupuestoMController.nameCCosto(centro_costo.nombre);
         var presupuestoId = model.presupuestoMController.presupuesto.id();


         var codigo = model.presupuestoMController.codigoPresupuesto();

         if (codigo !== 'P_CT') {
             model.presupuestoMController.ListarDetalles(presupuestoId, centro_costo.Id);
             model.presupuestoMController.LisarByLotes(presupuestoId, centro_costo.Id);
         }


         if (codigo === 'P_AG') {
             model.presupuestoMController.listarDetalleGroupByGrupo(presupuestoId, centro_costo.Id);
             model.presupuestoMController.listarDetalleGroupByCategoria(presupuestoId, centro_costo.Id);
         } else if (codigo == 'P_VT') {
             model.presupuestoMController.listarDetalleGroupByGrupoVenta(presupuestoId, centro_costo.Id);
         } else if (codigo == 'P_CT') {
             model.presupuestoMController.listarPContable(presupuestoId, centro_costo.Id);
         } else if (codigo === 'P_MO') {
         } else {
             //model.presupuestoMController.LisarByLotes(presupuestoId, centro_costo.Id);
         }
    },

    nuevoDetalle: function (presupuesto) {
        var codigo = model.presupuestoMController.codigoPresupuesto();

        model.presupuestoMController.insertModeDetalle(true); 

        model.presupuestoMController.disable(false);
        model.presupuestoMController.detalle.presupuestoId(presupuesto.Id);
        model.presupuestoMController.gridMode(false);

        model.presupuestoMController.setTitle2(presupuesto);
        model.presupuestoMController.total(presupuesto.total_estimado);
    },

    mapDetalleContable: function (contable) {
        var eContable = model.presupuestoMController.contable;
        eContable.id(contable.Id);
        eContable.loteId(contable.loteId);
        $('#loteIdC').selectpicker('refresh');
        eContable.cuenta_contable(contable.cuenta_contable);
        eContable.codigo_cuenta(contable.codigo_cuenta);
        $('#cuenta_contableC').selectpicker('refresh');
        eContable.enero(contable.enero);
        eContable.febrero(contable.febrero);
        eContable.marzo(contable.marzo);
        eContable.abril(contable.abril);
        eContable.mayo(contable.mayo);
        eContable.junio(contable.junio);
        eContable.julio(contable.julio);
        eContable.agosto(contable.agosto);
        eContable.septiembre(contable.septiembre);
        eContable.octubre(contable.octubre);
        eContable.noviembre(contable.noviembre);
        eContable.diciembre(contable.diciembre);
        eContable.total(contable.total);
    },

    nuevoDetalleContable: function(){
        var detalle = {
            Id: "",
            ccosto: "",
            loteId: "",
            cuenta_contable: "",
            codigo_cuenta: "",
            enero: 0,
            febrero: 0,
            marzo: 0,
            abril: 0,
            mayo: 0,
            junio: 0,
            julio: 0,
            agosto: 0,
            septiembre: 0,
            octubre: 0,
            noviembre: 0,
            diciembre: 0,
        }

        $("#displayId2").css("display", "block");
        model.presupuestoMController.mapDetalleContable(detalle);
    },

    editarDetalleContable: function (contable) {
        model.presupuestoMController.mapDetalleContable(contable);
        $("#displayId2").css("display", "block");
    },

    editar: function (presupuesto) {
        model.presupuestoMController.disable(false);
        model.presupuestoMController.map(presupuesto);

        model.presupuestoMController.gridMode(false);
        model.presupuestoMController.insertMode(true);
    },


    editarDetalle: function (detalle_p) {
        model.presupuestoMController.mapDetalle(detalle_p);
        $("#displayId").css("display", "block");
    },

    guardar: function () {
        var presupuesto = model.presupuestoMController.presupuesto;

        var codigo = model.presupuestoMController.codigoPresupuesto();

        if (codigo == 'P_CT') {
            presupuesto.tipo('A');
        }

        var presupuestoParam = ko.toJS(presupuesto);

        if (!model.validateForm('#presupuestoForm')) {
            return;
        }

        model.presupuestoMController.disable(true);

        if (presupuestoParam.id === "") {
            if (!model.presupuestoMController.validateRepite()) {
                toastr.error("Error! presupuesto ya ha sido creado");
                return;
            }
        }

        //call api save
        Presupuesto.Guardar(presupuestoParam, function (data) {
            console.log(data);
            toastr.info(data);
            model.presupuestoMController.ListarGrid(presupuestoParam.tipoId, presupuestoParam.tipo);
            model.presupuestoMController.insertMode(false);
            model.presupuestoMController.gridMode(true);
        });
    },

    guardarDetalle: function () {
        var detalle = model.presupuestoMController.detalle;
        var codigoPresupuesto = model.presupuestoMController.codigoPresupuesto();

        if (codigoPresupuesto === "P_AG") {
            detalle.referencia(detalle.referencia_producto());
        }

        if (!model.validateForm('#detalleForm')) {
            return;
        }

        model.presupuestoMController.disable(true);

        if (codigoPresupuesto === "P_CS") {
            detalle.total_referencia(detalle.cantidad() * detalle.costo_by_p_cs());
        } else {
            detalle.total_referencia(detalle.cantidad() * detalle.costo_actividad());
        }

        var detalleParams = ko.toJS(detalle);

        Detalle.Guardar(detalleParams, function (data) {
            toastr.info(data);
            model.presupuestoMController.ListarDetalles(detalleParams.presupuestoId, detalleParams.fincaId);
            model.presupuestoMController.getAllDetalle(detalleParams.presupuestoId);
            model.presupuestoMController.detalle.cantidad("");
            model.presupuestoMController.disable(false);
            $(".collapse").collapse('hide');
            $("#displayId").css("display", "none");
        });
    },

    guardarDetalleContable: function () {
        var contable = model.presupuestoMController.contable;
        var cuentas = model.presupuestoMController.cuentas();
        model.presupuestoMController.calculateTotalContable(contable);

        for (var i = 0; i < cuentas.length; i++) {
            if (contable.codigo_cuenta() === cuentas[i].numero) {
                contable.cuenta_contable(cuentas[i].nombre);
            }
        }

        var contableParams = ko.toJS(contable);
        Contable.Guardar(contableParams, function (data) {
            toastr.info(data);
            model.presupuestoMController.listarAllPContable(contableParams.presupuestoId);
            model.presupuestoMController.listarPContable(contableParams.presupuestoId, contableParams.subcentroId);
            $(".collapse").collapse('hide');
            $("#displayId2").css("display", "none");
        });
    },

    listarAllPContable: function (presupuestoId) {
        Contable.ListarByPresupuesto(presupuestoId, function (data) {
            var presupuestos = data;
            model.presupuestoMController.detalleAllContable(presupuestos);
            model.presupuestoMController.totalAmountAllContable();
            model.presupuestoMController.UpdateTotal(presupuestoId);
        });
    },

    listarPContable: function (presupuestoId, fincaId) {
        $.blockUI(blockUIoptions);
        Contable.ListarByPresupuestoFinca(presupuestoId, fincaId, function (data) {
            var presupuestos = data;
            model.presupuestoMController.detalleContable(presupuestos);
            model.presupuestoMController.totalAmountContable();
            //model.presupuestoMController.UpdateTotal(presupuestoId);
            $.unblockUI();
        });
    },

    //calcular total presupuesto contable
    calculateTotalContable: function (contable) {
        var enero = contable.enero();
        var febrero = contable.febrero();
        var marzo = contable.marzo();
        var abril = contable.abril();
        var mayo = contable.mayo();
        var junio = contable.junio();
        var julio = contable.julio();
        var agosto = contable.agosto();
        var septiembre = contable.septiembre();
        var octubre = contable.octubre();
        var noviembre = contable.noviembre();
        var diciembre = contable.diciembre();

        model.presupuestoMController.contable.total(
            parseInt(enero) + parseInt(febrero) + parseInt(marzo) + parseInt(abril) + parseInt(mayo)
            + parseInt(junio) + parseInt(julio) + parseInt(agosto) + parseInt(septiembre) + parseInt(octubre)
            + parseInt(noviembre) + parseInt(diciembre)
        );

    },

    clearDetalle: function () {
        model.presupuestoMController.detalle.cantidad("");
        model.presupuestoMController.detalle.id("");
        model.presupuestoMController.detalle.costo_by_p_cs("");
    },

    nuevoDetallePresupuesto: function () {
        model.presupuestoMController.clearDetalle();
        //$(".collapse").collapse('show');
        $("#displayId").css("display", "block");
    },

    ListarDetalles: function (presupuestoId, fincaId) {
        var codigoPresupuesto = model.presupuestoMController.codigoPresupuesto();
        if (codigoPresupuesto === "P_MO") {
            $.blockUI(blockUIoptions);
            Detalle.ListarByCCosto(presupuestoId, fincaId, function (data) {
                var detalles = data;
                model.presupuestoMController.presupuestoManoObra(detalles);
                model.presupuestoMController.totalAmountFinca();
                $.unblockUI();
            });
        } else if (codigoPresupuesto === "P_AG") {
            $.blockUI(blockUIoptions);
            Bi.ListarByCCosto(presupuestoId, fincaId, function (data) {
                var detalles = data;
                model.presupuestoMController.presupuestoManoObra(detalles);
                model.presupuestoMController.totalAmountFinca();
                $.unblockUI();
            });
        } else if (codigoPresupuesto === "P_CS") {
            $.blockUI(blockUIoptions);
            Detalle.ListarByCCostoCosecha(presupuestoId, fincaId, function (data) {
                var detalles = data;
                model.presupuestoMController.presupuestoManoObra(detalles);
                model.presupuestoMController.totalAmountFinca();
                $.unblockUI();
            });
        }else {
            $.blockUI(blockUIoptions);
            Bi.ListarByCCostoVenta(presupuestoId, fincaId, function (data) {
                var detalles = data;
                model.presupuestoMController.presupuestoManoObra(detalles);
                model.presupuestoMController.totalAmountFinca();
                $.unblockUI();
            });
        }
    },

    getAllDetalle: function (presupuestoId) {
        Detalle.ListarByPresupuesto(presupuestoId, function (data) {
            var presupuestos = data;
            model.presupuestoMController.allPresupuestos(presupuestos);

            var total = model.presupuestoMController.totalAmount();
            model.presupuestoMController.total(total);
            model.presupuestoMController.UpdateTotal(presupuestoId);
        });
    },

    LisarByLotes: function (presupuestoId, fincaId) {
        Detalle.ListarByGroup(presupuestoId, fincaId, function (data) {
            var presupuestos = data;
            var detalles = data;
            model.presupuestoMController.groupByLotes(detalles);
            var totalLote = model.presupuestoMController.totalAmountLote();
            model.presupuestoMController.totalLotes(totalLote);
        });
    },

    //listar detalles por grupo en presupuesto agricola
    listarDetalleGroupByGrupo: function (presupuestoId, fincaId) {
        Bi.ListarDetalleGroupByGrupo(presupuestoId, fincaId, function (data) {
            var detalleGrupos = data;
            model.presupuestoMController.detalleGrupos(detalleGrupos);
        });
    },

    listarDetalleGroupByGrupoVenta: function (presupuestoId, fincaId) {
        Bi.ListarDetalleGroupByGrupoVenta(presupuestoId, fincaId, function (data) {
            var detalleGrupos = data;
            model.presupuestoMController.detalleGrupos(detalleGrupos);
        });
    },


    //listar detalle por categoria en presupuesto agricola
    listarDetalleGroupByCategoria: function (presupuestoId, fincaId) {
        Bi.listarDetalleGroupByCategoria(presupuestoId, fincaId, function (data) {
            var detalleCategorias = data;
            model.presupuestoMController.detalleCategorias(detalleCategorias);
        });
    },

    UpdateTotal: function (presupuestoId) {
        var total_estimado = model.presupuestoMController.total();

        Detalle.UpdateTotal(presupuestoId, total_estimado, function (data) {
            console.log(data);
            var tipoId = model.presupuestoMController.presupuesto.tipoId();
            var tipo = model.presupuestoMController.presupuesto.tipo();

            model.presupuestoMController.ListarGrid(tipoId, tipo);
        });
    },

    remover: function (presupuesto) {
        bootbox.confirm("¿Esta seguro que quiere anular presupuesto ?", function (result) {
            if (result) {
                //call api remove
                Presupuesto.Remover(presupuesto.Id,'A', function (data) {
                    model.presupuestoMController.ListarGrid(presupuesto.tipoId, presupuesto.tipo);
                });
            }
        });
    },

    removerDetalle: function (presupuesto) {
        bootbox.confirm("¿Esta seguro que quiere eliminar este registro?", function (result) {
            if (result) {
                //call api remove
                Detalle.Remover(presupuesto, function (data) {
                    model.presupuestoMController.ListarDetalles(presupuesto.presupuestoId, presupuesto.fincaId);
                    model.presupuestoMController.getAllDetalle(presupuesto.presupuestoId);
                });
            }
        });
    },

    removerDetalleContable: function (detalle) {
        Contable.Remover(detalle, function (data) {
            model.presupuestoMController.listarPContable(detalle.presupuestoId, detalle.subcentroId);
            model.presupuestoMController.listarAllPContable(detalle.presupuestoId);
        });
    },

    cancelar: function () {
        model.presupuestoMController.insertMode(false);
        model.presupuestoMController.gridMode(true);
        model.presupuestoMController.insertModeDetalle(false);
        model.presupuestoMController.detalleMode(false);
        model.presupuestoMController.isFile(false);
        model.presupuestoMController.buttonFile(false);

        model.clearErrorMessage('#presupuestoForm');
        model.presupuestoMController.clearInput();
    },

    volver: function () {
        model.presupuestoMController.gridMode(false);
        model.presupuestoMController.optionMode(true);
        model.presupuestoMController.isContable(false);
    },

    volverToCreate: function () {
        model.presupuestoMController.viewMode(false);
        model.presupuestoMController.createMode(true);
    },

    volverToSelect: function () {
        model.presupuestoMController.detalleMode(false);
        model.presupuestoMController.insertModeDetalle(true);
        model.presupuestoMController.isContable(false);
    },

    seleccionar: function (opcion) {
        if (opcion.valor === 'S') {
            model.presupuestoMController.title("Presupuesto " + model.presupuestoMController.tipoName()+ " Semanal");
            model.presupuestoMController.namePresupuesto("Semana");
        } else if (opcion.valor === "Q") {
            model.presupuestoMController.title("Presupuesto  "+ model.presupuestoMController.tipoName() + " Quincenal");
            model.presupuestoMController.namePresupuesto("Quincena");
        } else if (opcion.valor === 'M') {
            model.presupuestoMController.title("Presupuesto " + model.presupuestoMController.tipoName() + " Mensual");
            model.presupuestoMController.namePresupuesto("Mes");
        } else {
            model.presupuestoMController.title("Presupuesto " + model.presupuestoMController.tipoName() + " Anual");
            model.presupuestoMController.namePresupuesto("");
        }

        model.presupuestoMController.presupuesto.tipo(opcion.valor);
        model.presupuestoMController.optionMode(false);
        model.presupuestoMController.gridMode(true);

        var tipoId = model.presupuestoMController.presupuesto.tipoId();

        model.presupuestoMController.ListarGrid(tipoId, opcion.valor);
    },

    //seteamos el nombre de la finca para mostrar en pantalla
    setFincaName: function (fincaId) {
        var ccostos = model.presupuestoMController.subccostos();
        for (var i = 0; i < ccostos.length; i++) {
            if (fincaId === ccostos[i].Id) {
                model.presupuestoMController.nameFinca(ccostos[i].nombre);
            }
        }
    },

    selectCCosto: function (finca) {
        $(".collapse").collapse('hide');
        $("#displayId").css("display", "none");
        var fincaId = finca.fincaId();
        var ccosto = finca.codigoCC();
        var codigoPresupuesto = model.presupuestoMController.codigoPresupuesto();
        model.presupuestoMController.isCosecha(false);
        

        var presupuestoId = model.presupuestoMController.detalle.presupuestoId();
        model.presupuestoMController.setFincaName(fincaId);

        model.presupuestoMController.totalFinca(0);
        if (codigoPresupuesto !== 'P_CT') {
            model.presupuestoMController.ListarDetalles(presupuestoId, fincaId);
        }

        model.presupuestoMController.detalleMode(true);
        model.presupuestoMController.insertModeDetalle(false);

        if (codigoPresupuesto !== 'P_CT') {
            SubLote.ListarByFinca(ccosto, function (data) {
                var lotes = data;
                model.presupuestoMController.lotes(lotes);
            });
        }

        if (codigoPresupuesto === 'P_MO') {
            model.presupuestoMController.referencia("Actividad");
            model.presupuestoMController.selectTipoActividad(finca.tipo());
        } else if (codigoPresupuesto == 'P_AG') {
            model.presupuestoMController.referencia("Producto");
            Bi.ListarCategorias(function (data) {
                var categorias = data;
                model.presupuestoMController.categorias(categorias);
            });
        } else if (codigoPresupuesto === 'P_VT') {
            model.presupuestoMController.referencia("Producto");
            model.presupuestoMController.selectCategoria(12);
        } else if (codigoPresupuesto === 'P_CS') {
            model.presupuestoMController.getUnidades();
            model.presupuestoMController.isCosecha(true);
        }else {
            model.presupuestoMController.detalleMode(false);
            model.presupuestoMController.isContable(true);
            model.presupuestoMController.contable.presupuestoId(presupuestoId);
            model.presupuestoMController.listarPContable(presupuestoId, fincaId);

            Dimension.ListarCuentas(function (data) {
                var cuentas = data;
                model.presupuestoMController.cuentas(cuentas);
            });

            model.presupuestoMController.selectLote(ccosto);
            model.presupuestoMController.contable.presupuestoId(presupuestoId);
            model.presupuestoMController.contable.subcentroId(fincaId);
        }
    },

    //setear actividad
    selectActividad: function (actividad) {
        if (actividad != undefined) {
            var actividades = model.presupuestoMController.actividades();
            for (var i = 0; i < actividades.length; i++) {
                if (actividad === actividades[i].codigo) {
                    model.presupuestoMController.detalle.costo_actividad(actividades[i].costo.toFixed(2));
                    model.presupuestoMController.detalle.unidad_medida(actividades[i].unidad_medida);
                }
            }
        }
    },


    //obtener por tipo temporal o fijo
    selectTipoActividad: function (tipo) {
        var fincaId = model.presupuestoMController.detalle.fincaId();
        if (fincaId != undefined) {
            Actividad.ListarByFinca(fincaId, tipo, function (data) {
                var actividades = data;
                model.presupuestoMController.actividades(actividades);
            });
        }
    },

    totalAmount: function () {
        var total = model.presupuestoMController.allPresupuestos().reduce(function (a, b) {
            return parseFloat(a) + parseFloat(b.total_referencia);
        }, 0);

        model.presupuestoMController.total(total);
        return total;
    },

    totalAmountContable: function () {
        var total = model.presupuestoMController.detalleContable().reduce(function (a, b) {
            return parseFloat(a) + parseFloat(b.total);
        }, 0);

        model.presupuestoMController.totalContable(total);
        return total;
    },

    totalAmountAllContable: function () {
        var total = model.presupuestoMController.detalleAllContable().reduce(function (a, b) {
            return parseFloat(a) + parseFloat(b.total);
        }, 0);

        model.presupuestoMController.total(total);
        return total;
    },

    totalAmountFinca: function () {
        var total = model.presupuestoMController.presupuestoManoObra().reduce(function (a, b) {
            return parseFloat(a) + parseFloat(b.total_referencia);
        }, 0);

        model.presupuestoMController.totalFinca(total);
        return total;
    },

    totalAmountLote: function () {
        var total = model.presupuestoMController.groupByLotes().reduce(function (a, b) {
            return parseFloat(a) + parseFloat(b.total_lote);
        }, 0);

        return total;
    },
    
    ListarGrid: function (tipoId, tipo) {
        var paisId = window.Pais.Id;
        Presupuesto.Listar(tipoId, tipo, paisId, function (data) {
            var presupuestos = data;
            model.presupuestoMController.presupuestos(presupuestos);
        });
    },

    //valida que no se ingresen presupuestos repetidos
    validateRepite: function () {
        var presupuestos = model.presupuestoMController.presupuestos();

        var semana = model.presupuestoMController.presupuesto.semana();
        var quincena = model.presupuestoMController.presupuesto.quincena();
        var mes = model.presupuestoMController.presupuesto.mes();
        var año = model.presupuestoMController.presupuesto.año();
        var tipo = model.presupuestoMController.presupuesto.tipo();
        var validate = true;

        for (var i = 0; i < presupuestos.length; i++) {
            if (tipo === "S") {
                console.log(presupuestos[i].semana);
                if (parseInt(semana) === presupuestos[i].semana && parseInt(año) === presupuestos[i].año) {
                    validate = false;
                }
            }
            if (tipo === "M") {
                if (parseInt(mes) === presupuestos[i].mes && parseInt(año) === presupuestos[i].año) {
                    validate = false;
                }
            }
        }

        return validate;
    },

    //finished: function (presupuestoId, fincaId) {
    //    var finished = false;
    //    ManoObra.IsFinished(presupuestoId, fincaId, function (data) {
    //    });
        
    //},

    FinishByFinca: function () {
        var presupuestoId = model.presupuestoMController.detalle.presupuestoId();
        var fincaId = model.presupuestoMController.detalle.fincaId();
        model.presupuestoMController.setFincaName(fincaId);
        var centro_costo = model.presupuestoMController.nameFinca();

        bootbox.confirm("¿Esta seguro que quiere finalizar presupuesto para centro de costo " + centro_costo + "?", function (result) {
            if (result) {
                //call api remove
                Detalle.FinalizarByFinca(presupuestoId, fincaId, function (data) {
                    console.log(data);
                    model.presupuestoMController.cancelar();
                    if (data === true ? toastr.info("Presupuesto de " + centro_costo + " finalizado") : toastr.error("No se pudo finalizar presupuesto"));
                });
            }
        });

    },

    finalizar: function () {
        var presupuestoId = model.presupuestoMController.presupuesto.id();
        var tipoId = model.presupuestoMController.presupuesto.tipoId();
        var tipo = model.presupuestoMController.presupuesto.tipo();
        bootbox.confirm("¿Esta seguro que quiere finalizar presupuesto ?", function (result) {
            if (result) {
                //call api remove
                Presupuesto.Remover(presupuestoId,'F', function (data) {
                    model.presupuestoMController.initialize();
                    if (data === true ? toastr.info("Presupuesto de finalizado") : toastr.error("No se pudo finalizar presupuesto"));
                    model.presupuestoMController.ListarGrid(tipoId, tipo);
                    $('#status').modal('hide');
                });
            }
        });
    },

    selectSubCentroCosto: function (fincaId) {
        console.log(fincaId);

        SubCentro.Listar(fincaId, function (data) {
            var subccostos = data;
            model.presupuestoMController.subccostos(subccostos);
        });

        model.presupuestoMController.selectLote(fincaId);
    },

    selectCategoria: function (categoriaId) {
        console.log(categoriaId);
        if (categoriaId != undefined) {
            Bi.ListarByCategoria(parseInt(categoriaId), function (data) {
                var grupos = data;
                model.presupuestoMController.grupos(grupos);
            });
        }
    },

    selectGrupo: function (grupoId) {
        console.log(grupoId);
        if (grupoId != undefined) {
            var codigo_presupuesto = model.presupuestoMController.codigoPresupuesto();
            if (codigo_presupuesto == 'P_AG') {
                Bi.ListarByGrupo(parseInt(grupoId), function (data) {
                    var productos = data;
                    model.presupuestoMController.productos(productos);
                });
            } else {
                Bi.ListarByGrupoVenta(parseInt(grupoId), function (data) {
                    var productos = data;
                    model.presupuestoMController.productos(productos);
                });
            }
        }
    },

    selectProducto: function (producto) {
        var productos = model.presupuestoMController.productos();
        var referencia = producto;
        for (var i = 0; i < productos.length; i++) {
            if (producto === productos[i].codigo) {
                model.presupuestoMController.detalle.costo_actividad(productos[i].costo.toFixed(2));
                model.presupuestoMController.detalle.unidad_medida(productos[i].unidad_medida);
            }
        }
    },

    exelAgricola: function (presupuesto) {
        var url = "/reporte/ReporteAgricola?presupuestoId=" + presupuesto.Id;
        $(location).attr('href', url);
    },

    cargaMasiva: function (presupuesto) {
        model.presupuestoMController.setTitle2(presupuesto);
        model.presupuestoMController.map(presupuesto);
        model.presupuestoMController.isFile(true);
        model.presupuestoMController.disable(false);
        model.presupuestoMController.gridMode(false);
        model.presupuestoMController.insertModeDetalle(false);
        model.presupuestoMController.GetItemsByCargaMasiva();
         
    },

    saveFile: function () {
        var arrayMasivo = JSON.parse(model.presupuestoMController.arrayMasivo());
        var presupuestoId = model.presupuestoMController.presupuesto.id();
        var codigoTipo = model.presupuestoMController.codigoPresupuesto();

        var newArray = [];

        var carga = model.presupuestoMController.detalleCarga;
        carga.presupuestoId = presupuestoId;
        carga.codigoTipo = codigoTipo;

        for (var i = 0; i < arrayMasivo.length; i++) {
            model.presupuestoMController.selectSubcentro(arrayMasivo[i].subcentro_costo);
            var lote = arrayMasivo[i].lote;
            if (lote != undefined) {
                model.presupuestoMController.selectSublote(arrayMasivo[i].lote);
            } else {
                model.presupuestoMController.selectSublote(arrayMasivo[i].productor);
            }

            if (codigoTipo === 'P_MO') {
                model.presupuestoMController.selectActividad(arrayMasivo[i].referencia);
            } else if (codigoTipo === 'P_VT' || codigoTipo === 'P_AG') {
                model.presupuestoMController.selectProducto(arrayMasivo[i].referencia);
            }

            var nDetalle = new Object();
            if (codigoTipo === 'P_CT') {
                nDetalle.subcentroId = model.presupuestoMController.detalle.fincaId();
                nDetalle.loteId = model.presupuestoMController.detalle.loteId();
                nDetalle.codigo_cuenta = arrayMasivo[i].codigo_cuenta;
                nDetalle.cuenta_contable = arrayMasivo[i].nombre_cuenta;
                nDetalle.enero = arrayMasivo[i].enero;
                nDetalle.febrero = arrayMasivo[i].febrero;
                nDetalle.marzo = arrayMasivo[i].marzo;
                nDetalle.abril = arrayMasivo[i].abril;
                nDetalle.mayo = arrayMasivo[i].mayo;
                nDetalle.junio = arrayMasivo[i].junio;
                nDetalle.julio = arrayMasivo[i].julio;
                nDetalle.agosto = arrayMasivo[i].agosto;
                nDetalle.septiembre = arrayMasivo[i].septiembre;
                nDetalle.octubre = arrayMasivo[i].octubre;
                nDetalle.noviembre = arrayMasivo[i].noviembre;
                nDetalle.diciembre = arrayMasivo[i].diciembre;
            } else if (codigoTipo === 'P_CS') {
                nDetalle.fincaId = model.presupuestoMController.detalle.fincaId();
                nDetalle.subloteId = model.presupuestoMController.detalle.loteId();
                nDetalle.unidad_by_p_cs = arrayMasivo[i].unidad;
                nDetalle.costo_by_p_cs = arrayMasivo[i].costo_unitario;
                nDetalle.cantidad = arrayMasivo[i].cantidad;
                nDetalle.total_referencia = nDetalle.costo_by_p_cs * nDetalle.cantidad;
            } else {
                nDetalle.fincaId = model.presupuestoMController.detalle.fincaId();
                nDetalle.subloteId = model.presupuestoMController.detalle.loteId();
                nDetalle.referencia = arrayMasivo[i].referencia;
                nDetalle.cantidad = arrayMasivo[i].cantidad;
                nDetalle.total_referencia = nDetalle.cantidad * model.presupuestoMController.detalle.costo_actividad();
            }

            newArray.push(nDetalle);
        }

        carga.detalle(newArray);

        cargaParams = ko.toJS(carga);

        $.blockUI(blockUIoptions);
        if (codigoTipo === 'P_CT') {
            Detalle.SaveFileContable(cargaParams, function (data) {
                bootbox.alert(data);
                model.presupuestoMController.listarAllPContable(cargaParams.presupuestoId);

                var tipoId = model.presupuestoMController.presupuesto.tipoId();
                var tipo = model.presupuestoMController.presupuesto.tipo();

                model.presupuestoMController.ListarGrid(tipoId, tipo);
                model.presupuestoMController.isFile(false);
                model.presupuestoMController.gridMode(true);
                $.unblockUI();
                model.presupuestoMController.clearInput();
            });
        } else {
            Detalle.SaveFile(cargaParams, function (data) {
                bootbox.alert(data);
                model.presupuestoMController.getAllDetalle(cargaParams.presupuestoId);
                var tipoId = model.presupuestoMController.presupuesto.tipoId();
                var tipo = model.presupuestoMController.presupuesto.tipo();

                model.presupuestoMController.ListarGrid(tipoId, tipo);
                model.presupuestoMController.isFile(false);
                model.presupuestoMController.gridMode(true);
                $.unblockUI();
                model.presupuestoMController.clearInput();
            });
        }
    },

    clearInput: function () {
        $("#inputFile")[0].value = "";
        $("#xlx_json").val("");
    },

    //convert excel to json
    exelToJson: function (file) {
        var reader = new FileReader();
        var json_object;

        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });
            workbook.SheetNames.forEach(function (sheetName) {
                // Here is your object
                var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                json_object = JSON.stringify(XL_row_object);
                model.presupuestoMController.arrayMasivo(json_object);
                console.log(JSON.parse(json_object));
                jQuery('#xlx_json').val(JSON.parse(json_object).length+" Lineas importadas");
            })
        };

        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    },

    GetItemsByCargaMasiva: function () {
        var codigo = model.presupuestoMController.codigoPresupuesto();

        SubCentro.GetAll(function (data) {
            var subccostos = data;
            model.presupuestoMController.subccostos(subccostos);
        }); 

        if (codigo === 'P_MO') {
            Actividad.Listar(function (data) {
                var actividades = data;
                model.presupuestoMController.actividades(actividades);
            });
        } else if (codigo === 'P_AG') {
            Bi.GetAllProductosA(function (data) {
                var productos = data;
                model.presupuestoMController.productos(productos);
            });
        } else if(codigo === 'P_VT') {
            Bi.GetAllProductosV(function (data) {
                var productos = data;
                model.presupuestoMController.productos(productos);
            });
        }

        if (codigo === 'P_CT') {
            var paisId = window.Pais.Id;
            Lote.Listar(paisId,function (data) {
                var lotes = data;
                model.presupuestoMController.lotes(lotes);
            });
        } else {
            SubLote.GetAll(function (data) {
                var lotes = data;
                model.presupuestoMController.lotes(lotes);
            });
        }
    },

    selectSubcentro: function (codigo) {
        var subccostos = model.presupuestoMController.subccostos();
        for (var i = 0; i < subccostos.length; i++) {
            if (codigo === subccostos[i].codigo || codigo === subccostos[i].codigo_nav) {
                model.presupuestoMController.detalle.fincaId(subccostos[i].Id);
            }
        }
    },

    selectSubLotebyId: function (Id) {
        var lotes = model.presupuestoMController.lotes();
        for (var i = 0; i < lotes.length; i++) {
            if (Id === lotes[i].Id) {
                model.presupuestoMController.detalle.cultivo(lotes[i].cultivo);
            }
        }
    },

    selectSublote: function (codigo) {
        var lotes = model.presupuestoMController.lotes();
        for (var i = 0; i < lotes.length; i++) {
            if (codigo === lotes[i].codigo) {
                model.presupuestoMController.detalle.loteId(lotes[i].Id);
            }
        }
    },

    getUnidades: function () {
        Unidad.Listar(function (data) {
            var unidades = data;
            model.presupuestoMController.unidades(unidades);
        });
    },

    selectLote: function (fincaId) {
        Lote.ListarByFinca(fincaId, function (data) {
            var lotes = data;
            model.presupuestoMController.lotes(lotes);
        });
    },

    initialize: function (tipoId) {
        model.presupuestoMController.presupuesto.tipoId(tipoId);

        var paisId = window.Pais.Id;
         CCosto.Listar(paisId,function (data) {
            var ccostos = data;
            model.presupuestoMController.ccostos(ccostos);
        });
    }
};