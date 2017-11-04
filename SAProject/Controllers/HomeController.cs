using SAProject.Attributes;
using SAProject.DAL;
using SAProject.Extensions;
using SAProject.Models.Entities;
using SAProject.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAProject.Controllers
{
 
    public class HomeController : Controller
    {
        private DataContext _context;
        public HomeController()
        {
            _context = new DataContext();
        } 
     
        public ActionResult Index()
        {          
            return View();
        }
         
        /// <summary>
        /// Сохранение временного списка добавленных студентов в БД
        /// </summary>
        /// <returns>RedirectToAction Index</returns>
        public ActionResult SaveStudent()
        {
            var studentSavedList = Session.GetData<List<StudentViewModel>>(nameof(List<StudentViewModel>));
            if (studentSavedList != null && studentSavedList.Count > 0)
            {

                List<Student> studentsRange = new List<Student>();
                studentSavedList.ForEach(s => studentsRange.Add(new Student()
                {
                    Surname = s.Surname,
                    Name = s.Name,
                    Patronymic = s.Patronymic,
                    PhoneNumber = s.PhoneNumber
                }));
                _context.Students.AddRange(studentsRange);
                _context.SaveChanges();

                Session.SetData<List<StudentViewModel>>(nameof(List<StudentViewModel>), null);
            }

            return RedirectToAction(nameof(HomeController.Index));
        }

        /// <summary>
        /// Асинхронный метод извлечения списка студентов 
        /// </summary>
        /// <returns>Частичное представление _StudentsList</returns>
        [HttpGet]
        public async Task<ActionResult> GetStudentsList()
        {
            var studentsList = await _context.Students.ToListAsync();
            return PartialView("_StudentsList", studentsList);
        }

        /// <summary>
        /// Отображение временного списка добавленных студентов.  Session.GetData
        /// </summary>
        /// <returns>PartialView _StudentTempList</returns>
        [HttpGet]
        public ActionResult GetStudentTempList()
        {
            var list = Session.GetData<List<StudentViewModel>>(nameof(List<StudentViewModel>));
            if (list!=null)
            {
                return PartialView("_StudentTempList", list);
            }
            return null;
        }

        /// <summary>
        /// Добавление сущности студента
        /// </summary>
        /// <param name="vm">Input vm</param>
        /// <returns>PartialView _StudentInput</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(StudentViewModel vm)
        { 
            if (ModelState.IsValid)
            {
                SaveStudentInSession(vm);
                return PartialView("_StudentInput", new StudentViewModel() { isInputsClear = true });
            }    
            
            return PartialView("_StudentInput", vm);
        }

        /// <summary>
        /// Сохранить объект студента в Session
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private List<StudentViewModel> SaveStudentInSession(StudentViewModel vm)
        {
            List<StudentViewModel> list = 
                Session.GetData<List<StudentViewModel>>(nameof(List<StudentViewModel>)) ?? new List<StudentViewModel>();

            list.Add(vm);

            Session.SetData<List<StudentViewModel>>(nameof(List<StudentViewModel>), list);

            return list;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


         
        public ActionResult Error()
        {
            ViewBag.Message = "Redirect from overrided HandleUnauthorizedRequest method is worked!";

            return View();
        }
    }
}