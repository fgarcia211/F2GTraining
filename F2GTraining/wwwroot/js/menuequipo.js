function esconderEquipos() {

    var equipoSelecc = $("#equiposeleccionado").val()

    var divisoresImagen = $(".image-team");

    for (var i = 0; i < divisoresImagen.length; i++) {
        $(divisoresImagen[i]).hide()
    }

    var divisoresEquipo = $("link-team-actions");

    for (var i = 0; i < divisoresEquipo.length; i++) {
        $(divisoresEquipo[i]).hide()
    }

    $("#teamimage-" + equipoSelecc).show();
    $("#teamaction-" + equipoSelecc).show();

    $("#equiposeleccionado").val() = $("#selectorequipo").val();

}

function muestraOpcionesJug(idjugador) {
    var divisor = $("#optplayer" + idjugador).toggle(300);
}
