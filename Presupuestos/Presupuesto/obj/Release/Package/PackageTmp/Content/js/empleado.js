console.log("Define empleado controller ");


model.empleadoController = {

    empleado: {
        empleadoId: ko.observable(""),
        codigo: ko.observable(""),
        cargoId: ko.observable(""),
        departamentoId: ko.observable(""),
        fincaId: ko.observable(""),
        primer_nombre: ko.observable(""),
        segundo_nombre: ko.observable(""),
        primer_apellido: ko.observable(""),
        segundo_apellido: ko.observable(""),
        dpi: ko.observable(""),
        codPila: ko.observable(""),
        tipoPila: ko.observable(""),
        foto: ko.observable(""),
        salario: ko.observable(""),
        dpi: ko.observable(""),
        telefono: ko.observable(""),
        tipoId: ko.observable("")
    },

   
    empleados: ko.observableArray([]),
    cargos: ko.observableArray([]),
    departamentos: ko.observableArray([]),
    fincas: ko.observableArray([]),
    tipos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),



    map: function (empleado) {
        var eEmpleado = model.empleadoController.empleado;
        eEmpleado.empleadoId(empleado.empleadoId);
        eEmpleado.codigo(empleado.codigo);
        eEmpleado.dpi(empleado.dpi);
        eEmpleado.fincaId(empleado.fincaId);
        eEmpleado.cargoId(empleado.cargoId);
        eEmpleado.tipoId(empleado.tipoId);
        eEmpleado.departamentoId(empleado.departamentoId);
        eEmpleado.primer_nombre(empleado.primer_nombre);
        eEmpleado.segundo_nombre(empleado.segundo_nombre);
        eEmpleado.primer_apellido(empleado.primer_apellido);
        eEmpleado.segundo_apellido(empleado.segundo_apellido);
        eEmpleado.telefono(empleado.telefono);
        eEmpleado.salario(empleado.salario);
        eEmpleado.codPila(empleado.codPila);
        eEmpleado.tipoPila(empleado.tipoPila);
    },

    nuevo: function () {
        var empleado = {
            empleadoId: "",
            codigo: "",
            cargoId: "",
            tipoId: "",
            departamentoId: "",
            fincaId: "",
            primer_nombre: "",
            segundo_nombre: "",
            primer_apellido: "",
            segundo_apellido: "",
            dpi: "",
            telefono: "",
            salario: "",
            codPila: "",
            tipoPila: "",

        };

        model.empleadoController.map(empleado);

        model.empleadoController.insertMode(true);
        model.empleadoController.gridMode(false);
    },


    editar: function (empleado) {


        model.empleadoController.map(empleado);

        model.empleadoController.gridMode(false);
        model.empleadoController.insertMode(true);
    },

    guardar: function () {

        var empleado = model.empleadoController.empleado;

        var empleadoParam = ko.toJS(empleado);

        if (!model.validateForm('#empleadoForm')) {
            return;
        }
        //call api save
        Empleado.Guardar(empleadoParam, function (data) {
            toastr.info(data);
            model.empleadoController.insertMode(false);
            model.empleadoController.gridMode(true);
        });
    },

    remover: function (empleado) {
        bootbox.confirm("¿Esta seguro que quiere remover empleado " + empleado.codigo + "?", function (result) {
            if (result) {
                //call api remove
                Empleado.Remover(empleado, function (data) {
                    model.empleadoController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.empleadoController.insertMode(false);
        model.empleadoController.gridMode(true);
        model.empleadoController.addPresentaciones([]);

        model.clearErrorMessage('#empleadoForm');
    },

    initialize: function () {

        Empleado.Listar(function (data) {
            var empleados = data;
            model.empleadoController.empleados(empleados);
        });

        CCosto.Listar(function (data) {
            var fincas = data;
            model.empleadoController.fincas(fincas);
        });

        Departamento.Listar(function (data) {
            var departamentos = data;
            model.empleadoController.departamentos(departamentos);
        });

        Cargo.Listar(function (data) {
            var cargos = data;
            model.empleadoController.cargos(cargos);
        });

        TipoEmpleado.Listar(function (data) {
            var tipos = data;
            model.empleadoController.tipos(tipos);
        });
    }
};