console.log("define grupoDimension ajax");

GrupoDimension = {
    Listar: function (callback) {
        $.ajax(route + "/api/grupoDimension", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las grupoDimensions");
            }
        });
    },

    Guardar: function (grupoDimension, callback) {
        $.ajax(route + "/api/grupoDimension/post", {
            type: "POST",
            data: grupoDimension,
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

    Remover: function (grupoDimension, callback) {
        $.ajax(route + "/api/grupoDimension/delete?id=" + grupoDimension.Id, {
            type: "DELETE",
            data: grupoDimension,
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