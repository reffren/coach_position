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
            List<int> cars;
            section = new Dictionary<int, string>();

            if (ModelState.IsValid)
            {
                var infoTrain = _repository.Trains.FirstOrDefault(f => f.NumTrain == model.NumTrain);
                if (infoTrain != null)
                {
                    int n = 0;
                    cars = new List<int>();
                    string s = infoTrain.NumCars;
                    string[] values = s.Split(','); //first we are splitting by comma

                    for (int i = 0; i < values.Length; i++)
                    {
                        string d = values[i];
                        bool isNumeric = int.TryParse(d, out n); //in case if d=0 
                        if (isNumeric)
                        {
                            cars.Add(0);
                        }
                        else
                        {
                            string[] range = d.Split('-'); //after we are splitting by dash


                            for (int l = 0; l < range.Length; l++) //range from 1-6, for example (1,2,3,4,5,6)
                            {
                                int a = Int32.Parse(range[l]); //for example a = 1
                                l++;
                                int b = Int32.Parse(range[l]); //for example b = 6

                                if (a > b) //in case if the range will be wice versa (6-1)
                                {
                                    //int tempVar = a;
                                    //a = b;
                                    //b = tempVar;
                                    for (int y = a; y > b - 1; y--) //range from 6-1, for example (6,5,4,3,2,1)
                                    {
                                        cars.Add(y);
                                    }
                                }
                                else
                                {
                                    for (int y = a; y < b + 1; y++) //range from 1-6, for example (1,2,3,4,5,6)
                                    {
                                        cars.Add(y);
                                    }
                                }
                            }
                        }
                    }


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