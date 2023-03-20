function vuelveInicio() {
    window.location.href = "/equipos/MenuEquipo";
}

function cargaGrafica(etiquetas, valores) {

    console.log(etiquetas);
    var data = [];

    for (var i = 0; i < etiquetas.length; i++) {
        data.push([etiquetas[i],valores[i]]);
    }

    Highcharts.chart("graf-player", {
        chart: {
            backgroundColor: "transparent",
            plotBorderWidth: 0,
            plotShadow: false,
        },
        title: {
            useHTML: true,
            text: 'GRAFICA DEL JUGADOR',
            y: 60,
            style: {
                fontWeight: 'bold',
                color: 'white',
                fontSize: '25px',
                'padding': '10px 50% 10px 50%',
                'background-color': '#000000',
                'text-align': "center"
            }
        },
        plotOptions: {
            pie: {
                dataLabels: {
                    enabled: true,
                    distance: -50,
                    style: {
                        fontWeight: 'bold',
                        color: 'white',
                        fontSize: '16px'
                    }
                },
                startAngle: -90,
                endAngle: 90,
                center: ['50%', '80%'],
                size: '110%'
            }
        },
        series: [{
            type: 'pie',
            name: 'Valoracion',
            innerSize: '40%',
            data: data
        }],
    });

    $("text.highcharts-credits").hide();
}

