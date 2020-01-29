console.log("define sub centro de costo ajax");

SubCentro = {
    Listar: function (fincaId, callback) {
        $.ajax(route + "/api/SubCCosto/get?codigo=" + fincaId, {
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

    GetAll: function (callback) {
        $.ajax(route + "/api/SubCCosto/get", {
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

    ListarS: function (callback) {
        $.ajax(route + "/api/SubCCosto", {
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
        $.ajax(route + "/api/SubCCosto/get?subcentroId=" + codigo, {
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

    Guardar: function (subcentro, callback) {
        $.ajax(route + "/api/SubCCosto/post", {
            type: "POST",
            data: subcentro,
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

    Remover: function (SubCCosto, callback) {
        $.ajax(route + "/api/SubCCosto/delete?id=" + SubCCosto.Id, {
            type: "DELETE",
            data: SubCCosto,
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