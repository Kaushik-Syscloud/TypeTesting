namespace TypeTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1234" && textBox2.Text == "4444")
                MessageBox.Show("WELCOME!!!");
            else
                MessageBox.Show("Invalid Credentials");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}