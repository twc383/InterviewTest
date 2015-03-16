using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Repositories;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class FundManagerController : Controller
    {
        private readonly IFundManagerModelRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public FundManagerController()
            : this(new FundManagerModelRepository())
        {}

        public FundManagerController(IFundManagerModelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null) { return UnverifiedFundManagerId("A Fund Manager Id was not provided or was in an invalid format"); }
            var result = await _repository.Get(id.Value);
            if (result == null) { return UnverifiedFundManagerId(string.Format("The Fund Manager id {0} was not found", id.Value)); }
            return View(result);
        }

       private RedirectToRouteResult UnverifiedFundManagerId(string errorMessage) {
            return RedirectToAction("Index", "Error", new { errorMessage = errorMessage });
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Models.FundManagerModel newManager)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Post(newManager);
                return RedirectToAction("Details", new { id = result });
            }
            return View(newManager);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id) { 
            if (id == null) { return UnverifiedFundManagerId("A Fund Manager Id was not provided or was in an invalid format"); }
            var result = await _repository.Delete(id.Value);
            if (result) {
                return RedirectToAction("Index"); 
            }else{
                return RedirectToAction("Details", new  {id = id.Value });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id) {
            if (id == null) { return UnverifiedFundManagerId("A Fund Manager Id was not provided or was in an invalid format"); }
            var result = await _repository.Get(id.Value);
            if (result == null) { return UnverifiedFundManagerId(string.Format("The Fund Manager id {0} was not found", id.Value)); }
            return View(result);
        }
       
        [HttpPost]
        public async Task<ActionResult> Edit(Models.FundManagerModel editManager) {
            if (ModelState.IsValid)
            {
                var result = await _repository.Put(editManager);
                return RedirectToAction("Details", new { id = result });
            }
            else {
                return View(editManager);
            }
        }
    }
}
