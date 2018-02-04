using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Profesor : Osoba
    {
        private string brojRadneKnjizice;

        public string BrojRadneKnjizice
        {
            get { return brojRadneKnjizice; }
            set { brojRadneKnjizice = value; }
        }

        public Profesor(string ime, string prezime, string brojRadneKnjizice) : base(ime, prezime)
        {
            this.brojRadneKnjizice = brojRadneKnjizice;
        }

        public override string DajPodatke()
        {
            return string.Format("{0}, {1}", base.DajPodatke(), brojRadneKnjizice);
        }

        public override bool Equals(object obj)
        {
            Profesor p = (Profesor)obj;
            if (this.brojRadneKnjizice == p.BrojRadneKnjizice)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
