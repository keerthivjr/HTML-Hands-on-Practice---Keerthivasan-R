/**
 * authService.js
 * Owns all authentication logic.
 * Stores admin credentials and session state as in-memory variables.
 *
 * @module authService
 */

const authService = (() => {
  /** @type {Array<{username: string, password: string}>} In-memory admin credentials */
  let _admins = [{ ...INITIAL_ADMIN }];

  /** @type {string|null} Currently logged-in username, or null */
  let _currentUser = null;

  /**
   * Registers a new admin.
   * @param {string} username
   * @param {string} password
   * @returns {{ success: boolean, error?: string }}
   */
  function signup(username, password) {
    const trimmed = username.trim();
    if (_admins.some(a => a.username.toLowerCase() === trimmed.toLowerCase())) {
      return { success: false, error: 'Username already exists. Please choose a different username.' };
    }
    _admins.push({ username: trimmed, password });
    return { success: true };
  }

  /**
   * Authenticates an admin with username/password.
   * @param {string} username
   * @param {string} password
   * @returns {{ success: boolean }}
   */
  function login(username, password) {
    const admin = _admins.find(
      a => a.username.toLowerCase() === username.trim().toLowerCase() && a.password === password
    );
    if (admin) {
      _currentUser = admin.username;
      return { success: true };
    }
    return { success: false };
  }

  /**
   * Clears the session.
   */
  function logout() {
    _currentUser = null;
  }

  /**
   * Returns whether an admin is currently logged in.
   * @returns {boolean}
   */
  function isLoggedIn() {
    return _currentUser !== null;
  }

  /**
   * Returns the currently logged-in username, or null.
   * @returns {string|null}
   */
  function getCurrentUser() {
    return _currentUser;
  }

  return { signup, login, logout, isLoggedIn, getCurrentUser };
})();
