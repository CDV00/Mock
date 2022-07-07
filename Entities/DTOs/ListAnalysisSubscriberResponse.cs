using System.Collections.Generic;

namespace Entities.DTOs
{
    public class ListAnalysisSubscriberResponse
    {
        public decimal Total { get; set; }
        public List<List<AnalysisSubscriberDTO>> Data { get; set; }
    }

    public class AnalysisSubscriberDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}
