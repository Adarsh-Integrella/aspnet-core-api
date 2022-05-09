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

public class PublishersServiceTest
{
    private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "BookDbTest")
        .Options;

    AppDbContext context;
    PublishersService publishersService;

    [OneTimeSetUp]
    public void Setup()
    {
        context = new AppDbContext(dbContextOptions);
        context.Database.EnsureCreated();
        SeedDatabase();

        publishersService = new PublishersService(context);
    }

    [Test, Order(1)]
    public void GetAllPublisher_WithNoSort_WithNoSearchString()
    {
        var _result = publishersService.GetAllPublishers("", "");
        Assert.That(_result.Count, Is.EqualTo(6));
        Assert.AreEqual(_result.Count, 6);
    }

    [Test, Order(2)]
    public void GetAllPublisher_WithNoSort_WithSearchString()
    {
        var _result = publishersService.GetAllPublishers("", "3");
        Assert.That(_result.Count, Is.EqualTo(1));
        Assert.That(_result.FirstOrDefault().Name, Is.EqualTo("Publisher 3"));
    }

    [Test, Order(3)]
    public void GetAllPublisher_WithSort_WithNoSearchString()
    {
        var _result = publishersService.GetAllPublishers("name_desc", "");
        Assert.That(_result.Count, Is.EqualTo(6));
        Assert.That(_result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));
    }

    [Test, Order(4)]
    public void GetPublisherByIdWithResponse()
    {
        var _result = publishersService.GetPublisherById(1);
        Assert.That(_result.Id, Is.EqualTo(1));
        Assert.That(_result.Name, Is.EqualTo("Publisher 1"));
    }

    [Test, Order(5)]
    public void GetPublisherByIdWithoutResponse()
    {
        var _result = publishersService.GetPublisherById(99);
        Assert.That(_result, Is.Null);
    }

    [Test, Order(6)]
    public void AddPublisher()
    {
        var newPublisher = new PublisherVM()
        {
            Name = "Ramesh"
        };
        var _result = publishersService.AddPublisher(newPublisher);
        Assert.That(_result, Is.Not.Null);
        Assert.That(_result.Id, Is.Not.Null);
    }

    [Test, Order(7)]
    public void GetPublisherData_Test()
    {
        var result = publishersService.GetPublisherData(1);

            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
            Assert.That(result.BookAuthors, Is.Not.Empty);
            Assert.That(result.BookAuthors.Count, Is.GreaterThan(0));

            var firstBookName = result.BookAuthors.OrderBy(n => n.BookName).FirstOrDefault().BookName;
            Assert.That(firstBookName, Is.EqualTo("Book 1 Title"));
    }
    
    [Test, Order(8)]
    public void DeletePublisherById_test()
    {
        publishersService.DeletePublisherById(1);
        var _check = publishersService.GetPublisherById(1);
        Assert.That(_check, Is.Null);
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

