using Microsoft.EntityFrameworkCore;
using MozoVirtualMicroservicios.Login.Infrastucture.Models;

namespace MozoVirtualMicroservicios.Login.Infrastucture
{
    public class EmployeeDbContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
  
        protected readonly IConfiguration Configuration;
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://docs.microsoft.com/en-us/ef/core/modeling/
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        #endregion

    }
}
