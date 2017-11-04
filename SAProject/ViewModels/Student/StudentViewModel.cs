using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace SAProject.ViewModels.Student
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Patronymic { get; set; }

        
        [Required(ErrorMessage = "Необходимо указать телефон")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Не верный номер")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Не верный номер")]
        public string PhoneNumber { get; set; }

        public bool IsRemove { get; set; }

        [BindNever]
        public bool isInputsClear { get; set; }
        
    }
}