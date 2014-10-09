

$(document).ready(function () {
    $("#password").keyup(function (event) {
        if (event.keyCode == 13) {
            login();
        }
    });

    $("#username").focus();
});


function login() {

    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value

    $.ajax({
        type: "POST",
        url: "Login/CheckLogin",
        data: JSON.stringify({ username: username, password: password }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.location = "/OldExpressionStudy";
        },
        error: function (ex) {
            alert(ex);
        }
    });
}

