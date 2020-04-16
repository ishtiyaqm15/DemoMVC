function DeleteProduct(deleteResource, redirectUrl) {
    $.ajax({
        type: "POST",
        url: deleteResource,
        contentType: "application/json",
        success: function (message) {
            alert(message);
            window.location.href = redirectUrl;
            return true;
        },
        error: function (e) {
            console.log(e);
        }
    });
}