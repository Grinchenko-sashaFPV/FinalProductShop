using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Repositories.Implementation.Manager
{
    public partial class ModelsManager : DbContext
    {
        public ModelsManager() : base("name=ModelsManager") { }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
