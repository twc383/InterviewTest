using System;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Controllers;
using FundsLibrary.InterviewTest.Service.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Service.UnitTests.Controllers
{
	[TestFixture]
	public class FundManagerControllerTests
	{
		[Test]
		public async Task ShouldGet()
		{
			var mock = new Mock<IFundManagerRepository>();
			var controller = new FundManagerController(mock.Object);
			var newGuid = Guid.NewGuid();
			var fundManager = new FundManager();
			mock.Setup(m => m.GetBy(newGuid)).Returns(Task.FromResult(fundManager));
			
			var res = controller.Get(newGuid);

			mock.Verify();
			Assert.That(await res, Is.EqualTo(fundManager));
		}

		[Test]
		public async Task ShouldGetAll()
		{
			var mock = new Mock<IFundManagerRepository>();
			var controller = new FundManagerController(mock.Object);
			var valueFunction = new[] { new FundManager() }.AsQueryable();
			mock.Setup(m => m.GetBy()).Returns(Task.FromResult(valueFunction));

			var res = await controller.Get();

			Assert.That(res.Count(), Is.EqualTo(1));
		}
	}
}