using SAProject.Attributes;
using SAProject.DAL;
using SAProject.Models.Entities;
using SAProject.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SAProject.Controllers
{
    public class StudentController : Controller
    {
        private DataContext _context;
        private List<StudentViewModel> studentViewModel => GetStudentViewModel();

        public StudentController()
        {
            _context = new DataContext();
        }

        // GET: Students
        [HttpGet]
        //[CustomAuthorize] // Убрать комментарий для проверки 
        public ActionResult Index()
        {
             return View(studentViewModel); 
        }
                
        [HttpPost]
        public ActionResult Index(List<StudentViewModel> vm, string command)
        {
            vm = vm ?? new List<StudentViewModel>();
            if (command == "Add")
            {
                vm.Add(new StudentViewModel() { Id = -1 });
            }
            else if (ModelState.IsValid)
            {
                AddOrUpdateStudentRange(vm);
                return RedirectToAction(nameof(StudentController.Index));
            }
            return View(vm);
        }
        
        [HttpPost]
        public ActionResult Remove(int? id)
        {
            if (!id.HasValue || !_context.Students.Any(x => x.Id == id.Value))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }

            RemoveStudentEntity(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private void RemoveStudentEntity(int? id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public void AddOrUpdateStudentRange(List<StudentViewModel> vm)
        {
            if (vm.Count() > 0)
            {
                using (var _context = new DataContext())
                {
                    var vmItemsArr = vm.Select(s => s.Id).ToArray();
                    var updateList = _context.Students.Where(x => vmItemsArr.Contains(x.Id)).AsQueryable();
                    var itemsToUpdateArr = updateList.Select(x => x.Id).ToArray();
                    // В ДАННЫХ СЛУЧАЯХ ИСПОЛЬЗУЕТСЯ AUTOMAPPER
                    // Обновление данных
                    foreach (var item in updateList)
                    {
                        var vmItem = vm.First(x => x.Id == item.Id);
                        item.Name = vmItem.Name;
                        item.Surname = vmItem.Surname;
                        item.Patronymic = vmItem.Patronymic;
                        item.PhoneNumber = vmItem.PhoneNumber;
                    }
                    // Добаление
                    List<Student> studentsInsertRange = new List<Student>();
                    // В ДАННЫХ СЛУЧАЯХ ИСПОЛЬЗУЕТСЯ AUTOMAPPER
                    vm.Where(x => !itemsToUpdateArr.Contains(x.Id)).ToList().ForEach(s => studentsInsertRange.Add(new Student()
                    {
                        Surname = s.Surname,
                        Name = s.Name,
                        Patronymic = s.Patronymic,
                        PhoneNumber = s.PhoneNumber
                    }));
                    _context.Students.AddRange(studentsInsertRange);
                    _context.SaveChanges();
                }
            }
        }
        
        private List<StudentViewModel> GetStudentViewModel()
        {
            var result = new List<StudentViewModel>();
            if (_context.Students.Any())
            {
                var studentsSourceList = _context.Students.ToList();
                studentsSourceList.ForEach(s => result.Add(new StudentViewModel()
                {
                    Id = s.Id,
                    Surname = s.Surname,
                    Name = s.Name,
                    Patronymic = s.Patronymic,
                    PhoneNumber = s.PhoneNumber
                }));
            }
            else
            {
                result.Add(new StudentViewModel() { Id = -1 });
            }
            return result;
        }
    }
}