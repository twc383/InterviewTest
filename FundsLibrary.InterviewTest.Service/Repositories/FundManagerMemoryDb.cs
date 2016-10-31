using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    /*
     * This class is the simplest possible implementation of the fund manager repository interface.
     * A real world implementation would wrap SQL Server / Couchbase / ElasticSearch /etc.
     */

    public class FundManagerMemoryDb : IFundManagerRepository
    {
        private static readonly ConcurrentDictionary<Guid, FundManager> _fundManagers = new ConcurrentDictionary<Guid, FundManager>();

        static FundManagerMemoryDb()
        {
            //fake data
            var items = new[]
            {
                new FundManager { Id = Guid.Parse("bf9e5061-37d2-49f1-9b9b-212c94a3e3ae"), Name = "Sanjiv Duggal", Biography = "some bio goes here", Location = Location.Amsterdam, ManagedSince = DateTime.Now.AddYears(-3) },
                new FundManager { Id = Guid.Parse("f67e8867-8e7b-41ee-81e9-dc75e3e32742"), Name = "Mitul Patel", Biography = "some bio goes here", Location = Location.Frankfurt, ManagedSince = DateTime.Now.AddYears(-1) },
                new FundManager { Id = Guid.Parse("b759ce77-f469-4884-bcb5-efeaeec11872"), Name = "Iain Campbell", Biography = "some bio goes here", Location = Location.Luxembourg, ManagedSince = DateTime.Now.AddYears(-4) },
                new FundManager { Id = Guid.Parse("2714c37a-4d16-422e-82ed-e3f0ff62d931"), Name = "James Donald", Biography = "some bio goes here", Location = Location.London, ManagedSince = DateTime.Now.AddYears(-2) },
                new FundManager { Id = Guid.Parse("a4847d2b-3c18-4637-96c7-508f241fa45d"), Name = "Andrew Wickham", Biography = "some bio goes here", Location = Location.NewYork, ManagedSince = DateTime.Now.AddYears(-5) },
                new FundManager { Id = Guid.Parse("808069a8-1949-4909-a662-7f1af0fcfbc3"), Name = "Steve Land", Biography = "some bio goes here", Location = Location.Zurich, ManagedSince = DateTime.Now.AddYears(-7) },
                new FundManager { Id = Guid.Parse("0ee5458d-7d0d-46b8-8043-3215e67c0aa0"), Name = "Frederick Fromm", Biography = "some bio goes here", Location = Location.London, ManagedSince = DateTime.Now.AddYears(-2) }
            };

            foreach (var fundManager in items)
                _fundManagers.TryAdd(fundManager.Id, fundManager);
        }

        public Task<FundManager> GetById(Guid id)
        {
            FundManager value;
            _fundManagers.TryGetValue(id, out value); // If the key is not in the dictionary return null.

            return Task.FromResult(value);
        }

        public Task<IEnumerable<FundManager>> GetAll()
        {
            return Task.FromResult((IEnumerable<FundManager>)_fundManagers.Values);
        }

        public Task<Guid> Update(FundManager fundManager)
        {
            _fundManagers[fundManager.Id] = fundManager;
            return Task.FromResult(fundManager.Id);
        }

        public Task<bool> Delete(Guid id)
        {
            FundManager value;
            var result = _fundManagers.TryRemove(id, out value);
            return Task.FromResult(result);
        }

        public Task<Guid> Create(FundManager fundManager)
        {
            fundManager.Id = Guid.NewGuid();
            if (!_fundManagers.TryAdd(fundManager.Id, fundManager))
                throw new Exception("Cannot add manager - another manager with the same ID already exists."); // Unlikely as it's as the key is a GUID.

            return Task.FromResult(fundManager.Id);
        }
    }
}
