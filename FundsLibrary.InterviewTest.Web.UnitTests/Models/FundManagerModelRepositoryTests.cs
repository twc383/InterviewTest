using System;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Client;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Models
{
	public class FundManagerModelRepositoryTests
	{
		[Test]
		public async Task ShouldGetAll()
		{
			var mockServiceClient = new Mock<IServiceClient>();
			var mockToFundManagerModelMapper = new Mock<IMapper<FundManager, FundManagerModel>>();
			var mockFromFundManagerModelMapper = new Mock<IMapper<FundManagerModel, FundManager>>();
			var fundManagers = new[] { new FundManager() }.AsEnumerable();
			mockServiceClient
				.Setup(m => m.GetAll())
				.Returns(Task.FromResult(fundManagers));
			mockToFundManagerModelMapper
				.Setup(m => m.Map(It.IsAny<FundManager>()))
				.Returns(new FundManagerModel());
			var repository = new FundManagerModelRepository(
				mockServiceClient.Object,
				mockToFundManagerModelMapper.Object,
				mockFromFundManagerModelMapper.Object);

			var result = await repository.GetAll();

			mockToFundManagerModelMapper.Verify();
			mockServiceClient.Verify();
			Assert.That(result.Count(), Is.EqualTo(1));
		}

		[Test]
		public async void ShouldGet()
		{
			var mockServiceClient = new Mock<IServiceClient>();
			var mockToFundManagerModelMapper = new Mock<IMapper<FundManager, FundManagerModel>>();
			var mockFromFundManagerModelMapper = new Mock<IMapper<FundManagerModel, FundManager>>();
			var fundManager = new FundManager();
			var guid = Guid.NewGuid();
			mockServiceClient
				.Setup(m => m.GetById(guid))
				.Returns(Task.FromResult(fundManager));
			var fundManagerModel = new FundManagerModel();
			mockToFundManagerModelMapper
				.Setup(m => m.Map(It.IsAny<FundManager>()))
				.Returns(fundManagerModel);
			var repository = new FundManagerModelRepository(
				mockServiceClient.Object,
				mockToFundManagerModelMapper.Object,
				mockFromFundManagerModelMapper.Object);

			var result = await repository.Get(guid);

			mockToFundManagerModelMapper.Verify();
			mockServiceClient.Verify();
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo(fundManagerModel));
		}
	}
}
