console.log("define moneda ajax");

Moneda = {
    Listar: function (callback) {
        $.ajax(route + "/api/moneda", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las monedas");
            }
        });
    },

    Guardar: function (moneda, callback) {
        $.ajax(route + "/api/moneda/post", {
            type: "POST",
            data: moneda,
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

    Remover: function (moneda, callback) {
        $.ajax(route + "/api/moneda/delete?id=" + moneda.Id, {
            type: "DELETE",
            data: moneda,
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