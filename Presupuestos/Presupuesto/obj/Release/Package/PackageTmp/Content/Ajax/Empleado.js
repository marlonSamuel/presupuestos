console.log("define empleado ajax");

Empleado = {
    Listar: function (callback) {
        $.ajax(route + "/api/empleado", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los empleados");
            }
        });
    },

    Guardar: function (empleado, callback) {
        $.ajax(route + "/api/empleado/post", {
            type: "POST",
            data: empleado,
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

    Remover: function (empleado, callback) {
        $.ajax(route + "/api/departamento/delete?id=" + empleado.empleadoId, {
            type: "DELETE",
            data: empleado,
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