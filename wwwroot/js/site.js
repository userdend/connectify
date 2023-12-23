// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#searchFeature").on("submit", function (event) {
    if ($("#searchBar").val().length <= 0) {
        event.preventDefault();
    }
});

$("#inputForm").on("submit", function () {
    $("#inputButton").attr("disabled", true);
    $("#inputRedirection").css("display", "none");
});

$("#commentForm").on("submit", function (event) {
    var commentData = $(this).serialize();
    $.ajax({
        url: "/Comment/Create",
        type: "POST",
        data: commentData,
        success: function (response) {},
        error: function (status, error) {}
    });
});