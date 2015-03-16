using System;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public interface IFundManagerRepository
    {
        Task<FundManager> GetBy(Guid id);
        Task<IQueryable<FundManager>> GetAll();
        Task<Guid> Update(FundManager fundManager);
        Task<Guid> Create(FundManager fundManager);
        Task<Boolean> Delete(Guid id);
    }
}
