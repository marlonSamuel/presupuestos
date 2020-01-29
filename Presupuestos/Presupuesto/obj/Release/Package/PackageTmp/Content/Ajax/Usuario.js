console.log("define usuario ajax");

Usuario = {
    Listar: function (callback) {
        $.ajax(route + "/api/usuario", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los usuarios");
            }
        });
    },

    ListarPermisos: function (usuarioId, callback) {
        $.ajax(route + "/api/usuario?usuarioId=" + usuarioId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los permisos del usuario");
            }
        });
    },

    ListarPaises: function (usuarioId, callback) {
        $.ajax(route + "/api/usuario?userId=" + usuarioId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los paises del usuario");
            }
        });
    },

    Guardar: function (usuario, callback) {
        $.ajax(route + "/api/usuario/post", {
            type: "POST",
            data: usuario,
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

    Remover: function (usuario, callback) {
        $.ajax(route + "/api/usuario/delete?id=" + usuario.Id, {
            type: "DELETE",
            data: usuario,
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