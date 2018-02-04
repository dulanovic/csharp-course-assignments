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

        public static string M01_DaLiJePobedioNaTurniruIKojaJeVrsta(Teniser te, Turnir tu)
        {
            bool pobedio = false;
            foreach (RezultatNaTurniru rnt in te.listaRezultata)
            {
                if (rnt.Turnir.Godina == tu.Godina && rnt.Turnir.Naziv == tu.Naziv && rnt.BrojOsvojenihBodova == tu.MaksimalanBrojBodova)
                {
                    pobedio = true;
                }
            }
            if (pobedio)
            {
                return string.Format("Da, pobedio je, a vrsta datog turnira je: {0}", tu.Vrsta);
            }
            else
            {
                return string.Format("Nije pobedio, a vrsta datog turnira je: {0}", tu.Vrsta);
            }
        }

        public static int M02_BrojTurniraNaKojimaJeOsvojioViseOdPolaBodova(Teniser t)
        {
            int brojTurnira = 0;
            foreach (RezultatNaTurniru rnt in t.listaRezultata)
            {
                if (rnt.BrojOsvojenihBodova > (rnt.Turnir.MaksimalanBrojBodova / 2))
                {
                    brojTurnira++;
                }
            }
            return brojTurnira;
        }

        public static string M03_TurniriNaKojimaJeIgrao2015(Teniser t)
        {
            string ispis = "";
            foreach (RezultatNaTurniru rnt in t.listaRezultata)
            {
                if (rnt.Turnir.Godina == 2015)
                {
                    ispis += string.Format("{0}\n", rnt.Turnir.Naziv);
                }
            }
            return ispis;
        }

        public static double M04_ProsecanBrojBodovaNaDatojVrstiTurnira(Teniser t, Vrsta v)
        {
            double ukupanBrojBodova = 0;
            double brojTurnira = 0;
            foreach (RezultatNaTurniru rnt in t.listaRezultata)
            {
                if (rnt.Turnir.Vrsta == v)
                {
                    ukupanBrojBodova += rnt.BrojOsvojenihBodova;
                    brojTurnira++;
                }
            }
            return (ukupanBrojBodova / brojTurnira);
        }

        public static string M05_ProcenatZnacajnihTurnira(Teniser t)
        {
            double brojZnacajnih = 0;
            double brojUkupno = 0;
            foreach (RezultatNaTurniru rnt in t.listaRezultata)
            {
                brojUkupno++;
                if (rnt.Turnir.Vrsta == Vrsta.GrandSlam || rnt.Turnir.Vrsta == Vrsta.Masters)
                {
                    brojZnacajnih++;
                }
            }
            return string.Format("{0}%", (brojZnacajnih / brojUkupno) * 100);
        }

        public static int M06_MaksimalanBrojBodovaDaJeDobioSve(Teniser t)
        {
            int zbir = 0;
            foreach (RezultatNaTurniru rnt in t.ListaRezultata)
            {
                zbir += rnt.Turnir.MaksimalanBrojBodova;
            }
            return zbir;
        }

        public static string M07_SviTurniriNaKojimaJeIgrao(Teniser t)
        {
            string ispis = "";
            List<string> lista = new List<string>();
            foreach (RezultatNaTurniru rnt in t.listaRezultata)
            {
                if (!lista.Contains(rnt.Turnir.Naziv))
                {
                    lista.Add(rnt.Turnir.Naziv);
                }
            }
            foreach (string s in lista)
            {
                ispis += string.Format("{0}\n", s);
            }
            return ispis;
        }

        public static bool M08_DaLiJeVrhunski(Teniser t)
        {
            bool vrhunski;
            int brojPobeda = 0;
            foreach (RezultatNaTurniru rnt in t.listaRezultata)
            {
                if ((rnt.Turnir.Vrsta == Vrsta.GrandSlam || rnt.Turnir.Vrsta == Vrsta.Masters) && rnt.BrojOsvojenihBodova == rnt.Turnir.MaksimalanBrojBodova)
                {
                    brojPobeda++;
                }
            }
            if (brojPobeda >= 2)
            {
                vrhunski = true;
            }
            else
            {
                vrhunski = false;
            }
            return vrhunski;
        }

        public static string M09_DaLiJeIzZemljePrvaTriSuglasnikaBrojGodina(Teniser t, string zemlja)
        {
            string ispis = "";
            string[] prezimeNiz = t.Ime.Split(' ');
            string poreklo = prezimeNiz[prezimeNiz.Length - 1];
            if (poreklo.Equals(zemlja))
            {
                ispis += string.Format("Da, teniser jeste iz zemlje: {0}\n", zemlja);
            }
            else
            {
                ispis += string.Format("Ne, teniser nije iz zemlje: {0}, vec iz: {1}\n", zemlja, poreklo);
            }

            string prezime = prezimeNiz[prezimeNiz.Length - 2].Replace("a", "");
            prezime = prezime.Replace("e", "");
            prezime = prezime.Replace("i", "");
            prezime = prezime.Replace("o", "");
            prezime = prezime.Replace("u", "");
            prezime = prezime.Replace("A", "");
            prezime = prezime.Replace("E", "");
            prezime = prezime.Replace("I", "");
            prezime = prezime.Replace("O", "");
            prezime = prezime.Replace("U", "");
            ispis += string.Format("{0}\n", prezime.Substring(0, 3));

            DateTime danas = DateTime.Today;
            int brojGodina = danas.Year - t.DatumRodjenja.Year;
            if (t.datumRodjenja.AddYears(brojGodina) < danas)
            {
                brojGodina--;
            }
            ispis += string.Format("Broj godina: {0}.", brojGodina);

            return ispis;
        }

        public static bool M10_DaLiJeIgraoNaSvimIzSkupa(Teniser t, List<Turnir> lista)
        {
            int igrao = 0;
            foreach (RezultatNaTurniru rnt in t.listaRezultata)
            {
                foreach (Turnir tu in lista)
                {
                    if (rnt.Turnir.Naziv.Equals(tu.Naziv) && rnt.Turnir.Godina==tu.Godina)
                    {
                        igrao++;
                    }
                }
            }
            if (igrao == lista.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
