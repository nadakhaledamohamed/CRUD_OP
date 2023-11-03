


$(document).ready(function () {
    ShowEmployeList();
    LoadDepartment();
});
//Load Data into tables
function ShowEmployeList() {
    $.ajax({
        url: '/Emp/EmployeList',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result,status,xhr)
        {
            var obj = '';
            $.each(result, function (index, item) {
                obj += '<tr>';
                obj += '<td>' + item.employeeId + '</td>';
                obj += '<td>' + item.name + '</td>';
            
                obj += '<td>' + item.departmentId + '</td>';
                obj += '<td>' + item.phoneNumber + '</td>';
        
                obj += '<td><a href = "#" class="btn btn-primary btn-sm" onclick="Edit(' + item.employeeId + ')" >Edit</a > || <a href = "#" class="btn btn-danger btn-sm" onclick="Delete(' + item.employeeId +')" >Delete</a ></td>';
                obj += '</tr>';

            });
            $('#table_data').html(obj);
        },
        error: function () {}
    })
};
//Modal open 
$('#btnAddEmployee').click(function () {
    Clear();
    $('#Employeemodal').modal('show');
    $('#HeadId').text('Add Record');
    $('#btnUpdateEmployee').css('display', 'none');
    $('#InsertEmployee').css('display', 'block');
    
});
//Modal Hide
function HidePopUpModal()
{
    $('#Employeemodal').modal('hide');
}
//Clear value from textbox
function Clear() {
    $('#Name').val('')
  
    $('#PhoneNumber').val('');
 
    $('#ddldesignation').val('');
    
}
//Add Employee Data
function addEmployee() {
    
    var objData = {
        Name: $('#Name').val(),
  
        PhoneNumber: $('#PhoneNumber').val(),
    
        DepartmentId: $('#ddldesignation').val()
       
    }
    $.ajax({
        type: 'Post',
        url: '/Emp/addEmployee',
        data: objData,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (result) {
            alert("Data Save")
            Clear();
            ShowEmployeList();
            HidePopUpModal();
        },
        error: function () { }
    });
}
//Delete Employee Data
function Delete(id)
{
    if (confirm('Are you sure to want to delete this record?')) {
        $.ajax({
            url: '/Emp/Delete?id=' + id,
            success: function () {

                alert('Record is Deleted');
                ShowEmployeList();
            },
            error: function (error) {
                alert('Record is not Deleted');
            }
        })
    }
    
}
//Get Data by Id
function Edit(id) {
    
    
    $.ajax({
        type: 'Get',
        url: '/Emp/GetbyId?id=' +id,       
        dataType: 'json',
        success: function (result) {
            
            $('#Employeemodal').modal('show');
            $('#EmployeeId').val(result.employeeId);
            $('#Name').val(result.name);
            $('#ddldesignation').val(result.departmentId);
         
            $('#PhoneNumber').val(result.phoneNumber);
        
      
            $('#InsertEmployee').css('display','none');
            $('#btnUpdateEmployee').css('display', 'block');
            $('#HeadId').text('Update Record');


           

            
        },
        error: function () { }
    });
}
//Update data function
function UpdateEmployee() {
    
    var objData = {
        EmployeeId: $('#EmployeeId').val(),
        Name: $('#Name').val(),

        PhoneNumber: $('#PhoneNumber').val(),
        
        DepartmentId: $('#ddldesignation').val()
      

    }
    $.ajax({
        type: 'Post',
        url: '/Emp/Update',
        data: objData,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (result) {
            alert("Data Updated")
            Clear();
            ShowEmployeList();
            HidePopUpModal();
        },
        error: function () { }
    });
}
//Load Department in Dropdownlist
function LoadDepartment() {    
    $('#ddldesignation option').remove();
    $('#ddldesignation').append("<option value=''>Select Department</option>");

    $.ajax({
        url: '/Emp/DepartmentList',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (data, status, xhr) {
            
            if (data != null) {
                $.each(data, function (key, value) {
                    $('#ddldesignation').append("<option value='" + value.departmentId + "'>" + value.departmentName + "</option>");
                });
            }
        },
        error() {

        }

    });
}