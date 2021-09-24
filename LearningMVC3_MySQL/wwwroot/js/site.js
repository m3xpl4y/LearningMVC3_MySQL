// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.onload(toggleButton());

function toggleButton() {
    var x = document.getElementById("darkMode");
    var y = document.getElementById("lightMode");
    if (x.style.display === "block") {
        x.style.display = "none";
        y.style.display = "block";
        console.log("dark mode");

    } else {
        x.style.display = "block";
        y.style.display = "none";
        console.log("light mode");
    }
}



//Display and Hide buttons in Views/Administration/ListRoles.cshtml
function hideButton(elmnt) {
    var buttonList = document.getElementsByName(elmnt.name);
    for (var i = 0; i < buttonList.length; i++) {
        if (i == 0) {
            buttonList[i].style.display = "none";
        } else {
            buttonList[i].style.display = "";
        }
    }
}
function showButton(elmnt) {
    var buttonList = document.getElementsByName(elmnt.name);
    for (var i = 0; i < buttonList.length; i++) {
        if (i == 0) {
            buttonList[i].style.display = "";
        } else {
            buttonList[i].style.display = "none";
        }
    }
}