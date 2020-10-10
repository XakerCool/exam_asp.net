using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_asp.net.Models
{
    public class ResultModel
    {
        [Display(Name ="Все ответы")]
        public int AllAnswers { get; set; }
        [Display(Name = "Правильные ответы")]
        public int AllRightAnswers { get; set; }
        [Display(Name = "Время, затраченное на тестирование")]
        public string TimeOfTesting { get; set; }
        [Display(Name = "Результат")]
        public double ResultInPercent { get; set; }
    }
}