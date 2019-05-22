namespace InventoryManager
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model12")
        {
            this.Database.CreateIfNotExists();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Model1>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsFixedLength();

            modelBuilder.Entity<Subcategory>()
                .HasRequired(c => c.Category)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .Property(c => c.Name)
                .IsFixedLength();

            modelBuilder.Entity<Subcategory>()
                .Property(c => c.Name)
                .IsFixedLength();

        }
    }
}
