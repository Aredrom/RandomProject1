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
    }
}