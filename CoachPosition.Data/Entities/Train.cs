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
        [Display(Name = "Номер поезда *")]
        [RegularExpression(@"^[a-zA-ZА-Яа-я0-9+-]{1,6}$", ErrorMessage = "Недопустимый формат данных")]
        public string NumTrain { get; set; }

        [Required(ErrorMessage = "Не заполнено поле 'Номер пути'")]
        [Display(Name = "Номер пути")]
        [RegularExpression(@"^[0-9]{1,2}$", ErrorMessage = "Недопустимый формат данных")]
        public int NumWay { get; set; }

        [Required(ErrorMessage = "Не заполнено поле 'Номер вагона'")]
        [Display(Name = "Номер вагона **")]
        [RegularExpression(@"^[0-9,-]{1,80}$", ErrorMessage = "Недопустимый формат данных")]
        public string NumCars { get; set; }
    }
}