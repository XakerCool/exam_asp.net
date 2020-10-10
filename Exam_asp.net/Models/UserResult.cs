using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exam_asp.net.Models
{
    [Table("UserResult")]
    public class UserResult
    {
        public int Id { get; set; }
        [Display(Name = "Дата выполнения")]
        public DateTime ResultOfDone { get; set; }
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [Display(Name = "Результат в %")]
        public double ResultInPercent { get; set; }
    }
}