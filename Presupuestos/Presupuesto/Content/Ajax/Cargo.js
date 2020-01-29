console.log("define cargo ajax");

Cargo = {
    Listar: function (callback) {
        $.ajax(route + "/api/cargo", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los cargos");
            }
        });
    },

    Guardar: function (cargo, callback) {
        $.ajax(route + "/api/cargo/post", {
            type: "POST",
            data: cargo,
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

    Remover: function (cargo, callback) {
        $.ajax(route + "/api/cargo/delete?id=" + cargo.cargoId, {
            type: "DELETE",
            data: cargo,
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