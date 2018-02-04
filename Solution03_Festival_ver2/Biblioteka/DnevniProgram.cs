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
            int ukupnoTrajanje = 0;

            foreach (Film f in listaFilmova)
            {
                ukupnoTrajanje += f.Duzina;
            }

            List<Zanr> listaZanrova = new List<Zanr>();
            bool nadjenZanr = false;

            foreach (Film f in listaFilmova)
            {
                foreach (Zanr z in listaZanrova)
                {
                    if (z.Naziv == f.Zanr.Naziv)
                    {
                        nadjenZanr = true;
                    }
                }
                if (!nadjenZanr)
                {
                    listaZanrova.Add(f.Zanr);
                }
            }

            string ispis = string.Format("{0}, {1}\n", datum, ukupnoTrajanje);

            foreach (Zanr z in listaZanrova)
            {
                ispis += string.Format("\t{0}\n", z.DajPodatke());
                foreach (Film f in listaFilmova)
                {
                    if (f.Zanr.Naziv == z.Naziv)
                    {
                        ispis += string.Format("\t\t{0}\n", f.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public void DodajFilm(Film f)
        {
            int brojFilmovaZanra = 0;

            foreach (Film film in listaFilmova)
            {
                if (f.Zanr.Naziv == film.Zanr.Naziv)
                {
                    brojFilmovaZanra++;
                }
            }

            if (brojFilmovaZanra < 4 && listaFilmova.Count < maksimalanBrojFilmova)
            {
                listaFilmova.Add(f);
            }
            if (brojFilmovaZanra == 0)
            {
                if (Obavesti != null)
                {
                    Obavesti();
                }
            }
        }
    }
}
