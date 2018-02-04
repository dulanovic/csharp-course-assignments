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
        private int ukupanBrojZanrova;

        public int UkupanBrojZanrova
        {
            get { return ukupanBrojZanrova; }
            set { ukupanBrojZanrova = value; }
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
            listaPrograma = new List<DnevniProgram>();
        }

        public Film UcitajFilm()
        {
            Console.WriteLine("Unesite naziv filma: ");
            string naziv = Console.ReadLine();

            Boolean pogresanUnos = true;
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
                    Console.WriteLine("Uneli ste tekst umesto broja, molimo unesti broj.");
                }
            }
            Console.WriteLine("Unesite zanr filma: ");
            string nazivZanra = Console.ReadLine();

            Zanr z = new Zanr(nazivZanra);
            Film f = new Film(naziv, z, duzina);
            return f;
        }

        public DnevniProgram UcitajDnevniProgram()
        {
            Console.WriteLine("Unesite datum programa: ");
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
                    Console.WriteLine("Uneli se tekst umesto broja, molimo Vas da unesete broj!");
                }
            }
            DnevniProgram dp = new DnevniProgram(datum, maksimalanBrojFilmova);
            for (int i = 0; i < maksimalanBrojFilmova; i++)
            {
                dp.DodajFilm(UcitajFilm());
            }
            return dp;

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv festivala: ");
            string naziv = Console.ReadLine();

            Festival f = new Festival(naziv);

            f.listaPrograma.Add(f.UcitajDnevniProgram());
            //f.listaPrograma.Add(f.UcitajDnevniProgram());

            string ispis = string.Format("Naziv festivala: {0}\n\n", f.naziv);
            foreach (DnevniProgram dp in f.listaPrograma)
            {
                ispis += string.Format("{0}\n", dp.DajPodatke());
            }
            ispis += string.Format("Ukupan broj zanrova: {0}", f.UkupanBrojZanrova);
            Console.WriteLine(ispis);
            Console.ReadLine();
        }
    }
}
