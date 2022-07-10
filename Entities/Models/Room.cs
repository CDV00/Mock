using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Room : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public ICollection<MessageChat> MessageChats { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }

    public enum ChatType
    {
        group,
        @private
    }
}
