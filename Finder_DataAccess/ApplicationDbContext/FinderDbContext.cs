using Finder_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finder_DataAccess.ApplicationDbContext
{
   public class FinderDbContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        
        }

        public DbSet<Pharmacy> Pharmacies { get; set; }
    }
}
