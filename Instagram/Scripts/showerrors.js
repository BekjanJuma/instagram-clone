

// show modelstate errors
function showErrors(data) {
    var ediv = $("#errorsDiv");
    //console.log("show errors: " + data);
    $("#customError ul").empty();
    if (data && data.length > 0) {
        $(".errorsDiv").empty();
        if ((typeof data) == "string") {
            $("#customError ul").append("<li>" + data + "</li>");
        } else {
            $.each(data, function (index, value) {
                if (index < 10) $("#customError ul").append("<li>" + value + "</li>");
            });
            if (data.length > 10) $("#customError ul").append("<li>" + "..." + "</li>");
        }
        if (data.length > 0) $("#customError").show();
        $("#customError").fadeTo(10000, 500).slideUp(500, function () {
            $("#customError ul").empty();
            $("#customError").hide();
        });
        $("html, body").stop().animate({ scrollTop: 0 }, '500', 'swing');
    }
};


function alertMessage(alertType, msg) {
    if (alertType == "success") {
        $("#alertSuccess").text(msg);
        $("#alertSuccess").show();
        $("#alertSuccess").fadeTo(3000, 500).slideUp(500, function () {
            $("#alertSuccess").text("");
            $("#alertSuccess").hide();
        });
    } else if (alertType == "danger") {
        $("#alertDanger").text(msg);
        $("#alertDanger").show();
        $("#alertDanger").fadeTo(4000, 500).slideUp(500, function () {
            $("#alertDanger").text("");
            $("#alertDanger").hide();
        });
    }
    $("html, body").stop().animate({ scrollTop: 0 }, '500', 'swing');
};
