using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_asp.net.Models
{
    [Table("Question")]
    public class _Question
    {
        public int Id { get; set; }
        [Display(Name = "Вопрос")]
        public string Question { get; set; }
        [Display(Name = "Тип ответа")]
        public string AnswerType { get; set; }
        public string TrueAnswer { get; set; }
    }
}