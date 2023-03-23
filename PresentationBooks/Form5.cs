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
    public partial class Form5 : Form
    {
        repository re = new repository();
        korisnik a = new korisnik();
        BindingSource _tableBindingSource = new BindingSource();
        public Form5()
        {
            InitializeComponent();
           
            _tableBindingSource.DataSource = re.GetKorisnik();
            dataGridView1.DataSource = _tableBindingSource;
            DataGridViewImageColumn button = new DataGridViewImageColumn();
            button.Width = 20;
            button.Image = Image.FromFile("C:/Users/edit.png");
            button.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(button);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].HeaderText = "";
            dataGridView1.AutoGenerateColumns = false;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            button2.Hide();
            button1.Hide();
            button3.Hide();
            button4.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            dataGridView1.Rows[index].Selected = true;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(5) && e.RowIndex != -1)
            {
                label5.Hide();
                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                textBox3.Text = selectedRow.Cells[3].Value.ToString();
                textBox1.Show();
                textBox2.Show();
                textBox3.Show();
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                button1.Show();
                button2.Show();
                button3.Show();
                button4.Show();
                a.id = Int32.Parse(selectedRow.Cells[0].Value.ToString());
                a.oib = selectedRow.Cells[4].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            a.ime = textBox1.Text.ToString();
            a.prezime = textBox2.Text.ToString();
            a.adresa = textBox3.Text.ToString();
           
            re.UrediKorisnika(a);
            DialogResult dialog = MessageBox.Show("Uspiješno ste uredili korisnika knjižnice",
                 "Uredivanje korisnika", MessageBoxButtons.OK);
            return;
        }
        PK aw = new PK();
        private void button2_Click(object sender, EventArgs e)
        {
            
                foreach (PK pk in aw.GetBooksFromBase())
                    {
                
                    if(pk.id_korisnik == a.id)
                    {
                        MessageBox.Show("Ne možete izbrisati korisnika koji ima posuđenu knjigu!");
                        return;
                    }
                    else
                    {
                       
                    }
                }
            re.BrisiKorisnika(a.id);
            DialogResult dialog = MessageBox.Show("Uspiješno ste obrisali korisnika iz knjižnice",
       "Brisanje korisnika", MessageBoxButtons.OK);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 dvica = new Form2();
            this.Hide();
            dvica.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<korisnik> lista = new List<korisnik>();
            foreach (korisnik pkk in re.GetKorisnik())
            {
                lista.Add(pkk);
            }
            _tableBindingSource.DataSource = lista;
            dataGridView1.DataSource = _tableBindingSource;
            dataGridView1.AutoGenerateColumns = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dataGridView1.Update();
            dataGridView1.Refresh();
        }
    }
}
