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
			//Arrange
            var mock = new Mock<IFundManagerRepository>();
            var controller = new FundManagerController(mock.Object);
            var newGuid = Guid.NewGuid();
            var fundManager = new FundManagerDto();
            mock.Setup(m => m.GetBy(newGuid))
				.Returns(Task.FromResult(fundManager))
				.Verifiable();

			//Act
            var result = controller.Get(newGuid);

			//Assert
            mock.Verify();
            Assert.That(await result, Is.EqualTo(fundManager));
        }

        [Test]
        public async Task ShouldGetAll()
        {
			//Arrange
            var mock = new Mock<IFundManagerRepository>();
            var controller = new FundManagerController(mock.Object);
            var valueFunction = new[] { new FundManagerDto() }.AsQueryable();
            mock.Setup(m => m.GetAll())
				.Returns(Task.FromResult(valueFunction))
				.Verifiable();

			//Act
            var result = await controller.Get();

			//Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }
    }
}
