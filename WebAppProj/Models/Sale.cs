using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterProj.Models
{
    public class Sale
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }
              
        
        public int TitleID { get; set; }

        public Title Title { get; set; }

        public int StoreID { get; set; }

        public Store Store { get; set; }


        // Check if Presented in view/create/delete
        [DataType(DataType.Currency)]
        public decimal? TotalSalePrice
        {
            get {
                if (Title == null)
                    return 0;
                return Quantity * Title.Price; }

        }

    }
}