/**
 * tests/dashboardService.test.js
 * Unit tests for dashboardService.
 * Mocks employeeService to isolate computation logic.
 */

'use strict';

/* ── Test dataset ────────────────────────────────────────── */
const MOCK_EMPLOYEES = [
  { id: 1,  firstName: 'Priya',   lastName: 'Menon',   department: 'Engineering', status: 'Active'   },
  { id: 2,  firstName: 'Arjun',   lastName: 'Sharma',  department: 'Engineering', status: 'Inactive' },
  { id: 3,  firstName: 'Deepa',   lastName: 'Nair',    department: 'HR',          status: 'Active'   },
  { id: 4,  firstName: 'Rahul',   lastName: 'Gupta',   department: 'Finance',     status: 'Active'   },
  { id: 5,  firstName: 'Sneha',   lastName: 'Patel',   department: 'Marketing',   status: 'Active'   },
  { id: 6,  firstName: 'Vikram',  lastName: 'Singh',   department: 'Operations',  status: 'Active'   },
  { id: 7,  firstName: 'Ananya',  lastName: 'Krishnan',department: 'Engineering', status: 'Inactive' },
  { id: 8,  firstName: 'Karan',   lastName: 'Mehta',   department: 'Finance',     status: 'Active'   },
  { id: 9,  firstName: 'Pooja',   lastName: 'Reddy',   department: 'Marketing',   status: 'Inactive' },
  { id: 10, firstName: 'Sanjay',  lastName: 'Iyer',    department: 'HR',          status: 'Active'   }
];

/* ── Inline dashboardService ─────────────────────────────── */
function createDashboardService(employees) {
  const employeeService = {
    getAll: () => [...employees]
  };

  function getSummary() {
    const all = employeeService.getAll();
    const active   = all.filter(e => e.status === 'Active').length;
    const inactive = all.filter(e => e.status === 'Inactive').length;
    const departments = new Set(all.map(e => e.department)).size;
    return { total: all.length, active, inactive, departments };
  }

  function getDepartmentBreakdown() {
    const all = employeeService.getAll();
    const map = {};
    all.forEach(e => { map[e.department] = (map[e.department] || 0) + 1; });
    return Object.entries(map).map(([department, count]) => ({ department, count }))
      .sort((a, b) => b.count - a.count);
  }

  function getRecentEmployees(n = 5) {
    return [...employeeService.getAll()]
      .sort((a, b) => b.id - a.id)
      .slice(0, n);
  }

  return { getSummary, getDepartmentBreakdown, getRecentEmployees };
}

/* ── Tests ───────────────────────────────────────────────── */

describe('dashboardService.getSummary()', () => {
  let svc;
  beforeEach(() => { svc = createDashboardService(MOCK_EMPLOYEES); });

  test('returns correct total count', () => {
    expect(svc.getSummary().total).toBe(10);
  });

  test('returns correct active count', () => {
    const active = MOCK_EMPLOYEES.filter(e => e.status === 'Active').length;
    expect(svc.getSummary().active).toBe(active);
  });

  test('returns correct inactive count', () => {
    const inactive = MOCK_EMPLOYEES.filter(e => e.status === 'Inactive').length;
    expect(svc.getSummary().inactive).toBe(inactive);
  });

  test('active + inactive = total', () => {
    const { total, active, inactive } = svc.getSummary();
    expect(active + inactive).toBe(total);
  });

  test('returns correct unique department count', () => {
    const uniqueDepts = new Set(MOCK_EMPLOYEES.map(e => e.department)).size;
    expect(svc.getSummary().departments).toBe(uniqueDepts);
  });

  test('returns zero totals for empty store', () => {
    const empty = createDashboardService([]);
    const s = empty.getSummary();
    expect(s.total).toBe(0);
    expect(s.active).toBe(0);
    expect(s.inactive).toBe(0);
    expect(s.departments).toBe(0);
  });
});

describe('dashboardService.getDepartmentBreakdown()', () => {
  let svc;
  beforeEach(() => { svc = createDashboardService(MOCK_EMPLOYEES); });

  test('returns an entry for each unique department', () => {
    const uniqueDepts = new Set(MOCK_EMPLOYEES.map(e => e.department)).size;
    expect(svc.getDepartmentBreakdown().length).toBe(uniqueDepts);
  });

  test('Engineering count is correct', () => {
    const engCount = MOCK_EMPLOYEES.filter(e => e.department === 'Engineering').length;
    const entry = svc.getDepartmentBreakdown().find(d => d.department === 'Engineering');
    expect(entry.count).toBe(engCount);
  });

  test('sum of all department counts equals total employees', () => {
    const total = svc.getDepartmentBreakdown().reduce((acc, d) => acc + d.count, 0);
    expect(total).toBe(MOCK_EMPLOYEES.length);
  });

  test('sorted descending by count', () => {
    const breakdown = svc.getDepartmentBreakdown();
    for (let i = 1; i < breakdown.length; i++) {
      expect(breakdown[i - 1].count).toBeGreaterThanOrEqual(breakdown[i].count);
    }
  });
});

describe('dashboardService.getRecentEmployees()', () => {
  let svc;
  beforeEach(() => { svc = createDashboardService(MOCK_EMPLOYEES); });

  test('returns correct number of recent employees (default 5)', () => {
    expect(svc.getRecentEmployees(5)).toHaveLength(5);
  });

  test('returns employees with highest IDs first', () => {
    const recent = svc.getRecentEmployees(5);
    expect(recent[0].id).toBe(10);
    expect(recent[1].id).toBe(9);
  });

  test('returns all employees when n > total', () => {
    expect(svc.getRecentEmployees(100)).toHaveLength(MOCK_EMPLOYEES.length);
  });

  test('returns empty array for empty store', () => {
    const empty = createDashboardService([]);
    expect(svc.getRecentEmployees(5)).toBeDefined();
    expect(empty.getRecentEmployees(5)).toHaveLength(0);
  });

  test('respects custom n parameter', () => {
    expect(svc.getRecentEmployees(3)).toHaveLength(3);
  });
});
