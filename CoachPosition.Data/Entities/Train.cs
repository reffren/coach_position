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

        [Required(ErrorMessage = "Не заполнено поле 'Номер поезда'")]
        public string NumTrain { get; set; }

        [Required(ErrorMessage = "Не заполнено поле 'Номер вагона'")]
        [RegularExpression(@"^[0-9,-]{1,80}$", ErrorMessage = "Недопустимый формат данных")]
        public string NumCars { get; set; }
    }
}