var viewModel = new function () {
    // [ Data ]
    var self = this;
    this.busquedaProveedor = ko.observable("");
    this.busquedaArticulo = ko.observable("");
    this.proveedores = ko.observableArray([]);
    this.articulos = ko.observableArray([]);
    this.referencia = ko.observable("");
    this.idCompra = ko.observable($("#txtIdCompra").val() || 0);
    this.items = ko.observableArray([]);

    this.proveedor = ko.observable({
        IdContacto: 0,
        Nombre: "",
        Apellidos: ""
    });

    this.articuloActual = ko.observable({
        IdArticulo: 0,
        Descripcion: "",
        Precio: 0
    });



    this.busquedaProveedor.subscribe(function () {
        setTimeout(self.buscarProveedores, 400);
    });

    this.busquedaArticulo.subscribe(function () {
        setTimeout(self.buscarArticulos, 400);
    });

    // [ Methods ]
    this.buscarProveedores = function () {
        $.get("/Contacto/BuscarProveedores?busqueda=" + self.busquedaProveedor(), function (data) {
            self.proveedores(data);
        });
    }
    this.buscarArticulos = function () {
        $.get("/Articulo/Buscar?busqueda=" + self.busquedaArticulo(), function (data) {
            self.articulos(data);
        });
    }

    this.escogerProveedor = function (item) {
        self.proveedor(item);
        $(".modal").modal("hide");
    }

    this.escogerArticulo = function (item) {
        self.articuloActual(item)
        $(".modal").modal("hide");
        $("#txtPrecio").val(item.Precio);
        $("#txtCantidad").val("1");
        $("#txtDescuento").val("0");
    }

    this.crearCompra = function (frm) {
        if (self.idCompra() == 0) {
            $.ajax({
                url: '/Compra/Crear',
                method: 'POST',
                contentType: false,
                processData: false,
                cache: false,
                data: new FormData(frm),
                success: function (response) {
                    console.log(response);
                    if (response > 0) {
                        $.notify({
                            message: "Se ha creado el registro"
                        }, {
                            timer: 900,
                            delay: 500
                        });
                        self.idCompra(response)
                    }
                }
            });
        }
    }

    this.detalle = function () {
        if (self.idCompra() != '0') {
            $.get("/Compra/Detalle?id=" + self.idCompra(), function (data) {
                if (data.IdCompra) {
                    self.proveedor(data.Proveedor)
                    self.referencia(data.Referencia);
                }
            })
        }
    }
    this.cargarItems = function () {
        if (self.idCompra() != '0') {
            $.get("/Compra/Items?idCompra=" + self.idCompra(), function (data) {
                self.items(data);
            })
        }
    }


    this.agregarArticulo = function (_frm) {
        let frm = new FormData(_frm)
        frm.append("IdCompra", self.idCompra());
        $.ajax({
            url: '/Compra/AgregarItem',
            method: 'POST',
            contentType: false,
            processData: false,
            cache: false,
            data: frm,
            success: function (response) {
                if (response) {
                    $.notify({
                        message: "Se ha agregado el registro"
                    }, {
                        timer: 900,
                        delay: 500
                    });
                    self.cargarItems();
                }
            }
        });
    }

    // [ Start Up ]
    this.buscarProveedores();
    this.buscarArticulos();
    this.cargarItems();
    this.detalle();
}

ko.applyBindings(viewModel);