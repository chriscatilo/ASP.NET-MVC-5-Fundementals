using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PrefSurf.Counters
{
    public class PerfCounterWrapper
    {
        private readonly PerformanceCounter _counter;

        public PerfCounterWrapper(string name, string category, string counter, string instance = "")
        {
            _counter = new PerformanceCounter(category, counter, instance, readOnly: true);

            Name = name;
        }

        public string Name { get; set; }

        public float Value => _counter.NextValue();
    }

    public class PerfCounterService
    {
        private List<PerfCounterWrapper> _counters;

        public PerfCounterService()
        {
            _counters = new List<PerfCounterWrapper>
            {
                new PerfCounterWrapper("Processor", "Processor", "% Processor Time", "_Total"),
                new PerfCounterWrapper("Paging", "Memory", "Pages/sec"),
                new PerfCounterWrapper("Disk", "PhysicalDisk", "% Disk Time", "_Total")
            };
        }

        public dynamic GetResults()
        {
            return _counters.Select(c => new {name = c.Name, value = c.Value});
        }

    }
}