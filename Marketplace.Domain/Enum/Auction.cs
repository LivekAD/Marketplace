using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Domain.Enum
{
    public enum Auction
    {
        [Display(Name = "На 12 годин")]
        Hour12 = 12,
        [Display(Name = "На 24 годин")]
        Hour24 = 24,
        [Display(Name = "На 36 годин")]
        Hour36 = 36,
        [Display(Name = "На 48 годин")]
        Hour48 = 48,
    }
}
