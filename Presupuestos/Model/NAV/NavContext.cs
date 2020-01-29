namespace Model.NAV
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NavContext : DbContext
    {
        public NavContext()
            : base("name=NavContext")
        {
        }

        public virtual DbSet<SPECIAL_FRUITS___VEGETABLE_G_L_Account> SPECIAL_FRUITS___VEGETABLE_G_L_Account { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SPECIAL_FRUITS___VEGETABLE_G_L_Account>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<SPECIAL_FRUITS___VEGETABLE_G_L_Account>()
                .Property(e => e.Unit_Cost)
                .HasPrecision(38, 20);

            modelBuilder.Entity<SPECIAL_FRUITS___VEGETABLE_G_L_Account>()
                .Property(e => e.Unit_Price)
                .HasPrecision(38, 20);
        }
    }
}
