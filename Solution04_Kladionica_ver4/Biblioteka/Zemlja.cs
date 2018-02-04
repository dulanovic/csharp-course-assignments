using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public enum Kontinent
    {
        EU, AZ, AF, SA, JA, AU
    }

    public class Zemlja
    {
        private string naziv;
        private double kvota;
        private Kontinent kontinent;

        public Kontinent Kontinent
        {
            get { return kontinent; }
            set { kontinent = value; }
        }

        public double Kvota
        {
            get { return kvota; }
            set { kvota = value; }
        }

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Zemlja(string naziv, double kvota, Kontinent kontinent)
        {
            this.naziv = naziv;
            this.kvota = kvota;
            this.kontinent = kontinent;
        }
    }
}
