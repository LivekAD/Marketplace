using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Domain.Enum
{
    //Підкатегорії позначаються 4NX, де 4 це файл з підкатегоріями, N це номер категорії, X це номер підкатегорії
    public enum SubCategory
    {
        // Електроніка N = 31

        [Display(Name = "Cмартфони")]
        Smartphones = 4311,
        [Display(Name = "Комп'ютери")]
        Computers = 4312,
        [Display(Name = "Телевізори")]
        TVs = 4313,
        [Display(Name = "Аудіотехніка")]
        Audio_Equipment = 4314,
        [Display(Name = "Відеотехніка")]
        Video_Equipment = 4315,

        // Одяг та взуття N = 32

        [Display(Name = "Жіночий одяг")]
        Womens_clothing = 4321,
        [Display(Name = "Жіноче взуття")]
        Womens_shoes = 4322,

        [Display(Name = "Чоловічий одяг")]
        Man_clothing = 4323,
        [Display(Name = "Чоловіче взуття")]
        Man_shoes = 4324,

        [Display(Name = "Дитячий одяг")]
        Children_clothing = 4325,
        [Display(Name = "Дитяче взуття")]
        Children_shoes = 4326,

        // Домашні товари N = 33



        // Спорт та відпочинок N = 34



        // Краса та догляд N = 35



        // Їжа та напої N = 36



        // Автомобілі та мотоцикли N = 37



        // Товари для тварин N = 38



        // Інструменти та обладнання N = 39



        // Книги та медіа N = 310


    }
}
