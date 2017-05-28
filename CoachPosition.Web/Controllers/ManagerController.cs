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
                    //if (d.Equals("") && d.Equals("-"))
                    //{
                    //    ViewBag.Message = "Недопустимый формат данных.";
                    //    return View();
                    //}
                    bool isNumeric = int.TryParse(d, out n); //checking d is number or not(1-6)

                    if (isNumeric)
                    {
                        int notZero = Int32.Parse(d);
                        if (notZero > 0) //in case if d>0 - for string: 0,7 
                            cars.Add(notZero);
                        else
                            cars.Add(0); //in case if d=0 
                    }
                    else
                    {
                        string[] range = d.Split('-'); //after we are split by dash


                        for (int l = 0; l < range.Length; l++) //range from 1-6, for example (1,2,3,4,5,6)
                        {
                            if (range[l].Equals("")) // check if char array element is empty (in case user add string: "1," or "1-" and vice versa)
                            {
                                ViewBag.Message = "Недопустимый формат данных.";
                                return View();
                            }
                            int a = Int32.Parse(range[l]); //for example a = 1
                            l++;

                            if (range[l].Equals(""))  // check another char (after l++) array element is empty (in case user add string: "1," or "1-" and vice versa)
                            {
                                ViewBag.Message = "Недопустимый формат данных.";
                                return View();
                            }
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