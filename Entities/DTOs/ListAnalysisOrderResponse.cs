using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ListAnalysisOrderResponse 
    {
        public decimal Total { get; set; }
        public List<List<OrderAnalysisDisplayDTO>> Data { get; set; }
    }
    public class OrderAnalysisDisplayDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}
