namespace GreetingsWinForms
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1() => this.InitializeComponent();

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultText;
            if (this.textBox1.Text.Length != 0 && this.textBox2.Text.Length != 0)
            {
                ResultText = $"Your name is {this.textBox1.Text} and you are {this.textBox2.Text} years";
            }
            else
            {
                ResultText = "You should input value!!!";
            }
            MessageBox.Show(
                ResultText,
                "Result of survey",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk
            );
        }
    }
}
