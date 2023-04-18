// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function(){


    $(document).on("change", "#Registration", function(){
        let option = $("#Registration").val();
        if (option) {
            let url = (option == "Farmer") ? "/Farmer/Register" : "/Customer/Register";
            window.location.href = url;
            // alert(url);
        }
    })
})


