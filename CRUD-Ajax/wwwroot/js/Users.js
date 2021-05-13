//FUNCION QUE INICIALIZA LOS DATOS EN LA TABLA
$(document).ready(function () {
    loadData();
});
//LISTA DE DATOS EN TABLA
function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.usuarioId + '</td>';
                html += '<td>' + item.nombre + '</td>';
                html += '<td>' + item.nombreUsuario + '</td>';
                html += '<td>' + item.estado + '</td>';                
                html += '<td><a href="#" onclick="return getbyID(' + item.usuarioId + ')">Edit</a> | <a href="#" onclick="Delele(' + item.usuarioId + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//GUARDAR LOS DATOS EN EL MODELO
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }   
    $.ajax({
        url: '/Home/Add',
        data: {
            UsuarioId: $('#txtUsuarioId').val(),
            Nombre: $('#txtNombre').val(),
            NombreUsuario: $('#txtNombreUsuario').val()            
        },          
        type: 'POST',        
        success: function (result) {
            loadData();
            if (result == true) {
                $('#myModal').modal('hide');
                                
                $("#Mensaje").html("<div class='alert alert-success' role='alert'>Usuario Guardado con Exito</div >");
            } else {
                //$('#myModal').modal('hide');
                $("#MsjError").html("<div class='alert alert-danger' role='alert'>El usuario ya existe...!</div >");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//MUESTRA DATOS SELECCIONADO PARA ACTUALIZAR
function getbyID(Id) {
    $('#txtNombre').css('border-color', 'lightgrey');
    $('#txtNombreUsuario').css('border-color', 'lightgrey');
    $('#txtEstado').css('border-color', 'lightgrey');
    
    $.ajax({
        url: "/Home/getbyID/" + Id,
        type: "GET",        
        success: function (result) {
            $('#txtUsuarioId').val(result.usuarioId);
            $('#txtNombre').val(result.nombre);
            $('#txtNombreUsuario').val(result.nombreUsuario);                   
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//ACTUALIZA DATOS
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    
    $.ajax({
        url: "/Home/Update",
        data: {
            UsuarioId: $('#txtUsuarioId').val(),
            Nombre: $('#txtNombre').val(),
            NombreUsuario: $('#txtNombreUsuario').val(),
            Estado: $('#txtEstado').val()
        }, 
        type: "POST",        
        success: function (result) {
            loadData();
            if (result == true) {
                $('#myModal').modal('hide');
                $('#txtUsuarioId').val("");
                $('#txtNombre').val("");
                $('#txtNombreUsuario').val("");  

                $("#Mensaje").html("<div class='alert alert-success' role='alert'>Usuario Actualizado con Exito</div >");
            } else {
                //$('#myModal').modal('hide');
                $("#MsjError").html("<div class='alert alert-danger' role='alert'>El usuario ya existe...!</div >");
            }                              
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//ELIMINA UN REGISTRO
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Home/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//LIMPIA LOS CAMPOS DE TEXTOS
function clearTextBox() {
    $('#txtUsuarioId').val("");
    $('#txtNombre').val("");
    $('#txtNombreUsuario').val("");       
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtNombre').css('border-color', 'lightgrey');
    $('#txtNombreUsuario').css('border-color', 'lightgrey');
    $('#txtEstado').css('border-color', 'lightgrey');     
}

//VALIDACIÓN DE CAMPOS DE TEXTOS
function validate() {
    var isValid = true;
    if ($('#txtNombre').val().trim() == "") {
        $('#txtNombre').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#txtNombre').css('border-color', 'lightgrey');
    }
    if ($('#txtNombreUsuario').val().trim() == "") {
        $('#txtNombreUsuario').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#txtNombreUsuario').css('border-color', 'lightgrey');
    }      
    return isValid;
}


