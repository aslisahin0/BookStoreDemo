using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; } // Kategori adını göstermek için ekledim
    }
}
