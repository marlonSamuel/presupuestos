console.log("define TipoActividad ajax");

TipoActividad = {
    Listar: function (callback) {
        $.ajax(route + "/api/TipoActividad", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las TipoActividads");
            }
        });
    },

    ListarPrincipales: function (callback) {
        $.ajax(route + "/actividades", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los productos");
            }
        });
    },

    Guardar: function (TipoActividad, callback) {
        $.ajax(route + "/api/TipoActividad/post", {
            type: "POST",
            data: TipoActividad,
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

    Remover: function (TipoActividad, callback) {
        $.ajax(route + "/api/TipoActividad/delete?id=" + TipoActividad.Id, {
            type: "DELETE",
            data: TipoActividad,
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