

$(document).on("click", ".modal-bt", function (e) {
    e.preventDefault();
    let url = $(this).attr("href");
    fetch(url).then(response => response.text())
        .then(data => {
            console.log(data)
            $("#quickModal .modal-dialog").html(data)
        })
    $("#quickModal").modal("show")
})