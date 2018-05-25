using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterProj.Models
{
    public class Store
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string StoreAdress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string StoreCity { get; set; }

        [Required]
        [Display(Name = "State")]
        public string StoreState { get; set; }


        public ICollection<Sale> Sales { get; set; }


    }
}