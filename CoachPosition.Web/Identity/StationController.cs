using CoachPosition.Data.Abstract;
using CoachPosition.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoachPosition.Web.Controllers
{
    [Authorize]
    public class StationController : Controller
    {
        private IRepository _repository;

        public StationController(IRepository repository)
        {
            _repository = repository;
        }
        public ActionResult AddWay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWay(Train train)
        {
            if (ModelState.IsValid)
            {
                int numWay = train.NumWay;
                train = _repository.Trains.FirstOrDefault(f => f.NumTrain == train.NumTrain);
                if (train != null)
                {
                    train.NumWay = numWay;
                    _repository.SaveTrain(train);
                }
                else
                {
                    ViewBag.Message = "Данного поезда не существует, пожалуйста, проверьте введенные данные. ";
                    return View();
                }
                ViewBag.Message = "Путь - " + train.NumWay + ", для поезда - " + train.NumTrain + ", успешно сохранен.";
            }
            else
            {
                ViewBag.Message = "Что-то пошло не так, пожалуйста, проверьте формат, введенных данных и повторите попытку.";
            }
            return View();
        }
	}
}