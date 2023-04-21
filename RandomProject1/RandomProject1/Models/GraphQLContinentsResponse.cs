using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomProject1.Models
{
    public class GraphQLContinentsResponse
    {
        public class ContinentResponse
        {
            public Continent Continent { get; set; }
        }

        public class Continent
        {
            public List<Country> Countries { get; set; }
        }

        public class Country
        {
            public string Name { get; set; }
        }
    }
}
