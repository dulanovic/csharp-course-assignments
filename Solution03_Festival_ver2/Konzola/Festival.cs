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
        private List<DnevniProgram> listaDnevnihPrograma;
        private int brojZanrova;

        public List<DnevniProgram> ListaDnevnihPrograma
        {
            get { return listaDnevnihPrograma; }
            set { listaDnevnihPrograma = value; }
        }


        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Festival(string naziv)
        {
            this.naziv = naziv;
            this.listaDnevnihPrograma = new List<DnevniProgram>();
            this.brojZanrova = 0;
        }

        public Film UcitajFilm()
        {
            Console.WriteLine("Unesite naziv filma: ");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite zanr filma: ");
            string zanr = Console.ReadLine();

            bool pogresanUnos = true;
            int duzina = 0;

            while (pogresanUnos)
            {
                Console.WriteLine("Unesite duzinu filma: ");

                try
                {
                    duzina = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Uneli ste tekst umesto broja za duzinu filma, molimo ispravite gresku.");
                }
            }
            return new Film(naziv, new Zanr(zanr), duzina);
        }

        public DnevniProgram UcitajDnevniProgram()
        {
            Console.WriteLine("Unesite datum: ");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            bool pogresanUnos = true;
            int maksimalanBrojFilmova = 0;

            while (pogresanUnos)
            {
                Console.WriteLine("Unesite maksimalan broj filmova: ");
                try
                {
                    maksimalanBrojFilmova = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Uneli ste tekst umesto broja, molimo ispravite gresku.");
                }
            }
            DnevniProgram dp = new DnevniProgram(datum, maksimalanBrojFilmova);
            for (int i = 0; i < maksimalanBrojFilmova; i++)
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
            Console.WriteLine("Unesite naziv festivala: ");
            string naziv = Console.ReadLine();

            Festival f = new Festival(naziv);
            DnevniProgram.Obavesti += f.PovecajBrojZanrova;

            f.listaDnevnihPrograma.Add(f.UcitajDnevniProgram());
            //f.listaDnevnihPrograma.Add(f.UcitajDnevniProgram());

            string ispis = string.Format("Naziv festivala: {0}\n", naziv);
            foreach (DnevniProgram dp in f.listaDnevnihPrograma)
            {
                ispis += string.Format("\n{0}", dp.DajPodatke());
            }
            ispis += string.Format("\nUkupan broj zanrova: {0}", f.brojZanrova);

            Console.WriteLine(ispis);
            Console.ReadLine();
        }
    }
}
