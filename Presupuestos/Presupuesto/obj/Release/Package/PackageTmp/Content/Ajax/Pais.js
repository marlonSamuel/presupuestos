console.log("define pais ajax");

Paises = {
    Listar: function (callback) {
        $.ajax(route + "/api/pais", {
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

    GetById: function (paisId, callback) {
        $.ajax(route + "/api/pais?id=" + paisId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener el pais");
            }
        });
    },

    Guardar: function (pais, callback) {
        $.ajax(route + "/api/pais/post", {
            type: "POST",
            data: pais,
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

    Remover: function (pais, callback) {
        $.ajax(route + "/api/pais/delete?id=" + pais.Id, {
            type: "DELETE",
            data: pais,
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