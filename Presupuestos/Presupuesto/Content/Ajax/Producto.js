console.log("define producto ajax");

Producto = {
    Listar: function (categoriaId, callback) {
        $.ajax(route + "/api/producto/get?categoriaId=" + categoriaId, {
            type: "GET",
            dataType: "json",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los productos");
            }
        });
    },

    ListarD: function (id, callback) {
        $.ajax(route + "/api/producto/get?id=" + id+"&detalle=true", {
            type: "GET",
            dataType: "json",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los productos");
            }
        });
    },

    Guardar: function (producto, callback) {
        $.ajax(route +"/api/producto/post", {
            type: "POST",
            data: producto,
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

    Remover: function (producto, callback) {
        $.ajax(route +"/api/producto/delete?id=" + producto.productoId, {
            type: "DELETE",
            data: producto,
            success: function (data) {
                if (data != null) {

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