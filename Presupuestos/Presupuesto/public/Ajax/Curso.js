console.log("define curso ajax");

Curso = {
    Listar: function (callback) {
        $.ajax("/api/curso/Listar", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las categorias");
            }
        });
    },

    Guardar: function (curso, callback) {
        $.ajax("/api/curso/Guardar", {
            type: "POST",
            data: curso,
            success: function (data) {
                if (data != null) {
                    toastr.info("categoria guardada correctamente");

                }
                else {
                    toastr.error("Error al guardar");
                }
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (data) {
                toastr.error("Error al guardar");
            }
        });
    },

    Remover: function (curso, callback) {
        $.ajax("/api/curso/Remover?id="+curso.cursoId, {
            type: "DELETE",
            data: categoria,
            success: function (data) {
                if (data != null) {
                    toastr.info("categoria removida correctamente");

                }
                else {
                    toastr.error("Error al eliminar");
                }
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (data) {
                toastr.error("Error al eliminar");
            }
        });
    },


}