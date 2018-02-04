using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Konzola
{
    public class Festival
    {
        private string naziv;
        private List<DnevniProgram> listaPrograma;
        private int brojZanrova;

        public int BrojZanrova
        {
            get { return brojZanrova; }
            set { brojZanrova = value; }
        }


        public List<DnevniProgram> ListaPrograma
        {
            get { return listaPrograma; }
            set { listaPrograma = value; }
        }


        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Festival(string naziv)
        {
            this.naziv = naziv;
            this.listaPrograma = new List<DnevniProgram>();
            this.brojZanrova = 0;
        }

        public Film UcitajFilm()
        {
            Console.WriteLine("Unesite naziv filma:");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite naziv zanra:");
            string nazivZanra = Console.ReadLine();

            bool pogresanUnos = true;
            int duzina = 0;
            while (pogresanUnos)
            {
                Console.WriteLine("Unesite duzinu filma:");
                try
                {
                    duzina = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Pogresan unos!, Uneli ste slovo umesto broja za duzinu, molimo ispravite gresku!");
                }
            }
            Zanr z = new Zanr(nazivZanra);
            Film f = new Film(naziv, z, duzina);
            return f;
        }

        public DnevniProgram UcitajDnevniProgram()
        {
            Console.WriteLine("Unesite datum:");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            bool pogresanUnos = true;
            int maksimalanBrojFilmova = 0;
            while (pogresanUnos)
            {
                Console.WriteLine("Unesite maksimalan broj filmova:");
                try
                {
                    maksimalanBrojFilmova = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Pogresan unos! Uneli ste slovo umesto broja, molimo ispravite gresku!");
                }
            }
            DnevniProgram dp = new DnevniProgram(datum, maksimalanBrojFilmova);

            Console.WriteLine("Unesite broj filmova:");
            int brojFilmova = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < brojFilmova; i++)
            {
                dp.DodajFilm(UcitajFilm());
            }
            return dp;
        }

        public void PovecajBrojZanrova()
        {
            brojZanrova++;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv festivala:");
            string naziv = Console.ReadLine();

            Festival f = new Festival(naziv);
            DnevniProgram.Obavesti += f.PovecajBrojZanrova;
            f.listaPrograma.Add(f.UcitajDnevniProgram());
            //f.listaPrograma.Add(f.UcitajDnevniProgram());

            string ispis = string.Format("Naziv festivala: {0}", f.naziv);
            foreach (DnevniProgram dp in f.ListaPrograma)
            {
                ispis += string.Format("\n\n\t{0}", dp.DajPodatke());
            }
            ispis += string.Format("\n\nUkupan broj zanrova: {0}", f.BrojZanrova);

            Console.WriteLine(ispis);

            Console.ReadLine();
        }
    }
}
