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
        public ICollection<Book> Books { get; set; } = new List<Book>();

        // EF Core için boş constructor
        private Category() { }

        // Ana constructor – kurallar burada uygulanabilir
        public Category(string name)
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
    }
}
