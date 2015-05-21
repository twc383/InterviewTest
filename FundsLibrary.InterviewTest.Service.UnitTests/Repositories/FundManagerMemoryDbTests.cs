using System;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Repositories;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Service.UnitTests.Repositories
{
    public class FundManagerMemoryDbTests
    {
        [Test]
        public async Task ShouldGetItems()
        {
			//Arrange
            var repo = new FundManagerMemoryDb();

			//Act
            var result = await repo.GetAll();

            //Assert
            Assert.That(result.Count(), Is.GreaterThan(3));
        }

        [Test]
        public async Task ShouldGetById()
        {
			//Arrange
            var repo = new FundManagerMemoryDb();
			var firstItem = (await repo.GetAll()).First();

			//Act
			var result = await repo.GetBy(firstItem.Id);

			//Assert
            Assert.That(result, Is.EqualTo(firstItem));
        }

        [Test]
        public async Task ShouldAddItem()
        {
			//Arrange
            var repo = new FundManagerMemoryDb();
            var beforeCount = (await repo.GetAll()).Count();
            var fundManager = new FundManager()
            {
                Name = "TestFundManager",
                Biography = "test bio",
                Location = Location.NewYork,
                ManagedSince = DateTime.Now.AddYears(-1)
            };

			//Act
            repo.Create(fundManager);

			//Assert
            Assert.That(fundManager.Id, Is.Not.EqualTo(Guid.Empty));
            var afterCount = (await repo.GetAll()).Count();
            Assert.That(afterCount, Is.EqualTo(beforeCount + 1));
        }

        [Test]
        public async Task ShouldUpdateItem()
        {
			//Arrange
            var repo = new FundManagerMemoryDb();
            var firstItem = (await repo.GetAll()).First();
			firstItem.Name = "NewName";

			//Act
			repo.Update(firstItem);

			//Assert
            Assert.That((await repo.GetBy(firstItem.Id)).Name, Is.EqualTo("NewName"));
        }

        [Test]
        public async Task ShouldRemoveItem()
        {
			//Arrange
            var repo = new FundManagerMemoryDb();
            var fundManagers = (await repo.GetAll());
            var beforeCount = fundManagers.Count();
            var firstItem = fundManagers.First();

			//Act
            repo.Delete(firstItem.Id);

			//Assert
            var afterCount = (await repo.GetAll()).Count();
            Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
        }
    }
}
