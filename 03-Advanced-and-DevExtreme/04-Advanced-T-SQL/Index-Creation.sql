-- ******************************* Indexing *********************************** 
-- In T-SQL, Indexing allows us to fetch the row faster rather than scanning the entire table
-- Eg: 
		-- Some index create a separate memory table where the data is sorted
		-- This new sorted table also contains pointer column to store pointer to the row of original table


-- Creating a separate sorted index of 'Employee' table on the basis of salary
CREATE INDEX IDX_Employee_Salary ON Employee(salary);