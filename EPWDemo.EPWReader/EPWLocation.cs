using System;

namespace EPWDemo.EPWReader
{
    public class EPWLocation
    {
        public string City { get; set; }
        public string StateProvReg { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public string WMO { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double TimeZone { get; set; }
        public double Elevation { get; set; }
    }
}