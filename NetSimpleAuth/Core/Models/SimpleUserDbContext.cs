namespace Core.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SimpleUserDbContext : DbContext
    {
        public SimpleUserDbContext()
            : base("name=SimpleUserDbContext")
        {
        }

        public virtual DbSet<LoginUser> LoginUser { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
