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
            string[] niz = adresa.Split(' ');
            string ulica = "";
            for (int i = 0; i < niz.Length - 1; i++)
            {
                ulica += " " + niz[i];
            }
            string gradBezSamoglasnika = grad.Replace("a", "");
            string gradBezSamoglasnika1 = gradBezSamoglasnika.Replace("e", "");
            string gradBezSamoglasnika2 = gradBezSamoglasnika.Replace("i", "");
            string gradBezSamoglasnika3 = gradBezSamoglasnika.Replace("o", "");
            string gradBezSamoglasnika4 = gradBezSamoglasnika.Replace("u", "");

            double ukupnaUplata = 0;

            foreach (Igrac i in listaIgraca)
            {
                ukupnaUplata += i.IznosUplate;
            }
            return string.Format("{0}, {1}, Ukupna uplata: {2}", ulica, gradBezSamoglasnika4, ukupnaUplata);
        }

        public void DodajIgraca(Igrac i)
        {
            if (i.IznosUplate >= 50 && i.IznosUplate <= 100000)
            {
                bool pronasaoIgraca = false;
                int indeksIgraca = 0;

                for (int j = 0; j < listaIgraca.Count; j++)
                {
                    if (i.Ime == listaIgraca[j].Ime && i.Prezime == listaIgraca[j].Prezime && i.DatumRodjenja == listaIgraca[j].DatumRodjenja)
                    {
                        if (i.Zemlja.Naziv == listaIgraca[j].Zemlja.Naziv && i.Zemlja.Kvota == listaIgraca[j].Zemlja.Kvota)
                        {
                            pronasaoIgraca = true;
                            indeksIgraca = j;
                        }

                    }
                }
                if (pronasaoIgraca)
                {
                    listaIgraca[indeksIgraca] = i;
                }
                else
                {
                    listaIgraca.Add(i);
                }
                if(KladioSe != null)
                {
                    KladioSe();
                }
            }

        }

        public Igrac UcitajIgraca()
        {
            Console.WriteLine("Unesite ime igraca: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime igraca: ");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite datum rodjenja igraca: ");
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
                    Console.WriteLine("Niste uneli iznos uplate u pravilnom formatu, trazi se broj!!!");
                }
            }

            Console.WriteLine("Unesite naziv zemlje: ");
            string nazivZemlje = Console.ReadLine();

            Console.WriteLine("Unesite kvotu zemlje: ");
            double kvota = Double.Parse(Console.ReadLine());

            Console.WriteLine("Unesite kontinent: ");
            Kontinent kontinent = (Kontinent)Enum.Parse(typeof(Kontinent), Console.ReadLine());

            Zemlja z = new Zemlja(nazivZemlje, kvota, kontinent);
            Igrac i = new Igrac(ime, prezime, datumRodjenja, iznosUplate, z);
            return i;
        }

        public static Delegat KladioSe;
    }
}
