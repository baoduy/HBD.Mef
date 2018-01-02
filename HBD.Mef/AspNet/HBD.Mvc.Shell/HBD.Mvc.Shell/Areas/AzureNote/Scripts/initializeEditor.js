$(function() {
    var height = $(document).height() - 270;

    $("textarea").summernote({
        height: height,
        minHeight: 300,
        maxHeight: null,
        focus: true
    });

    //$("textarea").hide();

    //if (height < 300)
    //    height = 300;

    //jQuery.getScript(
    //    "https://cloud.tinymce.com/stable/tinymce.min.js?apiKey=2np81nxdus6e9u0e1o4g08ma8up9gsganbdjnbzwujvg6mct",
    //    function() {
    //        $("textarea").show();

    //        tinymce.init({
    //            selector: "textarea",
    //            height: height,
    //            menubar: false,
    //            plugins: [
    //                "advlist autolink lists link image charmap print preview anchor",
    //                "searchreplace visualblocks code fullscreen",
    //                "insertdatetime media table contextmenu paste code"
    //            ],
    //            toolbar:
    //                "undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
    //            content_css: "//www.tinymce.com/css/codepen.min.css"
    //        });
    //    });
})