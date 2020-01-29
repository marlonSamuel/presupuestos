console.log("define grafica controller");

Grafica = {
    ListarPresupuestos: function (callback) {
        $.ajax(route + "/graficas", {
            type: "GET",
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }

            }, error: function (data) {
                toastr.error("Error al obtener las graficas");
            }
        });
    }

}