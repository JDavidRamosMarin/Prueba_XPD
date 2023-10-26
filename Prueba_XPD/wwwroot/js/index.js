const _Peliculas = {
    IdPelicula: 0,
    Titulo: "",
    IdGenero: 0,
    Director: "",
    FechaEstreno: "",
    Idioma: "",
    Estatus: 1,
    IdBoleto: 0
}

function MostrarPeliculas() {

    fetch("/Home/ListaPeliculasActGen")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaPeliculas tbody").html("");


                responseJson.forEach((peliculas) => {
                    $("#tablaPeliculas tbody").append(
                        $("<tr>").append(
                            $("<td>").text(peliculas.Titulo),
                            $("<td>").text(peliculas.IdGenero.nombre),
                            $("<td>").text(peliculas.Director),
                            $("<td>").text(peliculas.FechaEstreno),
                            $("<td>").text(peliculas.Idioma),
                            $("<td>").text(peliculas.Estatus),
                            $("<td>").text(peliculas.IdBoleto.precio),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-Pelicula").text("Editar").data("dataPelicula", peliculas)
                            )
                        )
                    )
                })

            }


        })


}

document.addEventListener("DOMContentLoaded", function () {

    MostrarPeliculas();

    fetch("/Home/ListaGenero")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {

            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#idDescripcion-peliculas").append(
                        $("<option>").val(item.IdGenero).text(item.nombre)
                    )

                })
            }

        })

    $("#txtFechaEstreno").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    })


}, false)

function MostrarModal() {

    $("#txtTituloPelicula").val(_Peliculas.Titulo);
    $("#idDescripcion-peliculas").val(_Peliculas.IdGenero == 0 ? $("#idDescripcion-peliculas option:first").val() : _Peliculas.IdGenero)
    $("#txtDirector").val(_Peliculas.Director);
    $("#txtFechaEstreno").val(_Peliculas.FechaEstreno);
    $("#txtIdioma").val(_Peliculas.Idioma);
    $("#txtEstatus").val(_Peliculas.Estatus);
    $("idPrecio-peliculas").val(_Peliculas.IdBoleto == 0 ? $("#idPrecio-peliculas option:first").val() : _Peliculas.IdBoleto)
    $("#PeliculasModal").modal("show");

}

$(document).on("click", ".btn-nueva-venta", function () {

    _Peliculas.IdPelicula = 0;
    _Peliculas.Titulo = "";
    _Peliculas.IdGenero = 0;
    _Peliculas.Director = "";
    _Peliculas.FechaEstreno = "";
    _Peliculas.Idioma = "";
    _Peliculas.Estatus = 0;
    _Peliculas.IdBoleto = 0;

    MostrarModal();

})

$(document).on("click", ".boton-editar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");


    _modeloEmpleado.idEmpleado = _empleado.idEmpleado;
    _modeloEmpleado.nombreCompleto = _empleado.nombreCompleto;
    _modeloEmpleado.idDepartamento = _empleado.refDepartamento.idDepartamento;
    _modeloEmpleado.sueldo = _empleado.sueldo;
    _modeloEmpleado.fechaContrato = _empleado.fechaContrato;

    MostrarModal();

})

$(document).on("click", ".btn-Agregar-Pelicula", function () {

    const modelo = {
        IdPelicula: _Peliculas.IdPelicula,
        Titulo: $("#txtTituloPelicula").val(),
        refGenero: {
            IdGenero: $("#idDescripcion-peliculas").val()
        },
        Director: $("#txtDirector").val(),
        FechaEstreno: $("#txtFechaEstreno").val(),
        Idioma: $("#txtIdioma").val(),
        Estatus: $("#txtEstatus").val(),
        refboleto: {
            IdBoleto: $("#idPrecio-peliculas").val()
        }
    }


    if (_Peliculas.IdPelicula == 0) {

        fetch("/Home/InsertPelicula", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#PeliculasModal").modal("hide");
                    Swal.fire("Listo!", "La pelicula fue creada", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Home/ActualPelicula", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#PeliculasModal").modal("hide");
                    Swal.fire("Listo!", "La pelicula fue actualizada", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})
