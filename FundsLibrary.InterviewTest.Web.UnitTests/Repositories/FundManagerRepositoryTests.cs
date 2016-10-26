using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Repositories
{
    public class FundManagerRepositoryTests
    {
        [Test]
        public async Task ShouldGetAll()
        {
            //Arrange
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            IEnumerable<FundManager> fundManagers = new[] { new FundManager() };
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<IEnumerable<FundManager>>("api/FundManager/"))
                .Returns(Task.FromResult(fundManagers))
                .Verifiable();
            var repository = new FundManagerRepository(mockServiceClient.Object);

            //Act
            var result = await repository.GetAll();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            mockServiceClient.Verify();
        }

        [Test]
        public async void ShouldGet()
        {
            //Arrange
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            var fundManager = new FundManager();
            var guid = Guid.NewGuid();
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<FundManager>("api/FundManager/" + guid))
                .Returns(Task.FromResult(fundManager))
                .Verifiable();
            var fundManagerModel = new FundManager();
            var repository = new FundManagerRepository(mockServiceClient.Object);

            //Act
            var result = await repository.Get(guid);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(fundManagerModel));
            mockServiceClient.Verify();
        }
    }
}
