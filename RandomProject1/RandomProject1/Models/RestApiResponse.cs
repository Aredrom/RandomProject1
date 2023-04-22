using System.Xml.Linq;

namespace RandomProject1.Models
{
    public class RestApiResponse
    {
        public class CountryInfo
        {
            public Name Name { get; set; }
            public List<string> Capital { get; set; }
            public int Population { get; set; }
            public Dictionary<string, Currencies> Currencies { get; set; }
            public string Subregion { get; set; }
            public Dictionary<string, string> Languages { get; set; }
        }
    }
}
