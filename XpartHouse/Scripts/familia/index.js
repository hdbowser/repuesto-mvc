var viewModel = new function () {
    // [ Data ]
    var self = this;
    this.busqueda = ko.observable("");
    this.familias = ko.observableArray([]);

    // [ Methods ]
    this.buscar = function () {
        $.get("/Familia/Buscar", function (data) {
            self.familias(data);
        });
    }

    // [ Start Up ] 
    this.buscar();
}

ko.applyBindings(viewModel);