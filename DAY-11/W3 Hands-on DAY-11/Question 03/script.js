$(document).ready(function () {

    let cartCount = 0;

    // Event Delegation
    $("#product-list").on("click", ".add-to-cart", function () {

        // Increase counter
        cartCount++;
        $("#cart-count").text(cartCount);  // .text() used

        // Disable button
        $(this).prop("disabled", true);    // .prop() used
        $(this).addClass("disabled-btn");

        // Change button text
        $(this).text("Added");

        // Store attribute (example usage)
        $(this).attr("data-added", "true");  // .attr() used

        // Show confirmation message
        $(this).siblings(".added-msg").text("✔ Added to cart!");

    });

});