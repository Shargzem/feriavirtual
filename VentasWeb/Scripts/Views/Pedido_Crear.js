
var tabladata;
$(document).ready(function () {
    activarMenu("Pedidos");


    ////validamos el formulario
    $("#form").validate({
        rules: {
            tipopedido: "required",
            pedidodatos: "required",
            pedidodestino: "required"
        },
        messages: {
            tipopedido: "(*)", 
            pedidodatos: "(*)",
            pedidodestino: "(*)"
        },
        errorElement: 'span'
    });

    //OBTENER Usuario
    jQuery.ajax({
        url: $.MisUrls.url._ObtenerUsuarios,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#cboUsuario").html("");

            if (data.data != null) {
                $.each(data.data, function (i, item) {

                    if (item.Activo == true) {
                        $("<option>").attr({ "value": item.IdUsuario }).text(item.Nombres).appendTo("#cboUsuario");
                    }
                })
                $("#cboUsuario").val($("#cboUsuario option:first").val());
            }

        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {
        },
    });


    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url._ObtenerPedidos,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "oUsuario", render: function (data) {
                    return data.Nombres
                }
            },
            { "data": "TipoPedido" },
            { "data": "PedidoDatos" },
            { "data": "PedidoDestino" },
            {
                "data": "Activo", "render": function (data) {
                    if (data) {
                        return '<span class="badge badge-success">Terminado</span>'
                    } else {
                        return '<span class="badge badge-danger">En proceso</span>'
                    }
                }
            },
            {
                "data": "IdPedido", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            }

        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
        responsive: true
    });


})


function abrirPopUpForm(json) {

    $("#txtid").val(0);

    if (json != null) {

        $("#txtid").val(json.IdPedido);

        $("#cboUsuario").val(json.IdUsuario);
        $("#cboPedidoTipoPedido").val(json.TipoPedido);
        $("#txtPedidoDatos").val(json.PedidoDatos);
        $("#txtPedidoDestino").val(json.PedidoDestino);
        $("#cboEstado").val(json.Activo == true ? 1 : 0);

    } else {
        $("#cboUsuario").val($("#cboUsuario option:first").val());
        $("#cboPedidoTipoPedido").val($("#cboPedidoTipoPedido option:first").val());
        $("#txtPedidoDatos").val("");
        $("#txtPedidoDestino").val("");
        $("#cboEstado").val(0);

    }

    $('#FormModal').modal('show');

}


function Guardar() {

    if ($("#form").valid()) {

        var request = {
            objeto: {
                IdPedido: $("#txtid").val(),
                IdUsuario: $("#cboUsuario").val(),
                TipoPedido: $("#cboPedidoTipoPedido").val(),
                PedidoDatos: $("#txtPedidoDatos").val(),
                PedidoDestino: $("#txtPedidoDestino").val(),
                Activo: parseInt($("#cboEstado").val()) == 1 ? true : false
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url._GuardarPedido,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    tabladata.ajax.reload();
                    swal("Mensaje", "Registrado correctamente", "success")
                    $('#FormModal').modal('hide');
                } else {

                    swal("Mensaje", "No se pudo guardar los cambios", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }

}


function eliminar($id) {


    swal({
        title: "Mensaje",
        text: "¿Desea eliminar Pedido seleccionado?",
        type: "warning",
        showCancelButton: true,

        confirmButtonText: "Si",
        confirmButtonColor: "#DD6B55",

        cancelButtonText: "No",

        closeOnConfirm: true
    },

        function () {
            jQuery.ajax({
                url: $.MisUrls.url._EliminarPedido + "?id=" + $id,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data.resultado) {
                        tabladata.ajax.reload();
                    } else {
                        swal("Mensaje", "No se pudo eliminar Pedido", "warning")
                    }
                },
                error: function (error) {
                    console.log(error)
                },
                beforeSend: function () {

                },
            });
        });

}