console.log("define lote ajax");

Lote = {
    Listar: function (paisId, callback) {
        $.ajax(route + "/api/lote?paisId=" + paisId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los lotes");
            }
        });
    },

    ListarByFinca: function (fincaId, callback) {
        $.ajax(route + "/api/lote?fincaId=" + fincaId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los lotes");
            }
        });
    },

    Guardar: function (lote, callback) {
        $.ajax(route +"/api/lote/post", {
            type: "POST",
            data: lote,
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

    GuardarVariedades: function (lote, callback) {
        $.ajax(route + "/api/lote/post", {
            type: "POST",
            data: lote,
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

    ActivarODesactivar: function (lote, estado, callback) {
        $.ajax(route +"/api/lote?loteId=" + lote.Id + "&estado=" + estado, {
            type: "DELETE",
            data: lote,
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