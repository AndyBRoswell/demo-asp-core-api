namespace main.Models {
    using Microsoft.EntityFrameworkCore;

    public class product_database_context : DbContext {
        public product_database_context(DbContextOptions<product_database_context> options) : base(options) {}
        public DbSet<product> products { get; set; } = null!;
    }
}
