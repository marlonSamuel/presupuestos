console.log("define bi ajax");

Bi = {
    ListarByCategoria: function (categoriaId, callback) {
        $.ajax(route + "/grupos/" + categoriaId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los grupos de productos");
            }
        });
    },

    ListarByGrupo: function (grupoId, callback) {
        $.ajax(route + "/productos/" + grupoId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los productos");
            }
        });
    },

    GetAllProductosV: function (callback) {
        $.ajax(route + "/ProductosParaVentas", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener productos");
            }
        });
    },

    GetAllProductosA: function (callback) {
        $.ajax(route + "/ProductosAgricolas", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener productos");
            }
        });
    },


    ListarByGrupoVenta: function (grupoId,callback) {
        $.ajax(route + "/ventas/"+grupoId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los productos para venta");
            }
        });
    },
    ListarCategorias: function (callback) {
        $.ajax(route + "/api/BI/get", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las categorias de productos");
            }
        });
    },

    ListarByCCosto: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/productos/" + presupuestoId + "/" + fincaId, {
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

    ListarByCCostoVenta: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/ventas/" + presupuestoId + "/" + fincaId, {
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

    ListarProVenta: function (callback) {
        $.ajax(route + "/productosventa", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los productos");
            }
        });
    },

    ListarProAgricola: function (categoriaId, callback) {
        $.ajax(route + "/productosagricola/" + categoriaId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los productos");
            }
        });
    },

    ListarDetalleGroupByGrupo: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/detallegrupos/" + presupuestoId + "/" + fincaId, {
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

    listarDetalleGroupByCategoria: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/detallecategorias/" + presupuestoId + "/" + fincaId, {
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

    ListarDetalleGroupByGrupoVenta: function (presupuestoId, fincaId, callback) {
        $.ajax(route + "/detallegruposventa/" + presupuestoId + "/" + fincaId, {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener los presupuestos de venta");
            }
        });
    },
}