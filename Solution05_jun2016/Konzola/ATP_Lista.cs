using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Konzola
{
    public class ATP_Lista
    {
        private int godina;
        private List<Teniser> listaTenisera;

        public List<Teniser> ListaTenisera
        {
            get { return listaTenisera; }
            set { listaTenisera = value; }
        }

        public int Godina
        {
            get { return godina; }
            set { godina = value; }
        }

        public ATP_Lista(int godina)
        {
            this.godina = godina;
            this.listaTenisera = new List<Teniser>();
        }

        public string M11_ImeBrojOdigranihTurniraZaPrvihPetSaListe()
        {
            string ispis = "";
            foreach (Teniser t in ListaTenisera)
            {
                if (t.Rang >= 1 && t.Rang <= 5)
                {
                    int brojTurnira = 0;
                    foreach (RezultatNaTurniru rnt in t.ListaRezultata)
                    {
                        if (rnt.BrojOsvojenihBodova > 0)
                        {
                            brojTurnira++;
                        }
                    }
                    ispis += string.Format("Ime tenisera: {0}, broj turnira: {1}\n", t.Ime, brojTurnira);
                }
            }

            return ispis;
        }

        public int M12_BrojTeniseraIzZemlje(string zemlja)
        {
            int brojac = 0;
            foreach (Teniser t in ListaTenisera)
            {
                string[] niz = t.Ime.Split(' ');
                if (niz[niz.Length - 1] == zemlja)
                {
                    brojac++;
                }
            }
            return brojac;
        }

        public string M13_TeniserPoslednjiNaListi()
        {
            string ispis = "";
            foreach (Teniser t in listaTenisera)
            {
                if (t.Rang == listaTenisera.Count)
                {
                    ispis += string.Format("{0}", t.Ime);
                }
            }
            return ispis;
        }

        public string M14_TeniseriIzSrbijeSaBarJednimGrandSlamom()
        {
            string ispis = "";
            foreach (Teniser t in listaTenisera)
            {
                string[] niz = t.Ime.Split(' ');
                string zemlja = niz[niz.Length - 1].Trim('(', ')');
                if (zemlja == "MNE")
                {
                    ispis += string.Format("{0}\n", t.Ime);
                }
            }
            return ispis;
        }

        public string M15_TurniriNaKojimaJeIgraoPrviADrugiNije(Teniser t1, Teniser t2)
        {
            string ispis = "";
            foreach (RezultatNaTurniru rnt1 in t1.ListaRezultata)
            {
                foreach (RezultatNaTurniru rnt2 in t2.ListaRezultata)
                {
                    if (rnt1.Turnir.Naziv == rnt2.Turnir.Naziv && rnt1.Turnir.Godina == rnt2.Turnir.Godina && rnt1.BrojOsvojenihBodova > 0 && rnt2.BrojOsvojenihBodova == 0)
                    {
                        Console.WriteLine("{0} - {1}\n", rnt1.Turnir.Godina, rnt1.Turnir.Naziv);
                    }
                }
            }
            return ispis;
        }

        public string M16_TeniseriKojiSuPobediliNaNekomOdZadatihTurnira(List<Turnir> lista)
        {
            string ispis = "";
            foreach (Turnir tu in lista)
            {
                foreach (Teniser te in listaTenisera)
                {
                    foreach (RezultatNaTurniru rnt in te.ListaRezultata)
                    {
                        if (rnt.Turnir.Naziv == tu.Naziv && rnt.Turnir.Godina == tu.Godina && rnt.BrojOsvojenihBodova == tu.MaksimalanBrojBodova)
                        {
                            ispis += string.Format("{0} je osvojio {1}, {2}. godine\n", te.Ime, tu.Naziv, tu.Godina);
                        }
                    }
                }
            }
            return ispis;
        }

        public string M17_TeniseriMladjiOdSaViseOdDesetTurniraIBaremDvaZnacajna(int brojGodina)
        {
            string ispis = "";
            foreach (Teniser t in listaTenisera)
            {
                DateTime datumDanas = DateTime.Today;
                int godine = datumDanas.Year - t.DatumRodjenja.Year;
                if (t.DatumRodjenja.AddYears(godine) < datumDanas)
                {
                    godine--;
                }
                int brojacT = 0;
                int brojacZ = 0;
                if (godine < brojGodina)
                {
                    foreach (RezultatNaTurniru rnt in t.ListaRezultata)
                    {
                        if (rnt.BrojOsvojenihBodova > 0)
                        {
                            brojacT++;
                        }
                        if (rnt.Turnir.Vrsta == Vrsta.GrandSlam || rnt.Turnir.Vrsta == Vrsta.Masters)
                        {
                            brojacZ++;
                        }
                    }
                    if (brojacT > 10 && brojacZ >= 2)
                    {
                        ispis += string.Format("{0}\n", t.Ime);
                    }
                }
            }
            return ispis;
        }

        public bool M18_DaLiJePrvorangiraniPobedioNaSvimGrandSlamovimaNaKojimaJeIgraoUZadnjeDveGodine()
        {
            foreach (Teniser t in listaTenisera)
            {
                int brojacU = 0;
                int brojacP = 0;
                if (t.Rang == 1)
                {
                    foreach (RezultatNaTurniru rnt in t.ListaRezultata)
                    {
                        if (rnt.Turnir.Vrsta == Vrsta.GrandSlam && rnt.BrojOsvojenihBodova > 0 && (rnt.Turnir.Godina == godina || rnt.Turnir.Godina == godina - 1))
                        {
                            brojacU++;
                        }
                        if (rnt.Turnir.Vrsta == Vrsta.GrandSlam && rnt.BrojOsvojenihBodova == rnt.Turnir.MaksimalanBrojBodova && (rnt.Turnir.Godina == godina || rnt.Turnir.Godina == godina - 1))
                        {
                            brojacP++;
                        }
                    }
                    if (brojacP == brojacU)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public string M19_TeniseriSaSamoJednomVrstomTurnira()
        {
            string ispis = "";
            foreach (Teniser t in listaTenisera)
            {
                int brojU = 0;
                int brojV = 0;
                Vrsta v = t.ListaRezultata[0].Turnir.Vrsta;
                foreach (RezultatNaTurniru rnt in t.ListaRezultata)
                {
                    brojU++;
                    if (rnt.Turnir.Vrsta == v)
                    {
                        brojV++;
                    }
                }
                if (brojU == brojV)
                {
                    ispis += string.Format("{0}\n", t.Ime);
                }
            }
            return ispis;
        }

        public string M20_PrviPobednikNaGrandSlamu()
        {
            string ispis = "";
            int godina = this.godina;
            Teniser pobednik = new Teniser();
            Turnir tu = new Turnir();
            foreach (Teniser t in listaTenisera)
            {
                foreach (RezultatNaTurniru rnt in t.ListaRezultata)
                {
                    if (rnt.BrojOsvojenihBodova == rnt.Turnir.MaksimalanBrojBodova && rnt.Turnir.Vrsta == Vrsta.GrandSlam)
                    {
                        if (rnt.Turnir.Godina<godina)
                        {
                            godina = rnt.Turnir.Godina;
                            pobednik = t;
                            tu = rnt.Turnir;
                        }
                    }
                }
            }
            ispis += string.Format("{0}. {1} - {2}", tu.Godina, tu.Naziv, pobednik.Ime);
            return ispis;
        }

        static void Main(string[] args)
        {
            ATP_Lista atp = new ATP_Lista(2015);

            Turnir aus_g_2013 = new Turnir(2013, "Australian Open", 2000, Vrsta.GrandSlam);
            Turnir rgr_g_2013 = new Turnir(2013, "French Open", 2000, Vrsta.GrandSlam);
            Turnir wim_g_2013 = new Turnir(2013, "The Championships, Wimbledon", 2000, Vrsta.GrandSlam);
            Turnir uso_g_2013 = new Turnir(2013, "US Open", 2000, Vrsta.GrandSlam);

            Turnir mtc_m_2013 = new Turnir(2013, "Monte-Carlo Rolex Masters", 1000, Vrsta.Masters);
            Turnir mad_m_2013 = new Turnir(2013, "Mutua Madrid Open", 1000, Vrsta.Masters);
            Turnir rom_m_2013 = new Turnir(2013, "Internazionali BNL d'Italia", 1000, Vrsta.Masters);
            Turnir mrl_m_2013 = new Turnir(2013, "Rogers Cup", 1000, Vrsta.Masters);
            Turnir cin_m_2013 = new Turnir(2013, "Western & Southern Open", 1000, Vrsta.Masters);

            Turnir dub_d_2013 = new Turnir(2013, "Dubai Tennis Championships", 500, Vrsta.Drugi);
            Turnir ham_d_2013 = new Turnir(2013, "International German Open", 500, Vrsta.Drugi);
            Turnir bas_d_2013 = new Turnir(2013, "Swiss Indoors", 500, Vrsta.Drugi);


            Turnir aus_g_2014 = new Turnir(2014, "Australian Open", 2000, Vrsta.GrandSlam);
            Turnir rgr_g_2014 = new Turnir(2014, "French Open", 2000, Vrsta.GrandSlam);
            Turnir wim_g_2014 = new Turnir(2014, "The Championships, Wimbledon", 2000, Vrsta.GrandSlam);
            Turnir uso_g_2014 = new Turnir(2014, "US Open", 2000, Vrsta.GrandSlam);

            Turnir mtc_m_2014 = new Turnir(2014, "Monte-Carlo Rolex Masters", 1000, Vrsta.Masters);
            Turnir mad_m_2014 = new Turnir(2014, "Mutua Madrid Open", 1000, Vrsta.Masters);
            Turnir rom_m_2014 = new Turnir(2014, "Internazionali BNL d'Italia", 1000, Vrsta.Masters);
            Turnir mrl_m_2014 = new Turnir(2014, "Rogers Cup", 1000, Vrsta.Masters);
            Turnir cin_m_2014 = new Turnir(2014, "Western & Southern Open", 1000, Vrsta.Masters);

            Turnir dub_d_2014 = new Turnir(2014, "Dubai Tennis Championships", 500, Vrsta.Drugi);
            Turnir ham_d_2014 = new Turnir(2014, "International German Open", 500, Vrsta.Drugi);
            Turnir bas_d_2014 = new Turnir(2014, "Swiss Indoors", 500, Vrsta.Drugi);


            Turnir aus_g_2015 = new Turnir(2015, "Australian Open", 2000, Vrsta.GrandSlam);
            Turnir rgr_g_2015 = new Turnir(2015, "French Open", 2000, Vrsta.GrandSlam);
            Turnir wim_g_2015 = new Turnir(2015, "The Championships, Wimbledon", 2000, Vrsta.GrandSlam);
            Turnir uso_g_2015 = new Turnir(2015, "US Open", 2000, Vrsta.GrandSlam);

            Turnir mtc_m_2015 = new Turnir(2015, "Monte-Carlo Rolex Masters", 1000, Vrsta.Masters);
            Turnir mad_m_2015 = new Turnir(2015, "Mutua Madrid Open", 1000, Vrsta.Masters);
            Turnir rom_m_2015 = new Turnir(2015, "Internazionali BNL d'Italia", 1000, Vrsta.Masters);
            Turnir mrl_m_2015 = new Turnir(2015, "Rogers Cup", 1000, Vrsta.Masters);
            Turnir cin_m_2015 = new Turnir(2015, "Western & Southern Open", 1000, Vrsta.Masters);

            Turnir dub_d_2015 = new Turnir(2015, "Dubai Tennis Championships", 500, Vrsta.Drugi);
            Turnir ham_d_2015 = new Turnir(2015, "International German Open", 500, Vrsta.Drugi);
            Turnir bas_d_2015 = new Turnir(2015, "Swiss Indoors", 500, Vrsta.Drugi);


            List<RezultatNaTurniru> listaMonfils = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Monfils = new RezultatNaTurniru(aus_g_2013, 90); listaMonfils.Add(rnt_aus_g_2013_Monfils);
            RezultatNaTurniru rnt_dub_d_2013_Monfils = new RezultatNaTurniru(dub_d_2013, 0); listaMonfils.Add(rnt_dub_d_2013_Monfils);
            RezultatNaTurniru rnt_mtc_m_2013_Monfils = new RezultatNaTurniru(mtc_m_2013, 10); listaMonfils.Add(rnt_mtc_m_2013_Monfils);
            RezultatNaTurniru rnt_mad_m_2013_Monfils = new RezultatNaTurniru(mad_m_2013, 0); listaMonfils.Add(rnt_mad_m_2013_Monfils);
            RezultatNaTurniru rnt_rom_m_2013_Monfils = new RezultatNaTurniru(rom_m_2013, 0); listaMonfils.Add(rnt_rom_m_2013_Monfils);
            RezultatNaTurniru rnt_rgr_g_2013_Monfils = new RezultatNaTurniru(rgr_g_2013, 90); listaMonfils.Add(rnt_rgr_g_2013_Monfils);
            RezultatNaTurniru rnt_wim_g_2013_Monfils = new RezultatNaTurniru(wim_g_2013, 0); listaMonfils.Add(rnt_wim_g_2013_Monfils);
            RezultatNaTurniru rnt_ham_d_2013_Monfils = new RezultatNaTurniru(ham_d_2013, 20); listaMonfils.Add(rnt_ham_d_2013_Monfils);
            RezultatNaTurniru rnt_mrl_m_2013_Monfils = new RezultatNaTurniru(mrl_m_2013, 0); listaMonfils.Add(rnt_mrl_m_2013_Monfils);
            RezultatNaTurniru rnt_cin_m_2013_Monfils = new RezultatNaTurniru(cin_m_2013, 0); listaMonfils.Add(rnt_cin_m_2013_Monfils);
            RezultatNaTurniru rnt_uso_g_2013_Monfils = new RezultatNaTurniru(uso_g_2013, 45); listaMonfils.Add(rnt_uso_g_2013_Monfils);
            RezultatNaTurniru rnt_bas_d_2013_Monfils = new RezultatNaTurniru(bas_d_2013, 0); listaMonfils.Add(rnt_bas_d_2013_Monfils);

            RezultatNaTurniru rnt_aus_g_2014_Monfils = new RezultatNaTurniru(aus_g_2014, 90); listaMonfils.Add(rnt_aus_g_2014_Monfils);
            RezultatNaTurniru rnt_dub_d_2014_Monfils = new RezultatNaTurniru(dub_d_2014, 0); listaMonfils.Add(rnt_dub_d_2014_Monfils);
            RezultatNaTurniru rnt_mtc_m_2014_Monfils = new RezultatNaTurniru(mtc_m_2014, 0); listaMonfils.Add(rnt_mtc_m_2014_Monfils);
            RezultatNaTurniru rnt_mad_m_2014_Monfils = new RezultatNaTurniru(mad_m_2014, 0); listaMonfils.Add(rnt_mad_m_2014_Monfils);
            RezultatNaTurniru rnt_rom_m_2014_Monfils = new RezultatNaTurniru(rom_m_2014, 0); listaMonfils.Add(rnt_rom_m_2014_Monfils);
            RezultatNaTurniru rnt_rgr_g_2014_Monfils = new RezultatNaTurniru(rgr_g_2014, 360); listaMonfils.Add(rnt_rgr_g_2014_Monfils);
            RezultatNaTurniru rnt_wim_g_2014_Monfils = new RezultatNaTurniru(wim_g_2014, 45); listaMonfils.Add(rnt_wim_g_2014_Monfils);
            RezultatNaTurniru rnt_ham_d_2014_Monfils = new RezultatNaTurniru(ham_d_2014, 0); listaMonfils.Add(rnt_ham_d_2014_Monfils);
            RezultatNaTurniru rnt_mrl_m_2014_Monfils = new RezultatNaTurniru(mrl_m_2014, 45); listaMonfils.Add(rnt_mrl_m_2014_Monfils);
            RezultatNaTurniru rnt_cin_m_2014_Monfils = new RezultatNaTurniru(cin_m_2014, 90); listaMonfils.Add(rnt_cin_m_2014_Monfils);
            RezultatNaTurniru rnt_uso_g_2014_Monfils = new RezultatNaTurniru(uso_g_2014, 360); listaMonfils.Add(rnt_uso_g_2014_Monfils);
            RezultatNaTurniru rnt_bas_d_2014_Monfils = new RezultatNaTurniru(bas_d_2014, 0); listaMonfils.Add(rnt_bas_d_2014_Monfils);

            RezultatNaTurniru rnt_aus_g_2015_Monfils = new RezultatNaTurniru(aus_g_2015, 45); listaMonfils.Add(rnt_aus_g_2015_Monfils);
            RezultatNaTurniru rnt_dub_d_2015_Monfils = new RezultatNaTurniru(dub_d_2015, 0); listaMonfils.Add(rnt_dub_d_2015_Monfils);
            RezultatNaTurniru rnt_mtc_m_2015_Monfils = new RezultatNaTurniru(mtc_m_2015, 360); listaMonfils.Add(rnt_mtc_m_2015_Monfils);
            RezultatNaTurniru rnt_mad_m_2015_Monfils = new RezultatNaTurniru(mad_m_2015, 45); listaMonfils.Add(rnt_mad_m_2015_Monfils);
            RezultatNaTurniru rnt_rom_m_2015_Monfils = new RezultatNaTurniru(rom_m_2015, 0); listaMonfils.Add(rnt_rom_m_2015_Monfils);
            RezultatNaTurniru rnt_rgr_g_2015_Monfils = new RezultatNaTurniru(rgr_g_2015, 180); listaMonfils.Add(rnt_rgr_g_2015_Monfils);
            RezultatNaTurniru rnt_wim_g_2015_Monfils = new RezultatNaTurniru(wim_g_2015, 90); listaMonfils.Add(rnt_wim_g_2015_Monfils);
            RezultatNaTurniru rnt_ham_d_2015_Monfils = new RezultatNaTurniru(ham_d_2015, 0); listaMonfils.Add(rnt_ham_d_2015_Monfils);
            RezultatNaTurniru rnt_mrl_m_2015_Monfils = new RezultatNaTurniru(mrl_m_2015, 45); listaMonfils.Add(rnt_mrl_m_2015_Monfils);
            RezultatNaTurniru rnt_cin_m_2015_Monfils = new RezultatNaTurniru(cin_m_2015, 10); listaMonfils.Add(rnt_cin_m_2015_Monfils);
            RezultatNaTurniru rnt_uso_g_2015_Monfils = new RezultatNaTurniru(uso_g_2015, 10); listaMonfils.Add(rnt_uso_g_2015_Monfils);
            RezultatNaTurniru rnt_bas_d_2015_Monfils = new RezultatNaTurniru(bas_d_2015, 0); listaMonfils.Add(rnt_bas_d_2015_Monfils);

            Teniser monfils = new Teniser("Gael Monfils (FRA)", new DateTime(1986, 9, 1), 12);
            monfils.ListaRezultata = listaMonfils;


            List<RezultatNaTurniru> listaTsonga = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Tsonga = new RezultatNaTurniru(aus_g_2013, 360); listaTsonga.Add(rnt_aus_g_2013_Tsonga);
            RezultatNaTurniru rnt_dub_d_2013_Tsonga = new RezultatNaTurniru(dub_d_2013, 0); listaTsonga.Add(rnt_dub_d_2013_Tsonga);
            RezultatNaTurniru rnt_mtc_m_2013_Tsonga = new RezultatNaTurniru(mtc_m_2013, 360); listaTsonga.Add(rnt_mtc_m_2013_Tsonga);
            RezultatNaTurniru rnt_mad_m_2013_Tsonga = new RezultatNaTurniru(mad_m_2013, 180); listaTsonga.Add(rnt_mad_m_2013_Tsonga);
            RezultatNaTurniru rnt_rom_m_2013_Tsonga = new RezultatNaTurniru(rom_m_2013, 45); listaTsonga.Add(rnt_rom_m_2013_Tsonga);
            RezultatNaTurniru rnt_rgr_g_2013_Tsonga = new RezultatNaTurniru(rgr_g_2013, 360); listaTsonga.Add(rnt_rgr_g_2013_Tsonga);
            RezultatNaTurniru rnt_wim_g_2013_Tsonga = new RezultatNaTurniru(wim_g_2013, 45); listaTsonga.Add(rnt_wim_g_2013_Tsonga);
            RezultatNaTurniru rnt_ham_d_2013_Tsonga = new RezultatNaTurniru(ham_d_2013, 0); listaTsonga.Add(rnt_ham_d_2013_Tsonga);
            RezultatNaTurniru rnt_mrl_m_2013_Tsonga = new RezultatNaTurniru(mrl_m_2013, 0); listaTsonga.Add(rnt_mrl_m_2013_Tsonga);
            RezultatNaTurniru rnt_cin_m_2013_Tsonga = new RezultatNaTurniru(cin_m_2013, 0); listaTsonga.Add(rnt_cin_m_2013_Tsonga);
            RezultatNaTurniru rnt_uso_g_2013_Tsonga = new RezultatNaTurniru(uso_g_2013, 0); listaTsonga.Add(rnt_uso_g_2013_Tsonga);
            RezultatNaTurniru rnt_bas_d_2013_Tsonga = new RezultatNaTurniru(bas_d_2013, 0); listaTsonga.Add(rnt_bas_d_2013_Tsonga);

            RezultatNaTurniru rnt_aus_g_2014_Tsonga = new RezultatNaTurniru(aus_g_2014, 180); listaTsonga.Add(rnt_aus_g_2014_Tsonga);
            RezultatNaTurniru rnt_dub_d_2014_Tsonga = new RezultatNaTurniru(dub_d_2014, 90); listaTsonga.Add(rnt_dub_d_2014_Tsonga);
            RezultatNaTurniru rnt_mtc_m_2014_Tsonga = new RezultatNaTurniru(mtc_m_2014, 180); listaTsonga.Add(rnt_mtc_m_2014_Tsonga);
            RezultatNaTurniru rnt_mad_m_2014_Tsonga = new RezultatNaTurniru(mad_m_2014, 45); listaTsonga.Add(rnt_mad_m_2014_Tsonga);
            RezultatNaTurniru rnt_rom_m_2014_Tsonga = new RezultatNaTurniru(rom_m_2014, 90); listaTsonga.Add(rnt_rom_m_2014_Tsonga);
            RezultatNaTurniru rnt_rgr_g_2014_Tsonga = new RezultatNaTurniru(rgr_g_2014, 180); listaTsonga.Add(rnt_rgr_g_2014_Tsonga);
            RezultatNaTurniru rnt_wim_g_2014_Tsonga = new RezultatNaTurniru(wim_g_2014, 180); listaTsonga.Add(rnt_wim_g_2014_Tsonga);
            RezultatNaTurniru rnt_ham_d_2014_Tsonga = new RezultatNaTurniru(ham_d_2014, 0); listaTsonga.Add(rnt_ham_d_2014_Tsonga);
            RezultatNaTurniru rnt_mrl_m_2014_Tsonga = new RezultatNaTurniru(mrl_m_2014, 1000); listaTsonga.Add(rnt_mrl_m_2014_Tsonga);
            RezultatNaTurniru rnt_cin_m_2014_Tsonga = new RezultatNaTurniru(cin_m_2014, 10); listaTsonga.Add(rnt_cin_m_2014_Tsonga);
            RezultatNaTurniru rnt_uso_g_2014_Tsonga = new RezultatNaTurniru(uso_g_2014, 180); listaTsonga.Add(rnt_uso_g_2014_Tsonga);
            RezultatNaTurniru rnt_bas_d_2014_Tsonga = new RezultatNaTurniru(bas_d_2014, 0); listaTsonga.Add(rnt_bas_d_2014_Tsonga);

            RezultatNaTurniru rnt_aus_g_2015_Tsonga = new RezultatNaTurniru(aus_g_2015, 0); listaTsonga.Add(rnt_aus_g_2015_Tsonga);
            RezultatNaTurniru rnt_dub_d_2015_Tsonga = new RezultatNaTurniru(dub_d_2015, 0); listaTsonga.Add(rnt_dub_d_2015_Tsonga);
            RezultatNaTurniru rnt_mtc_m_2015_Tsonga = new RezultatNaTurniru(mtc_m_2015, 90); listaTsonga.Add(rnt_mtc_m_2015_Tsonga);
            RezultatNaTurniru rnt_mad_m_2015_Tsonga = new RezultatNaTurniru(mad_m_2015, 90); listaTsonga.Add(rnt_mad_m_2015_Tsonga);
            RezultatNaTurniru rnt_rom_m_2015_Tsonga = new RezultatNaTurniru(rom_m_2015, 45); listaTsonga.Add(rnt_rom_m_2015_Tsonga);
            RezultatNaTurniru rnt_rgr_g_2015_Tsonga = new RezultatNaTurniru(rgr_g_2015, 720); listaTsonga.Add(rnt_rgr_g_2015_Tsonga);
            RezultatNaTurniru rnt_wim_g_2015_Tsonga = new RezultatNaTurniru(wim_g_2015, 90); listaTsonga.Add(rnt_wim_g_2015_Tsonga);
            RezultatNaTurniru rnt_ham_d_2015_Tsonga = new RezultatNaTurniru(ham_d_2015, 0); listaTsonga.Add(rnt_ham_d_2015_Tsonga);
            RezultatNaTurniru rnt_mrl_m_2015_Tsonga = new RezultatNaTurniru(mrl_m_2015, 180); listaTsonga.Add(rnt_mrl_m_2015_Tsonga);
            RezultatNaTurniru rnt_cin_m_2015_Tsonga = new RezultatNaTurniru(cin_m_2015, 10); listaTsonga.Add(rnt_cin_m_2015_Tsonga);
            RezultatNaTurniru rnt_uso_g_2015_Tsonga = new RezultatNaTurniru(uso_g_2015, 360); listaTsonga.Add(rnt_uso_g_2015_Tsonga);
            RezultatNaTurniru rnt_bas_d_2015_Tsonga = new RezultatNaTurniru(bas_d_2015, 0); listaTsonga.Add(rnt_bas_d_2015_Tsonga);

            Teniser tsonga = new Teniser("Jo-Wilfried Tsonga (FRA)", new DateTime(1985, 4, 17), 11);
            tsonga.ListaRezultata = listaTsonga;


            List<RezultatNaTurniru> listaThiem = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Thiem = new RezultatNaTurniru(aus_g_2013, 0); listaThiem.Add(rnt_aus_g_2013_Thiem);
            RezultatNaTurniru rnt_dub_d_2013_Thiem = new RezultatNaTurniru(dub_d_2013, 0); listaThiem.Add(rnt_dub_d_2013_Thiem);
            RezultatNaTurniru rnt_mtc_m_2013_Thiem = new RezultatNaTurniru(mtc_m_2013, 0); listaThiem.Add(rnt_mtc_m_2013_Thiem);
            RezultatNaTurniru rnt_mad_m_2013_Thiem = new RezultatNaTurniru(mad_m_2013, 0); listaThiem.Add(rnt_mad_m_2013_Thiem);
            RezultatNaTurniru rnt_rom_m_2013_Thiem = new RezultatNaTurniru(rom_m_2013, 0); listaThiem.Add(rnt_rom_m_2013_Thiem);
            RezultatNaTurniru rnt_rgr_g_2013_Thiem = new RezultatNaTurniru(rgr_g_2013, 0); listaThiem.Add(rnt_rgr_g_2013_Thiem);
            RezultatNaTurniru rnt_wim_g_2013_Thiem = new RezultatNaTurniru(wim_g_2013, 0); listaThiem.Add(rnt_wim_g_2013_Thiem);
            RezultatNaTurniru rnt_ham_d_2013_Thiem = new RezultatNaTurniru(ham_d_2013, 0); listaThiem.Add(rnt_ham_d_2013_Thiem);
            RezultatNaTurniru rnt_mrl_m_2013_Thiem = new RezultatNaTurniru(mrl_m_2013, 0); listaThiem.Add(rnt_mrl_m_2013_Thiem);
            RezultatNaTurniru rnt_cin_m_2013_Thiem = new RezultatNaTurniru(cin_m_2013, 0); listaThiem.Add(rnt_cin_m_2013_Thiem);
            RezultatNaTurniru rnt_uso_g_2013_Thiem = new RezultatNaTurniru(uso_g_2013, 0); listaThiem.Add(rnt_uso_g_2013_Thiem);
            RezultatNaTurniru rnt_bas_d_2013_Thiem = new RezultatNaTurniru(bas_d_2013, 0); listaThiem.Add(rnt_bas_d_2013_Thiem);

            RezultatNaTurniru rnt_aus_g_2014_Thiem = new RezultatNaTurniru(aus_g_2014, 45); listaThiem.Add(rnt_aus_g_2014_Thiem);
            RezultatNaTurniru rnt_dub_d_2014_Thiem = new RezultatNaTurniru(dub_d_2014, 0); listaThiem.Add(rnt_dub_d_2014_Thiem);
            RezultatNaTurniru rnt_mtc_m_2014_Thiem = new RezultatNaTurniru(mtc_m_2014, 10); listaThiem.Add(rnt_mtc_m_2014_Thiem);
            RezultatNaTurniru rnt_mad_m_2014_Thiem = new RezultatNaTurniru(mad_m_2014, 90); listaThiem.Add(rnt_mad_m_2014_Thiem);
            RezultatNaTurniru rnt_rom_m_2014_Thiem = new RezultatNaTurniru(rom_m_2014, 0); listaThiem.Add(rnt_rom_m_2014_Thiem);
            RezultatNaTurniru rnt_rgr_g_2014_Thiem = new RezultatNaTurniru(rgr_g_2014, 45); listaThiem.Add(rnt_rgr_g_2014_Thiem);
            RezultatNaTurniru rnt_wim_g_2014_Thiem = new RezultatNaTurniru(wim_g_2014, 10); listaThiem.Add(rnt_wim_g_2014_Thiem);
            RezultatNaTurniru rnt_ham_d_2014_Thiem = new RezultatNaTurniru(ham_d_2014, 45); listaThiem.Add(rnt_ham_d_2014_Thiem);
            RezultatNaTurniru rnt_mrl_m_2014_Thiem = new RezultatNaTurniru(mrl_m_2014, 10); listaThiem.Add(rnt_mrl_m_2014_Thiem);
            RezultatNaTurniru rnt_cin_m_2014_Thiem = new RezultatNaTurniru(cin_m_2014, 10); listaThiem.Add(rnt_cin_m_2014_Thiem);
            RezultatNaTurniru rnt_uso_g_2014_Thiem = new RezultatNaTurniru(uso_g_2014, 180); listaThiem.Add(rnt_uso_g_2014_Thiem);
            RezultatNaTurniru rnt_bas_d_2014_Thiem = new RezultatNaTurniru(bas_d_2014, 0); listaThiem.Add(rnt_bas_d_2014_Thiem);

            RezultatNaTurniru rnt_aus_g_2015_Thiem = new RezultatNaTurniru(aus_g_2015, 10); listaThiem.Add(rnt_aus_g_2015_Thiem);
            RezultatNaTurniru rnt_dub_d_2015_Thiem = new RezultatNaTurniru(dub_d_2015, 0); listaThiem.Add(rnt_dub_d_2015_Thiem);
            RezultatNaTurniru rnt_mtc_m_2015_Thiem = new RezultatNaTurniru(mtc_m_2015, 10); listaThiem.Add(rnt_mtc_m_2015_Thiem);
            RezultatNaTurniru rnt_mad_m_2015_Thiem = new RezultatNaTurniru(mad_m_2015, 0); listaThiem.Add(rnt_mad_m_2015_Thiem);
            RezultatNaTurniru rnt_rom_m_2015_Thiem = new RezultatNaTurniru(rom_m_2015, 90); listaThiem.Add(rnt_rom_m_2015_Thiem);
            RezultatNaTurniru rnt_rgr_g_2015_Thiem = new RezultatNaTurniru(rgr_g_2015, 45); listaThiem.Add(rnt_rgr_g_2015_Thiem);
            RezultatNaTurniru rnt_wim_g_2015_Thiem = new RezultatNaTurniru(wim_g_2015, 45); listaThiem.Add(rnt_wim_g_2015_Thiem);
            RezultatNaTurniru rnt_ham_d_2015_Thiem = new RezultatNaTurniru(ham_d_2015, 0); listaThiem.Add(rnt_ham_d_2015_Thiem);
            RezultatNaTurniru rnt_mrl_m_2015_Thiem = new RezultatNaTurniru(mrl_m_2015, 10); listaThiem.Add(rnt_mrl_m_2015_Thiem);
            RezultatNaTurniru rnt_cin_m_2015_Thiem = new RezultatNaTurniru(cin_m_2015, 10); listaThiem.Add(rnt_cin_m_2015_Thiem);
            RezultatNaTurniru rnt_uso_g_2015_Thiem = new RezultatNaTurniru(uso_g_2015, 90); listaThiem.Add(rnt_uso_g_2015_Thiem);
            RezultatNaTurniru rnt_bas_d_2015_Thiem = new RezultatNaTurniru(bas_d_2015, 45); listaThiem.Add(rnt_bas_d_2015_Thiem);

            Teniser thiem = new Teniser("Dominic Thiem (AUT)", new DateTime(1993, 9, 3), 10);
            thiem.ListaRezultata = listaThiem;


            List<RezultatNaTurniru> listaCilic = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Cilic = new RezultatNaTurniru(aus_g_2013, 90); listaCilic.Add(rnt_aus_g_2013_Cilic);
            RezultatNaTurniru rnt_dub_d_2013_Cilic = new RezultatNaTurniru(dub_d_2013, 0); listaCilic.Add(rnt_dub_d_2013_Cilic);
            RezultatNaTurniru rnt_mtc_m_2013_Cilic = new RezultatNaTurniru(mtc_m_2013, 90); listaCilic.Add(rnt_mtc_m_2013_Cilic);
            RezultatNaTurniru rnt_mad_m_2013_Cilic = new RezultatNaTurniru(mad_m_2013, 10); listaCilic.Add(rnt_mad_m_2013_Cilic);
            RezultatNaTurniru rnt_rom_m_2013_Cilic = new RezultatNaTurniru(rom_m_2013, 45); listaCilic.Add(rnt_rom_m_2013_Cilic);
            RezultatNaTurniru rnt_rgr_g_2013_Cilic = new RezultatNaTurniru(rgr_g_2013, 90); listaCilic.Add(rnt_rgr_g_2013_Cilic);
            RezultatNaTurniru rnt_wim_g_2013_Cilic = new RezultatNaTurniru(wim_g_2013, 45); listaCilic.Add(rnt_wim_g_2013_Cilic);
            RezultatNaTurniru rnt_ham_d_2013_Cilic = new RezultatNaTurniru(ham_d_2013, 0); listaCilic.Add(rnt_ham_d_2013_Cilic);
            RezultatNaTurniru rnt_mrl_m_2013_Cilic = new RezultatNaTurniru(mrl_m_2013, 0); listaCilic.Add(rnt_mrl_m_2013_Cilic);
            RezultatNaTurniru rnt_cin_m_2013_Cilic = new RezultatNaTurniru(cin_m_2013, 0); listaCilic.Add(rnt_cin_m_2013_Cilic);
            RezultatNaTurniru rnt_uso_g_2013_Cilic = new RezultatNaTurniru(uso_g_2013, 0); listaCilic.Add(rnt_uso_g_2013_Cilic);
            RezultatNaTurniru rnt_bas_d_2013_Cilic = new RezultatNaTurniru(bas_d_2013, 0); listaCilic.Add(rnt_bas_d_2013_Cilic);

            RezultatNaTurniru rnt_aus_g_2014_Cilic = new RezultatNaTurniru(aus_g_2014, 45); listaCilic.Add(rnt_aus_g_2014_Cilic);
            RezultatNaTurniru rnt_dub_d_2014_Cilic = new RezultatNaTurniru(dub_d_2014, 0); listaCilic.Add(rnt_dub_d_2014_Cilic);
            RezultatNaTurniru rnt_mtc_m_2014_Cilic = new RezultatNaTurniru(mtc_m_2014, 45); listaCilic.Add(rnt_mtc_m_2014_Cilic);
            RezultatNaTurniru rnt_mad_m_2014_Cilic = new RezultatNaTurniru(mad_m_2014, 90); listaCilic.Add(rnt_mad_m_2014_Cilic);
            RezultatNaTurniru rnt_rom_m_2014_Cilic = new RezultatNaTurniru(rom_m_2014, 45); listaCilic.Add(rnt_rom_m_2014_Cilic);
            RezultatNaTurniru rnt_rgr_g_2014_Cilic = new RezultatNaTurniru(rgr_g_2014, 90); listaCilic.Add(rnt_rgr_g_2014_Cilic);
            RezultatNaTurniru rnt_wim_g_2014_Cilic = new RezultatNaTurniru(wim_g_2014, 360); listaCilic.Add(rnt_wim_g_2014_Cilic);
            RezultatNaTurniru rnt_ham_d_2014_Cilic = new RezultatNaTurniru(ham_d_2014, 0); listaCilic.Add(rnt_ham_d_2014_Cilic);
            RezultatNaTurniru rnt_mrl_m_2014_Cilic = new RezultatNaTurniru(mrl_m_2014, 90); listaCilic.Add(rnt_mrl_m_2014_Cilic);
            RezultatNaTurniru rnt_cin_m_2014_Cilic = new RezultatNaTurniru(cin_m_2014, 90); listaCilic.Add(rnt_cin_m_2014_Cilic);
            RezultatNaTurniru rnt_uso_g_2014_Cilic = new RezultatNaTurniru(uso_g_2014, 2000); listaCilic.Add(rnt_uso_g_2014_Cilic);
            RezultatNaTurniru rnt_bas_d_2014_Cilic = new RezultatNaTurniru(bas_d_2014, 0); listaCilic.Add(rnt_bas_d_2014_Cilic);

            RezultatNaTurniru rnt_aus_g_2015_Cilic = new RezultatNaTurniru(aus_g_2015, 0); listaCilic.Add(rnt_aus_g_2015_Cilic);
            RezultatNaTurniru rnt_dub_d_2015_Cilic = new RezultatNaTurniru(dub_d_2015, 0); listaCilic.Add(rnt_dub_d_2015_Cilic);
            RezultatNaTurniru rnt_mtc_m_2015_Cilic = new RezultatNaTurniru(mtc_m_2015, 180); listaCilic.Add(rnt_mtc_m_2015_Cilic);
            RezultatNaTurniru rnt_mad_m_2015_Cilic = new RezultatNaTurniru(mad_m_2015, 45); listaCilic.Add(rnt_mad_m_2015_Cilic);
            RezultatNaTurniru rnt_rom_m_2015_Cilic = new RezultatNaTurniru(rom_m_2015, 10); listaCilic.Add(rnt_rom_m_2015_Cilic);
            RezultatNaTurniru rnt_rgr_g_2015_Cilic = new RezultatNaTurniru(rgr_g_2015, 180); listaCilic.Add(rnt_rgr_g_2015_Cilic);
            RezultatNaTurniru rnt_wim_g_2015_Cilic = new RezultatNaTurniru(wim_g_2015, 360); listaCilic.Add(rnt_wim_g_2015_Cilic);
            RezultatNaTurniru rnt_ham_d_2015_Cilic = new RezultatNaTurniru(ham_d_2015, 0); listaCilic.Add(rnt_ham_d_2015_Cilic);
            RezultatNaTurniru rnt_mrl_m_2015_Cilic = new RezultatNaTurniru(mrl_m_2015, 45); listaCilic.Add(rnt_mrl_m_2015_Cilic);
            RezultatNaTurniru rnt_cin_m_2015_Cilic = new RezultatNaTurniru(cin_m_2015, 90); listaCilic.Add(rnt_cin_m_2015_Cilic);
            RezultatNaTurniru rnt_uso_g_2015_Cilic = new RezultatNaTurniru(uso_g_2015, 720); listaCilic.Add(rnt_uso_g_2015_Cilic);
            RezultatNaTurniru rnt_bas_d_2015_Cilic = new RezultatNaTurniru(bas_d_2015, 90); listaCilic.Add(rnt_bas_d_2015_Cilic);

            Teniser cilic = new Teniser("Marin Cilic (CRO)", new DateTime(1988, 9, 28), 9);
            cilic.ListaRezultata = listaCilic;


            List<RezultatNaTurniru> listaBerdych = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Berdych = new RezultatNaTurniru(aus_g_2013, 360); listaBerdych.Add(rnt_aus_g_2013_Berdych);
            RezultatNaTurniru rnt_dub_d_2013_Berdych = new RezultatNaTurniru(dub_d_2013, 300); listaBerdych.Add(rnt_dub_d_2013_Berdych);
            RezultatNaTurniru rnt_mtc_m_2013_Berdych = new RezultatNaTurniru(mtc_m_2013, 90); listaBerdych.Add(rnt_mtc_m_2013_Berdych);
            RezultatNaTurniru rnt_mad_m_2013_Berdych = new RezultatNaTurniru(mad_m_2013, 360); listaBerdych.Add(rnt_mad_m_2013_Berdych);
            RezultatNaTurniru rnt_rom_m_2013_Berdych = new RezultatNaTurniru(rom_m_2013, 360); listaBerdych.Add(rnt_rom_m_2013_Berdych);
            RezultatNaTurniru rnt_rgr_g_2013_Berdych = new RezultatNaTurniru(rgr_g_2013, 10); listaBerdych.Add(rnt_rgr_g_2013_Berdych);
            RezultatNaTurniru rnt_wim_g_2013_Berdych = new RezultatNaTurniru(wim_g_2013, 360); listaBerdych.Add(rnt_wim_g_2013_Berdych);
            RezultatNaTurniru rnt_ham_d_2013_Berdych = new RezultatNaTurniru(ham_d_2013, 0); listaBerdych.Add(rnt_ham_d_2013_Berdych);
            RezultatNaTurniru rnt_mrl_m_2013_Berdych = new RezultatNaTurniru(mrl_m_2013, 90); listaBerdych.Add(rnt_mrl_m_2013_Berdych);
            RezultatNaTurniru rnt_cin_m_2013_Berdych = new RezultatNaTurniru(cin_m_2013, 360); listaBerdych.Add(rnt_cin_m_2013_Berdych);
            RezultatNaTurniru rnt_uso_g_2013_Berdych = new RezultatNaTurniru(uso_g_2013, 180); listaBerdych.Add(rnt_uso_g_2013_Berdych);
            RezultatNaTurniru rnt_bas_d_2013_Berdych = new RezultatNaTurniru(bas_d_2013, 0); listaBerdych.Add(rnt_bas_d_2013_Berdych);

            RezultatNaTurniru rnt_aus_g_2014_Berdych = new RezultatNaTurniru(aus_g_2014, 720); listaBerdych.Add(rnt_aus_g_2014_Berdych);
            RezultatNaTurniru rnt_dub_d_2014_Berdych = new RezultatNaTurniru(dub_d_2014, 300); listaBerdych.Add(rnt_dub_d_2014_Berdych);
            RezultatNaTurniru rnt_mtc_m_2014_Berdych = new RezultatNaTurniru(mtc_m_2014, 90); listaBerdych.Add(rnt_mtc_m_2014_Berdych);
            RezultatNaTurniru rnt_mad_m_2014_Berdych = new RezultatNaTurniru(mad_m_2014, 180); listaBerdych.Add(rnt_mad_m_2014_Berdych);
            RezultatNaTurniru rnt_rom_m_2014_Berdych = new RezultatNaTurniru(rom_m_2014, 90); listaBerdych.Add(rnt_rom_m_2014_Berdych);
            RezultatNaTurniru rnt_rgr_g_2014_Berdych = new RezultatNaTurniru(rgr_g_2014, 360); listaBerdych.Add(rnt_rgr_g_2014_Berdych);
            RezultatNaTurniru rnt_wim_g_2014_Berdych = new RezultatNaTurniru(wim_g_2014, 90); listaBerdych.Add(rnt_wim_g_2014_Berdych);
            RezultatNaTurniru rnt_ham_d_2014_Berdych = new RezultatNaTurniru(ham_d_2014, 0); listaBerdych.Add(rnt_ham_d_2014_Berdych);
            RezultatNaTurniru rnt_mrl_m_2014_Berdych = new RezultatNaTurniru(mrl_m_2014, 90); listaBerdych.Add(rnt_mrl_m_2014_Berdych);
            RezultatNaTurniru rnt_cin_m_2014_Berdych = new RezultatNaTurniru(cin_m_2014, 45); listaBerdych.Add(rnt_cin_m_2014_Berdych);
            RezultatNaTurniru rnt_uso_g_2014_Berdych = new RezultatNaTurniru(uso_g_2014, 360); listaBerdych.Add(rnt_uso_g_2014_Berdych);
            RezultatNaTurniru rnt_bas_d_2014_Berdych = new RezultatNaTurniru(bas_d_2014, 0); listaBerdych.Add(rnt_bas_d_2014_Berdych);

            RezultatNaTurniru rnt_aus_g_2015_Berdych = new RezultatNaTurniru(aus_g_2015, 720); listaBerdych.Add(rnt_aus_g_2015_Berdych);
            RezultatNaTurniru rnt_dub_d_2015_Berdych = new RezultatNaTurniru(dub_d_2015, 180); listaBerdych.Add(rnt_dub_d_2015_Berdych);
            RezultatNaTurniru rnt_mtc_m_2015_Berdych = new RezultatNaTurniru(mtc_m_2015, 600); listaBerdych.Add(rnt_mtc_m_2015_Berdych);
            RezultatNaTurniru rnt_mad_m_2015_Berdych = new RezultatNaTurniru(mad_m_2015, 360); listaBerdych.Add(rnt_mad_m_2015_Berdych);
            RezultatNaTurniru rnt_rom_m_2015_Berdych = new RezultatNaTurniru(rom_m_2015, 180); listaBerdych.Add(rnt_rom_m_2015_Berdych);
            RezultatNaTurniru rnt_rgr_g_2015_Berdych = new RezultatNaTurniru(rgr_g_2015, 180); listaBerdych.Add(rnt_rgr_g_2015_Berdych);
            RezultatNaTurniru rnt_wim_g_2015_Berdych = new RezultatNaTurniru(wim_g_2015, 180); listaBerdych.Add(rnt_wim_g_2015_Berdych);
            RezultatNaTurniru rnt_ham_d_2015_Berdych = new RezultatNaTurniru(ham_d_2015, 0); listaBerdych.Add(rnt_ham_d_2015_Berdych);
            RezultatNaTurniru rnt_mrl_m_2015_Berdych = new RezultatNaTurniru(mrl_m_2015, 45); listaBerdych.Add(rnt_mrl_m_2015_Berdych);
            RezultatNaTurniru rnt_cin_m_2015_Berdych = new RezultatNaTurniru(cin_m_2015, 180); listaBerdych.Add(rnt_cin_m_2015_Berdych);
            RezultatNaTurniru rnt_uso_g_2015_Berdych = new RezultatNaTurniru(uso_g_2015, 180); listaBerdych.Add(rnt_uso_g_2015_Berdych);
            RezultatNaTurniru rnt_bas_d_2015_Berdych = new RezultatNaTurniru(bas_d_2015, 0); listaBerdych.Add(rnt_bas_d_2015_Berdych);

            Teniser berdych = new Teniser("Tomas Berdych (CZE)", new DateTime(1985, 9, 17), 8);
            berdych.ListaRezultata = listaBerdych;


            List<RezultatNaTurniru> listaNishikori = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Nishikori = new RezultatNaTurniru(aus_g_2013, 180); listaNishikori.Add(rnt_aus_g_2013_Nishikori);
            RezultatNaTurniru rnt_dub_d_2013_Nishikori = new RezultatNaTurniru(dub_d_2013, 0); listaNishikori.Add(rnt_dub_d_2013_Nishikori);
            RezultatNaTurniru rnt_mtc_m_2013_Nishikori = new RezultatNaTurniru(mtc_m_2013, 0); listaNishikori.Add(rnt_mtc_m_2013_Nishikori);
            RezultatNaTurniru rnt_mad_m_2013_Nishikori = new RezultatNaTurniru(mad_m_2013, 180); listaNishikori.Add(rnt_mad_m_2013_Nishikori);
            RezultatNaTurniru rnt_rom_m_2013_Nishikori = new RezultatNaTurniru(rom_m_2013, 45); listaNishikori.Add(rnt_rom_m_2013_Nishikori);
            RezultatNaTurniru rnt_rgr_g_2013_Nishikori = new RezultatNaTurniru(rgr_g_2013, 180); listaNishikori.Add(rnt_rgr_g_2013_Nishikori);
            RezultatNaTurniru rnt_wim_g_2013_Nishikori = new RezultatNaTurniru(wim_g_2013, 90); listaNishikori.Add(rnt_wim_g_2013_Nishikori);
            RezultatNaTurniru rnt_ham_d_2013_Nishikori = new RezultatNaTurniru(ham_d_2013, 0); listaNishikori.Add(rnt_ham_d_2013_Nishikori);
            RezultatNaTurniru rnt_mrl_m_2013_Nishikori = new RezultatNaTurniru(mrl_m_2013, 90); listaNishikori.Add(rnt_mrl_m_2013_Nishikori);
            RezultatNaTurniru rnt_cin_m_2013_Nishikori = new RezultatNaTurniru(cin_m_2013, 10); listaNishikori.Add(rnt_cin_m_2013_Nishikori);
            RezultatNaTurniru rnt_uso_g_2013_Nishikori = new RezultatNaTurniru(uso_g_2013, 10); listaNishikori.Add(rnt_uso_g_2013_Nishikori);
            RezultatNaTurniru rnt_bas_d_2013_Nishikori = new RezultatNaTurniru(bas_d_2013, 45); listaNishikori.Add(rnt_bas_d_2013_Nishikori);

            RezultatNaTurniru rnt_aus_g_2014_Nishikori = new RezultatNaTurniru(aus_g_2014, 180); listaNishikori.Add(rnt_aus_g_2014_Nishikori);
            RezultatNaTurniru rnt_dub_d_2014_Nishikori = new RezultatNaTurniru(dub_d_2014, 0); listaNishikori.Add(rnt_dub_d_2014_Nishikori);
            RezultatNaTurniru rnt_mtc_m_2014_Nishikori = new RezultatNaTurniru(mtc_m_2014, 0); listaNishikori.Add(rnt_mtc_m_2014_Nishikori);
            RezultatNaTurniru rnt_mad_m_2014_Nishikori = new RezultatNaTurniru(mad_m_2014, 600); listaNishikori.Add(rnt_mad_m_2014_Nishikori);
            RezultatNaTurniru rnt_rom_m_2014_Nishikori = new RezultatNaTurniru(rom_m_2014, 0); listaNishikori.Add(rnt_rom_m_2014_Nishikori);
            RezultatNaTurniru rnt_rgr_g_2014_Nishikori = new RezultatNaTurniru(rgr_g_2014, 10); listaNishikori.Add(rnt_rgr_g_2014_Nishikori);
            RezultatNaTurniru rnt_wim_g_2014_Nishikori = new RezultatNaTurniru(wim_g_2014, 180); listaNishikori.Add(rnt_wim_g_2014_Nishikori);
            RezultatNaTurniru rnt_ham_d_2014_Nishikori = new RezultatNaTurniru(ham_d_2014, 0); listaNishikori.Add(rnt_ham_d_2014_Nishikori);
            RezultatNaTurniru rnt_mrl_m_2014_Nishikori = new RezultatNaTurniru(mrl_m_2014, 0); listaNishikori.Add(rnt_mrl_m_2014_Nishikori);
            RezultatNaTurniru rnt_cin_m_2014_Nishikori = new RezultatNaTurniru(cin_m_2014, 0); listaNishikori.Add(rnt_cin_m_2014_Nishikori);
            RezultatNaTurniru rnt_uso_g_2014_Nishikori = new RezultatNaTurniru(uso_g_2014, 1200); listaNishikori.Add(rnt_uso_g_2014_Nishikori);
            RezultatNaTurniru rnt_bas_d_2014_Nishikori = new RezultatNaTurniru(bas_d_2014, 0); listaNishikori.Add(rnt_bas_d_2014_Nishikori);

            RezultatNaTurniru rnt_aus_g_2015_Nishikori = new RezultatNaTurniru(aus_g_2015, 360); listaNishikori.Add(rnt_aus_g_2015_Nishikori);
            RezultatNaTurniru rnt_dub_d_2015_Nishikori = new RezultatNaTurniru(dub_d_2015, 0); listaNishikori.Add(rnt_dub_d_2015_Nishikori);
            RezultatNaTurniru rnt_mtc_m_2015_Nishikori = new RezultatNaTurniru(mtc_m_2015, 0); listaNishikori.Add(rnt_mtc_m_2015_Nishikori);
            RezultatNaTurniru rnt_mad_m_2015_Nishikori = new RezultatNaTurniru(mad_m_2015, 360); listaNishikori.Add(rnt_mad_m_2015_Nishikori);
            RezultatNaTurniru rnt_rom_m_2015_Nishikori = new RezultatNaTurniru(rom_m_2015, 180); listaNishikori.Add(rnt_rom_m_2015_Nishikori);
            RezultatNaTurniru rnt_rgr_g_2015_Nishikori = new RezultatNaTurniru(rgr_g_2015, 360); listaNishikori.Add(rnt_rgr_g_2015_Nishikori);
            RezultatNaTurniru rnt_wim_g_2015_Nishikori = new RezultatNaTurniru(wim_g_2015, 45); listaNishikori.Add(rnt_wim_g_2015_Nishikori);
            RezultatNaTurniru rnt_ham_d_2015_Nishikori = new RezultatNaTurniru(ham_d_2015, 0); listaNishikori.Add(rnt_ham_d_2015_Nishikori);
            RezultatNaTurniru rnt_mrl_m_2015_Nishikori = new RezultatNaTurniru(mrl_m_2015, 360); listaNishikori.Add(rnt_mrl_m_2015_Nishikori);
            RezultatNaTurniru rnt_cin_m_2015_Nishikori = new RezultatNaTurniru(cin_m_2015, 0); listaNishikori.Add(rnt_cin_m_2015_Nishikori);
            RezultatNaTurniru rnt_uso_g_2015_Nishikori = new RezultatNaTurniru(uso_g_2015, 10); listaNishikori.Add(rnt_uso_g_2015_Nishikori);
            RezultatNaTurniru rnt_bas_d_2015_Nishikori = new RezultatNaTurniru(bas_d_2015, 0); listaNishikori.Add(rnt_bas_d_2015_Nishikori);

            Teniser nishikori = new Teniser("Kei Nishikori (JPN)", new DateTime(1989, 12, 29), 7);
            nishikori.ListaRezultata = listaNishikori;

            List<RezultatNaTurniru> listaRaonic = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Raonic = new RezultatNaTurniru(aus_g_2013, 180); listaRaonic.Add(rnt_aus_g_2013_Raonic);
            RezultatNaTurniru rnt_dub_d_2013_Raonic = new RezultatNaTurniru(dub_d_2013, 0); listaRaonic.Add(rnt_dub_d_2013_Raonic);
            RezultatNaTurniru rnt_mtc_m_2013_Raonic = new RezultatNaTurniru(mtc_m_2013, 45); listaRaonic.Add(rnt_mtc_m_2013_Raonic);
            RezultatNaTurniru rnt_mad_m_2013_Raonic = new RezultatNaTurniru(mad_m_2013, 45); listaRaonic.Add(rnt_mad_m_2013_Raonic);
            RezultatNaTurniru rnt_rom_m_2013_Raonic = new RezultatNaTurniru(rom_m_2013, 10); listaRaonic.Add(rnt_rom_m_2013_Raonic);
            RezultatNaTurniru rnt_rgr_g_2013_Raonic = new RezultatNaTurniru(rgr_g_2013, 90); listaRaonic.Add(rnt_rgr_g_2013_Raonic);
            RezultatNaTurniru rnt_wim_g_2013_Raonic = new RezultatNaTurniru(wim_g_2013, 45); listaRaonic.Add(rnt_wim_g_2013_Raonic);
            RezultatNaTurniru rnt_ham_d_2013_Raonic = new RezultatNaTurniru(ham_d_2013, 0); listaRaonic.Add(rnt_ham_d_2013_Raonic);
            RezultatNaTurniru rnt_mrl_m_2013_Raonic = new RezultatNaTurniru(mrl_m_2013, 600); listaRaonic.Add(rnt_mrl_m_2013_Raonic);
            RezultatNaTurniru rnt_cin_m_2013_Raonic = new RezultatNaTurniru(cin_m_2013, 90); listaRaonic.Add(rnt_cin_m_2013_Raonic);
            RezultatNaTurniru rnt_uso_g_2013_Raonic = new RezultatNaTurniru(uso_g_2013, 180); listaRaonic.Add(rnt_uso_g_2013_Raonic);
            RezultatNaTurniru rnt_bas_d_2013_Raonic = new RezultatNaTurniru(bas_d_2013, 0); listaRaonic.Add(rnt_bas_d_2013_Raonic);

            RezultatNaTurniru rnt_aus_g_2014_Raonic = new RezultatNaTurniru(aus_g_2014, 90); listaRaonic.Add(rnt_aus_g_2014_Raonic);
            RezultatNaTurniru rnt_dub_d_2014_Raonic = new RezultatNaTurniru(dub_d_2014, 0); listaRaonic.Add(rnt_dub_d_2014_Raonic);
            RezultatNaTurniru rnt_mtc_m_2014_Raonic = new RezultatNaTurniru(mtc_m_2014, 180); listaRaonic.Add(rnt_mtc_m_2014_Raonic);
            RezultatNaTurniru rnt_mad_m_2014_Raonic = new RezultatNaTurniru(mad_m_2014, 90); listaRaonic.Add(rnt_mad_m_2014_Raonic);
            RezultatNaTurniru rnt_rom_m_2014_Raonic = new RezultatNaTurniru(rom_m_2014, 360); listaRaonic.Add(rnt_rom_m_2014_Raonic);
            RezultatNaTurniru rnt_rgr_g_2014_Raonic = new RezultatNaTurniru(rgr_g_2014, 360); listaRaonic.Add(rnt_rgr_g_2014_Raonic);
            RezultatNaTurniru rnt_wim_g_2014_Raonic = new RezultatNaTurniru(wim_g_2014, 720); listaRaonic.Add(rnt_wim_g_2014_Raonic);
            RezultatNaTurniru rnt_ham_d_2014_Raonic = new RezultatNaTurniru(ham_d_2014, 0); listaRaonic.Add(rnt_ham_d_2014_Raonic);
            RezultatNaTurniru rnt_mrl_m_2014_Raonic = new RezultatNaTurniru(mrl_m_2014, 180); listaRaonic.Add(rnt_mrl_m_2014_Raonic);
            RezultatNaTurniru rnt_cin_m_2014_Raonic = new RezultatNaTurniru(cin_m_2014, 360); listaRaonic.Add(rnt_cin_m_2014_Raonic);
            RezultatNaTurniru rnt_uso_g_2014_Raonic = new RezultatNaTurniru(uso_g_2014, 180); listaRaonic.Add(rnt_uso_g_2014_Raonic);
            RezultatNaTurniru rnt_bas_d_2014_Raonic = new RezultatNaTurniru(bas_d_2014, 90); listaRaonic.Add(rnt_bas_d_2014_Raonic);

            RezultatNaTurniru rnt_aus_g_2015_Raonic = new RezultatNaTurniru(aus_g_2015, 360); listaRaonic.Add(rnt_aus_g_2015_Raonic);
            RezultatNaTurniru rnt_dub_d_2015_Raonic = new RezultatNaTurniru(dub_d_2015, 0); listaRaonic.Add(rnt_dub_d_2015_Raonic);
            RezultatNaTurniru rnt_mtc_m_2015_Raonic = new RezultatNaTurniru(mtc_m_2015, 180); listaRaonic.Add(rnt_mtc_m_2015_Raonic);
            RezultatNaTurniru rnt_mad_m_2015_Raonic = new RezultatNaTurniru(mad_m_2015, 180); listaRaonic.Add(rnt_mad_m_2015_Raonic);
            RezultatNaTurniru rnt_rom_m_2015_Raonic = new RezultatNaTurniru(rom_m_2015, 0); listaRaonic.Add(rnt_rom_m_2015_Raonic);
            RezultatNaTurniru rnt_rgr_g_2015_Raonic = new RezultatNaTurniru(rgr_g_2015, 0); listaRaonic.Add(rnt_rgr_g_2015_Raonic);
            RezultatNaTurniru rnt_wim_g_2015_Raonic = new RezultatNaTurniru(wim_g_2015, 90); listaRaonic.Add(rnt_wim_g_2015_Raonic);
            RezultatNaTurniru rnt_ham_d_2015_Raonic = new RezultatNaTurniru(ham_d_2015, 0); listaRaonic.Add(rnt_ham_d_2015_Raonic);
            RezultatNaTurniru rnt_mrl_m_2015_Raonic = new RezultatNaTurniru(mrl_m_2015, 45); listaRaonic.Add(rnt_mrl_m_2015_Raonic);
            RezultatNaTurniru rnt_cin_m_2015_Raonic = new RezultatNaTurniru(cin_m_2015, 10); listaRaonic.Add(rnt_cin_m_2015_Raonic);
            RezultatNaTurniru rnt_uso_g_2015_Raonic = new RezultatNaTurniru(uso_g_2015, 90); listaRaonic.Add(rnt_uso_g_2015_Raonic);
            RezultatNaTurniru rnt_bas_d_2015_Raonic = new RezultatNaTurniru(bas_d_2015, 0); listaRaonic.Add(rnt_bas_d_2015_Raonic);

            Teniser raonic = new Teniser("Milos Raonic (CAN)", new DateTime(1990, 12, 27), 6);
            raonic.ListaRezultata = listaRaonic;


            List<RezultatNaTurniru> listaNadal = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Nadal = new RezultatNaTurniru(aus_g_2013, 0); listaNadal.Add(rnt_aus_g_2013_Nadal);
            RezultatNaTurniru rnt_dub_d_2013_Nadal = new RezultatNaTurniru(dub_d_2013, 0); listaNadal.Add(rnt_dub_d_2013_Nadal);
            RezultatNaTurniru rnt_mtc_m_2013_Nadal = new RezultatNaTurniru(mtc_m_2013, 600); listaNadal.Add(rnt_mtc_m_2013_Nadal);
            RezultatNaTurniru rnt_mad_m_2013_Nadal = new RezultatNaTurniru(mad_m_2013, 1000); listaNadal.Add(rnt_mad_m_2013_Nadal);
            RezultatNaTurniru rnt_rom_m_2013_Nadal = new RezultatNaTurniru(rom_m_2013, 1000); listaNadal.Add(rnt_rom_m_2013_Nadal);
            RezultatNaTurniru rnt_rgr_g_2013_Nadal = new RezultatNaTurniru(rgr_g_2013, 2000); listaNadal.Add(rnt_rgr_g_2013_Nadal);
            RezultatNaTurniru rnt_wim_g_2013_Nadal = new RezultatNaTurniru(wim_g_2013, 10); listaNadal.Add(rnt_wim_g_2013_Nadal);
            RezultatNaTurniru rnt_ham_d_2013_Nadal = new RezultatNaTurniru(ham_d_2013, 0); listaNadal.Add(rnt_ham_d_2013_Nadal);
            RezultatNaTurniru rnt_mrl_m_2013_Nadal = new RezultatNaTurniru(mrl_m_2013, 1000); listaNadal.Add(rnt_mrl_m_2013_Nadal);
            RezultatNaTurniru rnt_cin_m_2013_Nadal = new RezultatNaTurniru(cin_m_2013, 1000); listaNadal.Add(rnt_cin_m_2013_Nadal);
            RezultatNaTurniru rnt_uso_g_2013_Nadal = new RezultatNaTurniru(uso_g_2013, 2000); listaNadal.Add(rnt_uso_g_2013_Nadal);
            RezultatNaTurniru rnt_bas_d_2013_Nadal = new RezultatNaTurniru(bas_d_2013, 0); listaNadal.Add(rnt_bas_d_2013_Nadal);

            RezultatNaTurniru rnt_aus_g_2014_Nadal = new RezultatNaTurniru(aus_g_2014, 1200); listaNadal.Add(rnt_aus_g_2014_Nadal);
            RezultatNaTurniru rnt_dub_d_2014_Nadal = new RezultatNaTurniru(dub_d_2014, 0); listaNadal.Add(rnt_dub_d_2014_Nadal);
            RezultatNaTurniru rnt_mtc_m_2014_Nadal = new RezultatNaTurniru(mtc_m_2014, 180); listaNadal.Add(rnt_mtc_m_2014_Nadal);
            RezultatNaTurniru rnt_mad_m_2014_Nadal = new RezultatNaTurniru(mad_m_2014, 1000); listaNadal.Add(rnt_mad_m_2014_Nadal);
            RezultatNaTurniru rnt_rom_m_2014_Nadal = new RezultatNaTurniru(rom_m_2014, 600); listaNadal.Add(rnt_rom_m_2014_Nadal);
            RezultatNaTurniru rnt_rgr_g_2014_Nadal = new RezultatNaTurniru(rgr_g_2014, 2000); listaNadal.Add(rnt_rgr_g_2014_Nadal);
            RezultatNaTurniru rnt_wim_g_2014_Nadal = new RezultatNaTurniru(wim_g_2014, 180); listaNadal.Add(rnt_wim_g_2014_Nadal);
            RezultatNaTurniru rnt_ham_d_2014_Nadal = new RezultatNaTurniru(ham_d_2014, 0); listaNadal.Add(rnt_ham_d_2014_Nadal);
            RezultatNaTurniru rnt_mrl_m_2014_Nadal = new RezultatNaTurniru(mrl_m_2014, 0); listaNadal.Add(rnt_mrl_m_2014_Nadal);
            RezultatNaTurniru rnt_cin_m_2014_Nadal = new RezultatNaTurniru(cin_m_2014, 0); listaNadal.Add(rnt_cin_m_2014_Nadal);
            RezultatNaTurniru rnt_uso_g_2014_Nadal = new RezultatNaTurniru(uso_g_2014, 0); listaNadal.Add(rnt_uso_g_2014_Nadal);
            RezultatNaTurniru rnt_bas_d_2014_Nadal = new RezultatNaTurniru(bas_d_2014, 90); listaNadal.Add(rnt_bas_d_2014_Nadal);

            RezultatNaTurniru rnt_aus_g_2015_Nadal = new RezultatNaTurniru(aus_g_2015, 360); listaNadal.Add(rnt_aus_g_2015_Nadal);
            RezultatNaTurniru rnt_dub_d_2015_Nadal = new RezultatNaTurniru(dub_d_2015, 0); listaNadal.Add(rnt_dub_d_2015_Nadal);
            RezultatNaTurniru rnt_mtc_m_2015_Nadal = new RezultatNaTurniru(mtc_m_2015, 360); listaNadal.Add(rnt_mtc_m_2015_Nadal);
            RezultatNaTurniru rnt_mad_m_2015_Nadal = new RezultatNaTurniru(mad_m_2015, 600); listaNadal.Add(rnt_mad_m_2015_Nadal);
            RezultatNaTurniru rnt_rom_m_2015_Nadal = new RezultatNaTurniru(rom_m_2015, 180); listaNadal.Add(rnt_rom_m_2015_Nadal);
            RezultatNaTurniru rnt_rgr_g_2015_Nadal = new RezultatNaTurniru(rgr_g_2015, 360); listaNadal.Add(rnt_rgr_g_2015_Nadal);
            RezultatNaTurniru rnt_wim_g_2015_Nadal = new RezultatNaTurniru(wim_g_2015, 45); listaNadal.Add(rnt_wim_g_2015_Nadal);
            RezultatNaTurniru rnt_ham_d_2015_Nadal = new RezultatNaTurniru(ham_d_2015, 500); listaNadal.Add(rnt_ham_d_2015_Nadal);
            RezultatNaTurniru rnt_mrl_m_2015_Nadal = new RezultatNaTurniru(mrl_m_2015, 180); listaNadal.Add(rnt_mrl_m_2015_Nadal);
            RezultatNaTurniru rnt_cin_m_2015_Nadal = new RezultatNaTurniru(cin_m_2015, 90); listaNadal.Add(rnt_cin_m_2015_Nadal);
            RezultatNaTurniru rnt_uso_g_2015_Nadal = new RezultatNaTurniru(uso_g_2015, 90); listaNadal.Add(rnt_uso_g_2015_Nadal);
            RezultatNaTurniru rnt_bas_d_2015_Nadal = new RezultatNaTurniru(bas_d_2015, 300); listaNadal.Add(rnt_bas_d_2015_Nadal);

            Teniser nadal = new Teniser("Rafael Nadal (ESP)", new DateTime(1986, 6, 3), 5);
            nadal.ListaRezultata = listaNadal;


            List<RezultatNaTurniru> listaFederer = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Federer = new RezultatNaTurniru(aus_g_2013, 720); listaFederer.Add(rnt_aus_g_2013_Federer);
            RezultatNaTurniru rnt_dub_d_2013_Federer = new RezultatNaTurniru(dub_d_2013, 180); listaFederer.Add(rnt_dub_d_2013_Federer);
            RezultatNaTurniru rnt_mtc_m_2013_Federer = new RezultatNaTurniru(mtc_m_2013, 0); listaFederer.Add(rnt_mtc_m_2013_Federer);
            RezultatNaTurniru rnt_mad_m_2013_Federer = new RezultatNaTurniru(mad_m_2013, 90); listaFederer.Add(rnt_mad_m_2013_Federer);
            RezultatNaTurniru rnt_rom_m_2013_Federer = new RezultatNaTurniru(rom_m_2013, 600); listaFederer.Add(rnt_rom_m_2013_Federer);
            RezultatNaTurniru rnt_rgr_g_2013_Federer = new RezultatNaTurniru(rgr_g_2013, 360); listaFederer.Add(rnt_rgr_g_2013_Federer);
            RezultatNaTurniru rnt_wim_g_2013_Federer = new RezultatNaTurniru(wim_g_2013, 45); listaFederer.Add(rnt_wim_g_2013_Federer);
            RezultatNaTurniru rnt_ham_d_2013_Federer = new RezultatNaTurniru(ham_d_2013, 180); listaFederer.Add(rnt_ham_d_2013_Federer);
            RezultatNaTurniru rnt_mrl_m_2013_Federer = new RezultatNaTurniru(mrl_m_2013, 0); listaFederer.Add(rnt_mrl_m_2013_Federer);
            RezultatNaTurniru rnt_cin_m_2013_Federer = new RezultatNaTurniru(cin_m_2013, 180); listaFederer.Add(rnt_cin_m_2013_Federer);
            RezultatNaTurniru rnt_uso_g_2013_Federer = new RezultatNaTurniru(uso_g_2013, 180); listaFederer.Add(rnt_uso_g_2013_Federer);
            RezultatNaTurniru rnt_bas_d_2013_Federer = new RezultatNaTurniru(bas_d_2013, 300); listaFederer.Add(rnt_bas_d_2013_Federer);

            RezultatNaTurniru rnt_aus_g_2014_Federer = new RezultatNaTurniru(aus_g_2014, 720); listaFederer.Add(rnt_aus_g_2014_Federer);
            RezultatNaTurniru rnt_dub_d_2014_Federer = new RezultatNaTurniru(dub_d_2014, 500); listaFederer.Add(rnt_dub_d_2014_Federer);
            RezultatNaTurniru rnt_mtc_m_2014_Federer = new RezultatNaTurniru(mtc_m_2014, 600); listaFederer.Add(rnt_mtc_m_2014_Federer);
            RezultatNaTurniru rnt_mad_m_2014_Federer = new RezultatNaTurniru(mad_m_2014, 0); listaFederer.Add(rnt_mad_m_2014_Federer);
            RezultatNaTurniru rnt_rom_m_2014_Federer = new RezultatNaTurniru(rom_m_2014, 45); listaFederer.Add(rnt_rom_m_2014_Federer);
            RezultatNaTurniru rnt_rgr_g_2014_Federer = new RezultatNaTurniru(rgr_g_2014, 180); listaFederer.Add(rnt_rgr_g_2014_Federer);
            RezultatNaTurniru rnt_wim_g_2014_Federer = new RezultatNaTurniru(wim_g_2014, 1200); listaFederer.Add(rnt_wim_g_2014_Federer);
            RezultatNaTurniru rnt_ham_d_2014_Federer = new RezultatNaTurniru(ham_d_2014, 0); listaFederer.Add(rnt_ham_d_2014_Federer);
            RezultatNaTurniru rnt_mrl_m_2014_Federer = new RezultatNaTurniru(mrl_m_2014, 600); listaFederer.Add(rnt_mrl_m_2014_Federer);
            RezultatNaTurniru rnt_cin_m_2014_Federer = new RezultatNaTurniru(cin_m_2014, 1000); listaFederer.Add(rnt_cin_m_2014_Federer);
            RezultatNaTurniru rnt_uso_g_2014_Federer = new RezultatNaTurniru(uso_g_2014, 720); listaFederer.Add(rnt_uso_g_2014_Federer);
            RezultatNaTurniru rnt_bas_d_2014_Federer = new RezultatNaTurniru(bas_d_2014, 500); listaFederer.Add(rnt_bas_d_2014_Federer);

            RezultatNaTurniru rnt_aus_g_2015_Federer = new RezultatNaTurniru(aus_g_2015, 90); listaFederer.Add(rnt_aus_g_2015_Federer);
            RezultatNaTurniru rnt_dub_d_2015_Federer = new RezultatNaTurniru(dub_d_2015, 500); listaFederer.Add(rnt_dub_d_2015_Federer);
            RezultatNaTurniru rnt_mtc_m_2015_Federer = new RezultatNaTurniru(mtc_m_2015, 90); listaFederer.Add(rnt_mtc_m_2015_Federer);
            RezultatNaTurniru rnt_mad_m_2015_Federer = new RezultatNaTurniru(mad_m_2015, 45); listaFederer.Add(rnt_mad_m_2015_Federer);
            RezultatNaTurniru rnt_rom_m_2015_Federer = new RezultatNaTurniru(rom_m_2015, 600); listaFederer.Add(rnt_rom_m_2015_Federer);
            RezultatNaTurniru rnt_rgr_g_2015_Federer = new RezultatNaTurniru(rgr_g_2015, 360); listaFederer.Add(rnt_rgr_g_2015_Federer);
            RezultatNaTurniru rnt_wim_g_2015_Federer = new RezultatNaTurniru(wim_g_2015, 1200); listaFederer.Add(rnt_wim_g_2015_Federer);
            RezultatNaTurniru rnt_ham_d_2015_Federer = new RezultatNaTurniru(ham_d_2015, 0); listaFederer.Add(rnt_ham_d_2015_Federer);
            RezultatNaTurniru rnt_mrl_m_2015_Federer = new RezultatNaTurniru(mrl_m_2015, 0); listaFederer.Add(rnt_mrl_m_2015_Federer);
            RezultatNaTurniru rnt_cin_m_2015_Federer = new RezultatNaTurniru(cin_m_2015, 1000); listaFederer.Add(rnt_cin_m_2015_Federer);
            RezultatNaTurniru rnt_uso_g_2015_Federer = new RezultatNaTurniru(uso_g_2015, 1200); listaFederer.Add(rnt_uso_g_2015_Federer);
            RezultatNaTurniru rnt_bas_d_2015_Federer = new RezultatNaTurniru(bas_d_2015, 500); listaFederer.Add(rnt_bas_d_2015_Federer);

            Teniser federer = new Teniser("Roger Federer (SUI)", new DateTime(1981, 8, 8), 4);
            federer.ListaRezultata = listaFederer;


            List<RezultatNaTurniru> listaWawrinka = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Wawrinka = new RezultatNaTurniru(aus_g_2013, 180); listaWawrinka.Add(rnt_aus_g_2013_Wawrinka);
            RezultatNaTurniru rnt_dub_d_2013_Wawrinka = new RezultatNaTurniru(dub_d_2013, 0); listaWawrinka.Add(rnt_dub_d_2013_Wawrinka);
            RezultatNaTurniru rnt_mtc_m_2013_Wawrinka = new RezultatNaTurniru(mtc_m_2013, 180); listaWawrinka.Add(rnt_mtc_m_2013_Wawrinka);
            RezultatNaTurniru rnt_mad_m_2013_Wawrinka = new RezultatNaTurniru(mad_m_2013, 600); listaWawrinka.Add(rnt_mad_m_2013_Wawrinka);
            RezultatNaTurniru rnt_rom_m_2013_Wawrinka = new RezultatNaTurniru(rom_m_2013, 45); listaWawrinka.Add(rnt_rom_m_2013_Wawrinka);
            RezultatNaTurniru rnt_rgr_g_2013_Wawrinka = new RezultatNaTurniru(rgr_g_2013, 360); listaWawrinka.Add(rnt_rgr_g_2013_Wawrinka);
            RezultatNaTurniru rnt_wim_g_2013_Wawrinka = new RezultatNaTurniru(wim_g_2013, 10); listaWawrinka.Add(rnt_wim_g_2013_Wawrinka);
            RezultatNaTurniru rnt_ham_d_2013_Wawrinka = new RezultatNaTurniru(ham_d_2013, 0); listaWawrinka.Add(rnt_ham_d_2013_Wawrinka);
            RezultatNaTurniru rnt_mrl_m_2013_Wawrinka = new RezultatNaTurniru(mrl_m_2013, 45); listaWawrinka.Add(rnt_mrl_m_2013_Wawrinka);
            RezultatNaTurniru rnt_cin_m_2013_Wawrinka = new RezultatNaTurniru(cin_m_2013, 45); listaWawrinka.Add(rnt_cin_m_2013_Wawrinka);
            RezultatNaTurniru rnt_uso_g_2013_Wawrinka = new RezultatNaTurniru(uso_g_2013, 720); listaWawrinka.Add(rnt_uso_g_2013_Wawrinka);
            RezultatNaTurniru rnt_bas_d_2013_Wawrinka = new RezultatNaTurniru(bas_d_2013, 0); listaWawrinka.Add(rnt_bas_d_2013_Wawrinka);

            RezultatNaTurniru rnt_aus_g_2014_Wawrinka = new RezultatNaTurniru(aus_g_2014, 2000); listaWawrinka.Add(rnt_aus_g_2014_Wawrinka);
            RezultatNaTurniru rnt_dub_d_2014_Wawrinka = new RezultatNaTurniru(dub_d_2014, 0); listaWawrinka.Add(rnt_dub_d_2014_Wawrinka);
            RezultatNaTurniru rnt_mtc_m_2014_Wawrinka = new RezultatNaTurniru(mtc_m_2014, 1000); listaWawrinka.Add(rnt_mtc_m_2014_Wawrinka);
            RezultatNaTurniru rnt_mad_m_2014_Wawrinka = new RezultatNaTurniru(mad_m_2014, 45); listaWawrinka.Add(rnt_mad_m_2014_Wawrinka);
            RezultatNaTurniru rnt_rom_m_2014_Wawrinka = new RezultatNaTurniru(rom_m_2014, 90); listaWawrinka.Add(rnt_rom_m_2014_Wawrinka);
            RezultatNaTurniru rnt_rgr_g_2014_Wawrinka = new RezultatNaTurniru(rgr_g_2014, 10); listaWawrinka.Add(rnt_rgr_g_2014_Wawrinka);
            RezultatNaTurniru rnt_wim_g_2014_Wawrinka = new RezultatNaTurniru(wim_g_2014, 360); listaWawrinka.Add(rnt_wim_g_2014_Wawrinka);
            RezultatNaTurniru rnt_ham_d_2014_Wawrinka = new RezultatNaTurniru(ham_d_2014, 0); listaWawrinka.Add(rnt_ham_d_2014_Wawrinka);
            RezultatNaTurniru rnt_mrl_m_2014_Wawrinka = new RezultatNaTurniru(mrl_m_2014, 90); listaWawrinka.Add(rnt_mrl_m_2014_Wawrinka);
            RezultatNaTurniru rnt_cin_m_2014_Wawrinka = new RezultatNaTurniru(cin_m_2014, 180); listaWawrinka.Add(rnt_cin_m_2014_Wawrinka);
            RezultatNaTurniru rnt_uso_g_2014_Wawrinka = new RezultatNaTurniru(uso_g_2014, 360); listaWawrinka.Add(rnt_uso_g_2014_Wawrinka);
            RezultatNaTurniru rnt_bas_d_2014_Wawrinka = new RezultatNaTurniru(bas_d_2014, 0); listaWawrinka.Add(rnt_bas_d_2014_Wawrinka);

            RezultatNaTurniru rnt_aus_g_2015_Wawrinka = new RezultatNaTurniru(aus_g_2015, 720); listaWawrinka.Add(rnt_aus_g_2015_Wawrinka);
            RezultatNaTurniru rnt_dub_d_2015_Wawrinka = new RezultatNaTurniru(dub_d_2015, 0); listaWawrinka.Add(rnt_dub_d_2015_Wawrinka);
            RezultatNaTurniru rnt_mtc_m_2015_Wawrinka = new RezultatNaTurniru(mtc_m_2015, 90); listaWawrinka.Add(rnt_mtc_m_2015_Wawrinka);
            RezultatNaTurniru rnt_mad_m_2015_Wawrinka = new RezultatNaTurniru(mad_m_2015, 90); listaWawrinka.Add(rnt_mad_m_2015_Wawrinka);
            RezultatNaTurniru rnt_rom_m_2015_Wawrinka = new RezultatNaTurniru(rom_m_2015, 360); listaWawrinka.Add(rnt_rom_m_2015_Wawrinka);
            RezultatNaTurniru rnt_rgr_g_2015_Wawrinka = new RezultatNaTurniru(rgr_g_2015, 2000); listaWawrinka.Add(rnt_rgr_g_2015_Wawrinka);
            RezultatNaTurniru rnt_wim_g_2015_Wawrinka = new RezultatNaTurniru(wim_g_2015, 360); listaWawrinka.Add(rnt_wim_g_2015_Wawrinka);
            RezultatNaTurniru rnt_ham_d_2015_Wawrinka = new RezultatNaTurniru(ham_d_2015, 0); listaWawrinka.Add(rnt_ham_d_2015_Wawrinka);
            RezultatNaTurniru rnt_mrl_m_2015_Wawrinka = new RezultatNaTurniru(mrl_m_2015, 45); listaWawrinka.Add(rnt_mrl_m_2015_Wawrinka);
            RezultatNaTurniru rnt_cin_m_2015_Wawrinka = new RezultatNaTurniru(cin_m_2015, 180); listaWawrinka.Add(rnt_cin_m_2015_Wawrinka);
            RezultatNaTurniru rnt_uso_g_2015_Wawrinka = new RezultatNaTurniru(uso_g_2015, 720); listaWawrinka.Add(rnt_uso_g_2015_Wawrinka);
            RezultatNaTurniru rnt_bas_d_2015_Wawrinka = new RezultatNaTurniru(bas_d_2015, 0); listaWawrinka.Add(rnt_bas_d_2015_Wawrinka);

            Teniser wawrinka = new Teniser("Stan Wawrinka (SUI)", new DateTime(1985, 3, 28), 3);
            wawrinka.ListaRezultata = listaWawrinka;


            List<RezultatNaTurniru> listaMurray = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Murray = new RezultatNaTurniru(aus_g_2013, 1200); listaMurray.Add(rnt_aus_g_2013_Murray);
            RezultatNaTurniru rnt_dub_d_2013_Murray = new RezultatNaTurniru(dub_d_2013, 0); listaMurray.Add(rnt_dub_d_2013_Murray);
            RezultatNaTurniru rnt_mtc_m_2013_Murray = new RezultatNaTurniru(mtc_m_2013, 90); listaMurray.Add(rnt_mtc_m_2013_Murray);
            RezultatNaTurniru rnt_mad_m_2013_Murray = new RezultatNaTurniru(mad_m_2013, 180); listaMurray.Add(rnt_mad_m_2013_Murray);
            RezultatNaTurniru rnt_rom_m_2013_Murray = new RezultatNaTurniru(rom_m_2013, 45); listaMurray.Add(rnt_rom_m_2013_Murray);
            RezultatNaTurniru rnt_rgr_g_2013_Murray = new RezultatNaTurniru(rgr_g_2013, 0); listaMurray.Add(rnt_rgr_g_2013_Murray);
            RezultatNaTurniru rnt_wim_g_2013_Murray = new RezultatNaTurniru(wim_g_2013, 2000); listaMurray.Add(rnt_wim_g_2013_Murray);
            RezultatNaTurniru rnt_ham_d_2013_Murray = new RezultatNaTurniru(ham_d_2013, 0); listaMurray.Add(rnt_ham_d_2013_Murray);
            RezultatNaTurniru rnt_mrl_m_2013_Murray = new RezultatNaTurniru(mrl_m_2013, 90); listaMurray.Add(rnt_mrl_m_2013_Murray);
            RezultatNaTurniru rnt_cin_m_2013_Murray = new RezultatNaTurniru(cin_m_2013, 180); listaMurray.Add(rnt_cin_m_2013_Murray);
            RezultatNaTurniru rnt_uso_g_2013_Murray = new RezultatNaTurniru(uso_g_2013, 360); listaMurray.Add(rnt_uso_g_2013_Murray);
            RezultatNaTurniru rnt_bas_d_2013_Murray = new RezultatNaTurniru(bas_d_2013, 0); listaMurray.Add(rnt_bas_d_2013_Murray);

            RezultatNaTurniru rnt_aus_g_2014_Murray = new RezultatNaTurniru(aus_g_2014, 360); listaMurray.Add(rnt_aus_g_2014_Murray);
            RezultatNaTurniru rnt_dub_d_2014_Murray = new RezultatNaTurniru(dub_d_2014, 0); listaMurray.Add(rnt_dub_d_2014_Murray);
            RezultatNaTurniru rnt_mtc_m_2014_Murray = new RezultatNaTurniru(mtc_m_2014, 0); listaMurray.Add(rnt_mtc_m_2014_Murray);
            RezultatNaTurniru rnt_mad_m_2014_Murray = new RezultatNaTurniru(mad_m_2014, 90); listaMurray.Add(rnt_mad_m_2014_Murray);
            RezultatNaTurniru rnt_rom_m_2014_Murray = new RezultatNaTurniru(rom_m_2014, 180); listaMurray.Add(rnt_rom_m_2014_Murray);
            RezultatNaTurniru rnt_rgr_g_2014_Murray = new RezultatNaTurniru(rgr_g_2014, 720); listaMurray.Add(rnt_rgr_g_2014_Murray);
            RezultatNaTurniru rnt_wim_g_2014_Murray = new RezultatNaTurniru(wim_g_2014, 360); listaMurray.Add(rnt_wim_g_2014_Murray);
            RezultatNaTurniru rnt_ham_d_2014_Murray = new RezultatNaTurniru(ham_d_2014, 0); listaMurray.Add(rnt_ham_d_2014_Murray);
            RezultatNaTurniru rnt_mrl_m_2014_Murray = new RezultatNaTurniru(mrl_m_2014, 180); listaMurray.Add(rnt_mrl_m_2014_Murray);
            RezultatNaTurniru rnt_cin_m_2014_Murray = new RezultatNaTurniru(cin_m_2014, 180); listaMurray.Add(rnt_cin_m_2014_Murray);
            RezultatNaTurniru rnt_uso_g_2014_Murray = new RezultatNaTurniru(uso_g_2014, 360); listaMurray.Add(rnt_uso_g_2014_Murray);
            RezultatNaTurniru rnt_bas_d_2014_Murray = new RezultatNaTurniru(bas_d_2014, 0); listaMurray.Add(rnt_bas_d_2014_Murray);

            RezultatNaTurniru rnt_aus_g_2015_Murray = new RezultatNaTurniru(aus_g_2015, 1200); listaMurray.Add(rnt_aus_g_2015_Murray);
            RezultatNaTurniru rnt_dub_d_2015_Murray = new RezultatNaTurniru(dub_d_2015, 90); listaMurray.Add(rnt_dub_d_2015_Murray);
            RezultatNaTurniru rnt_mtc_m_2015_Murray = new RezultatNaTurniru(mtc_m_2015, 0); listaMurray.Add(rnt_mtc_m_2015_Murray);
            RezultatNaTurniru rnt_mad_m_2015_Murray = new RezultatNaTurniru(mad_m_2015, 1000); listaMurray.Add(rnt_mad_m_2015_Murray);
            RezultatNaTurniru rnt_rom_m_2015_Murray = new RezultatNaTurniru(rom_m_2015, 90); listaMurray.Add(rnt_rom_m_2015_Murray);
            RezultatNaTurniru rnt_rgr_g_2015_Murray = new RezultatNaTurniru(rgr_g_2015, 720); listaMurray.Add(rnt_rgr_g_2015_Murray);
            RezultatNaTurniru rnt_wim_g_2015_Murray = new RezultatNaTurniru(wim_g_2015, 720); listaMurray.Add(rnt_wim_g_2015_Murray);
            RezultatNaTurniru rnt_ham_d_2015_Murray = new RezultatNaTurniru(ham_d_2015, 0); listaMurray.Add(rnt_ham_d_2015_Murray);
            RezultatNaTurniru rnt_mrl_m_2015_Murray = new RezultatNaTurniru(mrl_m_2015, 1000); listaMurray.Add(rnt_mrl_m_2015_Murray);
            RezultatNaTurniru rnt_cin_m_2015_Murray = new RezultatNaTurniru(cin_m_2015, 360); listaMurray.Add(rnt_cin_m_2015_Murray);
            RezultatNaTurniru rnt_uso_g_2015_Murray = new RezultatNaTurniru(uso_g_2015, 180); listaMurray.Add(rnt_uso_g_2015_Murray);
            RezultatNaTurniru rnt_bas_d_2015_Murray = new RezultatNaTurniru(bas_d_2015, 0); listaMurray.Add(rnt_bas_d_2015_Murray);

            Teniser murray = new Teniser("Andy Murray (GBR)", new DateTime(1987, 5, 15), 2);
            murray.ListaRezultata = listaMurray;


            List<RezultatNaTurniru> listaDjokovic = new List<RezultatNaTurniru>();

            RezultatNaTurniru rnt_aus_g_2013_Djokovic = new RezultatNaTurniru(aus_g_2013, 2000); listaDjokovic.Add(rnt_aus_g_2013_Djokovic);
            RezultatNaTurniru rnt_dub_d_2013_Djokovic = new RezultatNaTurniru(dub_d_2013, 500); listaDjokovic.Add(rnt_dub_d_2013_Djokovic);
            RezultatNaTurniru rnt_mtc_m_2013_Djokovic = new RezultatNaTurniru(mtc_m_2013, 1000); listaDjokovic.Add(rnt_mtc_m_2013_Djokovic);
            RezultatNaTurniru rnt_mad_m_2013_Djokovic = new RezultatNaTurniru(mad_m_2013, 45); listaDjokovic.Add(rnt_mad_m_2013_Djokovic);
            RezultatNaTurniru rnt_rom_m_2013_Djokovic = new RezultatNaTurniru(rom_m_2013, 180); listaDjokovic.Add(rnt_rom_m_2013_Djokovic);
            RezultatNaTurniru rnt_rgr_g_2013_Djokovic = new RezultatNaTurniru(rgr_g_2013, 720); listaDjokovic.Add(rnt_rgr_g_2013_Djokovic);
            RezultatNaTurniru rnt_wim_g_2013_Djokovic = new RezultatNaTurniru(wim_g_2013, 1200); listaDjokovic.Add(rnt_wim_g_2013_Djokovic);
            RezultatNaTurniru rnt_ham_d_2013_Djokovic = new RezultatNaTurniru(ham_d_2013, 0); listaDjokovic.Add(rnt_ham_d_2013_Djokovic);
            RezultatNaTurniru rnt_mrl_m_2013_Djokovic = new RezultatNaTurniru(mrl_m_2013, 360); listaDjokovic.Add(rnt_mrl_m_2013_Djokovic);
            RezultatNaTurniru rnt_cin_m_2013_Djokovic = new RezultatNaTurniru(cin_m_2013, 180); listaDjokovic.Add(rnt_cin_m_2013_Djokovic);
            RezultatNaTurniru rnt_uso_g_2013_Djokovic = new RezultatNaTurniru(uso_g_2013, 1200); listaDjokovic.Add(rnt_uso_g_2013_Djokovic);
            RezultatNaTurniru rnt_bas_d_2013_Djokovic = new RezultatNaTurniru(bas_d_2013, 0); listaDjokovic.Add(rnt_bas_d_2013_Djokovic);

            RezultatNaTurniru rnt_aus_g_2014_Djokovic = new RezultatNaTurniru(aus_g_2014, 360); listaDjokovic.Add(rnt_aus_g_2014_Djokovic);
            RezultatNaTurniru rnt_dub_d_2014_Djokovic = new RezultatNaTurniru(dub_d_2014, 180); listaDjokovic.Add(rnt_dub_d_2014_Djokovic);
            RezultatNaTurniru rnt_mtc_m_2014_Djokovic = new RezultatNaTurniru(mtc_m_2014, 360); listaDjokovic.Add(rnt_mtc_m_2014_Djokovic);
            RezultatNaTurniru rnt_mad_m_2014_Djokovic = new RezultatNaTurniru(mad_m_2014, 0); listaDjokovic.Add(rnt_mad_m_2014_Djokovic);
            RezultatNaTurniru rnt_rom_m_2014_Djokovic = new RezultatNaTurniru(rom_m_2014, 1000); listaDjokovic.Add(rnt_rom_m_2014_Djokovic);
            RezultatNaTurniru rnt_rgr_g_2014_Djokovic = new RezultatNaTurniru(rgr_g_2014, 1200); listaDjokovic.Add(rnt_rgr_g_2014_Djokovic);
            RezultatNaTurniru rnt_wim_g_2014_Djokovic = new RezultatNaTurniru(wim_g_2014, 2000); listaDjokovic.Add(rnt_wim_g_2014_Djokovic);
            RezultatNaTurniru rnt_ham_d_2014_Djokovic = new RezultatNaTurniru(ham_d_2014, 0); listaDjokovic.Add(rnt_ham_d_2014_Djokovic);
            RezultatNaTurniru rnt_mrl_m_2014_Djokovic = new RezultatNaTurniru(mrl_m_2014, 90); listaDjokovic.Add(rnt_mrl_m_2014_Djokovic);
            RezultatNaTurniru rnt_cin_m_2014_Djokovic = new RezultatNaTurniru(cin_m_2014, 90); listaDjokovic.Add(rnt_cin_m_2014_Djokovic);
            RezultatNaTurniru rnt_uso_g_2014_Djokovic = new RezultatNaTurniru(uso_g_2014, 720); listaDjokovic.Add(rnt_uso_g_2014_Djokovic);
            RezultatNaTurniru rnt_bas_d_2014_Djokovic = new RezultatNaTurniru(bas_d_2014, 0); listaDjokovic.Add(rnt_bas_d_2014_Djokovic);

            RezultatNaTurniru rnt_aus_g_2015_Djokovic = new RezultatNaTurniru(aus_g_2015, 2000); listaDjokovic.Add(rnt_aus_g_2015_Djokovic);
            RezultatNaTurniru rnt_dub_d_2015_Djokovic = new RezultatNaTurniru(dub_d_2015, 300); listaDjokovic.Add(rnt_dub_d_2015_Djokovic);
            RezultatNaTurniru rnt_mtc_m_2015_Djokovic = new RezultatNaTurniru(mtc_m_2015, 1000); listaDjokovic.Add(rnt_mtc_m_2015_Djokovic);
            RezultatNaTurniru rnt_mad_m_2015_Djokovic = new RezultatNaTurniru(mad_m_2015, 0); listaDjokovic.Add(rnt_mad_m_2015_Djokovic);
            RezultatNaTurniru rnt_rom_m_2015_Djokovic = new RezultatNaTurniru(rom_m_2015, 1000); listaDjokovic.Add(rnt_rom_m_2015_Djokovic);
            RezultatNaTurniru rnt_rgr_g_2015_Djokovic = new RezultatNaTurniru(rgr_g_2015, 1200); listaDjokovic.Add(rnt_rgr_g_2015_Djokovic);
            RezultatNaTurniru rnt_wim_g_2015_Djokovic = new RezultatNaTurniru(wim_g_2015, 2000); listaDjokovic.Add(rnt_wim_g_2015_Djokovic);
            RezultatNaTurniru rnt_ham_d_2015_Djokovic = new RezultatNaTurniru(ham_d_2015, 0); listaDjokovic.Add(rnt_ham_d_2015_Djokovic);
            RezultatNaTurniru rnt_mrl_m_2015_Djokovic = new RezultatNaTurniru(mrl_m_2015, 600); listaDjokovic.Add(rnt_mrl_m_2015_Djokovic);
            RezultatNaTurniru rnt_cin_m_2015_Djokovic = new RezultatNaTurniru(cin_m_2015, 600); listaDjokovic.Add(rnt_cin_m_2015_Djokovic);
            RezultatNaTurniru rnt_uso_g_2015_Djokovic = new RezultatNaTurniru(uso_g_2015, 2000); listaDjokovic.Add(rnt_uso_g_2015_Djokovic);
            RezultatNaTurniru rnt_bas_d_2015_Djokovic = new RezultatNaTurniru(bas_d_2015, 0); listaDjokovic.Add(rnt_bas_d_2015_Djokovic);

            Teniser djokovic = new Teniser("Novak Djokovic (SRB)", new DateTime(1987, 5, 22), 1);
            djokovic.ListaRezultata = listaDjokovic;



            atp.ListaTenisera.Add(djokovic);
            atp.ListaTenisera.Add(murray);
            atp.ListaTenisera.Add(wawrinka);
            atp.ListaTenisera.Add(federer);
            atp.ListaTenisera.Add(nadal);
            atp.ListaTenisera.Add(raonic);
            atp.ListaTenisera.Add(nishikori);
            atp.ListaTenisera.Add(berdych);
            atp.ListaTenisera.Add(cilic);
            atp.ListaTenisera.Add(thiem);
            atp.ListaTenisera.Add(tsonga);
            atp.ListaTenisera.Add(monfils);

            //List<Turnir> listaT = new List<Turnir>();
            //listaT.Add(dub_d_2014);
            //listaT.Add(ham_d_2015);
            //listaT.Add(rgr_g_2015);


            Console.WriteLine(atp.M20_PrviPobednikNaGrandSlamu());

            Console.ReadLine();
        }
    }
}
