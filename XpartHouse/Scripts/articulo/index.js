var viewModel = new function () {
    // [ Data ]
    var self = this;
    this.articulos = ko.observableArray([]);
    this.familias = ko.observableArray([]);
    this.busqueda = ko.observable("");

    this.busqueda.subscribe(function () {
        setTimeout(self.buscarArticulos, 300);
    })

    // [ Methods ]
    this.buscarArticulos = function () {
        $.get("/Articulo/Buscar?busqueda=" + self.busqueda(), function (data) {
            self.articulos(data);
        })
    }
    this.crearArticulo = function (frm) {
        if ($(frm).valid()) {
            $.ajax({
                url: '/Articulo/Crear',
                method: 'POST',
                contentType: false,
                processData: false,
                cache: false,
                data: new FormData(frm),
                success: function (response) {
                    if (response) {
                        $.notify({
                            message: "Articulo creado correctamente"
                        }, {
                            timer: 900,
                            delay: 500
                        });
                        $(".modal").modal("hide");
                        $("frmArticulo").trigger("reset");
                        self.buscarArticulos();
                    }
                }
            });
        }
    }

    this.cargarFamilias = function () {
        $.get("/Familia/Buscar", function (data) {
            self.familias(data);
        })
    }
    // [ Start Up ]
    this.cargarFamilias();
    this.buscarArticulos();
    $("#frmArticulo").validate({
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);
        },
        highlight: function (element, errorClass) {
            $(element).parent().addClass('has-danger')
            $(element).addClass('form-control-danger')
        }
    });
    $("#modalRegistroArticulo").on('hide.bs.modal', function () {
        $("#frmArticulo").trigger("reset");
    })
}

ko.applyBindings(viewModel);