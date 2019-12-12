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









    }
}
