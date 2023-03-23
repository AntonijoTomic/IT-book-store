using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kvnet
{
    public class PK // klasa za pisanje u bazi i dohvaacanje knjiga
    {
        public int id { get; set; }
        public int id_korisnik { get; set; }
        public string isbn13_knjiga { get; set; }


        public List<PK> GetBooksFromBase()
        {
            List<PK> knjige = new List<PK>();
            string connectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection connection = new SqlConnection(connectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Tomic_PKnjiga";
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        knjige.Add(new PK()
                        {
                            id = (int)reader["id"],
                            id_korisnik = (int)reader["id_korisnik"],
                            isbn13_knjiga = (string)reader["isbn13_knjiga"]
                        }) ; 
                    }
                }
            }

            return knjige;

        }
        public List<pk2> GetBookById()
        {
            korisnik koris = new korisnik();
            pk2 kk = new pk2();
            repository re = new repository();
            Kknjiga knigica = new Kknjiga();
            var kupci = re.GetKorisnik();
            var knjige2 = re.GetBooksFromBaseKK();
            List<pk2> knjiga = new List<pk2>();
            foreach (PK k in GetBooksFromBase())
            {
                knjiga.Add(new pk2()
                {        
                    title = knjige2.Where(c => c.isbn13 == k.isbn13_knjiga).Select(c => c.title).FirstOrDefault(),
                    subtitle = knjige2.Where(c => c.isbn13 == k.isbn13_knjiga).Select(c => c.subtitle).FirstOrDefault(),             
                    korisnik = kupci.Where(c => c.id == k.id_korisnik).Select(c => c.ime + c.prezime).FirstOrDefault()

                });
            }

            return knjiga;
        }
        public int Korisnik(string o)
        {
            repository re = new repository();
            var kupci = re.GetKorisnik();
            pk2 kk = new pk2();
            var knjige2 = re.GetBooksFromBaseKK();
            string l = "";
            int d = -1;
            foreach (PK k in GetBooksFromBase())
            {
                l = kupci.Where(c => c.id == k.id_korisnik).Select(c => c.ime + c.prezime).FirstOrDefault();
                if (l == o)
                {
                    foreach(korisnik kor in kupci.Where(c => c.id == k.id_korisnik))
                    {
                        d =kor.id;
                    }
                }
            }          
            return d;
        }

    }
}
