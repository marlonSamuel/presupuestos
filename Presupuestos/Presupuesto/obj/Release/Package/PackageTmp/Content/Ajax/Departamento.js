console.log("define departamento ajax");

Departamento = {
    Listar: function (callback) {
        $.ajax(route + "/api/departamento", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los departamento");
            }
        });
    },

    Guardar: function (departamento, callback) {
        $.ajax(route + "/api/departamento/post", {
            type: "POST",
            data: departamento,
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

    Remover: function (departamento, callback) {
        $.ajax(route + "/api/departamento/delete?id=" + departamento.departamentoId, {
            type: "DELETE",
            data: departamento,
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