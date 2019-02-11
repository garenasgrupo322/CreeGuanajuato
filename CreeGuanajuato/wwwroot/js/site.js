// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var URLBase = "https://garenas-developer.azurewebsites.net/";

// Write your Javascript code.
$(function () {

    /**
     * Funcionalidad para iniciar worker para sincronización
     * */
    function iniciaSincronizacion() {
        var notification = new Notification("Se inicio el proceso de sincronización, en cuanto termine recibira una nueva notificación");

        if (typeof (w) == "undefined") {
            w = new Worker(URLBase + "/js/SincronizaINEGI.js");
        }

        w.onmessage = function (event) {
            if (event.data.success) {
                var notification = new Notification("La sincronización finalizo");        
            }
        };
    } 

    /**
     * Funcionalidad para permitir notificaciones
     * */
    function validaNotificaciones() {
        if (!("Notification" in window)) {
            window.alert("Su explorador no soporta notificaciones, le recomendamos actualizar su explorador o utilizar uno alternativo.");
        }
        else if (Notification.permission !== 'denied') {
            Notification.requestPermission(function (permission) {
                if (permission === "granted") {

                }
            });
        }
    }

    $(document).ready(function () {
        validaNotificaciones();
        $("#btnSincronizaInegi").click(iniciaSincronizacion);
    })

})