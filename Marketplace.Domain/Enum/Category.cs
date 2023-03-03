using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Domain.Enum
{
    //Категорії позначаються 3X, де 3 це файл з категоріями, X це номер категорії
    public enum Category
    {
        [Display(Name = "Електроніка")]
        Electronics = 31,
        [Display(Name = "Одяг та взуття")]
        Clothing_and_footwear = 32,
        [Display(Name = "Домашні товари")]
        Home_goods = 33,
        [Display(Name = "Спорт та відпочинок")]
        Sports_and_recreation = 34,
        [Display(Name = "Краса та догляд")]
        Beauty_and_care = 35,
        [Display(Name = "Їжа та напої")]
        Food_and_beverages = 36,
        [Display(Name = "Автомобілі та мотоцикли")]
        Cars_and_motorcycles = 37,
        [Display(Name = "Товари для тварин")]
        Products_for_pets = 38,
        [Display(Name = "Інструменти та обладнання")]
        Tools_and_equipment = 39,
        [Display(Name = "Книги та медіа")]
        Books_and_media = 310,
    }
}
