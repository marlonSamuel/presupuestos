console.log("define tipo cambio ajax");

TipoCambio = {
    Listar: function (callback) {
        $.ajax(route + "/api/TipoCambio", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipo sde cambio");
            }
        });
    },

    GetById: function (TipoCambioId, callback) {
        $.ajax(route + "/api/TipoCambio?id=" + TipoCambioId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener el TipoCambio");
            }
        });
    },

    Guardar: function (TipoCambio, callback) {
        $.ajax(route + "/api/TipoCambio/post", {
            type: "POST",
            data: TipoCambio,
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

    Remover: function (TipoCambio, callback) {
        $.ajax(route + "/api/TipoCambio/delete?id=" + TipoCambio.Id, {
            type: "DELETE",
            data: TipoCambio,
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