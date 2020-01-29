console.log("define departamento ajax");

TipoEmpleado = {
    Listar: function (callback) {
        $.ajax(route + "/api/tipoEmpleado", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos empleados");
            }
        });
    },

    Guardar: function (tipo, callback) {
        $.ajax(route + "/api/tipoEmpleado/post", {
            type: "POST",
            data: tipo,
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

    Remover: function (tipo, callback) {
        $.ajax(route + "/api/tipoEmpleado/delete?id=" + tipo.tipoId, {
            type: "DELETE",
            data: tipo,
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