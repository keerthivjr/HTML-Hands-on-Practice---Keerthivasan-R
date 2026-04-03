/**
 * employeeService.js
 * Owns all employee business logic.
 * Uses storageService for all data access. Never touches the DOM.
 *
 * @module employeeService
 */

const employeeService = (() => {

  /**
   * Returns all employees.
   * @returns {Array<Object>}
   */
  function getAll() {
    return storageService.getAll();
  }

  /**
   * Returns a single employee by ID.
   * @param {number} id
   * @returns {Object|null}
   */
  function getById(id) {
    return storageService.getById(id);
  }

  /**
   * Adds a new employee (assigns next ID).
   * @param {Object} data - Employee fields (no id)
   * @returns {Object} The created employee
   */
  function add(data) {
    const employee = { ...data, id: storageService.nextId() };
    return storageService.add(employee);
  }

  /**
   * Updates an existing employee.
   * @param {number} id
   * @param {Object} data
   * @returns {Object|null}
   */
  function update(id, data) {
    return storageService.update(id, data);
  }

  /**
   * Removes an employee by ID.
   * @param {number} id
   * @returns {boolean}
   */
  function remove(id) {
    return storageService.remove(id);
  }

  /**
   * Searches employees by name (firstName + lastName) or email (case-insensitive).
   * @param {string} query
   * @returns {Array<Object>}
   */
  function search(query) {
    const q = query.trim().toLowerCase();
    if (!q) return storageService.getAll();
    return storageService.getAll().filter(e => {
      const fullName = `${e.firstName} ${e.lastName}`.toLowerCase();
      return fullName.includes(q) || e.email.toLowerCase().includes(q);
    });
  }

  /**
   * Filters employees by department.
   * @param {string} dept - Department name or '' for all
   * @returns {Array<Object>}
   */
  function filterByDepartment(dept) {
    if (!dept) return storageService.getAll();
    return storageService.getAll().filter(e => e.department === dept);
  }

  /**
   * Filters employees by status.
   * @param {string} status - 'Active', 'Inactive', or '' for all
   * @returns {Array<Object>}
   */
  function filterByStatus(status) {
    if (!status) return storageService.getAll();
    return storageService.getAll().filter(e => e.status === status);
  }

  /**
   * Applies search, department, and status filters simultaneously (AND logic).
   * @param {string} searchQuery
   * @param {string} dept
   * @param {string} status
   * @returns {Array<Object>}
   */
  function applyFilters(searchQuery, dept, status) {
    const q = (searchQuery || '').trim().toLowerCase();
    return storageService.getAll().filter(e => {
      const fullName = `${e.firstName} ${e.lastName}`.toLowerCase();
      const matchesSearch = !q || fullName.includes(q) || e.email.toLowerCase().includes(q);
      const matchesDept   = !dept   || e.department === dept;
      const matchesStatus = !status || e.status === status;
      return matchesSearch && matchesDept && matchesStatus;
    });
  }

  /**
   * Sorts an employee array by the given field and direction.
   * @param {Array<Object>} employees
   * @param {string} field  - 'name' | 'salary' | 'date'
   * @param {string} direction - 'asc' | 'desc'
   * @returns {Array<Object>} New sorted array
   */
  function sortBy(employees, field, direction) {
    const arr = [...employees];
    arr.sort((a, b) => {
      let valA, valB;
      if (field === 'name') {
        valA = a.lastName.toLowerCase();
        valB = b.lastName.toLowerCase();
        return direction === 'asc' ? valA.localeCompare(valB) : valB.localeCompare(valA);
      }
      if (field === 'salary') {
        valA = a.salary;
        valB = b.salary;
        return direction === 'asc' ? valA - valB : valB - valA;
      }
      if (field === 'date') {
        valA = new Date(a.joinDate).getTime();
        valB = new Date(b.joinDate).getTime();
        return direction === 'asc' ? valA - valB : valB - valA;
      }
      return 0;
    });
    return arr;
  }

  return { getAll, getById, add, update, remove, search, filterByDepartment, filterByStatus, applyFilters, sortBy };
})();
