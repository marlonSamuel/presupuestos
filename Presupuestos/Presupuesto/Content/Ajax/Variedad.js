console.log("define variedad ajax");

Variedad = {
    Listar: function (callback) {
        $.ajax(route +"/api/variedad", {
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

    ListarG: function (Grupo, callback) {
        $.ajax(route +"/api/variedad/get?grupo=" + Grupo, {
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

    Guardar: function (variedad, callback) {
        $.ajax(route +"/api/variedad/post", {
            type: "POST",
            data: variedad,
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

    Remover: function (variedad, callback) {
        $.ajax(route +"/api/cultivo/delete?id=" + variedad.codigo, {
            type: "DELETE",
            data: variedad,
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