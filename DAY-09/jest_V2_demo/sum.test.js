// test file for sum.js using jest

const sum = require('./sum'); // Fixed typo: 'require' instead of 'reuqire'

test('adds 1 + 2 to equal 3', () => {
    expect(sum(1, 2)).toBe(3);
});