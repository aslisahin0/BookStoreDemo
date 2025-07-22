using BookStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    //Constuctor'ı entity içinde tanımlıyoruz
    //Çünkü bir entity’nin doğru ve eksiksiz şekilde oluşturulmasını garanti altına almak için constructor kullanılır.
    //Bu, DDD (Domain-Driven Design) prensiplerine uygun bir yaklaşımdır.

    //entity + aggregate root  
    public class Book
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        //Public set kullanımı yerine private set tercih ediyoruz çünkü DDD'de Entity'lerin iç durumu dışarıdan kontrolsüzce değişmemeli.
        public string Title { get; private set; }
        public Author Author { get; private set; }
        public Price Price { get; private set; }

        // Category entitysi ile ilişki  
        public Category Category { get; private set; }

        // Constructor  

        // EF Core için gerekli - dışarıdan kullanılmasını istenmiyorsa private olarak işaretlenebilir.
        private Book() { }

        // DDD'de Entity'lerin oluşturulması için genellikle bir constructor kullanılır.
        public Book(string title, Author author, Price price, Category category)
        {
            Title = title;
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Price = price ?? throw new ArgumentNullException(nameof(price));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            CategoryId = category.Id;
        }

        public void UpdatePrice(Price newPrice)
        {
            if (newPrice is null || newPrice.Amount < 0)
                throw new ArgumentException("Fiyat negatif olamaz.");

            Price = newPrice;
        }

        public void ChangeCategory(Category newCategory)
        {
            Category = newCategory ?? throw new ArgumentNullException(nameof(newCategory));
            CategoryId = newCategory.Id;
        }
    }
}
