$(function (){
    $(".hide-header-button").on("click", function(event) {
        $("header").hide(200, function() {
            $(".show-header-button").show(200);
        })});

    $(".show-header-button").on("click", function(event) {
        $(".show-header-button").hide(200, function() {
            $("header").show(200)
        })});

    $(".show-header-button").hide();
});