using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class MyCoursesDTO
    {
      public Guid Id { get; set; }
      public string Title { get; set; }
      public DateTime CreatedAt { get; set; }
      public int Sale { get; set; }
      public int Parts { get; set; }
      public string Category { get; set; }
      public bool Status { get; set; }
    }
}
