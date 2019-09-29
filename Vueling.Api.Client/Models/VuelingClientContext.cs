using Microsoft.EntityFrameworkCore;

namespace Vueling.Api.Client.Models
{
    public partial class VuelingClientContext : DbContext
    {
        public VuelingClientContext()
        {
        }

        public VuelingClientContext(DbContextOptions<VuelingClientContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Passengers> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passengers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Flight)
                    .IsRequired()
                    .HasColumnName("flight")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Seat)
                    .IsRequired()
                    .HasColumnName("seat")
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });
        }
    }
}
