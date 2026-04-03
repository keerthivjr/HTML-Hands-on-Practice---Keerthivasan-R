/**
 * storageService.js
 * The sole interface between all service modules and the in-memory data store.
 * No other module reads or writes the employee array directly.
 *
 * @module storageService
 */

const storageService = (() => {
  /** @type {Array<Object>} In-memory employee store (deep copy of INITIAL_EMPLOYEES) */
  let _employees = JSON.parse(JSON.stringify(INITIAL_EMPLOYEES));

  /**
   * Returns a shallow copy of all employee records.
   * @returns {Array<Object>}
   */
  function getAll() {
    return [..._employees];
  }

  /**
   * Returns a single employee by ID, or null if not found.
   * @param {number} id
   * @returns {Object|null}
   */
  function getById(id) {
    return _employees.find(e => e.id === id) || null;
  }

  /**
   * Adds a new employee object to the store.
   * @param {Object} employee - Employee object (id already assigned)
   * @returns {Object} The added employee
   */
  function add(employee) {
    _employees.push({ ...employee });
    return employee;
  }

  /**
   * Updates an existing employee record by ID.
   * @param {number} id
   * @param {Object} data - Fields to update
   * @returns {Object|null} Updated employee or null
   */
  function update(id, data) {
    const idx = _employees.findIndex(e => e.id === id);
    if (idx === -1) return null;
    _employees[idx] = { ..._employees[idx], ...data, id };
    return _employees[idx];
  }

  /**
   * Removes an employee by ID.
   * @param {number} id
   * @returns {boolean} True if removed, false if not found
   */
  function remove(id) {
    const idx = _employees.findIndex(e => e.id === id);
    if (idx === -1) return false;
    _employees.splice(idx, 1);
    return true;
  }

  /**
   * Returns the next auto-incremented ID.
   * @returns {number}
   */
  function nextId() {
    if (_employees.length === 0) return 1;
    return Math.max(..._employees.map(e => e.id)) + 1;
  }

  return { getAll, getById, add, update, remove, nextId };
})();
