using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
    public class MapFromFundManagerDtoToFundManager : IMapper<FundManagerDto, FundManager>
    {
        public FundManager Map(FundManagerDto obj)
        {
            return new FundManager
            {
                Id = obj.Id,
                Biography = obj.Biography,
                Location = obj.Location,
                ManagedSince = obj.ManagedSince,
                Name = obj.Name
            };
        }
    }
}
