console.log("define unidad ajax");

Unidad = {
    Listar: function (callback) {
        $.ajax(route + "/api/unidad", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las unidades");
            }
        });
    },

    Guardar: function (unidad, callback) {
        $.ajax(route + "/api/unidad/post", {
            type: "POST",
            data: unidad,
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

    Remover: function (unidad, callback) {
        $.ajax(route + "/api/unidad/delete?id=" + unidad.unidadId, {
            type: "DELETE",
            data: unidad,
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