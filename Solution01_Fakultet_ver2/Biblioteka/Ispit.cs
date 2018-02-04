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
        private int ocena;
        private DateTime datum;

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }


        public int Ocena
        {
            get { return ocena; }
            set { ocena = value; }
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

        public Ispit(string predmet, Profesor profesor, int ocena, DateTime datum)
        {
            this.predmet = predmet;
            this.profesor = profesor;
            this.ocena = ocena;
            this.datum = datum;
        }

        public string DajPodatke()
        {
            return string.Format("{0}, {1}, {2}", predmet, datum, ocena);
        }

    }
}
