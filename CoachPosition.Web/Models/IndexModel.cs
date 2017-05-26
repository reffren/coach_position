using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoachPosition.Web.Models
{
    public class IndexModel
    {
        [Required(ErrorMessage = "Не заполнено поле 'Номер поезда'")]
        public string NumTrain { get; set; }

        [Required(ErrorMessage = "Не заполнено поле 'Номер вагона'")]
        //[RegularExpression(@"^\d[1-26]", ErrorMessage = "Недопустимый формат данных")]
        public int NumCar { get; set; }
    }
}