

let container = $("#image-zoom");
let image = $(".img-thumbnail");

image.mouseover(function () {
    var position = ($(this).position());

    image.stop();

    container
        .css("position", "absolute")
        .css("left", position.left)
        .css("top", position.top)
        .append($("<img>")
            .addClass("image-zoom")
            .attr("src", $(this).attr("src")))

    container.fadeIn("slow");
})

container.mouseleave(function () {
    container.fadeOut();
    container.html("")
        .css("left", "")
        .css("top", "")
});



