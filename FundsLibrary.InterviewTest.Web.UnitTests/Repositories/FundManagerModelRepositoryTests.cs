using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Repositories
{
    public class FundManagerModelRepositoryTests
    {
        [Test]
        public async Task ShouldGetAll()
        {
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            var mockToFundManagerModelMapper = new Mock<IMapper<FundManager, FundManagerModel>>();
            var fundManagers = new[] { new FundManager() }.AsEnumerable();
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<IEnumerable<FundManager>>("api/FundManager/"))
                .Returns(Task.FromResult(fundManagers));
            mockToFundManagerModelMapper
                .Setup(m => m.Map(It.IsAny<FundManager>()))
                .Returns(new FundManagerModel());
            var repository = new FundManagerModelRepository(
                mockServiceClient.Object,
                mockToFundManagerModelMapper.Object);

            var result = await repository.GetAll();

            mockToFundManagerModelMapper.Verify();
            mockServiceClient.Verify();
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async void ShouldGet()
        {
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            var mockToFundManagerModelMapper = new Mock<IMapper<FundManager, FundManagerModel>>();
            var fundManager = new FundManager();
            var guid = Guid.NewGuid();
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<FundManager>("api/FundManager/" + guid))
                .Returns(Task.FromResult(fundManager));
            var fundManagerModel = new FundManagerModel();
            mockToFundManagerModelMapper
                .Setup(m => m.Map(It.IsAny<FundManager>()))
                .Returns(fundManagerModel);
            var repository = new FundManagerModelRepository(
                mockServiceClient.Object,
                mockToFundManagerModelMapper.Object);

            var result = await repository.Get(guid);

            mockToFundManagerModelMapper.Verify();
            mockServiceClient.Verify();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(fundManagerModel));
        }
    }
}
