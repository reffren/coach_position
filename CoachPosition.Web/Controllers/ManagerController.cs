using CoachPosition.Data.Abstract;
using CoachPosition.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoachPosition.Web.Controllers
{
    public class ManagerController : Controller
    {
        private IRepository _repository;

        public ManagerController(IRepository repository)
        {
            _repository = repository;
        }
        public ActionResult AddTrain()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTrain(Train train)
        {
         //   if (ModelState.IsValid)
          //  {
                _repository.SaveTrain(train);
          //  }

            return RedirectToAction("AddTrain", "Manager");
        }

        public ActionResult ListTrains()
        {
            return View();
        }
	}
}