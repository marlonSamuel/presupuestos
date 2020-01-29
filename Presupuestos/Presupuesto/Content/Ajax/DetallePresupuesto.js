console.log("define detallePresupuesto");

Detalle = {
    ListarByPresupuesto: function (presupuestoId, callback) {
        $.ajax(route + "/api/DetallePresupuesto/get?presupuetoId=" + presupuestoId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos de presupuesto");
            }
        });
    },

    ListarAsignados: function (presupuestoId, callback) {
        $.ajax(route + "/api/DetallePresupuesto/getAsignados?presupuetoId=" + presupuestoId+"&filterCCosto=true", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos de presupuesto");
            }
        });
    },

    ListarByCCosto: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/api/DetallePresupuesto/get?presupuetoId=" + presupuestoId + "&fincaId=" + fincaId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos de presupuesto");
            }
        });
    },

    ListarByCCostoCosecha: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/cosecha/" + presupuestoId + "/" + fincaId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos de presupuesto");
            }
        });
    },


    ListarByGroup: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/api/DetallePresupuesto/get?presupuetoId=" + presupuestoId + "&fincaId=" + fincaId+"&opcion=G", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los tipos de presupuesto");
            }
        });
    },


    Guardar: function (manoObra, callback) {
        $.ajax(route + "/api/DetallePresupuesto/post", {
            type: "POST",
            data: manoObra,
            success: function (data) {
                if (data !== null) {

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

    UpdateTotal: function (presupuestoId, total_estimado, callback) {
        $.ajax(route + "/api/DetallePresupuesto/Put?presupuestoId=" + presupuestoId + "&total_estimado=" + total_estimado, {
            type: "PUT",
            data: "",
            success: function (data) {
                if (data !== null) {

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

    Remover: function (manoObra, callback) {
        $.ajax(route + "/api/DetallePresupuesto/delete?id=" + manoObra.id, {
            type: "DELETE",
            data: manoObra,
            success: function (data) {
                if (data !== null) {
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

    FinalizarByFinca: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/api/DetallePresupuesto/PutFinalizar?presupuestoId=" + presupuestoId + "&fincaId=" + fincaId, {
            type: "PUT",
            data: "",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (data) {
                toastr.error("Error al guardar");
            }
        });
    },

    IsFinished: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/api/validation/Get?presupuetoId=" + presupuestoId + "&fincaId=" + fincaId, {
            type: "GET",
            success: function (data) {
                if (data !== null) {

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

    SaveFile: function (cargaParams, callback) {
        $.ajax(route + "/api/file/post", {
            type: "POST",
            data: cargaParams,
            success: function (data) {
                if (data !== null) {

                }
                else {
                    toastr.error("Error al guardar");
                }
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (data) {
                if (typeof callback === 'function') {
                    callback(data.responseJSON.Message);
                }
            }
        });
    },

    SaveFileContable: function (cargaParams, callback) {
        $.ajax(route + "/fileContable", {
            type: "POST",
            data: cargaParams,
            success: function (data) {
                if (data !== null) {

                }
                else {
                    toastr.error("Error al guardar");
                }
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (data) {
                if (typeof callback === 'function') {
                    callback(data.responseJSON.Message);
                }
            }
        });
    },
}