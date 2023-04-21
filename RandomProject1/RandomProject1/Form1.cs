using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using RandomProject1.Models;
using static RandomProject1.Models.GraphQLContinentsResponse;

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

            listBox1.Items.Add(countries.Count);
        }
    }
}