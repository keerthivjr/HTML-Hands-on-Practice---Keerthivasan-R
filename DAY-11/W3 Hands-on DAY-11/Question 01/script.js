$(document).ready(function () {

    //Click Event on Submit Button
    $("#submitBtn").click(function () {

        // Capture input values using jQuery .val()
        var name = $("#name").val();
        var email = $("#email").val();
        var rating = $("#rating").val();
        var comments = $("#comments").val();

        //Clear previous messages
        $("#message").removeClass("error success").text("");

        //Validation: Check if Name or Email is empty
        if (name.trim() === "" || email.trim() === "" || rating === "" || comments.trim() === "") {

            //Show error message dynamically
            $("#message")
                .addClass("error")
                .text("Please fill in all fields.");

        } else {

            //Show success message dynamically
            $("#message")
                .addClass("success")
                .html("Thank you <b>" + name + "</b>! Your feedback has been submitted successfully.");

            // Clear form fields using jQuery
            $("#feedbackForm")[0].reset();
        }

    });

});