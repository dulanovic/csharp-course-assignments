using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Osoba
    {
        protected string ime;
        protected string prezime;
        protected DateTime datum;

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }


        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }


        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public Osoba(string ime, string prezime, DateTime datum)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.datum = datum;
        }

        public virtual string DajPodatke()
        {
            return string.Format("{0} {1} {2}", ime, prezime, datum);
        }
    }
}
