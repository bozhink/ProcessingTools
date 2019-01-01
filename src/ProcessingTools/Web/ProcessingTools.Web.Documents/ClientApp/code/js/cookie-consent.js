(function () {
    document.querySelector("#cookieConsent button[data-cookie-string]").addEventListener("click", function (el) {
        document.cookie = el.target.dataset.cookieString;
        document.getElementById("cookieConsent").classList.add("hidden");
    }, false);
})();
