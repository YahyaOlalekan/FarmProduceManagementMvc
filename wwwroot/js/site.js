// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $(document).on("change", "#Registration", function(){
        let option = $("#Registration").val();
        if (option) {
            let url = (option == "Farmer") ? "/Farmer/Register" : "/Customer/Register";
            window.location.href = url;
            // alert(url);
        }
    })


    $(document).on("change", "#CategoryId", function () {

        let CategoryId = $(this).val();

        if (CategoryId)
        {
            $.ajax({
                url: '/Produce/GetProduce/' + CategoryId,
                type: 'get',
                dataType: 'json',
                success: function (result) {

                    let item = '<option value="">---Choose Produce---</option>';

                    if (result.status) {

                        /*console.log(result.data);*/
                        for (let produce of result.data) {
                            item += '<option value="' + produce.id + '">' + produce.produceName + '</option>';
                        }

                        /*$.each(result.data, function (index, produce)
                        {
                            item += '<option value="' + produce.Id + '">' + produce.produceName + '</option>';
                        });*/

                        $("#ProduceId").html(item);
                    }
                    else {
                        item += '<option value=""> No produce found</option>';
                    }
                    
                },
                error: function () {

                }
            })
            // alert(url);
        }
    })



    $(document).on("change", "#ProduceId", function () {

        let ProduceId = $(this).val();

        if (ProduceId) {
            $.ajax({
                url: '/Produce/GetProduce/' + ProduceId,
                type: 'get',
                dataType: 'json',
                success: function (result) {

                    if (result.status) {

                        // console.log(result.data);
                        let price = 'Price: ' + result.data[0].costPrice
                        // console.log(price);
                        $("#Price").html(price);
                    }

                },
                error: function () {

                }
            })
            // alert(url);
        }
        else {

            let price = 'Price: 0';
            $("#Price").html(price);
        }
    })




})


