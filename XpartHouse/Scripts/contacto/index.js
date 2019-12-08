var viewModel = new function () {
    // [ Data ]
    var self = this;
    this.busqueda = ko.observable("");
    this.contactos = ko.observableArray([]);


    this.busqueda.subscribe(function () {
        setTimeout(self.buscarContactos, 500)
    })


    // [ Methods ]
    this.crearContacto = function (frm) {
        if ($(frm).valid()) {
            $.ajax({
                url: "/Contacto/Crear",
                method: 'POST',
                processData: false,
                contentType: false,
                cache: false,
                data: new FormData(frm),
                success: function (response) {
                    console.log(response);
                    if (response) {
                        $.notify({
                            message: "Contacto creado correctamente"
                        }, {
                            timer: 900,
                            delay: 500
                        });
                        $(".modal").modal("hide");
                        $("frmContacto").trigger("reset");
                        self.buscarContactos();
                    }
                }

            })
        }
    }// crearContacto()

    this.buscarContactos = function (event) {
        $.get("/Contacto/Buscar?busqueda=" + self.busqueda(),
            function (data) {
                self.contactos(data);
            }
        )
    }

    // [ Start Up ]
    this.buscarContactos();
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

ko.applyBindings(viewModel);