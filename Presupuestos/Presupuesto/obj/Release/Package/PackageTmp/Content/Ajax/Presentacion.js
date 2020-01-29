console.log("define centro de costo ajax");

Presentacion = {
    Listar: function (callback) {
        $.ajax(route + "/api/presentacion/get", {
            type: "GET",
            dataType: "json",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las presentaciones");
            }
        });
    },

    Guardar: function (presentacion, callback) {
        $.ajax(route + "/api/presentacion/post", {
            type: "POST",
            data: presentacion,
            success: function (data) {
                if (data != null) {

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

    Remover: function (presentacion, callback) {
        $.ajax(route + "/api/presentacion/delete?id=" + presentacion.codigo, {
            type: "DELETE",
            data: presentacion,
            success: function (data) {
                if (data != null) {
                    toastr.info(data);

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