$(document).ready(function () {

    $(".question").click(function () {

        // Remove active class from other questions
        $(".question").not(this).removeClass("active");

        // Toggle active class for clicked question
        $(this).toggleClass("active");

        // Slide toggle current answer
        $(this).next(".answer").slideToggle();

        // Close other open answers
        $(".answer").not($(this).next()).slideUp();

    });

});
