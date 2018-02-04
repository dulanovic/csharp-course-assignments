using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Igrac : Osoba
    {
        private double iznosUplate;
        private Zemlja zemlja;

        public Zemlja Zemlja
        {
            get { return zemlja; }
            set { zemlja = value; }
        }


        public double IznosUplate
        {
            get { return iznosUplate; }
            set { iznosUplate = value; }
        }

        public Igrac(string ime, string prezime, DateTime datumRodjenja, double iznosUplate, Zemlja zemlja) : base(ime, prezime, datumRodjenja)
        {
            this.iznosUplate = iznosUplate;
            this.zemlja = zemlja;
        }

        public override string DajPodatke()
        {
            return string.Format("{0} {1} {2}", zemlja.Naziv, iznosUplate*zemlja.Kvota, base.DajPodatke());
        }
    }
}
