using Microsoft.AspNetCore.Mvc;
using ProyectoLimpioCore.Models;
using ProyectoLimpioCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoLimpioCore.Controllers
{
    public class CochesController : Controller
    {
        IRepositoryCoches Repo;

        public CochesController(IRepositoryCoches repo)
        {
            this.Repo = repo;
        }

        public IActionResult Index()
        {
            List<Coche> coches = this.Repo.GetCoches();
            return View(coches);
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            this.Repo.EliminarCoche(id);
            List<Coche> coches = this.Repo.GetCoches();
            return View(coches);
        }

        public IActionResult Details(int id)
        {
            Coche coche = this.Repo.BuscarCoche(id);
            return View(coche);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Coche c)
        {
            this.Repo.InsertarCoche(c.Modelo, c.Marca, c.Conductor, c.Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Coche coche = this.Repo.BuscarCoche(id);
            return View(coche);
        }

        [HttpPost]
        public IActionResult Edit(Coche coche)
        {
            this.Repo.ModificarCoche(coche);
            return RedirectToAction("Index");
        }

        public IActionResult BuscadorMarca()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BuscadorMarca(String marca)
        {
            List<Coche> coches = this.Repo.BuscarMarca(marca);
            return View(coches);
        }
    }
}
