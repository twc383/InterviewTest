using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Controllers;
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
            //Arrange
            var mock = new Mock<IFundManagerRepository>();
            IEnumerable<FundManager> fundManagerModels = new FundManager[0];
            mock.Setup(m => m.GetAll())
                .Returns(Task.FromResult(fundManagerModels))
                .Verifiable();
            var controller = new FundManagerController(mock.Object);

            //Act
            var result = await controller.Index();

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult)result).Model, Is.EqualTo(fundManagerModels));
            mock.Verify();
        }

        [Test]
        public async void ShouldGetDetailsPage()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.Setup(m => m.Get(guid))
                .Returns(Task.FromResult(fundManagerModel))
                .Verifiable();
            var controller = new FundManagerController(mock.Object);

            //Act
            var result = await controller.Details(guid);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult)result).Model, Is.EqualTo(fundManagerModel));
        }

        [Test]
        public async void ShouldGetEditPage()
        {
            var guid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.SetupAllProperties();
            mock.Setup(m => m.Get(guid)).Returns(Task.FromResult(fundManagerModel));
            var controller = new FundManagerController(mock.Object);

            var result = await controller.Edit(guid);

            Assert.That(result, Is.TypeOf<ViewResult>());
            mock.Verify();
            Assert.That(((ViewResult)result).Model, Is.EqualTo(fundManagerModel));
        }

        [Test]
        public async void ShouldGetRedirectedToErrorFromEditPageIfNullGuid()
        {
            var validGuid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.SetupAllProperties();
            mock.Setup(m => m.Get(validGuid)).Returns(Task.FromResult(fundManagerModel));
            var controller = new FundManagerController(mock.Object);

            var result = await controller.Edit((Guid?)null);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            mock.Verify();
        }

        [Test]
        public async void ShouldGetIndexPageIfSuccessfulDelete()
        {
            var validGuid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            mock.SetupAllProperties();
            mock.Setup(m => m.Delete(validGuid)).Returns(Task.FromResult(true));
            var controller = new FundManagerController(mock.Object);

            var result = await controller.Delete(validGuid);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            mock.Verify();
        }

        [Test]
        public async void ShouldGetRedirectedToErrorFromDeletePageIfNullGuid()
        {
            var validGuid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.SetupAllProperties();
            mock.Setup(m => m.Get(validGuid)).Returns(Task.FromResult(fundManagerModel));
            var controller = new FundManagerController(mock.Object);

            var result = await controller.Delete(null);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            mock.Verify();
        }
    }
}
