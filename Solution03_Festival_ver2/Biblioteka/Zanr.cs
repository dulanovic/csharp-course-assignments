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
            return string.Format("{0}", naziv);
        }
    }
}
