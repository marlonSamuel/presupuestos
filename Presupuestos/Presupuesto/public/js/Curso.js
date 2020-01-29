console.log("Define curs0 ");


model.cursoController = {

    curso: {
        cursoId: ko.observable(""),
        nombre: ko.observable(""),
    },

    cursos: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),


    mapCurso: function (curso) {
        var eCurso = model.cursoController.curso;
        eCurso.cursoId(curso.idCurso);
        eCurso.nombre(curso.nombre);
    },

    nuevo: function () {
        var curso = {
            cursoId: "",
            nombre: "",
        };
        

        model.cursoController.mapCurso(curso);

        model.cursoController.insertMode(true);
        model.cursoController.gridMode(false);
    },


    editar: function (curso) {

        model.cursoController.mapCurso(curso);
        
        model.cursoController.gridMode(false);
        model.cursoController.insertMode(false);
    },

    guardar: function () {

        var curso = model.cursoController.curso;
        var cursoParam = ko.toJS(curso);

        if (!model.validateForm('#cursoform')) {
            return;
        }
        //call api save
        Curso.Guardar(cusoParam, function (data) {

            console.log(data);
            toastr.info(data.response);
        });
    },

    remover: function (curso) {
        bootbox.confirm("¿Esta seguro que quiere remover curso " + curso.nombre + "?", function (result) {
            if (result) {
                //call api remove
                Curso.Remover(curso, function (data) {
                    model.cursoController.initialize();
                });
            }
        })
    },

    cancelar: function () {
        model.cursoController.insertMode(false);
        model.cursoController.gridMode(true);

        model.clearErrorMessage('#cursoForm');
    },


    initialize: function () {
        var self = this;
        var cursos = this.cursos();

        Curso.Listar(function (data) {
            cursos = data;
            slef.cursos(cursos);
        });
    }
};