using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        [MaxLength]
        public string Message { get; set; }
        public int Level { get; set; }
    }
}
