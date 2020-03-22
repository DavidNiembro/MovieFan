using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieFan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieFan.Controllers
{
    public class MovieController : Controller
    {

        readonly MovieFanContext db;

        public MovieController( MovieFanContext db)
        {
            this.db = db;
        }
        // GET: Movie
        public ActionResult Index()
        {
            List<Movies> allmovies = db.Movies.Include(c=>c.Category).Include(r => r.Rating).ToList();
            return View(allmovies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            List<SelectListItem> categories = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name.ToString() }).ToList();
            ViewBag.Categories = categories;
            ViewBag.ratings = db.Ratings.ToList();
            Movies movie = db.Movies.Include(c => c.Category).Include(r => r.Rating).First(m => m.Id == id);
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {

            List<SelectListItem> categories = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name.ToString() }).ToList();
            ViewBag.Categories = categories;
            ViewBag.ratings = db.Ratings.ToList();
            Movies newMovie = new Movies();
            ViewBag.create = true;
            return View("Details", newMovie);
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Movies> Create([Bind("Title,Synopsis,CategoryId,RatingId")] Movies movie)
        {
            try
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                TempData["flashmessage"] = "Film ajouté";
                TempData["flashmessagetype"] = "info";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: Movie/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    //List<SelectListItem> categories = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name.ToString() }).ToList();
        //    //ViewBag.Categories = categories;
        //    //ViewBag.ratings = db.Ratings.ToList();
        //    //Movies movie = db.Movies.Include(c => c.Category).Include(r => r.Rating).First(m => m.Id == id);
        //    return View();
        //}

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Title,Synopsis,CategoryId,RatingId,Picture,ReleaseDate")] Movies movie)
        {
            if (ModelState.IsValid)
                try
                {
                    movie.Id = id;
                    db.Update(movie);
                    db.SaveChanges();
                    TempData["flashmessage"] = "Changement enregistré";
                    TempData["flashmessagetype"] = "info";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["flashmessage"] = "Un problème est survenu";
                    TempData["flashmessagetype"] = "danger";
                    return View("Details", movie);
                }
            else {
                return Details(id);
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                db.Remove(db.Movies.First(m => m.Id == id));
                db.SaveChanges();
                TempData["flashmessage"] = "Film supprimé";
                TempData["flashmessagetype"] = "info";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["flashmessage"] = "Un problème est survenu";
                TempData["flashmessagetype"] = "danger";
                return View();
            }
        }
    }
}