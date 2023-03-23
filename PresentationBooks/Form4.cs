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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            korisnik novi = new korisnik();
            repository u1 = new repository();
            string myString = textBox4.Text;
           
            int a = myString.ToString().Length;
            var stringNumber = textBox4.Text;
            long numericValue;
            bool isNumber = long.TryParse(stringNumber, out numericValue);
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Unesite sva polja!!!!!!");
            }
            else if(a != 11)
            {
                MessageBox.Show("Oib mora sadrzavati 11 brojeva");
            }
            else if (isNumber == false)
            {
                MessageBox.Show("Oib ne smije sadrzavati slova");
            }
            else
            {
                novi.ime = textBox1.Text;
                novi.prezime = textBox2.Text;
                novi.adresa = textBox3.Text;
                novi.oib = textBox4.Text;
               u1.AddKorisnik(novi);
                DialogResult dialog = MessageBox.Show("Uspiješno ste dodali korisnika",
   "Dodavanje korisnika", MessageBoxButtons.OK);
            }
           

        }
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 open = new Form2();
            this.Hide();
            open.Show();
        }

        private void Form4_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Zelite li stvarno izaci?",
                "Message", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Application.ExitThread();

            }
            else if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
