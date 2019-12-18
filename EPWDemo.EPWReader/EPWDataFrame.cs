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

        public EPWDataFrame MonthlyAverage()
        {

            var dataFrame = new EPWDataFrame
            {
                Location = new EPWLocation(),
                //Date = new DateTime[8760],
                DryBulb = new double[12],
                DewPoint = new double[12],
                RelHum = new double[12],
                Pressure = new double[12]
            };

            System.Collections.Generic.List<int> days_month = new System.Collections.Generic.List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            //sum all values
            for (int i = 0; i < 8760; i++)
            {
                dataFrame.DryBulb[Date[i].Month - 1] += DryBulb[i];
                dataFrame.DewPoint[Date[i].Month - 1] += DewPoint[i];
                dataFrame.RelHum[Date[i].Month - 1] += RelHum[i];
                dataFrame.Pressure[Date[i].Month - 1] += Pressure[i];
            }

            //divided summed values by number of hours in each month
            for (int i = 1; i < 13; i++)
            {
                dataFrame.DryBulb[i - 1] = System.Math.Round(dataFrame.DryBulb[i - 1] / (days_month[i - 1] * 24), 1);
                dataFrame.DewPoint[i - 1] = System.Math.Round(dataFrame.DewPoint[i - 1] / (days_month[i - 1] * 24), 1);
                dataFrame.RelHum[i - 1] = System.Math.Round(dataFrame.RelHum[i - 1] / (days_month[i - 1] * 24), 1);
                dataFrame.Pressure[i - 1] = System.Math.Round(dataFrame.Pressure[i - 1] / (days_month[i - 1] * 24), 1);
            }

            return dataFrame;
        }
    }
}
