using aspnetApi.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using aspnetApi.Models;
using System.Collections.Generic;
using System;
using aspnetApi.Services;
using System.Linq;
using aspnetApi.Models.ViewModels;

namespace aspnetApi.test;

public class BooksServiceTest
{
    private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "BookDbTest")
        .Options;

    AppDbContext context;
    BooksService booksService;

    [OneTimeSetUp]
    public void Setup()
    {
        context = new AppDbContext(dbContextOptions);
        context.Database.EnsureCreated();
        SeedDatabase();

        booksService = new BooksService(context);
    }

    [Test, Order(1)]
    public void GetBooksById_Test()
    {
        var _result = booksService.GetBookById(1);
        Assert.That(_result.Title, Is.EqualTo("Book 1 Title"));
        Assert.That(_result.Description, Is.EqualTo("Book 1 Description"));
        Assert.That(_result.IsRead, Is.EqualTo(false));
        Assert.That(_result.Genre, Is.EqualTo("Genre"));
        Assert.That(_result.CoverUrl, Is.EqualTo("https://..."));
        Assert.That(_result.PublisherName, Is.EqualTo("Publisher 1"));
    }


    [OneTimeTearDown]
    public void CleanUp()
    {
        context.Database.EnsureDeleted();
    }
    private void SeedDatabase()
    {
        var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
        context.publishers.AddRange(publishers);

        var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            context.Books.AddRange(books);

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            context.Book_Authors.AddRange(books_authors);


            context.SaveChanges();
        }
}

