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
    class PublisherControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName: "BookDbControllerTest")
      .Options;

        AppDbContext context;
        PublishersService publishersService;
        PublishersController publisherController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();

            publishersService = new PublishersService(context);
            publisherController = new PublishersController(publishersService);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void HTTP_GET_GetAllPublisher_Sort_SearchString_ReturnOk_Test()
        {
            IActionResult actionResult = publisherController.GetAllPublisher("name_desc", "Publisher");
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>; //Value is a list of publisher
            Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(6));
            Assert.That(actionResultData.Count, Is.EqualTo(6));
        }

        [Test, Order(2)]
        public void HTTP_GET_GetPublisherById_ReturnOk()
        {
            IActionResult actionResult = publisherController.GetPublisherById(1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            var publisherData = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(publisherData.Id, Is.EqualTo(1));
            Assert.That(publisherData.Name, Is.EqualTo("Publisher 1").IgnoreCase);
        }

        [Test, Order(3)]
        public void HTTP_GET_GetPublisherById_Return_NotFound()
        {
            IActionResult actionResult = publisherController.GetPublisherById(99);
            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTP_POST_AddPublisher_CreatedResult()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "New Publisher"
            };
            IActionResult actionResult = publisherController.AddPublisher(newPublisher);
            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(5)]
        public void HTTP_DELETE_DeletePublisherById_ReturnOk()
        {
            IActionResult actionResult = publisherController.DeletePublisherById(1);
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(6)]
        public void HTTP_DELETE_DeletePublisherById_ReturnBadRequest()
        {
            IActionResult actionResult = publisherController.DeletePublisherById(99);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
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

            context.SaveChanges();
        }
    }
}