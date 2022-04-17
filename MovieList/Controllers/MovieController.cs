using Microsoft.AspNetCore.Mvc;
using MovieList.Data;
using MovieList.Models;
using System.Linq;

namespace MovieList.Controllers
{
    public class MovieController : Controller
    {
        private MovieContext _context { get; set; }

        public MovieController(MovieContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = _context.Genres.OrderBy(g => g.Name).ToList();
            return View("Edit", new Movie());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = _context.Genres.OrderBy(g => g.Name).ToList();
            var movie = _context.Movies.Find(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.MovieId == 0) 
                    _context.Movies.Add(movie);
                else
                    _context.Movies.Update(movie);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.Action = (movie.MovieId == 0) ? "Add" : "Edit";
                ViewBag.Genres = _context.Genres.OrderBy(g => g.Name).ToList();
                return View(movie);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
