using System;
using aspnetApi.Data;
using aspnetApi.Models;
using aspnetApi.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using aspnetApi.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using aspnetApi.Models.ViewModels;

namespace aspnetApi.test
{
    class AuthorControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName: "BookDbControllerTest")
      .Options;

        AppDbContext context;
        AuthorsService authorsService;
        AuthorsController authorsController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();

            authorsService = new AuthorsService(context);
            authorsController = new AuthorsController(authorsService);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void HTTP_POST_AddAuthor_CreatedResult()
        {
            var newAuthor = new AuthorVM()
            {
                FullName = "New Author"
            };
            IActionResult actionResult = authorsController.AddAuthor(newAuthor);
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(2)]
        public void HTTP_GET_GetAuthorById_ReturnOk()
        {
            IActionResult actionResult = authorsController.GetAuthorById(1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            var authorData = (actionResult as OkObjectResult).Value as Author;
            Assert.That(authorData.Id, Is.EqualTo(1));
            Assert.That(authorData.FullName, Is.EqualTo("Author 1").IgnoreCase);
        }

        [Test, Order(3)]
        public void HTTP_DELETE_DeleteAuthorById_ReturnOk()
        {
            IActionResult actionResult = authorsController.DeleteAuthorById(1);
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

          [Test, Order(4)]
    public void HTTP_GET_GetAuthorWithBook_test()
    {
        var authorId = 2;
        IActionResult actionResult = authorsController.GetAuthorWithBook(authorId);
        Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        // var authorData = (actionResult as OkObjectResult).Value as ;
        // Assert.That(authorData, Is.EqualTo("Author 2"));
        // Assert.That(actionResult.BooksTitle[0], Is.EqualTo("Book 1 Title"));
        // Assert.That(actionResult.BooksTitle[1], Is.EqualTo("Book 2 Title"));
    }

        //In-memory database
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
}