-- ******************************* Advanced Stored Proceduer *********************************** 
-- Here, we implement input/output variables and IF-ELSE condition in stored procedure
-- IF-ELSE, WHILE, Try-Catch, etc.


CREATE PROCEDURE GetSalary

	@Name varchar(50),							-- Input parameter to get from user
	@Salary numeric(18, 0) OUTPUT					-- Output variable given to user

AS

BEGIN
	-- If condition to check the input variable and execute query only if it satisfies the condition
	IF @Name != 'Boss'
		BEGIN
			-- This collection of queries returns all employees first and then returns salary of specified employee name
			SELECT * FROM Employee;
			SELECT @Salary = salary FROM Employee WHERE empName = @Name;
		END
	ELSE
		BEGIN
			PRINT 'Cannot reveal the salary of the Boss!';
		END
		
END;