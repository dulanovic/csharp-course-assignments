using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Putnik : Osoba
    {
        private Sediste sediste;

        public Sediste Sediste
        {
            get { return sediste; }
            set { sediste = value; }
        }

        public Putnik(string ime, string prezime, Sediste sediste) : base(ime, prezime)
        {
            this.sediste = sediste;
        }

        public override string DajPodatke()
        {
            return string.Format("{0}, {1}", sediste.Broj, base.DajPodatke());
        }
    }
}
