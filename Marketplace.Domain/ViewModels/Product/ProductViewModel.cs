using Marketplace.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Введіть ім'я")]
        [MinLength(2, ErrorMessage = "Мінімальна довжина має бути більшою за два символи")]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        [MinLength(16, ErrorMessage = "Мінімальна довжина повинна бути більше 50 символів")]
        public string Description { get; set; }

        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Вкажіть ціну")]
        public decimal Price { get; set; }

        public string DateCreate { get; set; }

        [Display(Name = "Категорія")]
        [Required(ErrorMessage = "Оберіть категорію")]
        public string Category { get; set; }

        [Display(Name = "Підкатегорія")]
        [Required(ErrorMessage = "Оберіть підкатегорію")]
        public string SubCategory { get; set; }

        public string? OwnerName { get; set; }

        public IList<IFormFile> Photo { get; set; }

        public List<ProductPhoto>? Image { get; set; }

        [Display(Name = "Це аукціон")]
        public string? isAuction { get; set; }

        [Display(Name = "Коли закінчиться аукціон")]
        public DateTime? EndingAuction { get; set; }    
        
        public List<ChatMessage>? ChatMessages { get; set; }

    }
}
