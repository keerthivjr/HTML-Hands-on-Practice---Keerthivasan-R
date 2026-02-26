const { sum, multiply } = require('./calc');

describe('calc tests', () => {
    test('adds 1 + 2 to equal 3', () => {
        expect(sum(1, 2)).toBe(3);
    });
    test('mulplies 2 * 3 to equal 6', () => {
        expect(multiply(2, 3)).toBe(6);
    })
})