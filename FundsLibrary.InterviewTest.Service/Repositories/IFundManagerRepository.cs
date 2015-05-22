using System;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public interface IFundManagerRepository
    {
        Task<FundManagerDto> GetBy(Guid id);
        Task<IQueryable<FundManagerDto>> GetAll();
        Task<Guid> Update(FundManagerDto fundManager);
        Task<Guid> Create(FundManagerDto fundManager);
        Task<Boolean> Delete(Guid id);
    }
}
