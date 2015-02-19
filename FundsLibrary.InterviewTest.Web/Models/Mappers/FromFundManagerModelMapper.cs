using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
	public class FromFundManagerModelMapper: IMapper<FundManagerModel, FundManager>
	{
		public FundManager Map(FundManagerModel obj)
		{
			return new FundManager()
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