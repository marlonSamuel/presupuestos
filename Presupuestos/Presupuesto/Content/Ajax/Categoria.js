console.log("define categoria ajax");

Categoria = {
    Listar: function (callback) {
        $.ajax(route + "/api/categoria", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las categorias");
            }
        });
    },

    Guardar: function (categoria, callback) {
        $.ajax(route + "/api/categoria/post", {
            type: "POST",
            data: categoria,
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

    Remover: function (categoria, callback) {
        $.ajax(route + "/api/categoria/delete?id=" + categoria.categoriaId, {
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