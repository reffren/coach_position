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
            List<int> cars;
            if (ModelState.IsValid)
            {
                int n = 0;
                cars = new List<int>();
                string numCars = train.NumCars;
                string[] values = numCars.Split(','); //first we are splitting by comma

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

                            if (a > 26 && b > 26)
                            {
                                ViewBag.Message = "онда из цифер больше 26, пожалуйста, проверьте вводимые данные и повторите попытку.";
                                return View();
                            }

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
                train.NumCars = string.Join(",", cars); //convert array of integers (cars) to comma-separated string
                train.TrainID = _repository.Trains.Where(w => w.NumTrain == train.NumTrain).Select(s => s.TrainID).FirstOrDefault();
                _repository.SaveTrain(train);
                ViewBag.Message = "Внесенные данные " + numCars + " сохранены.";
            }
            else
            {
                 ViewBag.Message = "Что-то пошло не так, пожалуйста, проверьте формат, введенных данных и повторите попытку.";
            }
            return View();
        }

        public ActionResult ListTrains()
        {
            return View();
        }
	}
}