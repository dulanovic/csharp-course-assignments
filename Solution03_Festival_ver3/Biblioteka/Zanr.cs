using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Zanr
    {
        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Zanr(string naziv)
        {
            this.naziv = naziv;
        }

        public string DajPodatke()
        {
            return string.Format("{0}{1}", naziv[0], naziv[naziv.Length - 1]);
        }

        public override bool Equals(object obj)
        {
            Zanr z = (Zanr)obj;
            if (this.naziv == z.Naziv)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(Zanr z1, Zanr z2)
        {
            if (z1.naziv == z2.Naziv)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Zanr z1, Zanr z2)
        {
            return true;
        }
    }
}
