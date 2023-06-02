using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Вкажіть ім'я")]
        [MaxLength(20, ErrorMessage = "Ім'я повинно мати довжину менше 20 символів")]
        [MinLength(3, ErrorMessage = "Ім'я повинно мати довжину більше 3 символів")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        [MinLength(6, ErrorMessage = "Пароль повинен мати довжину більше 6 символів")]
        public string Password { get; set; }

        [Display(Name = "Адреса електронної пошти")]
        [Required(ErrorMessage = "Адреса електронної пошти є обов'язковою")]
        [EmailAddress(ErrorMessage = "Неправильна адреса електронної пошти")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Номер телефону є обов'язковим")]
        [Phone(ErrorMessage = "Неправельний номер телефону")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        public string PasswordConfirm { get; set; }
    }
}
