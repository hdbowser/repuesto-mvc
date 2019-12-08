var viewModel = new function () {
    // [ Data ]
    var self = this;

    // [ Methods ]
    this.actualizarContacto = function (frm) {
        if ($("#frmContacto").valid()) {

            $.ajax({
                url: '/Contacto/Actualizar',
                method: 'POST',
                contentType: false,
                processData: false,
                cache: false,
                data: new FormData(frm),
                success: function (response) {
                    if (response) {
                        $.notify({
                            message: "Contacto actualizado correctamente"
                        }, {
                            timer: 900,
                            delay: 500
                        });
                        $(".modal").modal("hide");
                    }
                }
            });
        }
    }// actualizarUsuario()

    // [ Start Up ]
    $("#frmContacto").validate({
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