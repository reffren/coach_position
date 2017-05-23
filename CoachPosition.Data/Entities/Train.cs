using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoachPosition.Data.Entities
{
    public class Train
    {
        [Key]
        public int TrainID { get; set; }
        public string NumTrain { get; set; }
        public int NumCars { get; set; }
    }
}