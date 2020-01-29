console.log("define centro de costo ajax");

Presupuesto = {
    Listar: function (tipoId, opcion, paisId, callback) {
        $.ajax(route + "/api/ps/get?tipoId=" + tipoId + "&opcion=" + opcion + "&paisId=" + paisId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los presupuestos");
            }
        });
    },

    ListarByUser: function (userId, callback) {
        $.ajax(route + "/api/tipoPs/get?userId=" + userId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos de presupuesto");
            }
        });
    },

    ListarTipos: function (callback) {
        $.ajax(route + "/api/tipoPs/get", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos de presupuesto");
            }
        });
    },


    Guardar: function (presupuesto, callback) {
        $.ajax(route + "/api/ps/post", {
            type: "POST",
            data: presupuesto,
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

    Remover: function (presupuestoId, estado, callback) {
        $.ajax(route + "/api/ps/delete?id=" + presupuestoId+'&estado='+estado, {
            type: "DELETE",
            data: '',
            success: function (data) {
                if (data != null) {

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