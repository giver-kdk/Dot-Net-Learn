-- ******************************* Stored Proceduer *********************************** 
-- Stored Procedure is basically a colleciton of SQL Queries where we can implement logics like 
-- IF-ELSE, WHILE, Try-Catch, etc.

CREATE PROCEDURE GetRichEmployees AS
BEGIN
	-- This colleciton of queries returns all employees first and then return high salaried employees
	SELECT * FROM Employee;
	SELECT * FROM Employee WHERE salary >= (SELECT MAX(salary) FROM Employee);
END;





-- Modifying the existing stored procedure
-- ALTER PROCEDURE GetRichEmployees AS
-- BEGIN
	-- SELECT * FROM Employee WHERE salary >= (SELECT MAX(salary) FROM Employee);
-- END;




-- Deleting the stored procedure
-- DROP PROCEDURE IF EXISTS GetRichEmployees;


