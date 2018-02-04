using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public delegate void Delegat();

    public class UplatnoMesto
    {
        private string adresa;
        private string grad;
        private List<Igrac> listaIgraca;
        public static event Delegat Obavesti;

        public List<Igrac> ListaIgraca
        {
            get { return listaIgraca; }
            set { listaIgraca = value; }
        }

        public string Grad
        {
            get { return grad; }
            set { grad = value; }
        }

        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        public UplatnoMesto(string adresa, string grad)
        {
            this.adresa = adresa;
            this.grad = grad;
            this.listaIgraca = new List<Igrac>();
        }

        public string DajPodatke()
        {
            string ispis = "";
            string ulicaBezBroja = "";
            string[] adresaNiz = adresa.Split(' ');
            for (int i = 0; i < adresaNiz.Length - 1; i++)
            {
                ulicaBezBroja += adresaNiz[i] + " ";
            }
            double ukupnaUplata = 0;
            foreach (Igrac i in listaIgraca)
            {
                ukupnaUplata += i.IznosUplate;
            }
            string gradBezSamoglasnika = grad.Replace("a", "");
            gradBezSamoglasnika = gradBezSamoglasnika.Replace("e", "");
            gradBezSamoglasnika = gradBezSamoglasnika.Replace("i", "");
            gradBezSamoglasnika = gradBezSamoglasnika.Replace("o", "");
            gradBezSamoglasnika = gradBezSamoglasnika.Replace("u", "");

            return string.Format("{0} {1} Ukupna uplata:{2}RSD", ulicaBezBroja, gradBezSamoglasnika, ukupnaUplata);
        }

        public void DodajIgraca(Igrac igrac)
        {
            DateTime danas = DateTime.Today;
            int brojGodina = danas.Year - igrac.Datum.Year;
            if (igrac.Datum.AddYears(brojGodina) < danas)
            {
                brojGodina--;
            }
            if (brojGodina < 18 || igrac.IznosUplate < 50 || igrac.IznosUplate > 100000)
            {
                return;
            }
            int indeks = 0;
            bool nadjen = false;
            for (int i = 0; i < listaIgraca.Count; i++)
            {
                if (listaIgraca[i].Ime == igrac.Ime && listaIgraca[i].Prezime == igrac.Prezime && listaIgraca[i].Datum == igrac.Datum && listaIgraca[i].OdabranaZemlja.Naziv == igrac.OdabranaZemlja.Naziv)
                {
                    nadjen = true;
                    indeks = i;
                }
            }
            if (nadjen)
            {
                listaIgraca[indeks] = igrac;
            }
            else
            {
                listaIgraca.Add(igrac);
            }
            int brojOpklada = 0;
            foreach (Igrac i in listaIgraca)
            {
                if (igrac.Ime == i.Ime && igrac.Prezime == i.Prezime && igrac.Datum == i.Datum)
                {
                    brojOpklada++;
                }
            }
            if (brojOpklada == 2)
            {
                if (Obavesti != null)
                {
                    Obavesti();
                }
            }
        }

        public Igrac UcitajIgraca()
        {
            Console.WriteLine("Unesite ime igraca:");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime igraca:");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite datum rodjenja igraca:");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Unesite zemlju:");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite kvotu:");
            double kvota = Double.Parse(Console.ReadLine());

            Console.WriteLine("Unesite kontinent:");
            Kontinent k = (Kontinent)Enum.Parse(typeof(Kontinent), Console.ReadLine());

            int iznosUplate = 0;
            bool pogresanUnos = true;
            while (pogresanUnos)
            {
                Console.WriteLine("Unesite iznos uplate:");
                try
                {
                    iznosUplate = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Pogresan unos! Uneli ste slovo umesto cifre, molimo ispravite gresku!");
                }
            }
            return new Igrac(ime, prezime, datum, iznosUplate, new Zemlja(naziv, kvota, k));
        }
    }
}
