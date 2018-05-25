using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterProj.Models
{
    public class Title
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title Name")]
        public string TitleName { get; set; }

        //check what is         [Column(TypeName = "money")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public int AuthorID { get; set; }

        public Author Author { get; set; }

        public int GenreID { get; set; }

        public Genre Genre { get; set; }




    }
}