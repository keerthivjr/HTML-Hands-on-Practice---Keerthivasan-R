XYZ Solutions — Employee Management System
==========================================
Mini Project 1 | Frontend Only

-----------------------------------------
HOW TO RUN THE APP
-----------------------------------------
1. Open index.html directly in any modern browser (Chrome, Firefox, Edge).
   No build step, no server needed.

Default login credentials:
  Username: admin
  Password: admin123

You may also Sign Up with any new username/password (min 6 chars).

-----------------------------------------
HOW TO RUN THE TESTS
-----------------------------------------
Prerequisites: Node.js v16+ installed.

1. Open a terminal in the project root (where package.json lives).
2. Run: npm install
3. Run: npm test

All 3 test files will execute:
  tests/employeeService.test.js
  tests/authService.test.js
  tests/dashboardService.test.js

-----------------------------------------
PROJECT STRUCTURE
-----------------------------------------
index.html              — Single HTML page (all views)
css/styles.css          — Custom styles (supplements Bootstrap 5)
js/data.js              — Static employee data + initial admin
js/storageService.js    — In-memory data read/write interface
js/authService.js       — Auth logic (signup, login, logout, session)
js/employeeService.js   — Employee CRUD, search, filter, sort
js/validationService.js — Form validation (employee + auth)
js/dashboardService.js  — Dashboard computation (summary, breakdown, recent)
js/uiService.js         — All DOM rendering and feedback
js/app.js               — Event orchestration entry point
tests/                  — Jest unit tests (3 files, 3+ tests each)
package.json            — Node manifest (jest dev dependency)
jest.config.js          — Jest configuration

-----------------------------------------
TECHNOLOGY
-----------------------------------------
HTML5, CSS3, Bootstrap 5.3, Bootstrap Icons 1.11
jQuery 3.7, Vanilla ES6+ JS
Jest 29 (unit tests only — no browser needed for tests)

-----------------------------------------
NOTES
-----------------------------------------
- All data is stored in-memory (no localStorage required).
- No frameworks (React/Vue/Angular) used.
- No backend or API calls.
- All JS logic is distributed across 7 service modules.
- No inline JS in HTML files.
