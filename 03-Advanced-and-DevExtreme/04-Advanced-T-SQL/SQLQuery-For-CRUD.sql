-- Insert new employees
INSERT INTO Employee (empName, salary, deptID)
VALUES ('Jenish Shrestha', 60000, 4);
INSERT INTO Employee (empName, salary, deptID)
VALUES ('Alex Maharjan', 50000, 5);
INSERT INTO Employee (empName, salary, deptID)
VALUES ('Sagar Khadka', 80000, 6);

-- Insert new departments
INSERT INTO Department (deptName)
VALUES ('Finance');
INSERT INTO Department (deptName)
VALUES ('IT');
INSERT INTO Department (deptName)
VALUES ('Finance');

-- Update the salary of an employee
UPDATE Employee
SET salary = 40000
WHERE empID = 2;

-- Query employees with their department names, sorted by empName
SELECT 
    e.empID,
    e.empName,
    d.deptName
FROM Employee e
INNER JOIN Department d ON e.deptID = d.deptID
WHERE d.deptName = 'IT'
ORDER BY e.empName ASC;

-- Convert the empName to uppercase. Here, UPPER() and LEN() is extended function provided by T-SQL
SELECT 
    UPPER(empName) AS CapitalName, -- UPPER converts to uppercase
    LEN(empName) AS EmpNameLength -- LEN returns the length of the string
FROM Employee;

-- Delete an employee
DELETE FROM Employee
WHERE empID = 3;