using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class NotificationConfiguration: IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {


            // 1-n:user-notifi
            builder.HasOne(c => c.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(c => c.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
            ;


        }
    }
}

