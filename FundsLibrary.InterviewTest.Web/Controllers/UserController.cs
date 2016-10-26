using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Repositories;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _userRepository.GetAll());
        }
    }
}
