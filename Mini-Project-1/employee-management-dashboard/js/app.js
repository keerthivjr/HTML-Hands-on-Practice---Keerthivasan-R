/**
 * app.js
 * Application entry point and event orchestrator.
 * Initialises the app on DOM ready, sets up jQuery event listeners,
 * and coordinates calls between service modules.
 * Contains NO business logic — delegates to services, then calls uiService.
 */

$(function () {

  /* ── State ─────────────────────────────────────────────────── */
  let _deleteTargetId = null;
  let _currentSearch  = '';
  let _currentDept    = '';
  let _currentStatus  = '';
  let _currentSort    = '';

  /* ── Helpers ───────────────────────────────────────────────── */

  /** Applies all current filters/sort and re-renders the table */
  function _refreshTable() {
    let employees = employeeService.applyFilters(_currentSearch, _currentDept, _currentStatus);
    if (_currentSort) {
      const [field, dir] = _currentSort.split('-');
      employees = employeeService.sortBy(employees, field, dir);
    }
    uiService.renderEmployeeTable(employees);
    uiService.updateRecordCount(employees.length);
  }

  /** Refreshes all dashboard panels */
  function _refreshDashboard() {
    uiService.renderDashboardCards(dashboardService.getSummary());
    uiService.renderDepartmentBreakdown(dashboardService.getDepartmentBreakdown());
    uiService.renderRecentEmployees(dashboardService.getRecentEmployees(5));
  }

  /** Shows the given section (dashboard or employees), hides the other */
  function _showSection(section) {
    if (section === 'dashboard') {
      $('#dashboardSection').removeClass('d-none');
      $('#employeeSection').addClass('d-none');
      $('#navDashboard').addClass('active');
      $('#navEmployees').removeClass('active');
      _refreshDashboard();
    } else {
      $('#employeeSection').removeClass('d-none');
      $('#dashboardSection').addClass('d-none');
      $('#navEmployees').addClass('active');
      $('#navDashboard').removeClass('active');
      _refreshTable();
    }
  }

  /** Shows auth wrapper and hides app */
  function _showAuth(view) {
    $('#appWrapper').addClass('d-none');
    $('#authWrapper').removeClass('d-none');
    if (view === 'signup') {
      $('#signupView').removeClass('d-none');
      $('#loginView').addClass('d-none');
    } else {
      $('#loginView').removeClass('d-none');
      $('#signupView').addClass('d-none');
    }
  }

  /** Shows app wrapper and hides auth */
  function _showApp() {
    $('#authWrapper').addClass('d-none');
    $('#appWrapper').removeClass('d-none');
    $('#navUsername').text(authService.getCurrentUser());
    _showSection('dashboard');
  }

  /* ── Initialisation ────────────────────────────────────────── */

  function init() {
    if (authService.isLoggedIn()) {
      _showApp();
    } else {
      _showAuth('login');
    }
  }

  /* ── Auth: Switch views ────────────────────────────────────── */

  $('#goToSignup').on('click', function (e) {
    e.preventDefault();
    uiService.clearAuthErrors('login');
    $('#loginUsername, #loginPassword').val('');
    _showAuth('signup');
  });

  $('#goToLogin').on('click', function (e) {
    e.preventDefault();
    uiService.clearAuthErrors('signup');
    $('#signupUsername, #signupPassword, #signupConfirm').val('');
    _showAuth('login');
  });

  /* ── Auth: Signup ──────────────────────────────────────────── */

  $('#signupBtn').on('click', function () {
    const formData = {
      username:        $('#signupUsername').val(),
      password:        $('#signupPassword').val(),
      confirmPassword: $('#signupConfirm').val()
    };

    const errors = validationService.validateAuthForm(formData, true);
    if (Object.keys(errors).length > 0) {
      uiService.showAuthErrors(errors, 'signup');
      return;
    }

    const result = authService.signup(formData.username, formData.password);
    if (!result.success) {
      uiService.showAuthErrors({ username: result.error }, 'signup');
      return;
    }

    uiService.showToast('Account created successfully! Please sign in.', 'success');
    setTimeout(() => {
      $('#signupUsername, #signupPassword, #signupConfirm').val('');
      uiService.clearAuthErrors('signup');
      _showAuth('login');
    }, 1000);
  });

  /* ── Auth: Login ───────────────────────────────────────────── */

  $('#loginBtn').on('click', function () {
    const formData = {
      username: $('#loginUsername').val(),
      password: $('#loginPassword').val()
    };

    const errors = validationService.validateAuthForm(formData, false);
    if (Object.keys(errors).length > 0) {
      uiService.showAuthErrors(errors, 'login');
      return;
    }

    const result = authService.login(formData.username, formData.password);
    if (!result.success) {
      $('#loginAlert').text('Invalid credentials. Please check your username and password.').removeClass('d-none').addClass('alert-danger');
      return;
    }

    $('#loginAlert').addClass('d-none').text('');
    _showApp();
  });

  /* Allow pressing Enter in auth forms */
  $('#loginPassword').on('keydown', function (e) {
    if (e.key === 'Enter') $('#loginBtn').trigger('click');
  });
  $('#signupConfirm').on('keydown', function (e) {
    if (e.key === 'Enter') $('#signupBtn').trigger('click');
  });

  /* Clear auth errors on typing */
  $('#loginUsername, #loginPassword').on('input', function () {
    $(this).removeClass('is-invalid');
    $('#loginAlert').addClass('d-none');
  });
  $('#signupUsername, #signupPassword, #signupConfirm').on('input', function () {
    $(this).removeClass('is-invalid');
  });

  /* ── Auth: Logout ──────────────────────────────────────────── */

  $('#logoutBtn').on('click', function () {
    authService.logout();
    _showAuth('login');
    uiService.showToast('You have been signed out.', 'info');
  });

  /* ── Navigation ────────────────────────────────────────────── */

  $('#navDashboard').on('click', function (e) {
    e.preventDefault();
    if (!authService.isLoggedIn()) { _showAuth('login'); return; }
    _showSection('dashboard');
  });

  $('#navEmployees').on('click', function (e) {
    e.preventDefault();
    if (!authService.isLoggedIn()) { _showAuth('login'); return; }
    _showSection('employees');
  });

  /* ── Open Add Employee Modal ───────────────────────────────── */

  function openAddModal() {
    if (!authService.isLoggedIn()) return;
    uiService.clearForm();
    const modal = new bootstrap.Modal(document.getElementById('empFormModal'));
    modal.show();
  }

  $('#navAddEmployee, #empAddEmployee').on('click', openAddModal);

  /* ── Employee Form Submit (Add / Edit) ─────────────────────── */

  $('#empFormSubmit').on('click', function () {
    const editId = $('#editEmployeeId').val() ? parseInt($('#editEmployeeId').val()) : null;

    const formData = {
      firstName:   $('#empFirstName').val().trim(),
      lastName:    $('#empLastName').val().trim(),
      email:       $('#empEmail').val().trim(),
      phone:       $('#empPhone').val().trim(),
      department:  $('#empDepartment').val(),
      designation: $('#empDesignation').val().trim(),
      salary:      $('#empSalary').val(),
      joinDate:    $('#empJoinDate').val(),
      status:      $('#empStatus').val()
    };

    const errors = validationService.validateEmployeeForm(formData, editId);
    if (Object.keys(errors).length > 0) {
      uiService.showInlineErrors(errors);
      return;
    }

    const payload = { ...formData, salary: parseFloat(formData.salary) };

    if (editId) {
      employeeService.update(editId, payload);
      uiService.showToast('Employee updated successfully.', 'success');
    } else {
      employeeService.add(payload);
      uiService.showToast('Employee added successfully.', 'success');
    }

    bootstrap.Modal.getInstance(document.getElementById('empFormModal')).hide();
    _refreshTable();
    _refreshDashboard();
  });

  /* Clear errors when user edits a field */
  $('#empFormModal').on('input change', '.form-control, .form-select', function () {
    $(this).removeClass('is-invalid');
    $(this).next('.invalid-feedback').text('').removeClass('show');
  });

  /* ── View Employee ─────────────────────────────────────────── */

  $('#employeeTableBody').on('click', '.btn-action.view', function () {
    const id = parseInt($(this).data('id'));
    const employee = employeeService.getById(id);
    if (employee) uiService.showViewModal(employee);
  });

  /* ── Edit Employee ─────────────────────────────────────────── */

  $('#employeeTableBody').on('click', '.btn-action.edit', function () {
    const id = parseInt($(this).data('id'));
    const employee = employeeService.getById(id);
    if (!employee) return;
    uiService.clearForm();
    uiService.populateForm(employee);
    const modal = new bootstrap.Modal(document.getElementById('empFormModal'));
    modal.show();
  });

  /* ── Delete Employee ───────────────────────────────────────── */

  $('#employeeTableBody').on('click', '.btn-action.del', function () {
    const id = parseInt($(this).data('id'));
    const employee = employeeService.getById(id);
    if (!employee) return;
    _deleteTargetId = id;
    $('#deleteEmpMessage').text(`Are you sure you want to delete ${employee.firstName} ${employee.lastName}?`);
    const modal = new bootstrap.Modal(document.getElementById('deleteEmpModal'));
    modal.show();
  });

  $('#confirmDeleteBtn').on('click', function () {
    if (_deleteTargetId === null) return;
    employeeService.remove(_deleteTargetId);
    _deleteTargetId = null;
    bootstrap.Modal.getInstance(document.getElementById('deleteEmpModal')).hide();
    uiService.showToast('Employee deleted.', 'danger');
    _refreshTable();
    _refreshDashboard();
  });

  /* ── Search ────────────────────────────────────────────────── */

  $('#searchInput').on('input', function () {
    _currentSearch = $(this).val();
    if (_currentSearch) $('#clearSearch').removeClass('d-none');
    else $('#clearSearch').addClass('d-none');
    _refreshTable();
  });

  $('#clearSearch').on('click', function () {
    $('#searchInput').val('');
    _currentSearch = '';
    $(this).addClass('d-none');
    _refreshTable();
  });

  /* ── Department Filter ─────────────────────────────────────── */

  $('#deptFilter').on('change', function () {
    _currentDept = $(this).val();
    _refreshTable();
  });

  /* ── Status Filter ─────────────────────────────────────────── */

  $('input[name="statusOpt"]').on('change', function () {
    _currentStatus = $(this).val();
    _refreshTable();
  });

  /* ── Sort ──────────────────────────────────────────────────── */

  $('#sortSelect').on('change', function () {
    _currentSort = $(this).val();
    _refreshTable();
  });

  /* ── Boot ──────────────────────────────────────────────────── */
  init();
});
