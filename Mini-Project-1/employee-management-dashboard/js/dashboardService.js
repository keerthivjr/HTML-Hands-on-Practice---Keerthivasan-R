/**
 * dashboardService.js
 * Owns all dashboard computation logic.
 * Uses employeeService for data. Never touches the DOM.
 *
 * @module dashboardService
 */

const dashboardService = (() => {

  /**
   * Returns summary KPI counts.
   * @returns {{ total: number, active: number, inactive: number, departments: number }}
   */
  function getSummary() {
    const all = employeeService.getAll();
    const active   = all.filter(e => e.status === 'Active').length;
    const inactive = all.filter(e => e.status === 'Inactive').length;
    const departments = new Set(all.map(e => e.department)).size;
    return { total: all.length, active, inactive, departments };
  }

  /**
   * Returns employee count per department.
   * @returns {Array<{ department: string, count: number }>}
   */
  function getDepartmentBreakdown() {
    const all = employeeService.getAll();
    const map = {};
    all.forEach(e => {
      map[e.department] = (map[e.department] || 0) + 1;
    });
    return Object.entries(map).map(([department, count]) => ({ department, count }))
      .sort((a, b) => b.count - a.count);
  }

  /**
   * Returns the last n employees by ID (highest IDs = most recently added).
   * @param {number} [n=5]
   * @returns {Array<Object>}
   */
  function getRecentEmployees(n = 5) {
    return [...employeeService.getAll()]
      .sort((a, b) => b.id - a.id)
      .slice(0, n);
  }

  return { getSummary, getDepartmentBreakdown, getRecentEmployees };
})();
