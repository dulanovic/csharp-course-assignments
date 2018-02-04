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
        private Zemlja odabranaZemlja;

        public Zemlja OdabranaZemlja
        {
            get { return odabranaZemlja; }
            set { odabranaZemlja = value; }
        }


        public double IznosUplate
        {
            get { return iznosUplate; }
            set { iznosUplate = value; }
        }

        public Igrac(string ime, string prezime, DateTime datum, double iznosUplate, Zemlja odabranaZemlja) : base(ime, prezime, datum)
        {
            this.iznosUplate = iznosUplate;
            this.odabranaZemlja = odabranaZemlja;
        }

        public override string DajPodatke()
        {
            return string.Format("{0} {1}, kvota:{2} uplata:{3}RSD isplata:{4}RSD {5}", odabranaZemlja.Naziv, odabranaZemlja.Kontinent, odabranaZemlja.Kvota, iznosUplate, odabranaZemlja.Kvota*iznosUplate, base.DajPodatke());
        }
    }
}
