using Microsoft.EntityFrameworkCore;
using ViberMessageSenderAPI.Models;

namespace ViberMessageSenderAPI.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<PhoneSender> PhoneSenders { get; set; }
        public DbSet<PhoneReceiver> PhoneReceivers { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneSender>()
                               .HasOne(p => p.PhoneReceiver)
                               .WithMany()
                               .HasForeignKey(p => p.PhoneReceiverId)
                               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
