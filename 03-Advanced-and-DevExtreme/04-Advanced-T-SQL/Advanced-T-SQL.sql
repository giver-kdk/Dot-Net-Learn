-- ******************************* GROUP BY Clause *********************************** 
--		- The GROUP BY clause works only in following two cases: 
--			- If all columns specified by SELECT clause is also specified by GROUP BY clause.
--			- If there is a use of atleast one Aggregate Function via SELECT clause

-- GROUP BY Clause by mentioning all the SELECT columns on GROUP BY clause
SELECT salary FROM Employee GROUP BY salary;

-- GROUP BY Clause by mentioning all the SELECT columns on GROUP BY clause
SELECT empName, salary FROM Employee GROUP BY empName, salary;

-- Here, this is not allowed because the 'empName' column is missed in GROUP BY clause
-- SELECT empName, salary FROM Employee GROUP BY salary;



-- GROUP BY Clause by using Aggregate Function
SELECT salary, COUNT(empName) as EmpCount FROM Employee GROUP BY salary;


-- Here, this is not allowed because 'empName' is missed in GROUP BY clause
-- SELECT salary, empName, COUNT(empName) as EmpCount FROM Employee GROUP BY salary;



-- ******************************* Aggregation Function *********************************** 
-- The Aggregate Function compresses the multiple columns into one column	

-- Here, STRING_AGG() concatinates the corresponding 'empName' grouped on the basis of same salary
-- Here, the argument ',' is the separator of the aggregated employees 
SELECT STRING_AGG(empName, ',') as EmployeeNames, COUNT(empName) as SameSalaryCount from Employee GROUP BY salary;

-- Here, this is not allowed because 'empName' either needs to be Aggregated or be mention on GROUP BY
-- SELECT empName, COUNT(empName) as SameSalaryCount from Employee GROUP BY salary;

-- Here, SUM() function returns the sum of numeric value from the column
SELECT SUM(salary) as TotalSalary from Employee;

-- Here, AVG() function returns the average of numeric value from the column
SELECT AVG(salary) as AverageSalary from Employee;

-- Here, MIN() function returns the miniimum numeric value from the column
SELECT MIN(salary) as LowSalary from Employee;	

-- Here, MAX() function returns the maximum numeric value from the column
SELECT MAX(salary) as HighSalary from Employee;	


-- ******************************* Subqueries *********************************** 
-- Subqueries is basically a query wrapped inside another query

-- Here, the inner query returns the maximum salary and outer query select those rows satisfying the condition
SELECT empName, salary FROM Employee WHERE salary >= (SELECT MAX(salary) from Employee);


-- ******************************* SET Operations *********************************** 
-- UNION combines the result of two query responses
SELECT empName, salary from Employee WHERE salary = 40000 UNION SELECT empName, salary from Employee WHERE salary = 80000;

-- INTERSECT returns the common result of two query responses
SELECT empName, salary from Employee WHERE salary >= 40000 INTERSECT SELECT empName, salary from Employee WHERE salary <= 60000;

-- EXCEPT returns the result of 1st query by excluding the response of 2nd query from 1st query
SELECT empName, salary from Employee WHERE salary >= 40000 EXCEPT SELECT empName, salary from Employee WHERE salary = 50000;



-- ******************************* Views in Database *********************************** 
-- View is a virtual table where the values are computed based upon a query.
-- It is used to store frequently needed values and avoid repeated query computation.
-- NOTE: The data on the view gets updated automatically if it's parent table is changed.


-- Here, the below code must be written in a separate file and it is written in 'Database-View.sql' file

-- CREATE VIEW RichEmployees AS 
-- SELECT empName, salary FROM Employee WHERE salary = 
-- (SELECT MAX(salary) FROM Employee);


-- Using query on a View. Run this query only after creating the 'RichEmployees' view from another file.
SELECT * FROM RichEmployees;



-- ******************************* Stored Proceduer *********************************** 
-- Stored Procedure is basically a colleciton of SQL Queries where we can implement logics like 
-- IF-ELSE, WHILE, Try-Catch, etc.


-- Here, the below code must be written in a separate file and it is written in 'Stored-Proceduere-Creation.sql' file

-- CREATE PROCEDURE GetRichEmployees AS
-- BEGIN
-- 	SELECT * FROM Employee;
-- 	SELECT * FROM Employee WHERE salary >= (SELECT MAX(salary) FROM Employee);
-- END;


-- Executing a stored procedure. Run this query only after creating the 'GetRichEmployees' procedure from another file.
EXEC GetRichEmployees;


-- Executing a stored procedure. Run this query only after creating the 'GetRichEmployees' procedure from another file.

DECLARE @SalaryResult1 numeric(18, 0);					-- Creating a variable in SQL to store the output of stored procedure
-- Here, we need to specify the argument as 'OUTPUT' explicitly
EXEC GetSalary @Name = 'Ram KC', @Salary = @SalaryResult1 OUTPUT;		
SELECT @SalaryResult1 as SalaryofEmployee;



DECLARE @SalaryResult2 numeric(18, 0);					-- Creating a variable in SQL to store the output of stored procedure

-- The 'IF' condition won't allow to fetch Boss's salary
EXEC GetSalary @Name = 'Boss', @Salary = @SalaryResult2 OUTPUT;					
SELECT @SalaryResult2 as SalaryofEmployee;



-- ******************************* Indexing *********************************** 
-- In T-SQL, Indexing allows us to fetch the row faster rather than scanning the entire table
-- Eg: 
		-- Some index create a separate memory table where the data is sorted
		-- This new sorted table also contains pointer column to store pointer to the row of original table

-- Faster Query because index is already created
SELECT * FROM Employee WHERE salary >= 60000;
