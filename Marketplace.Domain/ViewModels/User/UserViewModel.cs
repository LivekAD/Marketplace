using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Domain.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Вкажіть роль")]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Вкажіть логин")]
        [Display(Name = "Логін")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вкажіть пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
