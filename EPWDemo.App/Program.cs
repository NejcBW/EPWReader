using System;
using EPWDemo.EPWReader;

namespace EPWDemo.App
{
    class Program
    {
        static void Main()
        {
            string filePath = @"./EPWDemo.App/files/WAW.epw";

            var builder = new EPWDataFrameBuilder();
            var epw = builder.Build(@filePath);
            System.Console.WriteLine($"City: {epw.Location.City}");
            System.Console.WriteLine($"State: {epw.Location.StateProvReg}");
            System.Console.WriteLine($"Country: {epw.Location.Country}");
            System.Console.WriteLine($"Source: {epw.Location.Source}");
            System.Console.WriteLine($"WMO: {epw.Location.WMO}");
            System.Console.WriteLine($"Latitude: {epw.Location.Latitude}");
            System.Console.WriteLine($"Longitude: {epw.Location.Longitude}");
            System.Console.WriteLine($"Time zone: {epw.Location.TimeZone}");
            System.Console.WriteLine($"Elevation: {epw.Location.Elevation}");
            
            DateTime startDate = new DateTime(2002,1,1,00,00,00);

            for (int i = 0; i < epw.DryBulb.Length; i++)
            {
                DateTime currentHour = startDate.AddHours(i);
                System.Console.WriteLine(currentHour);
                System.Console.WriteLine($"{i+1}: {epw.DryBulb[i]}");
            }

            System.Console.WriteLine();
        }
    }
}
