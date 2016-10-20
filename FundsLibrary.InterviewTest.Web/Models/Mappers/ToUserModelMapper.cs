using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
    public class ToUserModelMapper : IMapper<User, UserModel>
    {
        public UserModel Map(User obj)
        {
            UserModel mappedModel = null;
            if (obj != null)
            {
                mappedModel = new UserModel
                {
                    Id = obj.Id,
                    EmailAddress = obj.emailAddress,
                    Username = obj.username,
                    Password = obj.password,
                    RegisteredSince = obj.registeredSince,
                    Role = obj.role
                };
            }
            return mappedModel;
        }
    }
}
