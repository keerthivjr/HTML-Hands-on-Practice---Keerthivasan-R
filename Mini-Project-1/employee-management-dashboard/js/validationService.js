/**
 * validationService.js
 * Owns all client-side form validation logic.
 * Returns field-level error messages. Never touches the DOM.
 *
 * @module validationService
 */

const validationService = (() => {

  /** Basic email regex */
  const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  /**
   * Validates the employee add/edit form.
   * @param {Object} formData - Keys: firstName, lastName, email, phone, department, designation, salary, joinDate, status
   * @param {number|null} editId - ID of employee being edited (null for add)
   * @returns {Object} Map of fieldName → error string (empty object = valid)
   */
  function validateEmployeeForm(formData, editId = null) {
    const errors = {};

    if (!formData.firstName || !formData.firstName.trim()) {
      errors.firstName = 'First name is required.';
    }

    if (!formData.lastName || !formData.lastName.trim()) {
      errors.lastName = 'Last name is required.';
    }

    if (!formData.email || !formData.email.trim()) {
      errors.email = 'Email address is required.';
    } else if (!EMAIL_REGEX.test(formData.email.trim())) {
      errors.email = 'Please enter a valid email address.';
    } else {
      // Duplicate email check
      const existing = employeeService.getAll().find(
        e => e.email.toLowerCase() === formData.email.trim().toLowerCase() && e.id !== editId
      );
      if (existing) {
        errors.email = 'This email address is already in use.';
      }
    }

    if (!formData.phone || !formData.phone.trim()) {
      errors.phone = 'Phone number is required.';
    } else if (!/^\d{10}$/.test(formData.phone.trim())) {
      errors.phone = 'Phone must be exactly 10 digits.';
    }

    if (!formData.department) {
      errors.department = 'Please select a department.';
    }

    if (!formData.designation || !formData.designation.trim()) {
      errors.designation = 'Designation is required.';
    }

    const salary = parseFloat(formData.salary);
    if (formData.salary === '' || formData.salary === undefined || formData.salary === null) {
      errors.salary = 'Salary is required.';
    } else if (isNaN(salary) || salary <= 0) {
      errors.salary = 'Salary must be a positive number.';
    }

    if (!formData.joinDate) {
      errors.joinDate = 'Join date is required.';
    }

    if (!formData.status) {
      errors.status = 'Please select a status.';
    }

    return errors;
  }

  /**
   * Validates the auth (signup/login) form.
   * @param {Object} formData - Keys: username, password, confirmPassword (confirmPassword optional for login)
   * @param {boolean} isSignup
   * @param {string|null} duplicateUsernameError - Pre-checked by authService
   * @returns {Object} Map of fieldName → error string
   */
  function validateAuthForm(formData, isSignup = false, duplicateUsernameError = null) {
    const errors = {};

    if (!formData.username || !formData.username.trim()) {
      errors.username = 'Username is required.';
    } else if (duplicateUsernameError) {
      errors.username = duplicateUsernameError;
    }

    if (!formData.password) {
      errors.password = 'Password is required.';
    } else if (isSignup && formData.password.length < 6) {
      errors.password = 'Password must be at least 6 characters.';
    }

    if (isSignup) {
      if (!formData.confirmPassword) {
        errors.confirmPassword = 'Please confirm your password.';
      } else if (formData.password && formData.password !== formData.confirmPassword) {
        errors.confirmPassword = 'Passwords do not match.';
      }
    }

    return errors;
  }

  return { validateEmployeeForm, validateAuthForm };
})();
