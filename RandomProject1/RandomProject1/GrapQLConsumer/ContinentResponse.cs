namespace RandomProject1.GrapQLConsumer
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
        public string Code { get; set; }
    }
}
