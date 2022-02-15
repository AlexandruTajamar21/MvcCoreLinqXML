using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreLinqXML.Controllers
{
    public class PeliculasController : Controller
    {
        private RepositoryPeliculas repo;

        public PeliculasController(RepositoryPeliculas repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Pelicula> pelis = this.repo.GetPeliculas();
            return View(pelis);
        }
        public IActionResult Escenas(int id)
        {
            List<Escena> escenas = this.repo.GetEscenasPelicula(id);
            return View(escenas);
        }

        public IActionResult EscenaPelicula(int idpelicula, int? posicion)
        {
            if(posicion == null)
            {
                posicion = 0;
            }
            int numeroregistros = 0;
            Escena escena = this.repo.GetEscenaPelicula(idpelicula,posicion.Value,ref numeroregistros);
            ViewData["REGISTROS"] = "Escena " +(posicion+1) + numeroregistros;
            int siguiente = posicion.Value + 1;
            if(siguiente >= numeroregistros)
            {
                siguiente = 0;
            }
            int anterior = posicion.Value - 1;
            if(anterior < 0)
            {
                anterior = numeroregistros - 1;
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            return View(escena);
        }
        public IActionResult DetallesPelicula(int idPelicula)
        {
            Pelicula peli = this.repo.GetPeliculaId(idPelicula);
            return View(peli);
        }
    }
}
