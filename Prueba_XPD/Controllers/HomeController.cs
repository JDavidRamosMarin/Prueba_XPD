using Microsoft.AspNetCore.Mvc;
using Prueba_XPD.Models;
using Prueba_XPD.Repositorio.Contrato;
using Prueba_XPD.Repositorio.Implementacion;
using System.Diagnostics;

namespace Prueba_XPD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Peliculas> peliculasRepository;
        private readonly IPeliculas<Peliculas> peliculasRepositoryii;
        private readonly IGenericRepository<Boleto> boletoRepository;
        private readonly IGenericRepository<Genero> generoRepository;
        private readonly IGenericRepository<Ventas> ventasRepository;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Peliculas> PeliculasRepository,
            IPeliculas<Peliculas> PeliculasRepositoryii,
            IGenericRepository<Boleto> BoletoRepository,
            IGenericRepository<Genero> GeneroRepository,
            IGenericRepository<Ventas> VentasRepository)
        {
            _logger = logger;
            peliculasRepository = PeliculasRepository;
            peliculasRepositoryii = PeliculasRepositoryii;
            boletoRepository = BoletoRepository;
            generoRepository = GeneroRepository;
            ventasRepository = VentasRepository;
        }

        // ENDOPOINTS PARA PELICULAS


        // Devuelve una lista de las peliculas con su genero y que estan activas 
        [HttpGet]
        public async Task<IActionResult> ListaPeliculasActGen()
        {
            List<Peliculas> lista = await peliculasRepository.GetAll();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // Devuelve un valor int el cual representa el numero de peliculas activas
        [HttpGet]
        public int PeliculasActivas()
        {
            int conteo = peliculasRepositoryii.GetCountActiveMovies();
            return conteo;
        }

        // Devuelve un listado de peliculas con su precio
        [HttpGet]
        public async Task<IActionResult> ListaPeliculasPrecio()
        {
            List<Peliculas> lista = await peliculasRepositoryii.GetMoviesPrice();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPelicula([FromBody] Peliculas modelo)
        {
            bool resultado = await peliculasRepository.Post(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }

        [HttpPut]
        public async Task<IActionResult> ActualPelicula([FromBody] Peliculas modelo)
        {
            bool resultado = await peliculasRepository.Update(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }

        // ENDPOINTS PARA GENERO
        [HttpGet]
        public async Task<IActionResult> ListaGenero()
        {
            List<Genero> lista = await generoRepository.GetAll();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> InsertGenero([FromBody] Genero modelo)
        {
            bool resultado = await generoRepository.Post(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }

        [HttpPut]
        public async Task<IActionResult> ActualGenero([FromBody] Genero modelo)
        {
            bool resultado = await generoRepository.Update(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }

        // ENDPOINTS OARA BOLETO
        [HttpGet]
        public async Task<IActionResult> ListaBoleto()
        {
            List<Boleto> lista = await boletoRepository.GetAll();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> InsertBoleto([FromBody] Boleto modelo)
        {
            bool resultado = await boletoRepository.Post(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }

        [HttpPut]
        public async Task<IActionResult> ActualBoleto([FromBody] Boleto modelo)
        {
            bool resultado = await boletoRepository.Update(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }

        // ENDPOINTS OARA VENTAS
        [HttpGet]
        public async Task<IActionResult> ListaVentas()
        {
            List<Ventas> lista = await ventasRepository.GetAll();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> InsertVentas([FromBody] Ventas modelo)
        {
            bool resultado = await ventasRepository.Post(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }

        [HttpPut]
        public async Task<IActionResult> ActualVentas([FromBody] Ventas modelo)
        {
            bool resultado = await ventasRepository.Update(modelo);
            if (resultado) return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            else return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "Error" });
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}