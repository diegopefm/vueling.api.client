using Microsoft.EntityFrameworkCore;

namespace Vueling.Data.Models
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Passenger> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Seat)
                    .IsRequired()
                    .HasColumnName("seat")
                    .HasMaxLength(5);

                entity.Property(e => e.Flight)
                    .IsRequired()
                    .HasColumnName("flight")
                    .HasMaxLength(10);
            });
        }
    }
}
