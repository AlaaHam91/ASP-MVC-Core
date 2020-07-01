using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Repositories;
using BookStore.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private IBookRepository<Author> authorRepository;
        // GET: /<controller>/

        public AuthorController(IBookRepository<Author> AuthorRepository)
        {
            this.authorRepository = AuthorRepository;
        }
        public ActionResult Index()
        {
            var authors = authorRepository.List();
            return View(authors);

        }

        public ActionResult Edit(int id)
        {
            var author = authorRepository.find(id);
            return View(author);
        }
        [HttpPost]
        public ActionResult Edit(int id,Author author)
        {
            try
            {
                authorRepository.Update(author, id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var author = authorRepository.find(id);
            return View(author);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            
            try
            {
                authorRepository.Add(author);
                return RedirectToAction("Index");
            }
            catch{
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            Author author = authorRepository.find(id);
            return View(author);
        }
        [HttpPost]
        public ActionResult Delete(int id,Author author)
        {
            try
            {
                authorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View();
            }
        }

    }
}
