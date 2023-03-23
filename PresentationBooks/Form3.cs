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
    public partial class Form3 : Form
    {
        repository re = new repository();
        public Form3()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            BindingSource _tableBindingSource3 = new BindingSource();
            PK sup = new PK();
            _tableBindingSource3.DataSource = sup.GetBookById();
            dataGridView1.DataSource = _tableBindingSource3;
            dataGridView1.AutoGenerateColumns = false;
            //DataGridViewImageColumn button = new DataGridViewImageColumn();
            //button.Width = 20;
            //button.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dataGridView1.Columns.Add(button);
            var buttonColumn2 = new DataGridViewButtonColumn()
            {
                Name = "statusButton",
                HeaderText = "Odaberi",
                UseColumnTextForButtonValue = false,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    NullValue = "vrati"

                }
            };
            this.dataGridView1.Columns.Add(buttonColumn2);
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1)
            {
              
                List<Kknjiga> a = new List<Kknjiga>();
                a = re.GetBooksFromBaseKK();
               
                
                int index = e.RowIndex;
                dataGridView1.Rows[index].Selected = true;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                PK p = new PK();
                
                string l = selectedRow.Cells[2].Value.ToString();
                int z = p.Korisnik(l);
                string n = selectedRow.Cells[0].Value.ToString();
                foreach (PK kp in p.GetBooksFromBase())
                {
                    foreach (Kknjiga kk in a)
                    {
                        if (kk.title == n && kp.id_korisnik == z && kk.isbn13 == kp.isbn13_knjiga)
                        {
                            knjiga k = new knjiga();
                            k.title = kk.title;
                            k.subtitle = kk.subtitle;
                            k.price = kk.isbn13;
                            k.isbn13 = kk.price;
                            re.VratiKnjigu(kp.id);
                            re.Edit(k, kk.stanje);
                            MessageBox.Show("Uspijesno ste vratili knjigu");
                            this.Close();
                            Form3 s = new Form3();
                            s.Show();
                            return;
                        }
                    }


                }
                
            }

        }
     
    }
}
