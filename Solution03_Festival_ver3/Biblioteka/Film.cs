using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Film
    {
        private string naziv;
        private Zanr zanr;
        private int duzina;

        public int Duzina
        {
            get { return duzina; }
            set { duzina = value; }
        }


        public Zanr Zanr
        {
            get { return zanr; }
            set { zanr = value; }
        }


        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Film(string naziv, Zanr zanr, int duzina)
        {
            this.naziv = naziv;
            this.zanr = zanr;
            this.duzina = duzina;
        }

        public string DajPodatke()
        {
            return string.Format("{0},\t\t\t{1} min.", naziv, duzina);
        }
    }
}
