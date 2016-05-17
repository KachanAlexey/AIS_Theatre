using AIS_Theatre.Web.Client.ApiClients;
using AIS_Theatre.Web.Client.ApiClients.Abstract;
using AIS_Theatre.Web.Client.Models;
using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AIS_Theatre.Web.Client.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreClient genreClient;

        public GenreController()
        {
            this.genreClient = new GenreClient();
        }

        public async Task<ActionResult> Index()
        {
            var genres = genreClient.GetAllGenres();
            return View(genres);
        }

        public async Task<ActionResult> Create()
        {
            GenreViewModel genreViewModel = new GenreViewModel();
            genreViewModel.Genre = new Genre();
            FillListBoxes(genreViewModel);
            return View(genreViewModel);
        }

        [HttpPost]
        public ActionResult Create(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                genreClient.Create(genreViewModel.Genre);
                return RedirectToAction("Index");
            }
            return View(genreViewModel);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            GenreViewModel genreViewModel = new GenreViewModel();
            genreViewModel.Genre = genreClient.GetGenre(id);
            FillListBoxes(genreViewModel);
            if (genreViewModel.Genre == null)
                return View("NotFound");
            return View(genreViewModel);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var Genre = genreClient.GetGenre(id);
            if (Genre == null)
                return View("NotFound");
            return View(Genre);
        }

        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                genreClient.Edit(genre);
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            genreClient.Delete(id);
            return RedirectToAction("Index");
        }

        private void FillListBoxes(GenreViewModel genreViewModel)
        {
        }
    }
}