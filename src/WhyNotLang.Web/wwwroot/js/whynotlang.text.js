﻿var WhyNotLang = WhyNotLang || {}

WhyNotLang.Text = WhyNotLang.Text || (function () {
    function scrollOutput() {
        document.getElementById("output").scrollTop = document.getElementById("output").scrollHeight;
    }

    function setFocus(id) {
        document.getElementById(id).focus();
        return true;
    }

    return {
        scrollOutput,
        setFocus
    };
})();
