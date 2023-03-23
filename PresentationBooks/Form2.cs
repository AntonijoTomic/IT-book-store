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
    public partial class Form2 : Form
    {
        repository re = new repository();
        string idd ="0";
        string ai="0";
        int stanje =0; 
        BindingSource _tableBindingSource2 = new BindingSource();
        List<Kknjiga> Posjedujem = new List<Kknjiga>();
        
                
public Form2()
        {
            InitializeComponent();
            this.MaximizeBox = false;

            BindingSource _tableBindingSource = new BindingSource();
            foreach (Kknjiga k in re.GetBooksFromBaseKK())
            {
                if (k.stanje > 0)
                {
                    Posjedujem.Add(k);
                }
            }

            _tableBindingSource2.DataSource = Posjedujem;
            _tableBindingSource.DataSource = re.GetKorisnik();
            dataGridView1.DataSource = _tableBindingSource;
            dataGridView2.DataSource = _tableBindingSource2;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            var buttonColumn = new DataGridViewButtonColumn()
            {
                Name = "statusButton",
                HeaderText = "Posudi",
                UseColumnTextForButtonValue = false,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    NullValue = "Posudi"
                }
            };
            this.dataGridView2.Columns.Add(buttonColumn);
            var buttonColumn2 = new DataGridViewButtonColumn()
            {
                Name = "statusButton",
                HeaderText = "Odaberi",
                UseColumnTextForButtonValue = false,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    NullValue = "Odaberi"

                }
            };
            this.dataGridView1.Columns.Add(buttonColumn2);
            DataGridViewImageColumn button = new DataGridViewImageColumn();
            button.HeaderText = "Obriši";
            button.Width = 20;
            button.Image = Image.FromFile("C:/Users/smece.png");
            button.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns.Add(button);
            dataGridView1.Hide();

        }
      
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Kknjiga> a = new List<Kknjiga>();
           
            Kknjiga knjigica = new Kknjiga();
            a = re.GetBooksFromBaseKK();
            int index = e.RowIndex;
            dataGridView2.Rows[index].Selected = true;
            DataGridViewRow selectedRow = dataGridView2.Rows[index];

            knjigica.title = selectedRow.Cells[0].Value.ToString();
            knjigica.subtitle = selectedRow.Cells[1].Value.ToString();
            knjigica.price = selectedRow.Cells[2].Value.ToString();
            knjigica.isbn13 = selectedRow.Cells[3].Value.ToString();
            if (dataGridView2[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                foreach (Kknjiga s in a)
                {
                    if (s.title == knjigica.title)
                    {
                        idd = s.isbn13;
                        stanje = s.stanje;
                    }
                }
                button3.Hide();
                dataGridView2.Hide();
                dataGridView1.Show();
            }
            else if (dataGridView2.CurrentCell.ColumnIndex.Equals(6) && e.RowIndex != -1)
                {
                foreach (Kknjiga s in a)
                {
                    if (s.title == knjigica.title)
                    {
                        idd = s.isbn13;
                    }
                }
                PK O = new PK();
                
                    foreach (PK kk in O.GetBooksFromBase())
                    {
                        foreach (Kknjiga s in a)
                        {
                            if (s.isbn13 == knjigica.isbn13)
                            {
                                            
                                if (idd == kk.isbn13_knjiga)
                                {
                                    MessageBox.Show("Ne možete obrisati knjigu koju ste posudili!!!!");
                                    return;
                                }
                                else
                                {
                                    ai = s.isbn13;
                                }
                            }
                        }
                    
                }
               
                re.Remove(ai);
                DialogResult dialog = MessageBox.Show("Uspiješno ste obrisali knjigu iz knjižnice",
    "Brisanje knjige", MessageBoxButtons.OK);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView1[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                int index = e.RowIndex;
                int n = dataGridView1.Rows.Count;
                dataGridView2.Columns[index].Selected = true;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                string broj = selectedRow.Cells[0].Value.ToString();
                MessageBox.Show("Uspijesno ste POSUDILI knjigu!");
                re.AddBook3(idd, broj);
                re.StanjeMinus(idd, stanje);
               //MessageBox.Show(selectedRow.Cells[0].Value.ToString());
               // MessageBox.Show(selectedRow.Cells[1].Value.ToString());
               // MessageBox.Show(selectedRow.Cells[2].Value.ToString());
               // MessageBox.Show(selectedRow.Cells[3].Value.ToString());
               // MessageBox.Show(selectedRow.Cells[4].Value.ToString());
                //MessageBox.Show(selectedRow.Cells[5].Value.ToString());///kreirati knjigu vratit ju dohvatit na korisnika i cao
            }

        }





        private void button1_Click(object sender, EventArgs e)
        {


    
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form3 posudene = new Form3();
           
            posudene.Show();
 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form4 korisnici = new Form4();

            korisnici.Show();
            this.Hide();
        }

      

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Zelite li stvarno izaci?", 
                "Izlazak iz aplikacije", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                Application.ExitThread();

            }
            else if(dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Kknjiga> Posjedujem2 = new List<Kknjiga>();
            foreach (Kknjiga k in re.GetBooksFromBaseKK())
            {
                if (k.stanje > 0)
                {
                    Posjedujem2.Add(k);
                }
            }
            _tableBindingSource2.DataSource = Posjedujem2;
            dataGridView2.DataSource = _tableBindingSource2;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Update();
            dataGridView2.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView2.Show();
            button3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 jedinica = new Form1();
            this.Hide();
            jedinica.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 forma = new Form5();
            this.Hide();
            forma.Show();
        }
    }
}
