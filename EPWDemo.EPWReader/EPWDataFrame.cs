using System;

namespace EPWDemo.EPWReader
{
    public class EPWDataFrame
    {
        public EPWLocation Location { get; set; }
        public DateTime[] Date {get;set;}
        public double[] DryBulb { get; set; }
        public double[] DewPoint { get; set; }
        public double[] RelHum { get; set; }
        public double[] Pressure { get; set; }
    }
}
