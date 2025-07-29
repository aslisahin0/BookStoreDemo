using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    //Constuctor'ı entity içinde tanımlıyoruz
    //Çünkü bir entity’nin doğru ve eksiksiz şekilde oluşturulmasını garanti altına almak için constructor kullanılır.
    //Bu, DDD (Domain-Driven Design) prensiplerine uygun bir yaklaşımdır.
    public class Category
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }

        //Book entitysi ile ilişki
        public ICollection<Book> Books { get; private set; } = new List<Book>();

        // EF Core için boş constructor
        protected Category() { }
        
        // Factory method
        public static Category Create(string name)
        {
            return new Category(name);
        }

        // Ana constructor – kurallar burada uygulanabilir
        private Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Kategori adı boş olamaz.");
            
            Name = name;
        }

        // Kategori adı değiştirme metodu
        //Değişiklikler kontrollü yapılır (iş kuralı uygulanabilir)
        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Kategori adı boş olamaz.");
            
            Name = newName;
        }

        public bool HasBooks()
        {
            return Books.Any();
        }
    }
}
