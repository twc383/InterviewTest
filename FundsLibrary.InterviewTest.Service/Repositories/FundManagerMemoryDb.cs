using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
	public class FundManagerMemoryDb : IFundManagerRepository
	{
		private static readonly IDictionary<Guid, FundManager> _database = new ConcurrentDictionary<Guid, FundManager>();

		static FundManagerMemoryDb()
		{
			//fake data
			var items = new[]
			{
				new FundManager { Id = Guid.Parse("83eed26a-d1e3-4e78-8d3d-c4c9b63eb548"), Name = "A fund manager", Biography = "some bio goes here", Location = Location.Amsterdam, ManagedSince = DateTime.Now.AddYears(-3) },
				new FundManager { Id = Guid.Parse("11a12c9f-417f-4225-ba33-d05ce638b457"), Name = "Another fund manager", Biography = "some bio goes here", Location = Location.Frankfurt, ManagedSince = DateTime.Now.AddYears(-1) },
				new FundManager { Id = Guid.Parse("e5ada48d-bf76-4290-8e8a-c33b39e04d5c"), Name = "A third fund manager", Biography = "some bio goes here", Location = Location.Luxembourg, ManagedSince = DateTime.Now.AddYears(-4) },
				new FundManager { Id = Guid.Parse("b44aa473-3d20-4bc1-b234-7d12dd19f233"), Name = "A fund manager", Biography = "some bio goes here", Location = Location.London, ManagedSince = DateTime.Now.AddYears(-2) },
				new FundManager { Id = Guid.Parse("317065b3-b366-43ae-8f12-2a29c60d451d"), Name = "A fund manager", Biography = "some bio goes here", Location = Location.NewYork, ManagedSince = DateTime.Now.AddYears(-5) },
				new FundManager { Id = Guid.Parse("4d335f80-de41-4bce-9733-625deca0704f"), Name = "A fund manager", Biography = "some bio goes here", Location = Location.Zurich, ManagedSince = DateTime.Now.AddYears(-7) },
				new FundManager { Id = Guid.Parse("11575c63-5cbb-4844-8c2c-bada70bf0305"), Name = "A fund manager", Biography = "some bio goes here", Location = Location.London, ManagedSince = DateTime.Now.AddYears(-2) }
			};

			foreach (var fundManager in items)
				_database.Add(fundManager.Id, fundManager);
		}

		public FundManagerMemoryDb()
		{}

		public Task<FundManager> GetBy(Guid id)
		{
			FundManager value;
			if (_database.TryGetValue(id, out value))
				return Task.FromResult(value);
			return Task.FromResult<FundManager>(null);
		}

		public Task<IQueryable<FundManager>> GetBy()
		{
			return Task.FromResult(_database.Values.AsQueryable());
		}

		public void Update(Guid id, FundManager fundManager)
		{
			_database[id] = fundManager;
		}

		public void Delete(Guid id)
		{
			_database.Remove(id);
		}

		public Guid Create(FundManager fundManager)
		{
			var newGuid = Guid.NewGuid();
			fundManager.Id = newGuid;
			_database[newGuid] = fundManager;
			return newGuid;
		}
	}
}
