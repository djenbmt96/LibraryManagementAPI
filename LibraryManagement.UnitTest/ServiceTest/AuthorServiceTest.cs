using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryManagement.Data;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.Services.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LibraryManagement.UnitTest.ServiceTest
{
    [TestClass]
    public class AuthorServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseService<Author> _service;
        public AuthorServiceTest()
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<LibraryManagementContext>()
                            .UseSqlServer(config.GetConnectionString("LibraryDB"));
            var context = new LibraryManagementContext(optionsBuilder.Options);
            _unitOfWork = new UnitOfWork(context);
            _service = new AuthorService(_unitOfWork);
            InitTestData();
        }
        private void InitTestData()
        {
            var result = _service.GetAll().Result;
            if(result.Data.Count() == 0)
            {
                _service.Insert(new Author
                {
                    Name = "Author Test 1",
                    YearOfBirth = "1997"
                }).Wait();
                _service.Insert(new Author
                {
                    Name = "Author Test 2",
                    YearOfBirth = "1998"
                }).Wait();
                _service.Insert(new Author
                {
                    Name = "Author Test 3",
                    YearOfBirth = "1999"
                }).Wait();
            }
        }
        [TestMethod]
        public void TestInsert()
        {
            var author = new Author
            {
                Name = "Author_Unit_Test_xxx",
                YearOfBirth = "1997"
            };
            _service.Insert(author).Wait();
            var Result = _service.GetByPageAsync(1, 1, "Author_Unit_Test_xxx").Result;
            var authorResult = Result.Data.FirstOrDefault();
            Assert.AreEqual(authorResult.Name, author.Name);
            Assert.AreEqual(authorResult.YearOfBirth, author.YearOfBirth);
            _service.Delete(author).Wait();
        }
        [TestMethod]
        public void TestUpdate()
        {
            var result = _service.GetByPageAsync(1, 1, "Author Test 1").Result;
            var authorFromDb = result.Data.FirstOrDefault();
            authorFromDb.Name = "Author Test 1 Updated";
            _service.Update(authorFromDb).Wait();
            var resultUpdated = _service.GetById(authorFromDb.Id).Result;
            var authorUpdated = resultUpdated.Data;
            Assert.AreEqual(authorUpdated.Name, "Author Test 1 Updated");
            authorUpdated.Name = "Author Test 1";
            _service.Update(authorUpdated).Wait();
        }
        [TestMethod]
        public void TestDelete()
        {
            var result = _service.GetByPageAsync(1, 1, "Author Test 3").Result;
            var authorFromDb = result.Data.FirstOrDefault();
            _service.Delete(authorFromDb).Wait();
            var resultDeleted = _service.GetById(authorFromDb.Id).Result;
            var authorDeleted = resultDeleted.Data;
            Assert.IsNull(authorDeleted);
            _service.Insert(new Author {
                Name = authorFromDb.Name,
                YearOfBirth = authorFromDb.YearOfBirth
            }).Wait();
        }
        [TestMethod]
        public void TestReturnAll()
        {
            var entities = _service.GetAll();
            var data = entities.Result;
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void TestReturnById()
        {
            var result = _service.GetAll().Result;
            var author = result.Data.FirstOrDefault();
            var resultFromDb = _service.GetById(author.Id).Result;
            var authorFromDb = resultFromDb.Data;
            Assert.IsNotNull(authorFromDb);
        }
        [TestMethod]
        public void TestReturnByPage()
        {
            var entities = _service.GetByPageAsync(1,10).Result;
            var data = entities.Data;
            Assert.IsNotNull(data);
        }
    }
}
