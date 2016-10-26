using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public interface IFundManagerRepository
    {
        Task<FundManager> GetById(Guid id);
        Task<IEnumerable<FundManager>> GetAll();
        Task<Guid> Update(FundManager fundManager);
        Task<Guid> Create(FundManager fundManager);
        Task<bool> Delete(Guid id);
    }
}
