console.log("define presupuesto contable ajax");

Contable = {
    Listar: function (callback) {
        $.ajax(route + "/api/pcontable", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los presupuestos");
            }
        });
    },

    ListarByPresupuesto: function (presupuestoId, callback) {
        $.ajax(route + "/api/pcontable?id="+presupuestoId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los presupuestos");
            }
        });
    },

    ListarByPresupuestoFinca: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/api/pcontable?id=" + presupuestoId + "&fincaId=" + fincaId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los presupuestos");
            }
        });
    },

    Guardar: function (contable, callback) {
        $.ajax(route + "/api/pcontable/post", {
            type: "POST",
            data: contable,
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

    Remover: function (contable, callback) {
        $.ajax(route + "/api/pcontable/delete?id=" + contable.Id, {
            type: "DELETE",
            data: contable,
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