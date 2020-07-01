using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {

        public int Id { get; set; }

        [Required,StringLength(40,MinimumLength =5)]
        
        public string Title { get; set; }

        [Required, StringLength(100, MinimumLength = 10)]

        public string Description { get; set; }

        public string ImgUrl { get; set; }
        public Author Author { get; set; }
    }
}
