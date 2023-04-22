using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System.Net.Http.Json;
using RandomProject1.GrapQLConsumer;
using RandomProject1.RestApiConsumer;

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

            labelLoading.Visible = false;
        }

        private async void GetCountriesBtn_Click(object sender, EventArgs e)
        {
            labelLoading.Visible = true;
            listBox1.Items.Clear();

            var selectedContinentCode = comboBoxContinent.SelectedValue.ToString();
            var countries = await GetCountriesByContinentCode(selectedContinentCode);
            var countryCodes = GetRandomCountriesByCode(countries);
            var allCountryInfo = await GetCountryInfo(countryCodes);
            var sortedCountries = allCountryInfo.OrderBy(x => x.Name.official);
            if (sortedCountries.Any() == false)
            {
                listBox1.Items.Add($"No country information found!");
                listBox1.Items.Add("");
            }
            else
            {
                foreach (var sortedCountry in sortedCountries)
                {
                    DisplayCountryInfo(listBox1, sortedCountry);
                }
            }
            labelLoading.Visible = false;
        }



        private async Task<List<Country>> GetCountriesByContinentCode(string selectedContinentCode)
        {
            var graphQLClient = new GraphQLHttpClient("https://countries.trevorblades.com/graphql", new SystemTextJsonSerializer());

            var graphQLRequest = new GraphQLRequest
            {
                Query = @"
                query ($continentCode: ID!) {
                    continent(code: $continentCode) {
                        countries {
                            name
                            code
                        }
                    }
                }",
                Variables = new { continentCode = selectedContinentCode }
            };

            var graphQLResponse = await graphQLClient.SendQueryAsync<ContinentResponse>(graphQLRequest);
            return graphQLResponse.Data.Continent.Countries;
        }

        private string GetRandomCountriesByCode(List<Country> countries)
        {
            var random = new Random();
            var selectedCountries = countries.OrderBy(x => random.Next()).Take((int)numericUpDown1.Value);
            return string.Join(",", selectedCountries.Select(x => x.Code));
        }

        private async Task<List<CountryInfo>> GetCountryInfo(string countryCodes)
        {
            var apiClient = new HttpClient();
            var apiUrl = $"https://restcountries.com/v3.1/alpha?codes={countryCodes}";
            var apiResponse = await apiClient.GetAsync(apiUrl);
            if (apiResponse.IsSuccessStatusCode)
            {
                var content = await apiResponse.Content.ReadFromJsonAsync<List<CountryInfo>>();
                return content;
            }
            return null;
        }

        private void DisplayCountryInfo(ListBox listBox, CountryInfo sortedCountry)
        {
            listBox.Items.Add($"Official name: {sortedCountry.Name.official}");
            listBox.Items.Add($"Capital: {sortedCountry.Capital?.First() ?? "Unknown"}");
            listBox.Items.Add($"Population: {(sortedCountry.Population > 0 ? sortedCountry.Population.ToString() : "Unknown")}");
            listBox.Items.Add($"Currency: {(sortedCountry.Currencies?.Any() == true ? string.Join(", ", sortedCountry.Currencies.Select(x => x.Value.Name)) : "Unknown")}");
            listBox.Items.Add($"Subregion: {(string.IsNullOrWhiteSpace(sortedCountry.Subregion) ? "Unknown" : sortedCountry.Subregion)}");
            listBox.Items.Add($"Languages: {(sortedCountry.Languages?.Any() == true ? string.Join(", ", sortedCountry.Languages.Select(x => x.Value)) : "Unknown")}");
            listBox.Items.Add("");
        }
    }
}