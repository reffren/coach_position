using CoachPosition.Data.Abstract;
using CoachPosition.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoachPosition.Web.Controllers
{
    public class InformationBoardController : Controller
    {
        IRepository _repository;

        public InformationBoardController(IRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Info()
        {
            var infoTrain = _repository.Trains.FirstOrDefault();
            var cars = infoTrain.NumCars.Split(',').Select(int.Parse).ToList();
            var letters = from letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray() select letter.ToString();
            InformationBoardModel pos = new InformationBoardModel()
            {
                Cars = cars,
                Way = infoTrain.NumWay,
                Letters = letters
            };
            return View(pos);
        }
    }
}