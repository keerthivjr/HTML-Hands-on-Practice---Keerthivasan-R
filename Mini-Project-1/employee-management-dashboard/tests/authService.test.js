/**
 * tests/authService.test.js
 * Unit tests for authService.
 */

'use strict';

/* ── Inline authService (mirrors js/authService.js) ─────── */
function createAuthService(initialAdmin) {
  let _admins = [{ ...initialAdmin }];
  let _currentUser = null;

  function signup(username, password) {
    const trimmed = username.trim();
    if (_admins.some(a => a.username.toLowerCase() === trimmed.toLowerCase())) {
      return { success: false, error: 'Username already exists. Please choose a different username.' };
    }
    _admins.push({ username: trimmed, password });
    return { success: true };
  }

  function login(username, password) {
    const admin = _admins.find(
      a => a.username.toLowerCase() === username.trim().toLowerCase() && a.password === password
    );
    if (admin) { _currentUser = admin.username; return { success: true }; }
    return { success: false };
  }

  function logout() { _currentUser = null; }
  function isLoggedIn() { return _currentUser !== null; }
  function getCurrentUser() { return _currentUser; }

  return { signup, login, logout, isLoggedIn, getCurrentUser };
}

/* ── Tests ───────────────────────────────────────────────── */

describe('authService.signup()', () => {
  let auth;
  beforeEach(() => {
    auth = createAuthService({ username: 'admin', password: 'admin123' });
  });

  test('successfully registers a new admin', () => {
    const result = auth.signup('newuser', 'pass123');
    expect(result.success).toBe(true);
  });

  test('rejects duplicate username (case-insensitive)', () => {
    const result = auth.signup('Admin', 'anotherpass');
    expect(result.success).toBe(false);
    expect(result.error).toMatch(/already exists/i);
  });

  test('rejects duplicate username (exact match)', () => {
    const result = auth.signup('admin', 'newpass');
    expect(result.success).toBe(false);
  });

  test('allows multiple different users to sign up', () => {
    auth.signup('alice', 'alice123');
    const result = auth.signup('bob', 'bob123');
    expect(result.success).toBe(true);
  });
});

describe('authService.login()', () => {
  let auth;
  beforeEach(() => {
    auth = createAuthService({ username: 'admin', password: 'admin123' });
  });

  test('returns success for valid credentials', () => {
    const result = auth.login('admin', 'admin123');
    expect(result.success).toBe(true);
  });

  test('sets current user on successful login', () => {
    auth.login('admin', 'admin123');
    expect(auth.getCurrentUser()).toBe('admin');
    expect(auth.isLoggedIn()).toBe(true);
  });

  test('returns failure for wrong password', () => {
    const result = auth.login('admin', 'wrongpass');
    expect(result.success).toBe(false);
    expect(auth.isLoggedIn()).toBe(false);
  });

  test('returns failure for unknown username', () => {
    const result = auth.login('unknown', 'admin123');
    expect(result.success).toBe(false);
  });

  test('is case-insensitive for username', () => {
    const result = auth.login('ADMIN', 'admin123');
    expect(result.success).toBe(true);
  });
});

describe('authService.logout()', () => {
  let auth;
  beforeEach(() => {
    auth = createAuthService({ username: 'admin', password: 'admin123' });
    auth.login('admin', 'admin123');
  });

  test('clears session on logout', () => {
    expect(auth.isLoggedIn()).toBe(true);
    auth.logout();
    expect(auth.isLoggedIn()).toBe(false);
    expect(auth.getCurrentUser()).toBeNull();
  });

  test('isLoggedIn is false before any login', () => {
    const freshAuth = createAuthService({ username: 'test', password: 'test' });
    expect(freshAuth.isLoggedIn()).toBe(false);
  });
});

describe('authService.session state', () => {
  test('getCurrentUser returns null before login', () => {
    const auth = createAuthService({ username: 'admin', password: 'admin123' });
    expect(auth.getCurrentUser()).toBeNull();
  });

  test('getCurrentUser returns username after successful login', () => {
    const auth = createAuthService({ username: 'admin', password: 'admin123' });
    auth.login('admin', 'admin123');
    expect(auth.getCurrentUser()).toBe('admin');
  });

  test('newly registered user can log in immediately', () => {
    const auth = createAuthService({ username: 'admin', password: 'admin123' });
    auth.signup('newadmin', 'securepass');
    const result = auth.login('newadmin', 'securepass');
    expect(result.success).toBe(true);
    expect(auth.isLoggedIn()).toBe(true);
  });
});
