using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Profesor : Osoba
    {
        private int brojRadneKnjizice;

        public int BrojRadneKnjizice
        {
            get { return brojRadneKnjizice; }
            set { brojRadneKnjizice = value; }
        }

        public Profesor(string ime, string prezime, int brojRadneKnjizice) : base(ime, prezime)
        {
            this.brojRadneKnjizice = brojRadneKnjizice;
        }

        public override string DajPodatke()
        {
            return string.Format("{0} - {1}", base.DajPodatke(), brojRadneKnjizice);
        }
    }
}
