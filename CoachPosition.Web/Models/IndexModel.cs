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
        [Display(Name = "Номер поезда")]
        [RegularExpression(@"^[a-zA-ZА-Яа-я0-9]{1,5}$", ErrorMessage = "Недопустимый формат данных")]
        public string NumTrain { get; set; }

        [Required(ErrorMessage = "Не заполнено поле 'Номера вагонов'")]
        [Display(Name = "Номер вагона")]
        [RegularExpression(@"^[1-9][0-9]{0,3}$", ErrorMessage = "Номер вагона должен содержать цифры от 1 до 22.")]
        public int NumCar { get; set; }
    }
}