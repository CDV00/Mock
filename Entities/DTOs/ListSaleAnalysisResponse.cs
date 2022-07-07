using System;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class ListSaleAnalysisResponse
    {
        public List<SaleAnalysisDTO> Data { get; set; }
        public decimal Total { get; set; }
    }

    public class SaleAnalysisDTO
    {
        public DateTime Date { get; set; }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return this.Date.DayOfWeek;
            }
        }
        public string Name
        {
            get
            {
                return this.Date.DayOfWeek.ToString().Substring(0, 3);
            }
        }
        public decimal Sale { get; set; }
    }
}
