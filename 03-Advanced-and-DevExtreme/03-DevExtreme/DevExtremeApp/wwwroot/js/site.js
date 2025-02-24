// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {


    // Data Grid Script
    $(function () {
		// 'employeeData' JS variable is already created in Employee/Index.cshtml file
        console.log(employeeData);

        $("#dataGrid").dxDataGrid({
            dataSource: employeeData,
            editing: {
                allowAdding: true,
                allowUpdating: true,
                allowDeleting: true,
				mode: "popup"                   // Opens an edit form in a popup window
            },
			// Fields to be displayed in the grid
            columns: [
                { dataField: "Id", allowEditing: false },
                { dataField: "Name", caption: "Name" },
                { dataField: "Position", caption: "Position" },
                { dataField: "Salary", caption: "Salary", dataType: "number" }
            ],
            showBorders: true,                  // Adds borders in the grid
            paging: {
                pageSize: 5                     // Number of rows per page
            },
            pager: {
				showPageSizeSelector: true,     // Allows user to control number of rows per page
				allowedPageSizes: [5, 10, 20]   // Gives options of 5, 10 or 20 rows per page
            },
            filterRow: {
                visible: true
            },
            headerFilter: {
                visible: true
            },
            groupPanel: {
                visible: true
            },
            searchPanel: {
                visible: true
            }
        });
    });
});