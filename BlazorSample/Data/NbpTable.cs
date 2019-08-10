using System;
using System.Collections.Generic;

namespace BlazorSample.Data
{
    public class NbpTable
    {
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public IEnumerable<NbpRate> Rates { get; set; }
    }
}
