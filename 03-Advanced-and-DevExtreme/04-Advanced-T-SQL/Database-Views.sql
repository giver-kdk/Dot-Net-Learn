-- View is a virtual table where the values are computed based upon a query.
-- It is used to store frequently needed values and avoid repeated query computation.
-- NOTE: The data on the view gets updated automatically if it's parent table is changed.

-- Here, the result of the max salary query is stored in the view named 'RichEmployees'
CREATE VIEW RichEmployees AS 
SELECT empName, salary FROM Employee WHERE salary = 
(SELECT MAX(salary) FROM Employee);