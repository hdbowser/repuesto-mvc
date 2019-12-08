var viewModel = function () {
    var self = this;


    // [ Methods ]
    this.actualizarUsuario = function (frm) {
        if ($("#frmUsuario").valid()) {

            $.ajax({
                url: '/Usuario/Actualizar',
                method: 'POST',
                contentType: false,
                processData: false,
                cache: false,
                data: new FormData(frm),
                success: function (response) {
                    if (response) {
                        $.notify({
                            message: "Usuario actualizado correctamente"
                        }, {
                            timer: 900,
                            delay: 500
                        });
                        $(".modal").modal("hide");
                    }
                }
            });
        }
    }

    // [ Start Up ]
    $("#frmUsuario").validate({
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);
        },
        highlight: function (element, errorClass) {
            $(element).parent().addClass('has-danger')
            $(element).addClass('form-control-danger')
        }
    })
}

ko.applyBindings(viewModel)