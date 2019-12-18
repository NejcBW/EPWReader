using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPWDemo.EPWReader
{
    public class EPWDB
    {
        public List<Location> Locations = new List<Location>
            {
            new Location() {City = "Brussels", Country="Belgium"},
            new Location("Oostende","Belgium"),
            new Location("HCMC","Vietnam"),
            new Location("Hanoi","Vietnam"),
            };
        public void getCityList()
        {
            List<string> cityList = new List<string>();
            Locations.ForEach(x => cityList.Add(x.City));

            foreach (var city in cityList)
            {
                Console.WriteLine(city);
            }
            return;
        }
        public List<string> getCountryList()
        {
            List<string> countryList = new List<string>();
            Locations.ForEach(x => countryList.Add(x.Country));

            return countryList.Distinct().ToList();
        }
    }

    public class Location
    {
        public string City;
        public string Country;

        public Location()
        {
            City = "";
            Country = "";
        }
        public Location(string city, string country)
        {
            City= city;
            Country = country;
        }
    }
}
