using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Konzola
{
    public class Aerodrom
    {
        private string naziv;
        private List<Let> listaLetova;
        private int brojPutnikaUBiznisKlasi;

        public int BrojPutnikaUBiznisKlasi
        {
            get { return brojPutnikaUBiznisKlasi; }
            set { brojPutnikaUBiznisKlasi = value; }
        }


        public List<Let> ListaLetova
        {
            get { return listaLetova; }
            set { listaLetova = value; }
        }


        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Aerodrom(string naziv)
        {
            this.naziv = naziv;
            this.listaLetova = new List<Let>();
        }

        public string DajPodatke()
        {
            int ukupanBrojPutnika = 0;

            foreach (Let l in listaLetova)
            {
                ukupanBrojPutnika += l.ListaPutnika.Count;
            }

            string ispis = string.Format("Naziv aerodroma: {0}, ukupan broj putnika je: {1}\n", naziv, ukupanBrojPutnika);

            foreach (Let l in listaLetova)
            {
                ispis += string.Format("\t{0}\n", l.DajPodatke());

                foreach (Putnik p in l.ListaPutnika)
                {
                    if (p.Sediste.Klasa == Klasa.Biznis)
                    {
                        ispis += string.Format("\t\t{0}\n", p.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public Putnik UcitajPutnika()
        {
            Console.WriteLine("Unesite ime putnika: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime putnika: ");
            string prezime = Console.ReadLine();

            bool pogresanUnos = true;
            int brojSedista = 0;
            while (pogresanUnos)
            {
                Console.WriteLine("Unesite broj sedista: ");
                string brojSedistaString = Console.ReadLine();

                try
                {
                    brojSedista = Convert.ToInt32(brojSedistaString);
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Niste uneli broj, molimo Vas da unesete broj sedista!");
                }
            }

            Console.WriteLine("Unesite klasu: ");
            string klasa = Console.ReadLine();

            Klasa odabranaKlasa;

            switch (klasa)
            {
                case "Ekonomska": odabranaKlasa = Klasa.Ekonomska; break;
                case "Biznis": odabranaKlasa = Klasa.Biznis; break;
                default: odabranaKlasa = Klasa.Ekonomska; break;
            }
            Sediste s = new Sediste(brojSedista, odabranaKlasa);
            Putnik p = new Putnik(ime, prezime, s);
            return p;
        }

        public Let UcitajLet()
        {
            Console.WriteLine("Unesite relaciju: ");
            string relacija = Console.ReadLine();

            Console.WriteLine("Unesite datum: ");
            DateTime datum = Convert.ToDateTime(Console.ReadLine());

            Let l = new Let(relacija, datum);

            Console.WriteLine("Unesite broj putnika na letu: ");
            int brojPutnika = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < brojPutnika; i++)
            {
                l.DodajPutnika(UcitajPutnika());
            }
            return l;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv Aerodroma: ");
            string naziv = Console.ReadLine();

            Aerodrom a = new Aerodrom(naziv);

            a.listaLetova.Add(a.UcitajLet());
            //a.listaLetova.Add(a.UcitajLet());

            Console.WriteLine(a.DajPodatke());
            Console.WriteLine("Broj putnika u biznis klasi je: {0}", a.brojPutnikaUBiznisKlasi);
            Console.ReadLine();
        }
    }
}
