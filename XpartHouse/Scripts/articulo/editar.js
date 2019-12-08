var viewModel = function () {
    // [ Data ]
    var self = this;

    // [ Methods ]
    this.actualizarArticulo = function (frm) {
        if ($("#frmArticulo").valid()) {

            $.ajax({
                url: '/Articulo/Actualizar',
                method: 'POST',
                contentType: false,
                processData: false,
                cache: false,
                data: new FormData(frm),
                success: function (response) {
                    if (response) {
                        $.notify({
                            message: "Articulo actualizado correctamente"
                        }, {
                            timer: 900,
                            delay: 500,
                            placement: {
                                from: "top",
                                align: "right"
                            },
                        });
                        $(".modal").modal("hide");
                    }
                }
            });
        }
    }// actualizarArticulo()

    // [ Start Up ]
    $("#frmArticulo").validate({
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