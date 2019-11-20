$(function () {
    $("#menuItem li").each(function () {
        if ($(this).find("ul").length > 0) {

            //show subnav on hover  
            $(this).mouseenter(function () {
                $(this).find("ul").stop(true, true).slideDown();
            });

            //hide submenus on exit  
            $(this).mouseleave(function () {
                $(this).find("ul").stop(true, true).slideUp();
            });

            $(this).find("ul").mousemove(function () {
                $(this).stop(true, true).show();
            });
        }
    });
});