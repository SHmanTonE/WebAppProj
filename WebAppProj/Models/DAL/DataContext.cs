using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MasterProj.Models.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Genre> Generes { get; set; }
    }
}
