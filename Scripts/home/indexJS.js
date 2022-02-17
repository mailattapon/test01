$(document).ready(function () {
    getTable()
});

function clickAdd() {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: urlClickAdd,
        data: {
            data: $('#txtInput').val()
        },
        success: function (data) {
            getTable()
        },
        error: function (err) {
            console.log(err.responseText);
        }
    });
}

function clickSearch() {

}

function clickComplete(row, id) {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: urlClickEdit,
        data: {
            id: id
        },
        success: function (data) {
            getTable()
        },
        error: function (err) {
            console.log(err.responseText);
        }
    });
}

function clickDelete(row, id) {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: urlClickDelete,
        data: {
            id: id
        },
        success: function (data) {
            getTable()
        },
        error: function (err) {
            console.log(err.responseText);
        }
    });
}

function clickClear() {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: urlClickClear,
        data: {
        },
        success: function (data) {
            getTable()
        },
        error: function (err) {
            console.log(err.responseText);
        }
    });
}

function sortTable(data){
    var optionhtml = "";
    $.each(data, function (i, item) {
        if (item.status == "true") {
            optionhtml += "<tr><td><s>" + item.id + "</s></td><td><s>" + item.data + "</s></td><td><s>" + item.time + "</s></td><td><button id='" + item.id + "' type='button' class='btn btn-success' onclick='clickComplete(this,this.id)'>complete</button>&nbsp;&nbsp;<button id='" + item.id + "' type='button' class='btn btn-danger' onclick='clickDelete(this,this.id)'>delete</button></td></tr>";                //} else {
        }
        else {
            optionhtml += "<tr><td>" + item.id + "</td><td>" + item.data + "</td><td>" + item.time + "</td><td><button id='" + item.id + "' type='button' class='btn btn-success' onclick='clickComplete(this,this.id)'>complete</button>&nbsp;&nbsp;<button id='" + item.id + "' type='button' class='btn btn-danger' onclick='clickDelete(this,this.id)'>delete</button></td></tr>";                //} else {
        }
    });

    $("#tableData").empty();
    $("#tableData").append(optionhtml);
}

function getTable() {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: urlGet,
        data: {},
        success: function (data) {
            var optionhtml = "";
            $.each(data, function (i, item) {
                if (item.status == "true") {
                    optionhtml += "<tr><td><s>" + item.id + "</s></td><td><s>" + item.data + "</s></td><td><s>" + item.time + "</s></td><td><button id='" + item.id + "' type='button' class='btn btn-success' onclick='clickComplete(this,this.id)'>complete</button>&nbsp;&nbsp;<button id='" + item.id + "' type='button' class='btn btn-danger' onclick='clickDelete(this,this.id)'>delete</button></td></tr>";                //} else {
                }
                else {
                    optionhtml += "<tr><td>" + item.id + "</td><td>" + item.data + "</td><td>" + item.time + "</td><td><button id='" + item.id + "' type='button' class='btn btn-success' onclick='clickComplete(this,this.id)'>complete</button>&nbsp;&nbsp;<button id='" + item.id + "' type='button' class='btn btn-danger' onclick='clickDelete(this,this.id)'>delete</button></td></tr>";                //} else {
                }
            });

            let table1 = document.querySelector('#table1');
            let dataTable = new simpleDatatables.DataTable(table1);

            $("#tableData").empty();
            $("#tableData").append(optionhtml);
        },
        error: function (err) {
            console.log(err.responseText);
        }
    });
}

function clickPrint() {
    window.open(urlPrintExcel);
}

function clickImport() {

    var fileUpload = document.getElementById("myFile");
    if (fileUpload.value != null) {
        var uploadFile = new FormData();
        var files = $("#myFile").get(0).files;
        // Add the uploaded file content to the form data collection
        if (files.length > 0) {
            uploadFile.append("CsvDoc", files[0]);
            $.ajax({
                url: urlclickImport,
                contentType: false,
                processData: false,
                data: uploadFile,
                type: 'POST',
                success: function () {
                    getTable()
                }
            });
        }
    }
}

