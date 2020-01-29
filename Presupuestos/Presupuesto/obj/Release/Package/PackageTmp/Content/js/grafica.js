console.log("Define pais controller ");


model.graficaController = {
    grafica: ko.observable({}),
    graficas: ko.observableArray([]),
    labels: ko.observableArray([]),
    showLine: function () {
        model.graficaController.type('Line');
    },
    showBar: function () {
        model.graficaController.type('Bar');
    },
    type: ko.observable('Bar'),

    sales: ko.observable({
        labels: ['semana 1', 'semana 2', 'semana 3', 'semana 4'],
        datasets: [{
            label: 'Mano Obra',
            fillColor: '#152491',
            data: [3000.00, 5000.55, 40000.40, 8000.00]
        },{
            label: 'Agricola',
            fillColor: 'green',
            data: [4000.00, 8000.78, 4788.99, 4500.00]
            },
        {
            label: 'Ventas',
            fillColor: 'yellow',
            data: [4000.00, 8000.78, 4788.99, 4500.00]
        }]
    }),

    options: ko.observable({
        //segmentShowStroke: false,
        //animateRotate: true,
        //animateScale: false,
        //percentageInnerCutout: 50,
        tooltipTemplate: "hola"
    }),



    initialize: function () {
        Grafica.ListarPresupuestos(function (data) {
            var graficas = data.labels;
            for (var i = 0; i < graficas.length; i++) {
                model.graficaController.labels.push(graficas[i].label);
            }
        });
    }
};