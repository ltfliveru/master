using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace SAProject.ViewModels.Student
{
    public class StudentInputViewModel
    {
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
 
        [BindNever]
        public bool isInputsClear { get; set; }
 
    }
}