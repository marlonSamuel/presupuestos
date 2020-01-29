console.log("define centro de costo ajax");

CCosto = {
    Listar: function (paisId, callback) {
        $.ajax(route + "/api/ccosto/get?paisId=" + paisId, {
            type: "GET",
            dataType: "json",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los centros de costo");
            }
        });
    },

    ListarA: function (codigo, callback) {
        $.ajax(route + "/api/ccosto/get?codigo=" + codigo, {
            type: "GET",
            dataType: "json",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los centros de costo");
            }
        });
    },

    Guardar: function (ccosto, op, callback) {
        $.ajax(route +"/api/ccosto/post?op="+op, {
            type: "POST",
            data: ccosto,
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

    Remover: function (ccosto, callback) {
        $.ajax(route + "/api/ccosto/delete?codigo=" + ccosto.codigo, {
            type: "DELETE",
            data: ccosto,
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