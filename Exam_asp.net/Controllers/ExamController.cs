using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Exam_asp.net.Models;

namespace Exam_asp.net.Controllers
{
    public class ExamController : Controller
    {
        Context db;

        ResultModel resultModel;
        DateTime timeOfStartingTest;
        DateTime timeOfEndingTest;
        public List<string> answers;
        int allAnswers;
        public ExamController()
        {
            answers = new List<string>();
            db = new Context();
            allAnswers = db.Questions.Count();
            resultModel = new ResultModel();
            timeOfStartingTest = DateTime.Now;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAnswer(string answer_1, string answer_2, string answer_3, string answer_4, string answer_5, string userName)
        {
            answers.Add(answer_1);
            answers.Add(answer_2);
            answers.Add(answer_3);
            answers.Add(answer_4);
            answers.Add(answer_5);
            return ShowQuestions(userName);
        }
        [HttpGet]
        public ActionResult ShowQuestions()
        {
            timeOfStartingTest = DateTime.Now;
            return View(db.Questions);
        }
        [HttpPost]
        public ActionResult ShowQuestions(string userName)
        {
            string tempUserName = userName;
            timeOfEndingTest = DateTime.Now;
            if (tempUserName == null)
                tempUserName = "Empty";
            //thread2.Abort();
            UserResult result = new UserResult();
            double resultInPercents = 0;
            int rightAnswersCount = 0;
            User _user = db.Users.Where(z => z.Name == tempUserName).FirstOrDefault();
            if(_user == null)
            {
                _user = new User();
                _user.Name = tempUserName;
                db.Users.Add(_user);
            }
            foreach(var item in db.Questions)
            {
                for (int i = 0; i < answers.Count; i++)
                {
                    if (item.TrueAnswer == answers[i])
                    {
                        rightAnswersCount++;
                    }
                }
            }
            var dateOfTesting = DateTime.Now.Date;
            if(rightAnswersCount!=0)
                resultInPercents = (allAnswers / rightAnswersCount) * 100;

            result.ResultOfDone = dateOfTesting.Date;
            result.ResultInPercent = resultInPercents;
            result.UserName = _user.Name;

            db.UsersResults.Add(result);
            db.SaveChanges();

            resultModel.AllAnswers = allAnswers;
            resultModel.AllRightAnswers = rightAnswersCount;
            resultModel.TimeOfTesting = (timeOfEndingTest.Minute - timeOfStartingTest.Minute).ToString() + " минут";
            resultModel.ResultInPercent = resultInPercents;

            return RedirectToAction("ShowResults", resultModel);
        }

        public ActionResult ShowResults(ResultModel result)
        {
            return View(result);
        }

        [HttpGet]
        public ActionResult ShowResultsInDate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShowResultsInDate(string startDate, string endDate)
        {
            DateTime _startDate = Convert.ToDateTime(startDate);
            DateTime _endDate = Convert.ToDateTime(endDate);

            List<UserResult> usersResults = new List<UserResult>();

            do
            {
                var items = db.UsersResults.Where(z => z.ResultOfDone == _startDate).ToList();
                if(items!=null)
                    foreach(var item in items)
                        usersResults.Add(item);
                _startDate = _startDate.AddDays(1);
            } while (_startDate != _endDate);
            if (usersResults != null)
                return PartialView("UsersResults",usersResults);
            else
                return View();
        }
    }
}