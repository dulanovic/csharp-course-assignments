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
            string ispis = "";

            string predmetBezSamoglasnika = predmet.Replace("a","");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("e", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("i", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("o", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("u", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("A", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("E", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("I", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("O", "");
            predmetBezSamoglasnika = predmetBezSamoglasnika.Replace("U", "");

            ispis += string.Format("{0}, {1}, {2}", predmetBezSamoglasnika[0], datum.ToShortDateString(), ocena);
            return ispis;
        }
    }
}
