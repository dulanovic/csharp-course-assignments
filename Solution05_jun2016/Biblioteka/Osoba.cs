using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Osoba
    {
        private string ime;
        private DateTime datumRodjenja;

        public DateTime DatumRodjenja
        {
            get { return datumRodjenja; }
            set { datumRodjenja = value; }
        }


        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public Osoba(string ime, DateTime datumRodjenja)
        {
            this.ime = ime;
            this.datumRodjenja = datumRodjenja;
        }

        public Osoba()
        {

        }
    }
}
