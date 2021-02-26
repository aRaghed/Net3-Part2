using System;
using System.Collections.Generic;

namespace WeatherDb.Model
{
    public interface IPOCOClass { }

    public class WeatherForecast : IPOCOClass
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //Foreign key Person.TeamId -> Team.Id
        public int TeamId { get; set; }

        //Navigation property, browsing property
        public Team Team { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //Navigation property, browsing property
        public ICollection<Person> People { get; set; } = new HashSet<Person>();
    }
}