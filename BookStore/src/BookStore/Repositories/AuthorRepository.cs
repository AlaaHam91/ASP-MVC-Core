using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class AuthorRepository : IBookRepository<Author>
    {
        IList<Author> authors;
        public AuthorRepository() {
            authors = new List<Author>() {


                new Author { Id=1,FulName="Alaa Hameed"},
                new Author { Id=2,FulName="Emad Amro"},
                new Author { Id=3,FulName="Ali Fahad"}

            };
        }
        public void Add(Author author)
        {
            author.Id = authors.Max(a => a.Id) + 1;
            authors.Add(author);
        }

        public void Delete(int id)
        {
            Author author = find(id);
            authors.Remove(author);
        }

        public Author find(int id)
        {
            return authors.SingleOrDefault(a => a.Id == id);
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a=>a.FulName.Contains(term)).ToList();
        }

        public void Update(Author newauthor, int id)
        {
            Author author = find(id);
            author.FulName = newauthor.FulName;
        }
    }
}
