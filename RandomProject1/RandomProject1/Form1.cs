using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using RandomProject1.Models;
using System.Net.Http.Json;
using static RandomProject1.Models.GraphQLContinentsResponse;
using static RandomProject1.Models.RestApiResponse;

namespace RandomProject1
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> continentDictionary = new Dictionary<string, string>
        {
            { "AF", "Africa" },
            { "AN", "Antarctica" },
            { "AS", "Asia" },
            { "EU", "Europe" },
            { "NA", "North America" },
            { "OC", "Oceania" },
            { "SA", "South America" }
        };

        public Form1()
        {
            InitializeComponent();

            comboBoxContinent.DataSource = new BindingSource(continentDictionary, null);
            comboBoxContinent.DisplayMember = "Value";
            comboBoxContinent.ValueMember = "Key";

            numericUpDown1.Minimum = 2;
            numericUpDown1.Maximum = 10;
        }

        private async void GetCountriesBtn_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            var selectedContinentCode = comboBoxContinent.SelectedValue.ToString();

            var graphQLClient = new GraphQLHttpClient("https://countries.trevorblades.com/graphql", new SystemTextJsonSerializer());

            var graphQLRequest = new GraphQLRequest
            {
                Query = @"
                query ($continentCode: ID!) {
                    continent(code: $continentCode) {
                        countries {
                            name
                        }
                    }
                }",
                Variables = new { continentCode = selectedContinentCode }
            };

            var graphQLResponse = await graphQLClient.SendQueryAsync<ContinentResponse>(graphQLRequest);
            var countries = graphQLResponse.Data.Continent.Countries;

            var random = new Random();
            var selectedCountries = countries.OrderBy(x => random.Next()).Take((int)numericUpDown1.Value);
            var allCountryInfo = new List<CountryInfo>();

            foreach (var country in selectedCountries)
            {
                string countryName = country.Name;
                string apiUrl = $"https://restcountries.com/v3.1/name/{countryName}";

                var apiClient = new HttpClient();
                var apiResponse = await apiClient.GetAsync(apiUrl);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var content = await apiResponse.Content.ReadFromJsonAsync<List<CountryInfo>>();
                    foreach (var data in content)
                    {
                        allCountryInfo.Add(data);
                    }
                }
                else
                {
                    listBox1.Items.Add($"No country information found for '{country.Name}'.");
                    listBox1.Items.Add("");
                }


            }

            var sortedCountries = allCountryInfo.OrderBy(x => x.Name.official);
            foreach (var sortedCountry in sortedCountries)
            {
                listBox1.Items.Add($"Official name: {sortedCountry.Name.official}");
                listBox1.Items.Add($"Capital: {sortedCountry.Capital?.First() ?? "Unknown"}");
                listBox1.Items.Add($"Population: {(sortedCountry.Population > 0 ? sortedCountry.Population.ToString() : "Unknown")}");
                //listBox1.Items.Add($"Currency: {sortedCountry.CurrenciesList.Select(x => x.Value)}");
                listBox1.Items.Add($"Subregion: {(string.IsNullOrWhiteSpace(sortedCountry.Subregion) ? "Unknown" : sortedCountry.Subregion)}");
                listBox1.Items.Add($"Languages: {(sortedCountry.Languages?.Any() == true ? string.Join(", ", sortedCountry.Languages.Select(x => x.Value)) : "Unknown")}");
                listBox1.Items.Add("");
            }
        }
    }
}