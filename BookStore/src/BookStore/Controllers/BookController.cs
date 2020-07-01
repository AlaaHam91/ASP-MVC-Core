using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Repositories;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BookStore.Controllers
{

    public class BookController : Controller
    {
        private readonly IBookRepository<Book> bookRepository;
        private readonly IBookRepository<Author> authorRepository;
        private IHostingEnvironment hosting;

        // GET: /<controller>/

        public BookController(IBookRepository<Book> bookRepository, IBookRepository<Author> authorRepository,IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);

        }

        public ActionResult Edit(int id)
        {

            Book book = bookRepository.find(id);
            var authorId=book.Author==null?book.Author.Id = 0 : book.Author.Id;
            var bookAuthor = new BookAuthor {
                Id=book.Id,
                Title=book.Title,
                Description=book.Description,
                Authors=authorRepository.List().ToList(),
                AuthorId=authorId,
                ImageUrl=book.ImgUrl
            };
            return View(bookAuthor);
        }
        [HttpPost]
        public ActionResult Edit(BookAuthor bookAuthor)
        {
            try
            {
                string newImg = UploadFile(bookAuthor.File,bookAuthor.ImageUrl);
         


                var authors = authorRepository.find(bookAuthor.AuthorId);
                var book = new Book {
                    Title=bookAuthor.Title,
                    Description=bookAuthor.Description,
                    Author=authors,
                    ImgUrl= newImg,
                    Id=bookAuthor.Id
                };
                bookRepository.Update(book,bookAuthor.Id);



                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        public ActionResult Create()
        {
            var bookAuthor = new BookAuthor {
                Authors = FillAuthors()
            };
            return View(bookAuthor);
        }

        [HttpPost]
        public ActionResult Create(BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    string fileName = UploadFile(bookAuthor.File) ?? string.Empty;          
                    if (bookAuthor.AuthorId == -1)
                    {
                        ViewBag.msg = "Please select Author";
                        return View(GetAllAuthors());
                    }
                    Book book = new Book
                    {
                        Description = bookAuthor.Description,
                        Title = bookAuthor.Title,
                        Id = bookAuthor.Id,
                        Author = authorRepository.find(bookAuthor.AuthorId),
                        ImgUrl = fileName
                    };

                    bookRepository.Add(book);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "You have to fill required feild");
                return View(GetAllAuthors());
            }
        }
        public ActionResult Delete(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }
        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View();
            }
        }

        public List<Author> FillAuthors()
        {
            
            var authors= authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FulName = "--Plese select Author--" });
            return authors;
        }
        BookAuthor GetAllAuthors()
        {
            var model = new BookAuthor
            {
                Authors = FillAuthors()
            };
            return model;
        }
        string UploadFile(IFormFile File)
        {
            if (File != null)
            {
                string uploadPath = Path.Combine(hosting.WebRootPath, "uploads");
                string filePath = Path.Combine(uploadPath, File.FileName);
                File.CopyTo(new FileStream(filePath, FileMode.Create));
                return File.FileName;
            }
            return null;
        }
        string UploadFile(IFormFile File,string ImgUrl)
        {
            if (File != null)
            {
                string uploadPath = Path.Combine(hosting.WebRootPath, "uploads");
                string currentPath = Path.Combine(uploadPath, ImgUrl);
                string newPath = Path.Combine(uploadPath, File.FileName);

                if (ImgUrl != File.FileName)
                {
                    System.IO.File.Delete(currentPath);

                    //add the new image
                    File.CopyTo(new FileStream(newPath, FileMode.Create));


                }
                return File.FileName;

            }
            return null;
        }

        public ActionResult Search(string term)
        {
            return View("Index", bookRepository.Search(term));
        }

    }

}
