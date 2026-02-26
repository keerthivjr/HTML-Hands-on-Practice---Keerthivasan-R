// calculatorapi.test.js
const calculator = require('./calculatorAPI'); // matched casing

test('calculateTotal should return 110 when getNumberFromAPI is mocked to return 100', () => {
    // 1. Use 'calculator', fix the spelling, and put it inside the test
    jest.spyOn(calculator, 'getNumberFromAPI').mockReturnValue(100);

    // 2. Use 'calculator' to call the method
    const total = calculator.calculateTotal();

    // 3. Expect 110, not 52
    expect(total).toBe(110);
});