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
            string ispis = string.Format("{0}, {1}", naziv, brojPutnika);

            foreach (Let l in listaLetova)
            {
                ispis += string.Format("\n\t{0}", l.DajPodatke());
                foreach (Putnik p in l.ListaPutnika)
                {
                    if (p.Sediste.Klasa == Klasa.Biznis)
                    {
                        ispis += string.Format("\n\t\t{0}", p.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public Putnik UcitajPutnika()
        {
            Console.WriteLine("Unesite ime putnika:");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime putnika:");
            string prezime = Console.ReadLine();

            bool pogresanUnos = true;
            int brojSedista = 0;
            while (pogresanUnos)
            {
                Console.WriteLine("Unesite broj sedista:");
                try
                {
                    brojSedista = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Uneli ste slovo kao broj sediste, molimo ispravite gresku!");
                }
            }

            Console.WriteLine("Unesite klasu sedista:");
            Klasa klasa = (Klasa)Enum.Parse(typeof(Klasa), Console.ReadLine());

            Sediste s = new Sediste(brojSedista, klasa);
            Putnik p = new Putnik(ime, prezime, s);

            return p;
        }

        public Let UcitajLet()
        {
            Console.WriteLine("Unesite relaciju leta:");
            string relacija = Console.ReadLine();

            Console.WriteLine("Unesite datum leta:");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Unesite broj putnika na letu:");
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
            a.ListaLetova.Add(a.UcitajLet());
            //a.ListaLetova.Add(a.UcitajLet());

            int brojPutnika = 0;
            foreach (Let l in a.ListaLetova)
            {
                foreach (Putnik p in l.ListaPutnika)
                {
                    brojPutnika++;
                }
            }
            string ispis = string.Format("Naziv aerodroma: {0}, broj putnika: {1}", a.naziv, brojPutnika);
            foreach (Let l in a.ListaLetova)
            {
                ispis += string.Format("\n\n\t{0}", l.DajPodatke());
                foreach (Putnik p in l.ListaPutnika)
                {
                    ispis += string.Format("\n\t\t{0}", p.DajPodatke());
                }
            }
            ispis += string.Format("\nBroj putnika u biznis klasi: {0}", a.brojPutnikaUBiznisKlasi);

            Console.WriteLine(ispis);

            Console.ReadLine();
        }
    }
}
