using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    public class Category
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }

        //Book entitysi ile ilişki
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
