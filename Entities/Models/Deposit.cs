using Course.DAL.Models;
using System;

namespace Entities.Models
{
    public class Deposit : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public decimal Amount { get; set; }
    }
}
