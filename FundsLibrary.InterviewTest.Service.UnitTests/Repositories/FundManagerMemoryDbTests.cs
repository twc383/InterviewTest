using System;
using System.Diagnostics;
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
			var repo = new FundManagerMemoryDb();

			var res = await repo.GetBy();

			foreach (var it in res)
			{
				Console.WriteLine("{ \"" + it.Id + "\", new FundManager { Id = \"" + it.Id + "\"");
			}

			//assert we have some data
			Assert.That(res.Count(), Is.GreaterThan(3));
		}

		[Test]
		public async Task ShouldGetById()
		{
			var repo = new FundManagerMemoryDb();
			var firstItem =(await repo.GetBy()).First();

			var res = await repo.GetBy(firstItem.Id);
			Assert.That(res, Is.EqualTo(firstItem));
		}

		[Test]
		public async Task ShouldAddItem()
		{
			var repo = new FundManagerMemoryDb();
			var beforeCount = (await repo.GetBy()).Count();
			var fundManager = new FundManager()
			{
				Name = "TestFundManager",
				Biography = "test bio",
				Location = Location.NewYork,
				ManagedSince = DateTime.Now.AddYears(-1)
			};

			repo.Create(fundManager);

			Assert.That(fundManager.Id, Is.Not.EqualTo(Guid.Empty));
			var afterCount = (await repo.GetBy()).Count();
			Assert.That(afterCount, Is.EqualTo(beforeCount+1));
		}

		[Test]
		public async Task ShouldUpdateItem()
		{
			var repo = new FundManagerMemoryDb();
			var firstItem = (await repo.GetBy()).First();

			firstItem.Name = "NewName";
			repo.Update(firstItem.Id, firstItem);

			Assert.That((await repo.GetBy(firstItem.Id)).Name, Is.EqualTo("NewName"));
		}

		[Test]
		public async Task ShouldRemoveItem()
		{
			var repo = new FundManagerMemoryDb();
			var fundManagers = (await repo.GetBy());
			var beforeCount = fundManagers.Count();
			var firstItem = fundManagers.First();

			repo.Delete(firstItem.Id);

			var afterCount = (await repo.GetBy()).Count();
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
		}
	}
}