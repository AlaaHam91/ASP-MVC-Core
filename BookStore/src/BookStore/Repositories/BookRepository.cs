using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository<Book>
    {
        IList<Book> books;
        public BookRepository() {
            books = new List<Book>() {
             new Book { Id=1,Title="ASP Core",Description="Book for ASP core",Author=new Author { Id=1 ,FulName="Alaa"} ,ImgUrl="٢٠١٩٠٩٠٦_١٠١٥١٧.jpg"},
             new Book { Id=2,Title="ASP.Net",Description="Book for ASP.Net MVC",Author=new Author { Id=2} },
             new Book { Id=3,Title="Node Js",Description="Book for Node programing web",Author=new Author { Id=2} },
             new Book { Id=4,Title="Laravel PHP",Description="Book for Laravel",Author=new Author { Id=3} }};
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
           var book= find(id);
            books.Remove(book);
        }

        public Book find(int id)
        {
            return books.SingleOrDefault(book => book.Id == id);
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(b=>b.Title.Contains(term)).ToList() ;
        }

        public void Update(Book newbook,int id)
        {
            var book= find(id);
            book.Title = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImgUrl = newbook.ImgUrl;

        }
    }
}
