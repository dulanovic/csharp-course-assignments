using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public delegate void Delegat();

    public class DnevniProgram
    {
        private DateTime datum;
        private List<Film> listaFilmova;
        private int maksimalanBrojFilmova;
        public static event Delegat Obavesti;

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
            this.listaFilmova = new List<Film>();
            this.maksimalanBrojFilmova = maksimalanBrojFilmova;
        }

        public string DajPodatke()
        {
            string ispis = "";
            int ukupnaDuzina = 0;
            foreach (Film f in listaFilmova)
            {
                ukupnaDuzina += f.Duzina;
            }
            ispis += string.Format("{0},\t\t\t{1}", datum, ukupnaDuzina);

            List<Zanr> listaZanrova = new List<Zanr>();
            foreach (Film f in listaFilmova)
            {
                if (!listaZanrova.Contains(f.Zanr))
                {
                    listaZanrova.Add(f.Zanr);
                }
            }
            foreach (Zanr z in listaZanrova)
            {
                ispis += string.Format("\n\t{0}", z.DajPodatke());
                foreach (Film f in listaFilmova)
                {
                    if (f.Zanr.Equals(z))
                    {
                        ispis += string.Format("\n\t\t{0}", f.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public void DodajFilm(Film film)
        {
            int brojFilmovaZanra = 0;
            foreach (Film f in listaFilmova)
            {
                if (film.Zanr == f.Zanr)
                {
                    brojFilmovaZanra++;
                }
            }
            if (listaFilmova.Count == maksimalanBrojFilmova || brojFilmovaZanra >= 4)
            {
                return;
            }
            List<Zanr> listaZanrova = new List<Zanr>();
            foreach (Film f in listaFilmova)
            {
                if (!listaZanrova.Contains(f.Zanr))
                {
                    listaZanrova.Add(f.Zanr);
                }
            }
            if (!listaZanrova.Contains(film.Zanr))
            {
                if (Obavesti != null)
                {
                    Obavesti();
                }
            }

            listaFilmova.Add(film);
        }
    }
}
