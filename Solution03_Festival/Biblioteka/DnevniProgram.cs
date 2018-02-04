using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class DnevniProgram
    {
        private DateTime datum;
        private List<Film> listaFilmova;
        private int maksimalanBrojFilmova;

        public int MaksimalanBrojFilmova
        {
            get { return maksimalanBrojFilmova; }
            set { maksimalanBrojFilmova = value; }
        }


        public List<Film> ListaFilmova
        {
            get { return listaFilmova; }
            set { listaFilmova = value; }
        }


        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public DnevniProgram(DateTime datum, int maksimalanBrojFilmova)
        {
            this.datum = datum;
            this.maksimalanBrojFilmova = maksimalanBrojFilmova;
            this.listaFilmova = new List<Film>();
        }

        public string DajPodatke()
        {
            double ukupnaDuzina = 0;
            for (int i = 0; i < listaFilmova.Count; i++)
            {
                ukupnaDuzina += listaFilmova[i].Duzina;
            }
            List<Zanr> spisakZanrova = new List<Zanr>();
            foreach (Film film in listaFilmova)
            {
                if (!spisakZanrova.Contains(film.Zanr))
                {
                    Console.WriteLine("0");
                    spisakZanrova.Add(film.Zanr);
                }
            }
            string ispis = string.Format("Datum: {0}, Ukupno trajanje: {1}\n", datum, ukupnaDuzina);
            foreach (Zanr zanr in spisakZanrova)
            {
                ispis += string.Format("\t{0}\n", zanr.DajPodatke());
                foreach (Film film in listaFilmova)
                {
                    if (film.Zanr == zanr)
                    {
                        Console.WriteLine("1");
                        ispis += string.Format("\t\t{0}\n", film.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public void DodajFilm(Film f)
        {
            Zanr z = f.Zanr;
            int brojFilmovaZanra = 0;

            foreach (Film film in listaFilmova)
            {
                if (z.Naziv == film.Zanr.Naziv)
                {
                    brojFilmovaZanra++;
                }
            }
            if (brojFilmovaZanra == 4 && maksimalanBrojFilmova == listaFilmova.Count)
            {
                return;
            }
            listaFilmova.Add(f);
        }

    }
}
