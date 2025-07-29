using BookStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Core.ValueObjects.BookStore.Core.ValueObjects;

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
        public int CategoryId { get; private set; }
        //Public set kullanımı yerine private set tercih ediyoruz çünkü DDD'de Entity'lerin iç durumu dışarıdan kontrolsüzce değişmemeli.
        public string Title { get;  private set; }
        public Author Author { get;  private set; }
        public Price Price { get;  private set; }

        // Category entitysi ile ilişki  
        public Category Category { get; private set; }


        // EF Core için parameterless constructor - protected yaparak sadece persistence layer'ın erişimini sağlıyoruz
        protected Book() { }

        
        // Factory method
        public static Book Create(string title, Author author, Price price, Category category)
        {
            return new Book(title, author, price, category);
        }
        
        // DDD'de Entity'lerin oluşturulması için genellikle bir constructor kullanılır.
        private Book(string title, Author author, Price price, Category category)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Kitap başlığı boş olamaz.");
            
            Title = title;
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Price = price ?? throw new ArgumentNullException(nameof(price));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            CategoryId = category.Id;
        }

        public void UpdatePrice(Price newPrice)
        {
            Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
        }

        public void ChangeCategory(Category newCategory)
        {
            Category = newCategory ?? throw new ArgumentNullException(nameof(newCategory));
            CategoryId = newCategory.Id;
        }

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Kitap başlığı boş olamaz.");
            
            Title = newTitle;
        }
    }
}
