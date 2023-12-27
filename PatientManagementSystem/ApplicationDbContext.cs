using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Models;

namespace PatientManagementSystem
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<LabBilling> LabBillings { get; set; }
        public DbSet<PharmacyBilling> PharmacyBillings { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LabBilling>()
                .Property(l => l.Amount)
                .HasColumnType("decimal(18, 2)"); // Specify the appropriate precision and scale

            modelBuilder.Entity<PharmacyBilling>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)");
            // Other configurations for your entities...

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<PatientManagementSystem.Models.ViewModelForDashboard> ViewModelForDashboard { get; set; }
    }


}