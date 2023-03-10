function muestraOpciones(idjugador) {
    console.log(idjugador)
    var divisor = $("#optplayer-" + idjugador).toggle(300);
}

function esconderEquipos() {

    var equipoSelecc = $("#selectorequipo").val()

    var divisoresImagen = $(".image-team");

    for (var i = 0; i < divisoresImagen.length; i++) {
        $(divisoresImagen[i]).hide()
    }

    var divisoresEquipo = $(".link-team-actions");

    for (var i = 0; i < divisoresEquipo.length; i++) {
        $(divisoresEquipo[i]).hide()
    }

    $("#teamimage-" + equipoSelecc).show();
    $("#teamaction-" + equipoSelecc).show();

    cambiarJugadores(equipoSelecc, $("#equiposeleccionado").val());
    $("#equiposeleccionado").val(equipoSelecc);
}

function cambiarJugadores(idequiponuevo, idequipoviejo) {

    console.log(idequiponuevo, idequipoviejo);

    var divisoresOpcionesJug = $(".options-player");

    for (var i = 0; i < divisoresOpcionesJug.length; i++) {
        $(divisoresOpcionesJug[i]).hide()
    }

    $("[data-idequipo='" + idequipoviejo + "']").hide();
    $("[data-idequipo='" + idequiponuevo + "']").show();
}
