using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Ispit
    {
        private string predmet;

        public string Predmet
        {
            get { return predmet; }
            set { predmet = value; }
        }

        private Profesor profesor;

        public Profesor Profesor
        {
            get { return profesor; }
            set { profesor = value; }
        }

        private DateTime datum;

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        private int ocena;

        public int Ocena
        {
            get { return ocena; }
            set { ocena = value; }
        }

        public Ispit(string predmet, Profesor profesor, DateTime datum, int ocena)
        {
            this.predmet = predmet;
            this.profesor = profesor;
            this.datum = datum;
            this.ocena = ocena;
        }

        public string DajPodatke()
        {
            string s = predmet.Replace("a", "");
            s.Replace("e", "");
            s.Replace("i", "");
            s.Replace("o", "");
            s.Replace("u", "");
            return string.Format("{0} {1} {2}", s, datum, ocena);
        }


    }
}

