/**
 * data.js
 * Static data source for the Employee Management System.
 * Contains the initial employee array and admin credentials.
 * No logic — data declarations only.
 */

/** @type {Array<Object>} Initial employee records (min. 15, all departments, mixed statuses) */
const INITIAL_EMPLOYEES = [
  {
    id: 1,
    firstName: 'Priya',
    lastName: 'Menon',
    email: 'priya.menon@xyz.com',
    phone: '9876543210',
    department: 'Engineering',
    designation: 'Software Engineer',
    salary: 850000,
    joinDate: '2021-03-15',
    status: 'Active'
  },
  {
    id: 2,
    firstName: 'Arjun',
    lastName: 'Sharma',
    email: 'arjun.sharma@xyz.com',
    phone: '9812345678',
    department: 'Engineering',
    designation: 'Senior Developer',
    salary: 1200000,
    joinDate: '2019-07-01',
    status: 'Active'
  },
  {
    id: 3,
    firstName: 'Deepa',
    lastName: 'Nair',
    email: 'deepa.nair@xyz.com',
    phone: '9765432109',
    department: 'HR',
    designation: 'HR Executive',
    salary: 650000,
    joinDate: '2020-11-20',
    status: 'Active'
  },
  {
    id: 4,
    firstName: 'Rahul',
    lastName: 'Gupta',
    email: 'rahul.gupta@xyz.com',
    phone: '9654321098',
    department: 'Finance',
    designation: 'Finance Analyst',
    salary: 720000,
    joinDate: '2022-01-10',
    status: 'Active'
  },
  {
    id: 5,
    firstName: 'Sneha',
    lastName: 'Patel',
    email: 'sneha.patel@xyz.com',
    phone: '9543210987',
    department: 'Marketing',
    designation: 'Marketing Manager',
    salary: 950000,
    joinDate: '2018-06-25',
    status: 'Active'
  },
  {
    id: 6,
    firstName: 'Vikram',
    lastName: 'Singh',
    email: 'vikram.singh@xyz.com',
    phone: '9432109876',
    department: 'Operations',
    designation: 'Operations Lead',
    salary: 880000,
    joinDate: '2020-04-12',
    status: 'Active'
  },
  {
    id: 7,
    firstName: 'Ananya',
    lastName: 'Krishnan',
    email: 'ananya.krishnan@xyz.com',
    phone: '9321098765',
    department: 'Engineering',
    designation: 'QA Engineer',
    salary: 700000,
    joinDate: '2021-09-05',
    status: 'Inactive'
  },
  {
    id: 8,
    firstName: 'Karan',
    lastName: 'Mehta',
    email: 'karan.mehta@xyz.com',
    phone: '9210987654',
    department: 'Finance',
    designation: 'Senior Accountant',
    salary: 980000,
    joinDate: '2017-02-14',
    status: 'Active'
  },
  {
    id: 9,
    firstName: 'Pooja',
    lastName: 'Reddy',
    email: 'pooja.reddy@xyz.com',
    phone: '9109876543',
    department: 'Marketing',
    designation: 'Content Strategist',
    salary: 600000,
    joinDate: '2022-08-30',
    status: 'Active'
  },
  {
    id: 10,
    firstName: 'Sanjay',
    lastName: 'Iyer',
    email: 'sanjay.iyer@xyz.com',
    phone: '9098765432',
    department: 'HR',
    designation: 'HR Manager',
    salary: 1100000,
    joinDate: '2016-05-18',
    status: 'Active'
  },
  {
    id: 11,
    firstName: 'Meena',
    lastName: 'Bose',
    email: 'meena.bose@xyz.com',
    phone: '8987654321',
    department: 'Operations',
    designation: 'Logistics Coordinator',
    salary: 550000,
    joinDate: '2023-01-02',
    status: 'Inactive'
  },
  {
    id: 12,
    firstName: 'Rohan',
    lastName: 'Das',
    email: 'rohan.das@xyz.com',
    phone: '8876543210',
    department: 'Engineering',
    designation: 'DevOps Engineer',
    salary: 1050000,
    joinDate: '2020-10-08',
    status: 'Active'
  },
  {
    id: 13,
    firstName: 'Lakshmi',
    lastName: 'Pillai',
    email: 'lakshmi.pillai@xyz.com',
    phone: '8765432109',
    department: 'Finance',
    designation: 'Finance Manager',
    salary: 1300000,
    joinDate: '2015-03-22',
    status: 'Active'
  },
  {
    id: 14,
    firstName: 'Aditya',
    lastName: 'Kumar',
    email: 'aditya.kumar@xyz.com',
    phone: '8654321098',
    department: 'Marketing',
    designation: 'Digital Marketing Specialist',
    salary: 780000,
    joinDate: '2021-12-11',
    status: 'Inactive'
  },
  {
    id: 15,
    firstName: 'Nisha',
    lastName: 'Verma',
    email: 'nisha.verma@xyz.com',
    phone: '8543210987',
    department: 'Operations',
    designation: 'Supply Chain Analyst',
    salary: 690000,
    joinDate: '2022-07-19',
    status: 'Active'
  }
];

/**
 * Initial admin credentials object.
 * authService loads this to seed its in-memory store.
 */
const INITIAL_ADMIN = {
  username: 'admin',
  password: 'admin123'
};
