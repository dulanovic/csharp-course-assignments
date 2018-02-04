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
            return string.Format("{0} {1} Ukupna uplata: {2}", adresa, grad, ukupnaUplata);
        }

        public void DodajIgraca(Igrac i)
        {
            if (i.IznosUplate >= 50 && i.IznosUplate <= 100000)
            {
                bool nadjenIgrac = false;
                int indeksNadjenog = 0;

                for (int k = 0; k < listaIgraca.Count; k++)
                {
                    if (i.Ime == listaIgraca[k].Ime && i.Prezime == listaIgraca[k].Prezime && i.Zemlja.Naziv == listaIgraca[k].Zemlja.Naziv)
                    {
                        nadjenIgrac = true;
                        indeksNadjenog = k;
                    }
                }
                if (nadjenIgrac)
                {
                    listaIgraca[indeksNadjenog] = i;
                }
                else
                {
                    listaIgraca.Add(i);
                }
                int brojUplataIgraca = 0;
                foreach (Igrac igrac in listaIgraca)
                {
                    if (igrac.Ime == i.Ime && igrac.Prezime == i.Prezime && igrac.DatumRodjenja == i.DatumRodjenja)
                    {
                        brojUplataIgraca++;
                    }
                }
                if (brojUplataIgraca > 1)
                {
                    if (Obavesti != null)
                    {
                        Obavesti();
                    }
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
            DateTime datumRodjenja = DateTime.Parse(Console.ReadLine());

            bool pogresanUnos = true;
            double iznosUplate = 0;

            while (pogresanUnos)
            {
                Console.WriteLine("Unesite iznos uplate igraca: ");
                try
                {
                    iznosUplate = Double.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Pogresili ste pri unosu iznos uplate. Iznos mora biti broj!!!");
                }
            }
            Console.WriteLine("Unesite zemlju:");
            string nazivZemlje = Console.ReadLine();

            Console.WriteLine("Unesite kvotu zemlje:");
            double kvota = Double.Parse(Console.ReadLine());

            Console.WriteLine("Unesite kontinent:");
            Kontinent kontinent = (Kontinent)Enum.Parse(typeof(Kontinent), Console.ReadLine());

            return new Igrac(ime, prezime, datumRodjenja, iznosUplate, new Zemlja(nazivZemlje, kvota, kontinent));
        }
    }
}
