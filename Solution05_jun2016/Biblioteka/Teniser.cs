using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Teniser : Osoba
    {
        private int rang;
        private List<RezultatNaTurniru> listaRezultata;

        public List<RezultatNaTurniru> ListaRezultata
        {
            get { return listaRezultata; }
            set { listaRezultata = value; }
        }


        public int Rang
        {
            get { return rang; }
            set { rang = value; }
        }

        public Teniser(string ime, DateTime datumRodjenja, int rang) : base(ime, datumRodjenja)
        {
            this.rang = rang;
            this.listaRezultata = new List<RezultatNaTurniru>();
        }

        public Teniser()
        {

        }

        public static string M01_DaLiJePobedioNaTurniru(Teniser te, Turnir tu)
        {
            bool signal = true;
            foreach (RezultatNaTurniru rnt in te.ListaRezultata)
            {
                if (rnt.Turnir.Godina == tu.Godina && rnt.Turnir.Naziv == tu.Naziv && rnt.BrojOsvojenihBodova == tu.MaksimalanBrojBodova)
                {
                    signal = true;
                    break;
                }
                else
                {
                    signal = false;
                }
            }
            if (signal)
            {
                return string.Format("Jeste pobedio, a vrsta turninra je: {0}", tu.Vrsta);
            }
            else
            {
                return string.Format("Nije pobedio, a vrsta turninra je: {0}", tu.Vrsta);
            }
        }

        public static int M02_BrojTurniraNaKojimaJeOsvojenoBarPolaOdMaksimalnogBrojaBodova(Teniser t)
        {
            int brojac = 0;
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if (rnt.BrojOsvojenihBodova >= ((double)rnt.Turnir.MaksimalanBrojBodova / 2))
                {
                    brojac++;
                }
            }
            return brojac;
        }

        public static string M03_NaziviTurniraNaKojimaJeIgracIgrao2015(Teniser t)
        {
            string ispis = "";
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if (rnt.Turnir.Godina == 2015 && rnt.BrojOsvojenihBodova > 0)
                {
                    ispis += string.Format("{0}\n", rnt.Turnir.Naziv);
                }
            }
            return ispis;
        }

        public static double M04_ProsecanBrojOsvojenihBodovaNaDatojVrstiTurnira(Teniser t, Vrsta v)
        {
            double zbir = 0;
            double brojac = 0;
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if (rnt.Turnir.Vrsta == v && rnt.BrojOsvojenihBodova > 0)
                {
                    zbir += rnt.BrojOsvojenihBodova;
                    brojac++;
                }
            }
            return zbir / brojac;
        }

        public static double M05_ProcenatZnacajnihTurniraNaKojimaJeIgrao(Teniser t)
        {
            double brojVaznih = 0;
            double ukupanBrojTurnira = 0;
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if (rnt.BrojOsvojenihBodova > 0)
                {
                    ukupanBrojTurnira++;
                }
            }

            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if ((rnt.Turnir.Vrsta == Vrsta.GrandSlam || rnt.Turnir.Vrsta == Vrsta.Masters) && rnt.BrojOsvojenihBodova > 0)
                {
                    brojVaznih++;
                }
            }
            return brojVaznih / ukupanBrojTurnira;
        }

        public static int M06_MaksimalanBrojBodovaDaJePobedioNaSvakomTurniruNaKomJeIgrao(Teniser t)
        {
            int maxBrojBodova = 0;
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if (rnt.BrojOsvojenihBodova > 0)
                {
                    maxBrojBodova += rnt.Turnir.MaksimalanBrojBodova;
                }
            }
            return maxBrojBodova;
        }

        public static string M07_NaziviSvihTurniraNaKojimaJeIgrao(Teniser t)
        {
            string ispis = "";
            List<string> lista = new List<string>();
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if (rnt.BrojOsvojenihBodova > 0)
                {
                    if (!lista.Contains(rnt.Turnir.Naziv))
                    {
                        lista.Add(rnt.Turnir.Naziv);
                    }
                }
            }

            foreach (string naziv in lista)
            {
                ispis += string.Format("{0}\n", naziv);
            }
            return ispis;
        }

        public static bool M08_DaLiJePobedioNaBaremDvaZnacajnaTurnira(Teniser t)
        {
            int brojac = 0;
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                if ((rnt.Turnir.Vrsta == Vrsta.GrandSlam || rnt.Turnir.Vrsta == Vrsta.Masters) && rnt.BrojOsvojenihBodova == rnt.Turnir.MaksimalanBrojBodova)
                {
                    brojac++;
                }
            }
            if (brojac >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string M09_DaLiJeIzZemljePrvaTriSuglasnikaIBrojGodin(Teniser t, string zemlja)
        {
            string ispis = "";
            string[] niz = t.Ime.Split(' ');
            if (niz[niz.Length - 1] == zemlja)
            {
                ispis += string.Format("Da, dati teniser je iz date zemlje - {0}\n", zemlja);
            }
            else
            {
                ispis += string.Format("Ne, dati teniser nije iz date zemlje - {0}\n", zemlja);
            }
            string prezimePrvaTriSuglasnika = niz[niz.Length - 2].Replace("a", "");
            prezimePrvaTriSuglasnika = prezimePrvaTriSuglasnika.Replace("e", "");
            prezimePrvaTriSuglasnika = prezimePrvaTriSuglasnika.Replace("i", "");
            prezimePrvaTriSuglasnika = prezimePrvaTriSuglasnika.Replace("o", "");
            prezimePrvaTriSuglasnika = prezimePrvaTriSuglasnika.Replace("u", "");
            prezimePrvaTriSuglasnika = prezimePrvaTriSuglasnika.Substring(0, 3);
            ispis += string.Format("Prva 3 suglasnika u prezimenu tenisera: {0}\n", prezimePrvaTriSuglasnika);

            DateTime danas = DateTime.Today;
            int brojGodina = danas.Year - t.DatumRodjenja.Year;
            if (t.DatumRodjenja > danas.AddYears(-brojGodina))
            {
                brojGodina--;
            }
            ispis += string.Format("Broj godina: {0}", brojGodina);

            return ispis;
        }

        public static bool M10_DaLiJeIgraoNaSvimTurnirimaIzSkupa(Teniser t, List<Turnir> lista)
        {
            int brojac = 0;
            int listaSize = lista.Count;
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                foreach (Turnir tu in lista)
                {
                    if (rnt.Turnir.Naziv == tu.Naziv && rnt.Turnir.Godina == tu.Godina && rnt.BrojOsvojenihBodova > 0)
                    {
                        brojac++;
                    }
                }
            }
            return (brojac == listaSize);
        }

    }
}
