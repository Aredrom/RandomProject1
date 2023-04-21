namespace RandomProject1.Models
{
    public class RestApiResponse
    {
        public class CountryInfo
        {
            public string OfficialName { get; set; }
            public List<string> Capital { get; set; }
            public int Population { get; set; }
            public string Currency { get; set; }
            public string Subregion { get; set; }
            public Dictionary<string, string> Languages { get; set; }
        }
    }
}
