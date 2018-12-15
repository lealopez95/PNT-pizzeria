// Cuando se hace se elige si se desea cancelar o entregar un pedido, o resetear su estado a pendiente se manda una petición ajax al servidor.
// El servidor actualiza la base de datos y devuelve el nuevo estado con el ID del pedido, luego se llama a esta función y se cambia la vista sin tener que recargar la página.
// Si el servidor está caido o no responde que el estado de la base de datos fue cambiado, entonces la UI no cambia
function actualizarUI(data, status) {

    let datos = data.split(" ");
    let resultado = datos[0];
    let id = datos[1];

    let idBoton = "#boton" + id;
    let idForm = "#form" + id;
    let background;

    if (resultado === "ENTREGADO") {
        
        background = "#228B22";
        $(idForm).hide();
        $(idBoton).css("display", "block");
    } else if (resultado === "CANCELADO") {
        
        background = "#E03C31";
        $(idForm).hide();
        $(idBoton).css("display", "block");
    } else if (resultado === "PENDIENTE") {
        
        background = "#FFFFFF"
        $(idBoton).css("display", "none");
        $(idForm).css("display", "block");
    }

    $(idBoton).parents("tr").css("background", background);
}

botonCancelar = $(".entregar_cancelar").change(function () {

    var texto = $(this).val();

    let direccionAjax;

    if (texto === "Entregar") {
        direccionAjax = "/Pedidos/SetearEntregado/";
    } else if (texto === "Cancelar") {
        direccionAjax = "/Pedidos/SetearCancelado/";
    } else {
        return;
    }
    
    let num = $(this).data("id");
    direccionAjax += num;

    $.ajax({
        type: "GET",
        url: direccionAjax,
        success: actualizarUI
    });
    
});

botonReset = $(".botonReset").click(function () {

    var num = $(this).data("id");
    var direccionAjax = "/Pedidos/SetearPendiente/" + num;

    $.ajax({
        type: "GET",
        url: direccionAjax,
        success: actualizarUI
    });
});