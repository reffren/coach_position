﻿using CoachPosition.Data.Abstract;
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
        public virtual ActionResult Index(IndexModel model)
        {
            string getSection="";
            string numTrain = "";
            int numWay = 0;
            int passengerCar = model.NumCar;
            section = new Dictionary<int, string>();

            if (ModelState.IsValid)
            {
                if (passengerCar > 22)
                {
                    ViewBag.Message = "Номер вагона должен содержать цифры от 1 до 22.";
                    return View();
                }
                    
                var infoTrain = _repository.Trains.FirstOrDefault(f => f.NumTrain == model.NumTrain);
                if (infoTrain != null)
                {
                    numTrain = infoTrain.NumTrain; // number of train
                    numWay = infoTrain.NumWay;
                    var cars = infoTrain.NumCars.Split(',').Select(int.Parse).ToList(); //convert comma separated string into a List<int>

                    int x = 0;

                    if (cars[0] == 99) //way of train (if the locomotive(99) on the left side, section will be A,B,C,D,E...)
                    {
                        foreach (int car in cars)
                        {
                            if (car != 99) //skip the locomotive(99)
                            {
                                SectionsAZ sect = (SectionsAZ)x;
                                if (car == 0)
                                {
                                    int carZero = 1000 + x; //exclude double key in dictionary(in case 0 and 0)
                                    section.Add(carZero, sect.ToString());
                                }
                                else
                                    section.Add(car, sect.ToString());
                                x++;
                            }
                        }
                    }
                    else //way of train (if the locomotive(99) on the right side, section will be E,D,C,B,A...)
                    {
                        foreach (int car in cars) 
                        {
                            if (car != 99) //skip the locomotive(99)
                            {
                                SectionsZA sect = (SectionsZA)x;
                                if (car == 0)
                                {
                                    int carZero = 1000 + x; //exclude double key in dictionary(in case 0 and 0)
                                    section.Add(carZero, sect.ToString());
                                }
                                else
                                    section.Add(car, sect.ToString());
                                x++;
                            }
                        }
                    }

                    if (section.ContainsKey(model.NumCar))  //check (in Dictionary) value(car) of user exists or not
                    {
                    getSection = section[model.NumCar]; // get section from Dictionary by key
                    }
                    else
                    {
                        ViewBag.Message = "Данного вагона в поезде - " + numTrain + " не существует, пожалуйста, проверьте введенные данные.";
                        return View();
                    }

                    return RedirectToAction("Section", "Home", new { numTrain = numTrain, numCar = passengerCar, sectionValue = getSection, way = numWay });
                }
                else // db is empty
                {
                    ViewBag.Message = "Информация по данному поезду отсутствует.";
                    return View();
                }
            }
            else //Model is not valid
            {
                return View();
            }
        }

        public virtual ActionResult Section(string numTrain, int numCar, string sectionValue, int way)
        {
            SectionModel sectModel = new SectionModel
            {
                NumTrain = numTrain,
                NumWay = way,
                NumCar = numCar,
                SectionValue = sectionValue

            };
            return View(sectModel);
        }
	}

    public enum SectionsAZ
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V
    }

    public enum SectionsZA
    {
        V, U, T, S, R, Q, P, O, N, M, L, K, J, I, H, G, F, E, D, C, B, A
    }
}