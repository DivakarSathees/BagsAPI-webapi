
using BagAPI.Controllers;
using BagAPI.Data;
using BagAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BagAPITestProject

    {
        [TestFixture]
        public class BagsControllerTests
        {
            private BagsController _controller;
            private List<Bag> _bags;
        private ApplicationDbContext _dbContext;

        private void SeedDatabase()
        {
            _dbContext.ChangeTracker.Clear();
            _bags = new List<Bag>
            {
                new Bag { Id = 1, Name = "Backpack", Weight = 2.5, Capacity = 45 },
                new Bag { Id = 2, Name = "ToteBag", Weight = 1.8, Capacity = 30 },
                new Bag { Id = 3, Name = "DuffelBag", Weight = 3.2, Capacity = 50 },
                new Bag { Id = 4, Name = "DuffelBag", Weight = 2.5, Capacity = 50 }
            };


            _dbContext.Bags.AddRange(_bags);
            _dbContext.SaveChanges();
        }


      
            [SetUp]
        public void Setup()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: "TestDatabase")
                   .Options;
            _dbContext = new ApplicationDbContext(options);
            SeedDatabase();
            _dbContext.SaveChanges();

            _dbContext = new ApplicationDbContext(options);

            _controller = new BagsController(_dbContext);
        }

        [Test]
            public async Task GetBags_ReturnsOkStatus()
            {
                // Act
                var result =await  _controller.GetBags();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result.Result);

                var okResult = result.Result as OkObjectResult;
                var bags = okResult.Value as IEnumerable<Bag>;

                Assert.AreEqual(_bags.Count, bags.Count());
            }

        [Test]
        public async Task GetBags_ReturnsBags()
        {
            // Act
            var result = await  _controller.GetBags();
            var okResult = result.Result as OkObjectResult;

           // Assert
            Assert.NotNull(okResult.Value);
          
        }

        [Test]
        public async Task GetBags_ReturnsListOfBagsWhenDataIsPresent()
        {
            // Act
            var result = await _controller.GetBags();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Bag>>>(result);

        }


        [Test]
        public async Task GetBooks_ReturnsCorrectCountOfData()
        {
            // Act
            var actionResult = await _controller.GetBags();
            var okResult = actionResult.Result as OkObjectResult;

            var bags = okResult?.Value as IEnumerable<Bag>;

            Assert.NotNull(bags);

            Assert.That(bags.Count, Is.EqualTo(_bags.Count));
        }

        [Test]
        public async Task GetBagByWeight_ReturnsNotFoundStatus_WhenBagDoesNotExist()
        {


            // Act
            var result = await _controller.GetBagByWeightAsync(5.5);

            // Assert
           var notFoundResult= result as NotFoundResult;
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);


        }

        [Test]
        public async Task GetBagByWeight_ReturnsOk_WhenBagWithGivenWeightExist()
        {


            // Act
            var result = await _controller.GetBagByWeightAsync(2.5);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);


        }
        [Test]
        public async Task GetBagByWeight_ReturnsBag_WhenBagWithGivenWeightExist()
        {


            // Act
            var result = await _controller.GetBagByWeightAsync(2.5) as OkObjectResult;
            var data = result.Value as Bag;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bag>(data);


        }

        [Test]
        public async Task GetBagByWeight_ReturnstheFirstOccuranceBag_WhenBagWithGivenWeightExist()
        {


            // Act
            var result = await _controller.GetBagByWeightAsync(2.5) as OkObjectResult;
            var data = result.Value as Bag;
            var actual = _bags.FirstOrDefault(b => b.Weight == 2.5);
            // Assert
            Assert.IsNotNull(result);
            Assert.That(actual.Name, Is.EqualTo(data.Name));

        }

        [Test]
        public async Task SearchBag_ReturnsOk_WhenBagWithGivenNameExist()
        {


            // Act
            var result = await _controller.SearchBags("Back") ;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);


        }

        [Test]
        public async Task SearchBag_ReturnsNotFound_WhenBagWithGivenNameDoesNotExist()
        {


            // Act
            var result = await _controller.SearchBags("Trolley");
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);



        }

        [Test]
        public async Task SearchBag_ReturnsBag_WhenBagWithGivenNameExist()
        {


            // Act
            var result = await _controller.SearchBags("Back");
            var okResult = result.Result as OkObjectResult;



            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Bag>>(okResult.Value);
          

        }

        [Test]
        public async Task SearchBag_ReturnsBagWithCorrectCount_WhenBagWithGivenNameExist()
        {
            // Act
            var result = await _controller.SearchBags("DuffelBag");
            var okResult = result.Result as OkObjectResult;
            var bags = (List<Bag>)okResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Bag>>(okResult.Value);
            Assert.AreEqual(bags.Count, 2);

        }
        [Test]
        public async Task SearchBag_ReturnsBagWithCorrectType_WhenBagWithGivenNameExist()
        {
            // Act
            var result = await _controller.SearchBags("DuffelBag");
            var okResult = result.Result as OkObjectResult;
          


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Bag>>>(result);
     
        }

        [Test]
        public async Task SearchBag_ReturnsBagForPartialMatch_WhenBagWithGivenNameExist()
        {
            // Act
            var result = await _controller.SearchBags("Duffel");
            var okResult = result.Result as OkObjectResult;
            var bags = (List<Bag>)okResult.Value;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bags.Count, 2);

        }

        [Test]
        public async Task SearchBag_Returns_Data_Case_InsensitiveSearching_WhenBagWithGivenNameExist()
        {
            // Act
            var result = await _controller.SearchBags("duffel");
            var okResult = result.Result as OkObjectResult;
            var bags = (List<Bag>)okResult.Value;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bags.Count, 2);

        }


        [TearDown]
        public void TearDown()
        {
            // Dispose the in-memory database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();

        }
    }
    }


