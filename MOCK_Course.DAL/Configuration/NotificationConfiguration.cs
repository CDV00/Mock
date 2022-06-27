using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    internal class NotificationConfiguration: IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {


            // 1-n:user-notifi
        }
    }
}

