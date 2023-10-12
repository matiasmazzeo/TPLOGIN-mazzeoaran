﻿function validarFormulario() {
    var Contrasena = document.getElementsByName("Contrasena")[0].value;
    var confirmarContrasena = document.getElementsByName("Contrasena2")[0].value;
    var errorContrasena = document.getElementById("errorContrasena");
    var errorConfirmarContrasena = document.getElementById("errorConfirmarContrasena");

    // Verificar longitud mínima y otros requisitos usando expresiones regulares
    var regex = /[!@#$%^&*()_*{}\[\]:;<>,.?~\\]/;
    if (!(regex.test(Contrasena) && Contrasena.length > 8)) {
        errorContrasena.textContent = "La Contrasena debe tener al menos 8 caracteres, incluyendo al menos una letra mayúscula, un carácter especial y un número.";
        return false;
    } else {
        errorContrasena.textContent = "";
    }

    // Verificar coincidencia con la confirmación de Contrasena
    if (Contrasena !== confirmarContrasena) {
        errorConfirmarContrasena.textContent = "Las Contrasenas no coinciden. Por favor, inténtelo de nuevo.";
        return false;
    } else {
        errorConfirmarContrasena.textContent = "";
    }

    return true;
}