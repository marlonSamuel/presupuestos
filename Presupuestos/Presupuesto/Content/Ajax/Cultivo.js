console.log("define cultivo ajax");

Cultivo = {
    Listar: function (callback) {
        $.ajax(route +"/api/cultivo/get", {
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

    ListarA: function (callback) {
        $.ajax(route +"/api/cultivo/get?activos=A", {
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
        $.ajax(route +"/api/cultivo/post", {
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

    Remover: function (cultivo, callback) {
        $.ajax(route +"/api/cultivo/delete?id="+curso.codigo, {
            type: "DELETE",
            data: cultivo,
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