using System;

namespace Application.Plans.Query
{
    public class PlansDto
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public double MinProfitPercent { get;  set; }
        public double MaxProfitPercent { get;  set; }
        public double CurrentProfitPercent { get;  set; }
        public DateTime? LastModified { get; set; }
    }
}