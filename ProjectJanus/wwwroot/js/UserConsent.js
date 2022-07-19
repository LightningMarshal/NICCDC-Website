/*
 *
 *                  !!!IMPORTANT!!!
 *     This requires a jquery script call before
 *     it is called itself.
 *
 *     Usage:
 *     <script src="~/lib/jquery/dist/jquery.min.js"></script>
 *     <script src="~/js/UserConsent.js"></script>
 *
 *
 */

// This program will display a cookie popup on home page load if the "user_consent" cookie is not present.
$(document).ready(function () {
    var cookie_alert =
        '< div id="cookie_wrapper" style="position: fixed; top: 10%; left: 10%; background: #5f6061;" >' +
            
        '</div>';

    let cookie_consent = cookieVal("user_concent");
    if (cookie_consent == "") {
        $('.cookie_wrap').append(cookie_alert);
    } else if (cookie_consent != "") {
        $('.cookie_wrap').remove()
    }
});

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";

    document.cookie = name + "=" + value + expires + "; path=/";
}

function cookieVal(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function deleteCookie(name) {
    createCookie(name, "", -1);
}