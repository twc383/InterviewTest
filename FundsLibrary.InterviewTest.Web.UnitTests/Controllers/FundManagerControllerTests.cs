using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Controllers;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Controllers
{
	public class FundManagerControllerTests
	{
		[Test]
		public async void ShouldGetIndexPage()
		{
			var mock = new Mock<IFundManagerModelRepository>();
			var fundManagerModels = new FundManagerModel[0].AsEnumerable();
			mock.Setup(m => m.GetAll()).Returns(Task.FromResult(fundManagerModels));
			var controller = new FundManagerController(mock.Object);

			var result = await controller.Index();
			
			Assert.That(result, Is.TypeOf<ViewResult>());
			mock.Verify();
			Assert.That(((ViewResult)result).Model, Is.EqualTo(fundManagerModels));
		}

		[Test]
		public async void ShouldGetDetailsPage()
		{
			var guid = Guid.NewGuid();
			var mock = new Mock<IFundManagerModelRepository>();
			var fundManagerModel = new FundManagerModel();
			mock.Setup(m => m.Get(guid)).Returns(Task.FromResult(fundManagerModel));
			var controller = new FundManagerController(mock.Object);

			var result = await controller.Details(guid);

			Assert.That(result, Is.TypeOf<ViewResult>());
			mock.Verify();
			Assert.That(((ViewResult)result).Model, Is.EqualTo(fundManagerModel));
		}
	}
}