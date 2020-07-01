using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class AuthorDBRepository: IBookRepository<Author>
    {

        BookStoreDbContext db;
      public  AuthorDBRepository(BookStoreDbContext db)
        {
            this.db = db;
        }
      
        public void Add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Author author = find(id);
            db.Authors.Remove(author);
            db.SaveChanges();

        }

        public Author find(int id)
        {
            return db.Authors.SingleOrDefault(author=>author.Id==id);

        }

        public IList<Author> List()
        {
            return db.Authors.ToList();

        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a=>a.FulName.Contains(term)).ToList();
        }

        public void Update(Author newauthor, int id)
        {

            db.Authors.Update(newauthor);
            db.SaveChanges();

        }
    }
}
