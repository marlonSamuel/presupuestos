console.log("define actividad ajax");

Actividad = {
    Listar: function (callback) {
        $.ajax(route+"/api/actividad", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las actividades");
            }
        });
    },

    ListarByFinca: function (fincaId, tipo, callback) {
        $.ajax(route + "/api/actividad?fincaId=" + fincaId + "&tipo=" + tipo, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las actividades");
            }
        });
    },

    Guardar: function (actividad, callback) {
        $.ajax(route +"/api/actividad/post", {
            type: "POST",
            data: actividad,
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

    Remover: function (actividad, callback) {
        $.ajax(route +"/api/actividad/delete?id=" + actividad.Id, {
            type: "DELETE",
            data: actividad,
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