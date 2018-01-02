$(function() {
    var wrapper = $("#sidebar");

    //Using niceScroll
    wrapper.niceScroll({ autohidemode: "hidden" });

    //Un-comment this for non niceScroll.
    //wrapper.bind("DOMMouseScroll mousewheel",
    //    function(event) {
    //        event.preventDefault();
    //        var top = wrapper.scrollTop();

    //        if (event.originalEvent.detail > 0 || event.originalEvent.wheelDelta < 0) {
    //            //scroll down
    //            wrapper.animate({scrollTop: top + 50}, 30);
    //        } else {
    //            //scroll up
    //            wrapper.animate({ scrollTop: top - 50 }, 30);
    //        }

    //        return false;
    //    });

    $("#menu-toggle").on("click",
        function(e) {
            e.preventDefault();
            $("#sidebar").toggleClass("toggled");
            $("#sidebar-content").toggleClass("toggled");
        });
});