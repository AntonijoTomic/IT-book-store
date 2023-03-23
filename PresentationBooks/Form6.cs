using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kvnet;
namespace PresentationBooks
{
    public partial class Form6 : Form
    {
        repository re = new repository();

        public Form6()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.CharacterCasing = CharacterCasing.Lower;
            textBox2.TextAlign = HorizontalAlignment.Left;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            

            foreach(Admin a in re.Login())
            {
                
                if (textBox1.Text == a.username && textBox2.Text == a.password)
                {
                    Form1 forma = new Form1();
                    this.Hide();
                    forma.Show();
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Unijeli ste netočne podatke!!",
          "Upozorenje", MessageBoxButtons.OK);
                }
            }
          }
    }
}
