using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; } // DbSet para tarefas

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.User)  // Define a relação de TaskItem para ApplicationUser
            .WithMany(u => u.Tasks) // Define a relação de ApplicationUser para TaskItems
            .HasForeignKey(t => t.UserId); // Configura a chave estrangeira
    }
}

