using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModel
{
    public class BookAuthor
    {
        public int Id { get; set; }

        [Required, StringLength(40, MinimumLength = 5)]

        public string Title { get; set; }

        [Required, StringLength(100, MinimumLength = 10)]

        public string Description { get; set; }
        public int AuthorId { get; set; }

        public IFormFile File { get; set; }
        public List<Author> Authors { get; set; }

        public string ImageUrl { get; set; }
    }
}
