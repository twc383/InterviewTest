using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
    public class ToFundManagerModelMapper : IMapper<FundManager, FundManagerModel>
    {
        public FundManagerModel Map(FundManager obj)
        {
            FundManagerModel mappedModel = null;
            if (obj != null) { 
            mappedModel = new FundManagerModel
            {
                Id = obj.Id,
                Biography = obj.Biography,
                Location = obj.Location,
                ManagedSince = obj.ManagedSince,
                Name = obj.Name
            };
            }
            return mappedModel;
        }
    }
}
