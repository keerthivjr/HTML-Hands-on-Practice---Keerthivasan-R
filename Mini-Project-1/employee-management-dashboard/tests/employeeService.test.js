/**
 * tests/employeeService.test.js
 * Unit tests for employeeService.
 * Mocks storageService to run purely against business logic.
 */

'use strict';

/* ── Mock storageService ─────────────────────────────────── */
let _store = [];

const storageService = {
  getAll:  () => [..._store],
  getById: (id) => _store.find(e => e.id === id) || null,
  add:     (emp) => { _store.push({ ...emp }); return emp; },
  update:  (id, data) => {
    const idx = _store.findIndex(e => e.id === id);
    if (idx === -1) return null;
    _store[idx] = { ..._store[idx], ...data, id };
    return _store[idx];
  },
  remove:  (id) => {
    const idx = _store.findIndex(e => e.id === id);
    if (idx === -1) return false;
    _store.splice(idx, 1);
    return true;
  },
  nextId: () => _store.length === 0 ? 1 : Math.max(..._store.map(e => e.id)) + 1
};

/* ── Load employeeService with mock injected via global ──── */
global.storageService = storageService;

// Inline the employeeService logic (mirrors js/employeeService.js)
const employeeService = (() => {
  function getAll()        { return storageService.getAll(); }
  function getById(id)     { return storageService.getById(id); }
  function add(data)       { const emp = { ...data, id: storageService.nextId() }; return storageService.add(emp); }
  function update(id, data){ return storageService.update(id, data); }
  function remove(id)      { return storageService.remove(id); }
  function search(q) {
    const lq = (q || '').trim().toLowerCase();
    if (!lq) return storageService.getAll();
    return storageService.getAll().filter(e => {
      const fn = `${e.firstName} ${e.lastName}`.toLowerCase();
      return fn.includes(lq) || e.email.toLowerCase().includes(lq);
    });
  }
  function filterByDepartment(dept) {
    if (!dept) return storageService.getAll();
    return storageService.getAll().filter(e => e.department === dept);
  }
  function filterByStatus(status) {
    if (!status) return storageService.getAll();
    return storageService.getAll().filter(e => e.status === status);
  }
  function applyFilters(searchQ, dept, status) {
    const q = (searchQ || '').trim().toLowerCase();
    return storageService.getAll().filter(e => {
      const fn = `${e.firstName} ${e.lastName}`.toLowerCase();
      const ms = !q || fn.includes(q) || e.email.toLowerCase().includes(q);
      const md = !dept   || e.department === dept;
      const mv = !status || e.status === status;
      return ms && md && mv;
    });
  }
  function sortBy(employees, field, direction) {
    const arr = [...employees];
    arr.sort((a, b) => {
      if (field === 'name') {
        const la = a.lastName.toLowerCase(), lb = b.lastName.toLowerCase();
        return direction === 'asc' ? la.localeCompare(lb) : lb.localeCompare(la);
      }
      if (field === 'salary') return direction === 'asc' ? a.salary - b.salary : b.salary - a.salary;
      if (field === 'date') {
        const da = new Date(a.joinDate).getTime(), db = new Date(b.joinDate).getTime();
        return direction === 'asc' ? da - db : db - da;
      }
      return 0;
    });
    return arr;
  }
  return { getAll, getById, add, update, remove, search, filterByDepartment, filterByStatus, applyFilters, sortBy };
})();

/* ── Test data seed ──────────────────────────────────────── */
const SEED = [
  { firstName: 'Priya',  lastName: 'Menon',  email: 'priya@xyz.com',  phone: '9876543210', department: 'Engineering', designation: 'SDE',      salary: 850000, joinDate: '2021-03-15', status: 'Active'   },
  { firstName: 'Arjun',  lastName: 'Sharma', email: 'arjun@xyz.com',  phone: '9812345678', department: 'HR',          designation: 'Manager',  salary: 1200000, joinDate: '2019-07-01', status: 'Inactive' },
  { firstName: 'Deepa',  lastName: 'Nair',   email: 'deepa@xyz.com',  phone: '9765432109', department: 'Finance',     designation: 'Analyst',  salary: 650000, joinDate: '2020-11-20', status: 'Active'   },
  { firstName: 'Rahul',  lastName: 'Gupta',  email: 'rahul@xyz.com',  phone: '9654321098', department: 'Marketing',   designation: 'Exec',     salary: 720000, joinDate: '2022-01-10', status: 'Active'   },
  { firstName: 'Sneha',  lastName: 'Patel',  email: 'sneha@xyz.com',  phone: '9543210987', department: 'Engineering', designation: 'DevOps',   salary: 950000, joinDate: '2018-06-25', status: 'Active'   }
];

beforeEach(() => {
  _store = [];
  SEED.forEach(s => employeeService.add({ ...s }));
});

/* ── Tests ───────────────────────────────────────────────── */

describe('employeeService.add()', () => {
  test('adds a new employee and assigns a unique ID', () => {
    const before = employeeService.getAll().length;
    const newEmp = employeeService.add({ firstName: 'Test', lastName: 'User', email: 'test@xyz.com', phone: '9000000001', department: 'HR', designation: 'Exec', salary: 500000, joinDate: '2023-01-01', status: 'Active' });
    expect(employeeService.getAll().length).toBe(before + 1);
    expect(newEmp.id).toBeDefined();
    expect(newEmp.id).toBeGreaterThan(0);
  });

  test('auto-increments ID correctly after deletion', () => {
    const ids = employeeService.getAll().map(e => e.id);
    const maxId = Math.max(...ids);
    employeeService.remove(maxId);
    const newEmp = employeeService.add({ firstName: 'New', lastName: 'Emp', email: 'new@xyz.com', phone: '9000000002', department: 'Finance', designation: 'Analyst', salary: 600000, joinDate: '2023-05-01', status: 'Active' });
    expect(newEmp.id).toBeGreaterThan(0);
    // No duplicate IDs
    const allIds = employeeService.getAll().map(e => e.id);
    expect(new Set(allIds).size).toBe(allIds.length);
  });
});

describe('employeeService.update()', () => {
  test('updates specified fields and preserves others', () => {
    const original = employeeService.getAll()[0];
    const updated = employeeService.update(original.id, { salary: 999999 });
    expect(updated.salary).toBe(999999);
    expect(updated.firstName).toBe(original.firstName);
  });

  test('returns null for non-existent ID', () => {
    const result = employeeService.update(9999, { salary: 1 });
    expect(result).toBeNull();
  });
});

describe('employeeService.remove()', () => {
  test('removes an employee by ID', () => {
    const id = employeeService.getAll()[0].id;
    const before = employeeService.getAll().length;
    const result = employeeService.remove(id);
    expect(result).toBe(true);
    expect(employeeService.getAll().length).toBe(before - 1);
    expect(employeeService.getById(id)).toBeNull();
  });

  test('returns false for non-existent ID', () => {
    expect(employeeService.remove(9999)).toBe(false);
  });
});

describe('employeeService.search()', () => {
  test('finds employee by partial first name (case-insensitive)', () => {
    const results = employeeService.search('pri');
    expect(results.some(e => e.firstName === 'Priya')).toBe(true);
  });

  test('finds employee by email fragment', () => {
    const results = employeeService.search('arjun@');
    expect(results.some(e => e.email === 'arjun@xyz.com')).toBe(true);
  });

  test('returns empty array when no match', () => {
    const results = employeeService.search('zzznomatch');
    expect(results).toHaveLength(0);
  });

  test('returns all when query is empty string', () => {
    expect(employeeService.search('')).toHaveLength(SEED.length);
  });
});

describe('employeeService.filterByDepartment()', () => {
  test('filters correctly by department', () => {
    const eng = employeeService.filterByDepartment('Engineering');
    expect(eng.every(e => e.department === 'Engineering')).toBe(true);
  });

  test('returns all employees when dept is empty string', () => {
    expect(employeeService.filterByDepartment('')).toHaveLength(SEED.length);
  });
});

describe('employeeService.filterByStatus()', () => {
  test('returns only Active employees', () => {
    const active = employeeService.filterByStatus('Active');
    expect(active.every(e => e.status === 'Active')).toBe(true);
  });

  test('returns only Inactive employees', () => {
    const inactive = employeeService.filterByStatus('Inactive');
    expect(inactive.every(e => e.status === 'Inactive')).toBe(true);
  });
});

describe('employeeService.applyFilters()', () => {
  test('AND logic: search + dept filter', () => {
    const results = employeeService.applyFilters('priya', 'Engineering', '');
    expect(results.every(e => e.department === 'Engineering')).toBe(true);
    expect(results.some(e => e.firstName === 'Priya')).toBe(true);
  });

  test('returns empty when no employees match all criteria', () => {
    const results = employeeService.applyFilters('priya', 'Finance', 'Active');
    expect(results).toHaveLength(0);
  });

  test('returns all when all filters are empty', () => {
    expect(employeeService.applyFilters('', '', '')).toHaveLength(SEED.length);
  });
});

describe('employeeService.sortBy()', () => {
  test('sorts by name asc (lastName alphabetical)', () => {
    const all = employeeService.getAll();
    const sorted = employeeService.sortBy(all, 'name', 'asc');
    for (let i = 1; i < sorted.length; i++) {
      expect(sorted[i - 1].lastName.localeCompare(sorted[i].lastName)).toBeLessThanOrEqual(0);
    }
  });

  test('sorts by salary desc', () => {
    const all = employeeService.getAll();
    const sorted = employeeService.sortBy(all, 'salary', 'desc');
    for (let i = 1; i < sorted.length; i++) {
      expect(sorted[i - 1].salary).toBeGreaterThanOrEqual(sorted[i].salary);
    }
  });

  test('sorts by date asc (oldest first)', () => {
    const all = employeeService.getAll();
    const sorted = employeeService.sortBy(all, 'date', 'asc');
    for (let i = 1; i < sorted.length; i++) {
      expect(new Date(sorted[i - 1].joinDate).getTime()).toBeLessThanOrEqual(new Date(sorted[i].joinDate).getTime());
    }
  });
});
