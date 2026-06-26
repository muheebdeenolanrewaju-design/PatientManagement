using Microsoft.EntityFrameworkCore;
using PatientManagement.Models;

namespace PatientManagement.Data;

    public class ApplicationDbContext : DbContext

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientRecord>()
                .HasOne(x => x.Patient)
                .WithMany(x => x.PatientRecords)
                .HasForeignKey(x => x.PatientId);

            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Patient> Patients { get; set; }
        
        public DbSet<PatientRecord> PatientRecords { get; set; }
    }
