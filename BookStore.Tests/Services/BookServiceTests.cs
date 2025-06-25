using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Repository;
using BookStore.Core.Entities;
using BookStore.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBookRepository> _mockBookRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBookRepo = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork.Setup(uow => uow.BookRepository).Returns(_mockBookRepo.Object);
            _bookService = new BookService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedBooks()
        {
            // Arrange
            //metot çağrıldığında örnek bir kitap listesi dönmesini sağlıyoruz (mock data), gerçek DB çağrısı yapılmaz.
            var books = new List<Book> { new Book { Id = 1, Title = "Test Book" } };
            _mockBookRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(books);
            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(It.IsAny<IEnumerable<Book>>())).Returns(new List<BookDto> { new BookDto { Id = 1, Title = "Test Book" } });
            // Act
            var result = await _bookService.GetAllAsync();
            // Assert
            Assert.NotNull(result);  // Sonuç null olmamalı
            Assert.Single(result);  // Sonuçta tek bir kitap olmalı
            Assert.Equal("Test Book", result.First().Title); // Kitabın başlığı "Test Book" olmalı
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedDto()
        {
            // Arrange
            var createDto = new CreateBookDto { Title = "Test Book", Author = "Aslı", CategoryId = 1 };
            var book = new Book { Id = 1, Title = "Test Book", Author = "Aslı", CategoryId = 1 };

            //Örnek bir DTO ve ona karşılık gelen entity nesnesi oluşturduk.
            _mockMapper.Setup(m => m.Map<Book>(createDto)).Returns(book);
            _mockBookRepo.Setup(r => r.AddAsync(book)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            // DTO'yu tekrar mapleyerek geri dönmesini sağlıyoruz.
            _mockMapper.Setup(m => m.Map<CreateBookDto>(book)).Returns(createDto);

            // Act
            var result = await _bookService.CreateAsync(createDto);

            // Assert
            //Geri dönen DTO gönderdiğimiz veriyle eşleşiyor mu bunu test ediyoruz.
            Assert.Equal(createDto.Title, result.Title);
            Assert.Equal(createDto.Author, result.Author);
            _mockBookRepo.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId };
            _mockBookRepo.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);
            // Act
            var result = await _bookService.DeleteAsync(bookId);
            // Assert
            Assert.True(result); // Kitap silindiği için true dönmeli
            _mockBookRepo.Verify(r => r.Delete(book), Times.Once); // Delete metodu bir kez çağrılmalı
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBookDto_WhenBookExist()
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Title = "Test Book" };
            _mockBookRepo.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);
            _mockMapper.Setup(m => m.Map<BookDto>(book)).Returns(new BookDto { Id = bookId, Title = "Test Book" });
            // Act
            var result = await _bookService.GetByIdAsync(bookId);
            // Assert
            Assert.NotNull(result); // Sonuç null olmamalı
            Assert.Equal("Test Book", result.Title); // Kitabın başlığı "Test Book" olmalı
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenBookNotExists()
        {
            // Arrange
            var bookId = 1;
            _mockBookRepo.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((Book)null); // Kitap bulunamadı
            // Act
            var result = await _bookService.DeleteAsync(bookId);
            // Assert
            Assert.False(result); // Kitap silinemediği için false dönmeli
            _mockBookRepo.Verify(r => r.Delete(It.IsAny<Book>()), Times.Never); // Delete metodu çağrılmamalı
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenBookNotFound()
        {
            // Arrange
            var bookId = 1;
            _mockBookRepo.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((Book)null); // Kitap bulunamadı
            // Act
            var result = await _bookService.GetByIdAsync(bookId);
            // Assert
            Assert.Null(result); // Sonuç null olmalı
        }
    }

}
