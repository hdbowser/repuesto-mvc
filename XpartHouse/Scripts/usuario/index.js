var viewModel = new function () {
    // [ Data ]
    var self = this;
    this.busqueda = ko.observable("");
    this.roles = ko.observableArray([]);
    this.usuarios = ko.observableArray([]);

    this.busqueda.subscribe(function () {
        setTimeout(self.buscarUsuarios, 500)
    })

    // [ Methods ]
    this.cargarRoles = function () {
        $.get("/Usuario/ListadoRoles", function (data) {
            self.roles(data);
        })
    }

    this.crearUsuario = function (frm) {
        $.ajax({
            url: '/Usuario/Crear',
            method: 'POST',
            contentType: false,
            processData: false,
            cache: false,
            data: new FormData(frm),
            success: function (response) {
                if (response) {
                    $.notify({
                        message: "Usuario registrado correctamente"
                    }, {
                        timer: 900,
                        delay: 500
                    });
                    $(".modal").modal("hide");
                    $("frmRegistroUsuario").trigger("reset");
                    self.buscarUsuarios();
                }
            }
        });
    }

    this.buscarUsuarios = function (event) {
        $.get("/Usuario/Buscar?busqueda=" + self.busqueda(),
            function (data) {
                self.usuarios(data);
            }
        )
    }

    // [ Start Up ]
    this.cargarRoles();
    this.buscarUsuarios();


}

ko.applyBindings(viewModel)