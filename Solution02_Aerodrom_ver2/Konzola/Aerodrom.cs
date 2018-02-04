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
            this.brojPutnikaUBiznisKlasi = 0;
        }

        public string DajPodatke()
        {
            int brojPutnika = 0;

            foreach (Let l in listaLetova)
            {
                foreach (Putnik p in l.ListaPutnika)
                {
                    brojPutnika++;
                }
            }
            string ispis = string.Format("{0}, {1}\n", naziv, brojPutnika);
            foreach (Let l in listaLetova)
            {
                ispis += string.Format("\t{0}\n", l.DajPodatke());
                foreach (Putnik p in l.ListaPutnika)
                {
                    ispis += string.Format("\t\t{0}\n", p.DajPodatke());
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
                Console.WriteLine("Unesite broj sedista putnika: ");
                try
                {
                    brojSedista = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Uneli ste tekst kao broj sedista, molimo ispravite gresku.");
                }
            }

            Console.WriteLine("Unesite klasu: ");
            Klasa klasa = (Klasa)Enum.Parse(typeof(Klasa), Console.ReadLine());

            return new Putnik(ime, prezime, new Sediste(brojSedista, klasa));
        }

        public Let UcitajLet()
        {
            Console.WriteLine("Unesite relaciju: ");
            string relacija = Console.ReadLine();

            Console.WriteLine("Unesite datum: ");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Unesite broj putnika: ");
            int brojPutnika = Int32.Parse(Console.ReadLine());

            Let l = new Let(relacija, datum);

            for (int i = 0; i < brojPutnika; i++)
            {
                l.DodajPutnika(UcitajPutnika());
            }
            return l;
        }

        public void PovecajBrojPutnikaUBiznisKlasi()
        {
            brojPutnikaUBiznisKlasi++;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv aerodroma: ");
            string naziv = Console.ReadLine();

            Aerodrom a = new Aerodrom(naziv);
            Let.Obavesti += a.PovecajBrojPutnikaUBiznisKlasi;

            a.listaLetova.Add(a.UcitajLet());
            //a.listaLetova.Add(a.UcitajLet());

            string ispis = string.Format("Naziv aerodroma: {0}\nBroj putnika u biznis klasi: {1}\n", a.DajPodatke(), a.brojPutnikaUBiznisKlasi);
            Console.WriteLine(ispis);
            Console.ReadLine();
        }
    }
}
