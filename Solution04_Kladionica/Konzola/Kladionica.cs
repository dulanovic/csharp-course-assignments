using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Konzola
{
    public class Kladionica
    {
        private string takmicenje;
        private List<UplatnoMesto> listaUplatnihMesta;
        private int brojIgracaSaViseUplata;

        public int BrojIgracaSaViseUplata
        {
            get { return brojIgracaSaViseUplata; }
            set { brojIgracaSaViseUplata = value; }
        }


        public List<UplatnoMesto> ListaUplatnihMesta
        {
            get { return listaUplatnihMesta; }
            set { listaUplatnihMesta = value; }
        }


        public string Takmicenje
        {
            get { return takmicenje; }
            set { takmicenje = value; }
        }

        public Kladionica(string takmicenje)
        {
            this.takmicenje = takmicenje;
            this.listaUplatnihMesta = new List<UplatnoMesto>();
        }

        public string DajPodatke()
        {
            int ukupanBrojIgraca = 0;
            foreach (UplatnoMesto um in listaUplatnihMesta)
            {
                foreach (Igrac i in um.ListaIgraca)
                {
                    ukupanBrojIgraca++;
                }
            }

            string ispis = string.Format("{0}, {1}\n", takmicenje, ukupanBrojIgraca);

            foreach (UplatnoMesto um in listaUplatnihMesta)
            {
                ispis += string.Format("\n\t{0}", um.DajPodatke());
                foreach (Igrac i in um.ListaIgraca)
                {
                    ispis += string.Format("\n\t\t{0}", i.DajPodatke());
                }
            }
            return ispis;
        }

        public UplatnoMesto UcitajUplatnoMesto()
        {
            Console.WriteLine("Unesite adresu uplatnog mesta: ");
            string adresa = Console.ReadLine();

            Console.WriteLine("Unesite grad uplatnog mesta: ");
            string grad = Console.ReadLine();

            Console.WriteLine("Unesite broj igraca koje zelite da unesete: ");
            int brojIgraca = Int32.Parse(Console.ReadLine());

            UplatnoMesto um = new UplatnoMesto(adresa, grad);

            for (int i = 0; i < brojIgraca; i++)
            {
                um.DodajIgraca(um.UcitajIgraca());
            }
            return um;
        }

        public void PovecajBrojIgracaSaViseUplata()
        {
            brojIgracaSaViseUplata++;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite takmicenje: ");
            string takmicenje = Console.ReadLine();

            Kladionica k = new Kladionica(takmicenje);

            UplatnoMesto UplatnoMesto = k.UcitajUplatnoMesto();
            k.listaUplatnihMesta.Add(UplatnoMesto);
            UplatnoMesto.KladioSe = k.PovecajBrojIgracaSaViseUplata;
            //k.listaUplatnihMesta.Add(k.UcitajUplatnoMesto());

            string ispis = string.Format("{0}\nBroj igraca sa vise uplata: {1}", k.DajPodatke(), k.brojIgracaSaViseUplata);
            Console.WriteLine(ispis);
            Console.ReadLine();
        }
    }
}
