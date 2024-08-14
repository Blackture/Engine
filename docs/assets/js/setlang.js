function SetLanguage() {
    if (window.navigator.language == "de-DE" || window.navigator.language == "de")  {
        window.open("de/index.html", "_self");
    }
    else {
        window.open("en/index.html", "_self");
    }
}