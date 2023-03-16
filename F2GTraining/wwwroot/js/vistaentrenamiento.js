﻿function iniciarEntrenamiento(fecha) {

    var diferenciaSeg = Math.trunc((new Date() - new Date(fecha[0], fecha[1] - 1, fecha[2], fecha[3], fecha[4], fecha[5])) / 1000);
    calculaTemporizador(diferenciaSeg);
    $(".session-timer p").css("color","#2DCFF1")

    setInterval(function () {
        diferenciaSeg++;
        calculaTemporizador(diferenciaSeg);
    }, 1000);
    
}

function calculaTemporizador(tiempo) {

    var horas = Math.trunc(tiempo / 3600);
    var minutos = Math.trunc((tiempo - (horas * 3600)) / 60);
    var segundos = Math.trunc((tiempo - (horas * 3600) - (minutos * 60)));

    if (horas < 10) {
        horas = "0" + horas;
    }

    if (minutos < 10) {
        minutos = "0" + minutos
    }

    if (segundos < 10) {
        segundos = "0" + segundos
    }

    $(".session-timer p").text(horas + ":" + minutos + ":" + segundos)
}

function mostrarDuracion(fechaInicio, fechaFin) {

    var diferenciaSeg = Math.trunc((new Date(fechaFin[0], fechaFin[1] - 1, fechaFin[2], fechaFin[3], fechaFin[4], fechaFin[5]) - new Date(fechaInicio[0], fechaInicio[1] - 1, fechaInicio[2], fechaInicio[3], fechaInicio[4], fechaInicio[5])) / 1000);
    calculaTemporizador(diferenciaSeg);

}