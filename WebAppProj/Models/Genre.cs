using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterProj.Models
{
    public class Genre
    {
        public int ID { get; set; }

        [Display(Name = "Genre Type")]
        public string GenreType { get; set; }

        // need to create genre types before starting the web app
    }
}