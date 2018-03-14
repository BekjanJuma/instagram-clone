$(document).ready(function () {

    $(".likePostButton").on("click", function () {
        var postId = $(this).closest(".postId").data("id");
        console.log(postId);

        $.ajax("/User/Toggle", {
            data: JSON.stringify({ postId: postId }),
            type: "post", contentType: "application/json",
            success: function (result) {
                if (result.success == true) {
                    alertMessage("success", result.msg);
                } else {
                    showErrors(result.msg);
                }
            },
            error: function (data) {
                showErrors(["Uknown exception"]);
                console.log("exception: " + data);
            }
        });
    });

    $(".commentPostButton").on("click", function () {
        var postId = $(this).closest(".postId").data("id");

        var content = $(this).closest(".new-comment").find(".comment-txt").first().val();
        console.log(postId + ": " + content);

        $.ajax("/User/Comment", {
            data: JSON.stringify({ postId: postId, comment: content }),
            type: "post", contentType: "application/json",
            success: function (result) {
                if (result.success == true) {
                    alertMessage("success", result.msg);
                } else {
                    showErrors(result.msg);
                }
            },
            error: function (data) {
                showErrors(["Uknown exception"]);
                console.log("exception: " + data);
            }
        });
    });
});