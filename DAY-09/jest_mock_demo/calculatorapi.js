function getNumberFromAPI() {
    // this function simulates API call that return a number
    return 42;
}

function calculateTotal() {
    // Use module.exports instead of the shorthand exports
    const number = module.exports.getNumberFromAPI();
    return number + 10;
}

module.exports = { getNumberFromAPI, calculateTotal };