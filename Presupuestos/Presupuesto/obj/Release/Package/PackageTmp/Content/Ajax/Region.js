console.log("define region ajax");

Region = {
    Listar: function (callback) {
        $.ajax(route + "/api/region", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las regiones");
            }
        });
    },

    Guardar: function (categoria, callback) {
        $.ajax(route + "/api/region/post", {
            type: "POST",
            data: region,
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

    Remover: function (region, callback) {
        $.ajax(route + "/api/region/delete?id=" + region.Codigo, {
            type: "DELETE",
            data: region,
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