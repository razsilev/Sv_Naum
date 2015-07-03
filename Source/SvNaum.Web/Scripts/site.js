function displayMenu() {
    var x = 'smallmenu';
        if (document.getElementsByClassName(x)[0].style.display == "block") {
            document.getElementsByClassName(x)[0].style.display = "none";
        } else {
            document.getElementsByClassName(x)[0].style.display = "block";
        }

}

window.addEventListener("resize", function (event) {
    var width = document.body.clientWidth;
    var x = 'smallmenu';

    if (width > 733 && document.getElementsByClassName(x)[0].style.display == "block") {
        document.getElementsByClassName(x)[0].style.display = 'none';
    }
});