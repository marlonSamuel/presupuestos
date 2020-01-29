console.log("Define pais controller ");


model.seleccionController = {

    paises: ko.observableArray([]),
    usuario: ko.observableArray([]),

    pais: {
        Id: ko.observable(""),
        nombre: ko.observable(""),
        monedaId: ko.observable("")
    },

    mapPais: function (pais) {
        var ePais = model.seleccionController.pais;
        ePais.Id(pais.Id);
        ePais.nombre(pais.nombre);
        ePais.monedaId(pais.monedaId);
    },

    irA: function (pais) {
            var url = route + "/home/index?Id="+pais.paisId;
            $(location).attr('href', url);
    },

    setNamePais: function () {
        model.seleccionController.pais.nombre(window.Pais.nombre);
        model.seleccionController.usuario(window.User.usuario);
    },

    initialize: function () {
        var userId = window.User.Id;
        Usuario.ListarPaises(userId, function (data) {
            var paises = data;
            model.seleccionController.paises(paises);
        });
    }
};