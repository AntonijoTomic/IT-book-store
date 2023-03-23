using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace kvnet
{
    public class repository //glavna klasa
    {
        public static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(),
           enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }
        public List<knjiga> GetBooks(string nesto)//dohvaca knjige s linka
        {
            List<knjiga> knjige = new List<knjiga>();
            string url = "https://api.itbook.store/1.0/search/" + nesto;
            string json = CallRestMethod(url);
            var jsons = JObject.Parse(json);
            var books = jsons.GetValue("books");
            foreach (JObject item in books)
            {
                try
                {
                    knjige.Add(new knjiga
                    {
                        title = (string)item.GetValue("title"),
                        price = (string)item.GetValue("price"),
                        subtitle = (string)item.GetValue("subtitle"),
                        isbn13 = (string)item.GetValue("isbn13"),
                        image = (string)item.GetValue("image"),

                    });
                }
                catch
                {
                }
            }
            return knjige;

        }
        public void Remove(string id) // funkcija za porast stanja
        {

         
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";


            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {
                oCommand.CommandText = "DELETE FROM Tomic_Knjige WHERE isbn13  =" + id;
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }

            }
        }
        public List<Kknjiga> GetBooksFromBaseKK() /// dohvacanje kupljenih knjiga
        {
            List<Kknjiga> knjige = new List<Kknjiga>();
            string connectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection connection = new SqlConnection(connectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Tomic_Knjige";
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        knjige.Add(new Kknjiga()
                        {
                            title = (string)reader["title"],
                            subtitle = (string)reader["subtitle"],
                            isbn13=(string)reader["isbn13"],
                            price = (string)reader["price"],
                            stanje =(int)reader["stanje"]
                        });
                    }
                }
            }
            return knjige;
        }
        public void Edit(knjiga oKnjiga, int stanje) // funkcija za porast stanja
        {
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";


            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
                        using (DbCommand oCommand = oConnection.CreateCommand())
                        {
                            oCommand.CommandText = "UPDATE Tomic_Knjige SET stanje='" + (stanje + 1) + "' WHERE isbn13 =" + oKnjiga.price;
                            oConnection.Open();
                            using (DbDataReader oReader = oCommand.ExecuteReader())
                            {
                            }

                        }
        }
        public void AddBook1234(knjiga oKnjiga)///funkcija s kojom kupujemo knjige
        {
            int stanje = 1;
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {
                oCommand.CommandText = "INSERT INTO Tomic_Knjige(title, subtitle, price, isbn13, stanje) VALUES('" + oKnjiga.title + "', '" + oKnjiga.subtitle + "', '"+ oKnjiga.isbn13 + "', '" +oKnjiga.price + "', '" + stanje + "')";
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }
            }
        }
        public void StanjeMinus(string id, int stanje)/// funkcija s kojom smanjujem stanje
        {
           
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {
                oCommand.CommandText = "UPDATE Tomic_Knjige SET stanje = '" + (stanje - 1) + "' WHERE isbn13 = " + id;
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }
                return;
            }
        }

        public void AddBook3(string idd, string id)/// dodavanje u bazi posudene knjige
        {
            int numVal = Int32.Parse(id);
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {

                oCommand.CommandText = "INSERT INTO Tomic_PKnjiga(id_korisnik , isbn13_knjiga) VALUES('" + id + "', '" + idd +"')";
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }
            }
        }
        public void VratiKnjigu(int isbn13_knjiga)// funkcija za brisanje iz baze kada vratimo knjigu
        {
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {

         
                oCommand.CommandText = "DELETE FROM Tomic_PKnjiga WHERE id  =" + isbn13_knjiga;
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }
                return;
            }
        }
        public void BrisiKorisnika(int id)// funkcija za brisanje iz baze kada vratimo knjigu
        {
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {


                oCommand.CommandText = "DELETE FROM Tomic_Korisnik1 WHERE id  =" + id;
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }
                return;
            }
        }
        public void AddKorisnik(korisnik oKorisnik)//funkcija za dodavanje korisnika
        {
            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {
                oCommand.CommandText = "INSERT INTO Tomic_Korisnik1 (ime, prezime, adresa, oib) VALUES('" + oKorisnik.ime + "', '" + oKorisnik.prezime + "', '" + oKorisnik.adresa + "', '" + oKorisnik.oib + "')";
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }
            }
        }
        
        //public List<PK> GetBooksFromBase()//funkcija za dohvacne knjiga koje smo kupili s linka
        //{
        //    List<PK> knjige = new List<PK>();
        //    string connectionString = "Server=ANTONIJO\\SQLEXPRESS; Database=Knjige; Trusted_Connection=True;";
        //    using (DbConnection connection = new SqlConnection(connectionString))
        //    using (DbCommand command = connection.CreateCommand())
        //    {
        //        command.CommandText = "SELECT * FROM PK2";
        //        connection.Open();
        //        using (DbDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                knjige.Add(new PK()
        //                {
        //                    id = (int)reader["id"],
        //                    id_korisnik =(int)reader["id_korisnik"],
        //                    isbn13_knjiga=(string)reader["isbn13_knjiga"]
        //                });
        //            }
        //        }
        //    }
        //    return knjige;
        //}
        //public List<knjiga1> GetBooksFromBase1()//funkcija za ispis bez ID-a (spremanje u knjiga 1)
        //{
        //    List<knjiga1> knjige = new List<knjiga1>();
        //    string connectionString = "Server=ANTONIJO\\SQLEXPRESS; Database=Knjige; Trusted_Connection=True;";
        //    using (DbConnection connection = new SqlConnection(connectionString))
        //    using (DbCommand command = connection.CreateCommand())
        //    {
        //        command.CommandText = "SELECT * FROM knjige";
        //        connection.Open();
        //        using (DbDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                knjige.Add(new knjiga1()
        //                {
        //                    title = (string)reader["title"],
        //                    subtitle = (string)reader["subtitle"],
        //                    price = (string)reader["price"]
        //                });
        //            }
        //        }
        //    }
        //    return knjige;
        //}

        public List<korisnik> GetKorisnik()// dohvacanje korisnika
        {
            List<korisnik> korisnici = new List<korisnik>();
            string connectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection connection = new SqlConnection(connectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Tomic_Korisnik1";
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        korisnici.Add(new korisnik()
                        {
                            id = (int)  reader["id"],
                            ime = (string)reader["ime"],
                            prezime = (string)reader["prezime"],
                            adresa = (string)reader["adresa"],
                            oib = (string)reader["oib"]
                        });
                    }
                }
            }

            return korisnici;
        }
        public List<Admin> Login()// dohvacanje korisnika
        {
            List<Admin> admini = new List<Admin>();
            string connectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection connection = new SqlConnection(connectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Tomic_login";
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admini.Add(new Admin()
                        {
                            username = (string)reader["username"],
                            password = (string)reader["password"]
                        });
                    }
                }
            }

            return admini;
        }
        public void UrediKorisnika(korisnik a)/// funkcija s kojom 
        {

            string sSqlConnectionString = "Server=193.198.57.183; Database=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(sSqlConnectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {
                oCommand.CommandText = "UPDATE Tomic_Korisnik1 SET ime= '" + a.ime +"', prezime='" +a.prezime +"',adresa='"+ a.adresa +"'  WHERE id = " + a.id;
               
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                }
                return;
            }
        }


        //public void vracanje() // funckija za pozivanje prilikom vritiska gumba "vrati"
        //{

        //    PK a = new PK();
        //    int broj = -1;
        //    List<pk2> lista = new List<pk2>();
        //    lista = a.GetBookById();
        //    foreach (PK p in a.GetBooksFromBase())
        //    {

        //        foreach (korisnik kor in GetKorisnik())
        //        {
        //            foreach (Kknjiga kk in GetBooksFromBaseKK())
        //            {
        //                if (kor.id == p.id_korisnik && kk.isbn13 == p.isbn13_knjiga )
        //                {
        //                    broj = p.id;
        //                   VratiKnjigu(broj);                           
        //                    return;
        //                }
        //            }
        //        }
        //    }

        //}
    }
}
