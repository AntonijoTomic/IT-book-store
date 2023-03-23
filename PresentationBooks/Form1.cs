using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kvnet;
namespace PresentationBooks
{
    public partial class Form1 : Form
    {
        public string text { get; set; }
        private repository table = new repository();
        private BindingSource _tableBindingSource = new BindingSource();
        
        public Form1()
        {

            InitializeComponent();
            DataGridViewImageColumn button = new DataGridViewImageColumn();
            button.Width = 20;
            DataGridViewImageColumn button2 = new DataGridViewImageColumn();
            button2.Width = 20;
           // button.Image = Image.FromFile("C:/Users/ikona.jpg");
            //button2.Image = Image.FromFile("C:/Users/slika.png");
            button.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            button2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(button);
           
            dataGridView1.Columns.Add(button2);
            pictureBox1.Hide();

            this.MaximizeBox = false;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            knjiga knjigica = new knjiga();
            repository u1 = new repository();
            int index = e.RowIndex;
            dataGridView1.Rows[index].Selected = true;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(5) && e.RowIndex != -1)
            {
                if (MessageBox.Show("Jeste li sigurni?", "Kupovina knjige", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (selectedRow.Cells[0].Value != null || selectedRow.Cells[1].Value != null || selectedRow.Cells[2].Value != null)
                    {


                        knjigica.title = selectedRow.Cells[0].Value.ToString();
                        knjigica.subtitle = selectedRow.Cells[1].Value.ToString();
                        knjigica.isbn13 = selectedRow.Cells[2].Value.ToString();
                        knjigica.price = selectedRow.Cells[3].Value.ToString();
                        int la = u1.GetBooksFromBaseKK().Count();
                        int l = 0;
                        foreach(Kknjiga kk in u1.GetBooksFromBaseKK())
                        {
                            if(kk.isbn13 == knjigica.price)
                            {
                                u1.Edit(knjigica, kk.stanje);
                                l++;
                            }
                            
                        }
                        if(l ==0)
                        {
                            u1.AddBook1234(knjigica);
                            
                        }
                        string path = @"C:\Users\anton\OneDrive\Dokumenti\datoteka.txt";
                        using (StreamWriter writer = new StreamWriter(path, true))
                        {
                            writer.WriteLine("Kupljena knjiga: " + knjigica.title + " " +DateTime.Now.ToString(" dd/MM/yyyyhh: mm:ss tt"));
                            writer.Close();
                        }
                            MessageBox.Show("Uspijesno ste kupili knjigu!");
                    }
                    else
                    {

                    }
                }
                else
                {

                }


            }
            else if (dataGridView1.CurrentCell.ColumnIndex.Equals(6) && e.RowIndex != -1)
            {
                knjigica.image = selectedRow.Cells[4].Value.ToString();
                pictureBox1.Show();
                pictureBox1.Load(knjigica.image);


            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
            _tableBindingSource.DataSource = table.GetBooks(text);
            dataGridView1.DataSource = _tableBindingSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
       
        }

        private void button2_Click(object sender, EventArgs e)
        {

          
            Form2 kupljenekjige = new Form2();
          
            kupljenekjige.Show();
            this.Hide();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "pretrazi";
                textBox1.ForeColor = Color.Silver;
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "pretrazi")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                DialogResult dialog = MessageBox.Show("Zelite li stvarno izaci?",
                    "Izlazak iz aplikacije", MessageBoxButtons.YesNo);
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
