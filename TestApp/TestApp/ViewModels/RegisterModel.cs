using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestApp.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Никнейм")]
        public string username { get; set; }
        [Required(ErrorMessage = "Не указано Имя")]
        public string first_name { get; set; }
        [Required(ErrorMessage = "Не указана Фамилия")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
