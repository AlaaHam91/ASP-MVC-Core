using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class BookDbRepository : IBookRepository<Book>
    {


        BookStoreDbContext db;
       public BookDbRepository(BookStoreDbContext db)
        {
            this.db = db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            Book book = find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book find(int id)
        {
            return db.Books.Include(a=>a.Author).SingleOrDefault(book => book.Id == id);
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public List<Book> Search(string term)
        {
            var result=db.Books.Include(author=>author.Author).Where(b=>b.Title.Contains(term)
            || b.Description.Contains(term)).ToList();
            return result;
        }

        public void Update(Book newbook, int id)
        {
            db.Books.Update(newbook);
            db.SaveChanges();
           
        }
    }
}
