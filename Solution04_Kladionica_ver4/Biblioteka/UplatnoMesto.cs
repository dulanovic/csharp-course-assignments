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
            double ukupnaUplata = 0;
            foreach (Igrac i in listaIgraca)
            {
                ukupnaUplata += i.IznosUplate;
            }
            return string.Format("{0} {1} Ukupna uplata: {2}RSD", adresa, grad, ukupnaUplata);
        }

        public void DodajIgraca(Igrac ig)
        {
            if (ig.IznosUplate > 100000 || ig.IznosUplate < 50)
            {
                return;
            }
            DateTime datumDanas = DateTime.Today;
            int brojGodina = datumDanas.Year - ig.DatumRodjenja.Year;
            if (ig.DatumRodjenja.AddYears(brojGodina) > datumDanas)
            {
                brojGodina--;
            }
            if (brojGodina < 18)
            {
                return;
            }
            bool nadjen = false;
            int indeks = 0;
            for (int i = 0; i < listaIgraca.Count; i++)
            {
                if (ig.Ime.Equals(listaIgraca[i].Ime) && ig.Prezime.Equals(listaIgraca[i].Prezime) && listaIgraca[i].OdabranaZemlja.Naziv.Equals(ig.OdabranaZemlja.Naziv))
                {
                    nadjen = true;
                    indeks = i;
                }
            }
            if (nadjen)
            {
                listaIgraca[indeks] = ig;
            }
            else
            {
                listaIgraca.Add(ig);
            }
            int brojOpklada = 0;
            foreach (Igrac i in listaIgraca)
            {
                if (ig.Ime.Equals(i.Ime) && ig.Prezime.Equals(i.Prezime))
                {
                    brojOpklada++;
                }
            }
            if (brojOpklada==2)
            {
                if (Obavesti != null)
                {
                    Obavesti();
                }
            }
        }

        public Igrac UcitajIgraca()
        {
            Console.WriteLine("Unesite ime:");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime:");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite datum rodjenja:");
            DateTime datumRodjenja = DateTime.Parse(Console.ReadLine());

            bool pogresanUnos = true;
            double iznosUplate = 0;
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
                    Console.WriteLine("Pogresan unos, ispravite gresku!");
                }
            }

            Console.WriteLine("Unesite naziv zemlje:");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite kvotu zemlje:");
            double kvota = Double.Parse(Console.ReadLine());

            Console.WriteLine("Unesite kontinent:");
            Kontinent kontinent = (Kontinent)Enum.Parse(typeof(Kontinent), Console.ReadLine());

            return new Igrac(ime, prezime, datumRodjenja, iznosUplate, new Zemlja(naziv, kvota, kontinent));
        }
    }
}
