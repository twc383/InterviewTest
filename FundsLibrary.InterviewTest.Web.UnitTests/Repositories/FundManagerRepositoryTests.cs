using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;
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
            var mockToFundManagerModelMapper = new Mock<IMapper<FundManagerDto, FundManager>>();
            var fundManagers = new[] { new FundManagerDto() }.AsEnumerable();
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<IEnumerable<FundManagerDto>>("api/FundManager/"))
                .Returns(Task.FromResult(fundManagers))
				.Verifiable();
            mockToFundManagerModelMapper
                .Setup(m => m.Map(It.IsAny<FundManagerDto>()))
                .Returns(new FundManager())
				.Verifiable();
            var repository = new FundManagerRepository(
                mockServiceClient.Object,
                mockToFundManagerModelMapper.Object);

			//Act
            var result = await repository.GetAll();

			//Assert
			Assert.That(result.Count(), Is.EqualTo(1));
			mockServiceClient.Verify();
			mockToFundManagerModelMapper.Verify();
        }

        [Test]
        public async void ShouldGet()
        {
			//Arrange
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            var mockToFundManagerModelMapper = new Mock<IMapper<FundManagerDto, FundManager>>();
            var fundManager = new FundManagerDto();
            var guid = Guid.NewGuid();
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<FundManagerDto>("api/FundManager/" + guid))
                .Returns(Task.FromResult(fundManager))
				.Verifiable();
            var fundManagerModel = new FundManager();
            mockToFundManagerModelMapper
                .Setup(m => m.Map(It.IsAny<FundManagerDto>()))
                .Returns(fundManagerModel)
				.Verifiable();
            var repository = new FundManagerRepository(
                mockServiceClient.Object,
                mockToFundManagerModelMapper.Object);

			//Act
            var result = await repository.Get(guid);

			//Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(fundManagerModel));
			mockToFundManagerModelMapper.Verify();
			mockServiceClient.Verify();
		}
    }
}
