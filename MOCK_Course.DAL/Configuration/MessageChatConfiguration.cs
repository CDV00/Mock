using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;
using Entities.Models;

namespace Course.DAL.Configuration
{
    internal class MessageChatConfiguration : IEntityTypeConfiguration<MessageChat>
    {
        public void Configure(EntityTypeBuilder<MessageChat> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
