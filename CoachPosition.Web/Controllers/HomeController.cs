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
                numTrain = infoTrain.NumTrain; // number of train
                if (infoTrain != null)
                {
                    var cars = infoTrain.NumCars.Split(',').Select(int.Parse).ToList(); //convert comma separated string into a List<int>

                    int x = 0;
                    foreach (int car in cars)
                    {
                        Sections sect = (Sections)x;
                        if (car == 0)
                        {
                            int carZero = 1000 + x; //exclude double key in dictionary(in case 0 and 0)
                            section.Add(carZero, sect.ToString());
                        }
                        else
                            section.Add(car, sect.ToString());
                        x++;
                    }

                    getSection = section[model.NumCar]; // get section from Dictionary by key
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