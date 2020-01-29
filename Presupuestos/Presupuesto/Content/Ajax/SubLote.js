console.log("define sublote ajax");

SubLote = {

    GetAll: function (callback) {
        $.ajax(route + "/api/sublote", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los sublotes");
            }
        });
    },
    Listar: function (loteId, callback) {
        $.ajax(route + "/api/sublote?loteId=" + loteId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los sublotes");
            }
        });
    },

    Guardar: function (sublote, callback) {
        $.ajax(route + "/api/sublote/post", {
            type: "POST",
            data: sublote,
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

    ListarByFinca: function (fincaId, callback) {
        $.ajax(route + "/api/sublote?fincaId=" + fincaId, {
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

    ListarV: function (subloteId, cultivoId, callback) {
        $.ajax(route + "/api/sublote?subloteId=" + subloteId + "&cultivoId=" + cultivoId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error");
            }
        });
    },

    ActivarODesactivar: function (sublote, estado, callback) {
        $.ajax(route + "/api/sublote?subloteId=" + sublote.Id + "&estado=" + estado, {
            type: "DELETE",
            data: sublote,
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