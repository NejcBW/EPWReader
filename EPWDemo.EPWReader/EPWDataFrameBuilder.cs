using System;
using System.Diagnostics;

namespace EPWDemo.EPWReader
{
    public class EPWDataFrameBuilder
    {
        public EPWDataFrameBuilder()
        {
            // do nothing
        }

        public EPWDataFrame Build(string filePath)
        {
            string[] lines;

            var dataFrame = new EPWDataFrame
            {
                Location = new EPWLocation(),
                Date = new DateTime[8760],
                DryBulb = new double[8760],
                DewPoint = new double[8760],
                RelHum = new double[8760],
                Pressure = new double[8760]
            };

            var sw = new Stopwatch();
            try
            {
                sw.Start();
                lines = System.IO.File.ReadAllLines(filePath);
                sw.Stop();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception thrown");

                throw new ApplicationException("Exception message", ex);
            }

            // Parsing LOCATION header record
            {
                string[] fields = lines[0].Split(',');
                dataFrame.Location.City = fields[1];
                dataFrame.Location.StateProvReg = fields[2];
                dataFrame.Location.Country = fields[3];
                dataFrame.Location.Source = fields[4];
                dataFrame.Location.WMO = fields[5];
                dataFrame.Location.Latitude = double.Parse(fields[6]);
                dataFrame.Location.Longitude = double.Parse(fields[7]);
                dataFrame.Location.TimeZone = double.Parse(fields[8]);
                dataFrame.Location.Elevation = double.Parse(fields[9]);
            }
            
            DateTime[] Date = new DateTime[8760];
            double[] dryBulb = new double[8760];
            double[] dewPoint = new double[8760];
            double[] relHum = new double[8760];
            double[] pressure = new double[8760];

            int headerLength = 8;

            DateTime startDate = new DateTime(2002,1,1,00,00,00);

            // Parsing DATA PERIODS
            for (int i = headerLength; i < headerLength + 8760; i++)
            {
                DateTime currentHour = startDate.AddHours(i - headerLength);

                var j = i - headerLength;
                string[] fields = lines[i].Split(',');
                
                Date[j] = currentHour;
                dryBulb[j] = double.Parse(fields[6]);
                dewPoint[j] = double.Parse(fields[7]);
                relHum[j] = double.Parse(fields[8]);
                pressure[j] = double.Parse(fields[9]);
            }

            dataFrame.Date = Date;
            dataFrame.DryBulb = dryBulb;
            dataFrame.DewPoint = dewPoint;
            dataFrame.RelHum = relHum;
            dataFrame.Pressure = pressure;

            System.Console.WriteLine($"EPW file reading: {sw.Elapsed}");

            return dataFrame;
        }

        public EPWDataFrame MonthlyAverage(EPWDataFrame epw)
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

            System.Collections.Generic.List<int> days_month = new System.Collections.Generic.List<int> {31,28,31,30,31,30,31,31,30,31,30,31};
            
            //sum all values
            for (int i = 0; i < 8760; i++)
            {
                dataFrame.DryBulb[epw.Date[i].Month-1]+=epw.DryBulb[i];
                dataFrame.DewPoint[epw.Date[i].Month-1]+=epw.DewPoint[i];
                dataFrame.RelHum[epw.Date[i].Month-1]+=epw.RelHum[i];
                dataFrame.Pressure[epw.Date[i].Month-1]+=epw.Pressure[i];
            }

            //divided summed values by number of hours in each month
            for (int i = 1;i<13;i++)
            {
                dataFrame.DryBulb[i-1] = System.Math.Round(dataFrame.DryBulb[i-1] / (days_month[i-1]*24), 1);
                dataFrame.DewPoint[i-1] = System.Math.Round(dataFrame.DewPoint[i-1] / (days_month[i-1]*24), 1);
                dataFrame.RelHum[i-1] = System.Math.Round(dataFrame.RelHum[i-1] / (days_month[i-1]*24), 1);
                dataFrame.Pressure[i-1] = System.Math.Round(dataFrame.Pressure[i-1] / (days_month[i-1]*24), 1);
            }

            return dataFrame;
        }
    }
}
