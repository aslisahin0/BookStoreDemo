using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs
{
    public class UpdateBookDto
    {
        public string Title { get; set; }

        public AuthorDto Author { get; set; }
        public PriceDto Price { get; set; }

        public int CategoryId { get; set; }
    }

}
