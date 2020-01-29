console.log("define permiso ajax");

Permiso = {
    Listar: function (callback) {
        $.ajax(route + "/api/permiso", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los permisos");
            }
        });
    },

    Guardar: function (permiso, callback) {
        $.ajax(route + "/api/permiso/post", {
            type: "POST",
            data: permiso,
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

    Remover: function (permiso, callback) {
        $.ajax(route + "/api/permiso/delete?id=" + permiso.permisoId, {
            type: "DELETE",
            data: categoria,
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