using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Core.Entities;
using BookStore.Core.ValueObjects.BookStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();

        // Book → DTO (Display için)
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => new AuthorDto
            {
                FirstName = src.Author.FirstName,
                LastName = src.Author.LastName
            }))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new PriceDto
            {
                Amount = src.Price.Amount,
                Currency = src.Price.Currency
            }))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

    }

}


