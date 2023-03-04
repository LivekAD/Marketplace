using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "Користувач")]
        User = 0,
        [Display(Name = "Модератор")]
        Moderator = 1,
        [Display(Name = "Адмін")]
        Admin = 2,
    }
}
