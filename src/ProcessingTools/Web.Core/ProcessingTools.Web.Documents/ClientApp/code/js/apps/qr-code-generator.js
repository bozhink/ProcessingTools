$("#btn-generate").on("click", function () {
    var element = document.getElementById("qrCode");
    if (element) {
        element.innerHTML = "";
        new QRCode(element,
            {
                text: $("#text-content").val(),
                width: 300,
                height: 300
            });
    }
});
