var URLBase = "https://garenas-developer.azurewebsites.net/";

function sincronizaINEGI() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            postMessage(xhttp.responseText);
        }
    };
    xhttp.open("GET", URLBase + "api/Estadoes/sincroniza", false);
    xhttp.send();
}

sincronizaINEGI();