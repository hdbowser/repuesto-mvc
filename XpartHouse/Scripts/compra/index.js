var viewModel = new function () {
    // [ Data ]
    var self = this;
    this.busqueda = ko.observable("");
    this.compras = ko.observableArray([]);

    this.busqueda.subscribe(function () {
        setTimeout(self.buscar, 400);
    })

    // [ Methods ]
    this.buscar = function () {
        $.get("/Compra/Buscar?busqueda=" + self.busqueda(), function (data) {
            console.log(data);
            self.compras(data);
        })
    }

    // [ Start Up ]
    this.buscar();
}

ko.applyBindings(viewModel)