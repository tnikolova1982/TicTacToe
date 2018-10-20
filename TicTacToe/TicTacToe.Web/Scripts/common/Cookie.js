var Cookie = {
    TabsCookieIndex: "TabIndex",
    TabsCookieName: "TabName",

    SetCookie: function (cName, value, exseconds) {
        var exdate = new Date();
        exdate = exdate.setDate(exdate.getDate());
        exdate += exseconds * 1000;
        exdate = new Date(exdate);
        var cValue = encodeURIComponent(value) + ((exseconds == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = cName + "=" + cValue + "; path=/";
    },

    DeleteAllCookies: function () {
        /* .match(regExpr), takes all cookie names, before the equal sign, and puts them in an array. */
        var cookieList = document.cookie.match(/\w+(?==)/g);

        for (var i in cookieList) {
            Cookie.SetCookie(cookieList[i], '', 0);
        }
    },

    GetCookie: function (cName) {
        var i, x, y, arRcookies = document.cookie.split(";");
        for (i = 0; i < arRcookies.length; i++) {
            x = arRcookies[i].substr(0, arRcookies[i].indexOf("="));
            y = arRcookies[i].substr(arRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == cName) {
                return decodeURIComponent(y);
            }
        }
        return "";
    },

    DeleteCookie: function (cName) {
        Cookie.SetCookie(cName, undefined, 0);
    }
}