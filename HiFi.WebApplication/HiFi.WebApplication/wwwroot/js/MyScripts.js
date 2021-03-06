﻿
$(document).ready(function () {
    $("#myFlashMessageDiv").show().delay(2000).fadeOut();
    var wed = $('#adminCategoriesTable').DataTable({
        bSort: true,

        'aoColumns': [
            { sWidth: "25%", bSearchable: true, bSortable: true, className: "dt-left" },
            { sWidth: "20%", bSearchable: true, bSortable: true, className: "dt-left" },
            { sWidth: "10%", bSearchable: true, bSortable: true, className: "dt-left" },
            { sWidth: "22%", bSearchable: true, bSortable: true, className: "dt-left" },
            { sWidth: "22%", bSearchable: true, bSortable: true, className: "dt-left" },
            { sWidth: "10%", bSearchable: true, bSortable: true },
            { sWidth: "45%", bSearchable: true, bSortable: true }
            //{ sWidth: "10%", bSearchable: true, bSortable: true },
            //match the number of columns here for table1
        ],
        "bAutoWidth": true,
        "aLengthMenu": [[5, 10, 25, -1], [5, 10, 25, "All"]],
        "info": true,
        "paging": true,
        "fnInitComplete": function () {

            $("#adminCategoriesTable").css("width", "100%");
        }
    });

    $('#btnExportData').click(function () {
        $("#adminCategoriesTable").table2excel({
            // exclude: ".excludeThisClass",
            //name: "Worksheet Name",
            filename: "SomeFile.xls" //do not include extension
            //fnExcelReport();
        });
    });
    function fnExcelReport() {
        var tab_text = $('#adminCategoriesTable').val();

        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");

    }

    $('#adminSubCategoriesTable').DataTable({
        bSort: true,
        "bAutoWidth": true,
        "fnInitComplete": function () {
            $("#adminCategoriesTable").css("width", "100%");
        }
    });

    $('#adminProductTable').DataTable({
        bSort: true,
        "bAutoWidth": true,
        "fnInitComplete": function () {
            $("#adminCategoriesTable").css("width", "100%");
        }
    });

    $('#dateUpdated').datepicker({
        format: 'mm/dd/yyyy'
    });
})