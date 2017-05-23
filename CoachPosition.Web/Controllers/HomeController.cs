using CoachPosition.Data.Abstract;
using CoachPosition.Data.Entities;
using CoachPosition.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoachPosition.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
       // private const string Section = "A,B,C,D,E,F,G";
        Dictionary<int, string> section;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            string getSection="Пожалуйста проверьте введенные данные";
            string numTrain = "";
            int passengerCar = model.NumCar;
            section = new Dictionary<int, string>();

            if (ModelState.IsValid)
            {
                var infoTrain = _repository.Trains.FirstOrDefault(f => f.NumTrain == model.NumTrain);
                if (infoTrain != null)
                {
                    int carNumber = infoTrain.NumCars;
                    numTrain = infoTrain.NumTrain;
                    var cars = carNumber.ToString().Select(x => int.Parse(x.ToString())); //split integer
                    int i = 0;
                    foreach (int car in cars)
                    {
                        Sections sect = (Sections) i;
                        if (car == 0)
                        {
                            int carZero = 1000 + i; //exclude double key in dictionary(in case 0 and 0)
                            section.Add(carZero, sect.ToString());
                        } else
                            section.Add(car, sect.ToString());
                        i++;
                    }

                    getSection = section[model.NumCar];
                }
                else
                {
                    return RedirectToAction("Section", "Home");
                }
            }
            return RedirectToAction("Section", "Home", new { numTrain = numTrain, numCar = passengerCar, sectionValue = getSection });
        }

        public ActionResult Section(string numTrain, int numCar, string sectionValue)
        {
            SectionModel sectModel = new SectionModel
            {
                NumTrain = numTrain,
                NumCar = numCar,
                SectionValue = sectionValue

            };
            return View(sectModel);
        }
	}

    public enum Sections
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }
}