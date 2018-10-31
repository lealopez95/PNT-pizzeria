$(document).ready(ini);

var botonRegistrarse, password, confirmarPassword;

var botonEntregar, botonCancelar, botonReset;

function ini() {

    botonRegistrarse = $('#botonRegistrarse').click(function (e) {

        password = $("#passwordRegistro").val();

        if (password.length < 8) {

            e.preventDefault();
            $("#menos8Caracteres").css("display", "block");

        } else {
            $("#menos8Caracteres").css("display", "none");
        }

        confirmarPassword = $("#confirmarPasswordRegistro").val();

        if (password != confirmarPassword) {
            e.preventDefault();
            alert("Las contraseñas no coinciden.")
        }

    });

   
}