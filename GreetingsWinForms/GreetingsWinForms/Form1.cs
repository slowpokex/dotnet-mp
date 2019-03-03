namespace GreetingsWinForms
{
    using System;
    using System.Windows.Forms;
    using GreetingClassLibrary;

    public partial class Form1 : Form
    {
        public Form1() => this.InitializeComponent();

        private void button1_Click(object sender, EventArgs e) => MessageBox.Show(
                SharedGreetings.GetGreeting(this.textBox1.Text, this.textBox2.Text),
                "Result of survey",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk
            );
    }
}
