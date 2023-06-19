using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace W_Machine.Models
{
    public class Drink
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Введите наименование товара")]
        [Display(Name = "Товар")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Введите описание товара")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Введите количество")]
        [Display(Name = "Количество")]
        public int iCount { get; set; }
        [Display(Name = "Товар доступен")]
        public bool BThereIsDrink { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
