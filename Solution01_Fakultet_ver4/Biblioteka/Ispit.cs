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
        private Profesor profesor;
        private DateTime datum;
        private int ocena;

        public int Ocena
        {
            get { return ocena; }
            set { ocena = value; }
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public Profesor Profesor
        {
            get { return profesor; }
            set { profesor = value; }
        }

        public string Predmet
        {
            get { return predmet; }
            set { predmet = value; }
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
            return string.Format("{0}, {1}, {2}", predmet, datum, ocena);
        }
    }
}
