botonCancelar = $(".entregar_cancelar").change(function () {

    var texto = $(this).val();

    if (texto === "Entregar") {
        $(this).parents("tr").css("background", "#228B22");
    } else if (texto === "Cancelar") {
        $(this).parents("tr").css("background", "#E03C31");
    } else {
        return;
    }

    $(this).parent().hide();

    var id = "#boton" + $(this).data("id");

    $(id).css("display", "block");

});

botonReset = $(".botonReset").click(function () {

    $(this).parents("tr").css("background", "#FFFFFF");
    $(this).css("display", "none");

    var id = "#form" + $(this).data("id");

    $(id).css("display", "block");
});