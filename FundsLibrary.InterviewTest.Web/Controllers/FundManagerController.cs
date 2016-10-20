using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using FundsLibrary.InterviewTest.Web.App_Start;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class FundManagerController : Controller
    {
        private readonly IFundManagerRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public FundManagerController()
            : this(new FundManagerRepository())
        {}

        public FundManagerController(IFundManagerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string sortOrder = "" , int? page = 1)
        {
            IEnumerable<Models.FundManagerModel> fundManagersList = await _repository.GetAll();
            ViewBag.CurrentSort = sortOrder;

            switch (sortOrder)
            {
                case "Name" : fundManagersList = fundManagersList.OrderBy(n => n.Name);
                    break;
                case "Location" : fundManagersList = fundManagersList.OrderBy(n => n.Location.ToString());
                    break;
                case "Biography" : fundManagersList = fundManagersList.OrderBy(n => n.Biography);
                    break;
                case "ManagedSince" : fundManagersList = fundManagersList.OrderBy(n => n.ManagedSince);
                    break;
                default:
                    fundManagersList = fundManagersList.OrderByDescending(n => n.Name);
                    break;
            }
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            return View(fundManagersList.ToPagedList(pageNumber, pageSize));
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

        //[UserAuthorisation(Role = "Admin")]
        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        //[UserAuthorisation(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Add(FundManager newManager)
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
        public async Task<ActionResult> Edit(FundManager editManager)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Put(editManager);
                return RedirectToAction("Details", new { id = result });
            }

            return View(editManager);
        }
    }
}
