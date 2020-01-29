console.log("define dimension ajax");

Dimension = {
    Listar: function (callback) {
        $.ajax(route + "/api/dimension", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las dimensions");
            }
        });
    },

    ListarCuentas: function (callback) {
        $.ajax(route + "/api/cuenta", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las cuentas");
            }
        });
    },

    Guardar: function (dimension, callback) {
        $.ajax(route + "/api/dimension/post", {
            type: "POST",
            data: dimension,
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

    Remover: function (dimension, callback) {
        $.ajax(route + "/api/dimension/delete?id=" + dimension.Id, {
            type: "DELETE",
            data: dimension,
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