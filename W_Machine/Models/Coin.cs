using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace W_Machine.Models
{
    public class Coin
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CoinID { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Наименование монеты")]
        public string SNameCoin { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Номинал монеты")]
        public string SNameNumberCoin { get; set; }
        [Display(Name = "Количество монет")]
        public int iCountCoin { get; set; }
        [Display(Name = "Монета не доступна")]
        public bool BDontCoin { get; set; }

    }
}
