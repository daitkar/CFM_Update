(function () {
    "use strict";

    var css = $("link[rel*='stylesheet']");
    if (css.length == 2) {
        var link = document.createElement('link');
        link.type = 'image/x-icon';
        link.rel = 'shortcut icon';
        link.href = css[1].href.replace('style.css', 'favicon.png');
        document.getElementsByTagName('head')[0].appendChild(link);
    }
    if (css.length == 3) {
        var link = document.createElement('link');
        link.type = 'image/x-icon';
        link.rel = 'shortcut icon';
        link.href = css[2].href.replace('style.css', 'favicon.png');
        document.getElementsByTagName('head')[0].appendChild(link);
    }
})();
